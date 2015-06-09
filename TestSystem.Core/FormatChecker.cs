using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                    var res = testLine.TrimStart().Split(' ')[0];

                    if (!Regex.IsMatch(res.ToString(), @"^\d+$"))
                    {
                        throw new FormatException();
                    }

                    expectedResults.Append(res + " ");
                }
            }

            return expectedResults;
        }  
       

    }
}
