using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Core
{
    public class FormatChecker
    {
        public bool CheckTestFormat(string Test)
        {

            return true;
        }

        public StringBuilder GetExpectedValuesFromTestSet(MemoryStream ms)
        {
            var expectedResults = new StringBuilder();            

            using (StreamReader file = new System.IO.StreamReader(ms, true))
            {
                while (!file.EndOfStream)
                {
                    var testLine = file.ReadLine();
                    expectedResults.Append(testLine.TrimStart().ToCharArray()[0] + " ");
                }
            }

            return expectedResults;
        }  
       

    }
}
