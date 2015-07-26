using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataAccess;

namespace TestSystem.Core
{
    public class TestRatioFormatter
    {
        public byte[] GenerateTestData(int setId, string ratio)
        {
            using (var context = new Entities())
            {
                var testSet = context.TestSets.Find(setId);
                var rowsCount = 0;
                var ratioInt = Convert.ToInt32(ratio);

                for (var i = 0; i < testSet.Data.Length; i++)
                {
                    if (testSet.Data[i] == 10)
                    {
                        rowsCount++;
                    }
                }

                var rowsToTeach = rowsCount * ratioInt / 100;
                var teachBytes = 0;
                rowsCount = 0;

                for (var i = 0; i < testSet.Data.Length; i++)
                {
                    if (testSet.Data[i] == 10)
                    {
                        rowsCount++;
                    }
                    if (rowsCount > rowsToTeach)
                    {
                        teachBytes = i;
                        break;
                    }
                }

                List<byte> list = new List<byte>();

                list.AddRange(System.Text.Encoding.UTF8.GetBytes("Teach part: \n"));
                list.AddRange(testSet.Data.Take(teachBytes));
                list.AddRange(System.Text.Encoding.UTF8.GetBytes("Test part: \n"));
                var testPartSb = new StringBuilder();

                var testPart = testSet.Data.Skip(teachBytes).Take(testSet.Data.Length - teachBytes);
                var str = System.Text.Encoding.UTF8.GetString(testPart.ToArray());
                var array = str.TrimStart().TrimEnd().Split('\r');

                foreach(var part in array)
                {
                    var partArray = part.Split(' ');
                    for (var i = 1; i < partArray.Length; i++)
                    {
                        testPartSb.Append(partArray[i]);
                        if (i != partArray.Length - 1)
                        {
                            testPartSb.Append(" ");
                        }
                        
                    }
                    testPartSb.Append(Environment.NewLine);
                }

                list.AddRange(System.Text.Encoding.UTF8.GetBytes(testPartSb.ToString()));

                return list.ToArray(); 
            }

        }

    }
}
