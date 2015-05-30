using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.WebTests;

namespace TestSystem.Core
{
    public class DataWorker
    {
        //public string GetExpectedResults()
        //{
            

        //    FormatChecker formatConverter = new FormatChecker();
        //    var testPath20percent = @"D:\DPtests\test10percNoise.txt";
        //    var expectedResults = GetResultsFromTestSet(testPath20percent);

        //    return expectedResults;
        //}

        public List<string> GetTestingResults()
        {
            var testPath20percent = @"D:\DPtests\test10percNoise.txt";
            var outputPath = @"D:\DPtests\alphaChangeTestingResults.txt";
            var testingResults = new List<string>();
            for (var i = 0.10f; i < 0.20; i += 0.01f)
            {
                testingResults.Add(BiTestHelper.RunAlgorithmAnalyze(testPath20percent, outputPath, i));
            }

            return testingResults;
        }

          
    }
}
