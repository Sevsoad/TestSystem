using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.WebTests;

namespace TestSystem.Core
{
    public class DataWorker
    {
        public string GetExpectedResults()
        {
            TestFormatConverter formatConverter = new TestFormatConverter();
            var testPath20percent = @"D:\DPtests\test10percNoise.txt";
            var expectedResults = formatConverter.GetResultsFromTestSet(testPath20percent);

            return expectedResults;
        }

        public List<string> GetTestingResults()
        {
            var testPath20percent = @"D:\DPtests\test10percNoise.txt";
            var outputPath = @"D:\DPtests\alphaChangeTestingResults.txt";
            var testingResults = new List<string>();
            for (var i = 0.00f; i < 0.2; i += 0.01f)
            {
                testingResults.Add(BiTestHelper.RunAlgorithmAnalyze(testPath20percent, outputPath, i));
            }

            return testingResults;
        }

    }
}
