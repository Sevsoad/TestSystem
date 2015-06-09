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

                list.AddRange(System.Text.Encoding.UTF8.GetBytes("Teach part: "));
                list.AddRange(testSet.Data.Take(teachBytes));
                list.AddRange(System.Text.Encoding.UTF8.GetBytes("Test part: "));
                list.AddRange(testSet.Data.Skip(teachBytes).Take(testSet.Data.Length - teachBytes));

                return list.ToArray(); 
            }

            return null;
        }

    }
}
