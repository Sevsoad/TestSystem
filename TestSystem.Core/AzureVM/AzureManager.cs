using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System.IO;
using System.Management;
using System.Management.Automation;

namespace TestSystem.Core.AzureVM
{
    class AzureManager
    {
        private static NamespaceManager namespaceManager;
        private static String queueName = "TestSysQueue";
        private static QueueClient queueClient;


        public List<string> RunExecution(MemoryStream ms)
        {
            String connectionString = @"put your connection data here";

            namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            namespaceManager.CreateQueue(queueName);

            queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);

            using (StreamReader file = new System.IO.StreamReader(ms, true))
            {

                while (!file.EndOfStream)
                {
                    queueClient.Send(new BrokeredMessage(file.ReadLine()));
                }
            }

            queueClient.Send(new BrokeredMessage("Complete"));

            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                PowerShellInstance.AddScript(@"cd d:\; .\userProgramm.exe;");

                PowerShellInstance.Invoke();
            }

            var message = new BrokeredMessage();
            List<string> list = new List<string>();

            while (true)
            {
                message = queueClient.Receive();
                if (message.ToString() == "Complete")
                {
                    break;
                }

                list.Add(message.ToString());
            }

            queueClient.Close();
            return list;
        }


    }
}
