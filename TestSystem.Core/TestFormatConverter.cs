using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Core
{
    public class TestFormatConverter
    {
        /// <summary>
        /// Gets expected values from test set
        /// </summary>
        /// <returns>Set of expected testing results</returns>
        public string GetResultsFromTestSet(string testPath)
        {
            var expectedResults = string.Empty;

            using (StreamReader file = new System.IO.StreamReader(testPath, true))
            {

                while (!file.EndOfStream)
                {
                    var testLine = file.ReadLine();
                    expectedResults += testLine.ToCharArray()[0] + " ";
                }
            }

            return expectedResults;
        }       

       

    }
}
