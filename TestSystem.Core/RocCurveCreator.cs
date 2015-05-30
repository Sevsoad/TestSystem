using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Data.Entity;
using TestSystem.DataAccess;
using System.IO;
using System.Text;

namespace TestSystem.Core
{
    public class RocCurveCreator
    {
        public AnalyzedResults GenerateRocCurveCoordinates(string expectedResults, MemoryStream msActual, string trueClassNumber)
        {
            var analyzedResults = new AnalyzedResults();
            var analyzer = new TestResultsAnalyzer();
            var testingResults = new List<string>();

            string[] numbersActual = null;
            string[] numbersExpected = expectedResults.Split(' ');
            var sb = new StringBuilder();

            using (StreamReader file = new System.IO.StreamReader(msActual, true))
            {
                numbersActual = file.ReadToEnd().Split(new [] {'\r'});
                for (var i = 0; i < numbersActual.Count(); i++)
                {
                    numbersActual[i] = Regex.Replace(numbersActual[i], "\n", string.Empty);
                }
            }

            var classesInLine = numbersActual[0].Split(' ').Count();

            testingResults = numbersActual.ToList();

            if (testingResults.Count < 20 || testingResults.Count > 40)
            {
                throw new FormatException();
            }

            foreach (var testResult in testingResults)
            {
                var result = analyzer.CalcTrueFalseNumbers(expectedResults, testResult, trueClassNumber);
                analyzedResults.faultsNumbers.Add(result);
                var sensivityItem = result[0] == 0 ? 0 : (result[0] / (result[0] + result[2]));
                var specifityItem = result[1] == 0 ? 0 : (result[1] / (result[1] + result[3]));
                analyzedResults.sensivityNumbers.Add(sensivityItem);
                analyzedResults.specifityNumbers.Add(specifityItem);
                analyzedResults.TruePositiveNumber.Add(result[0] / (float)classesInLine);
                analyzedResults.FalsePositiveNumber.Add(result[1] / (float)classesInLine);
                analyzedResults.FalseNegativeNumber.Add(result[2] / (float)classesInLine);
                analyzedResults.TrueNegativeNumber.Add(result[3] / (float)classesInLine);
            }

            var sumTpr = analyzedResults.sensivityNumbers.Sum();
            var sumFpr = analyzedResults.specifityNumbers.Sum();
            var previousTPR = 0f;
            var previousFPR = 0f;

            foreach (var i in analyzedResults.sensivityNumbers)
            {
                previousTPR += (sumTpr == 0 ? 0 : i / sumTpr);
                analyzedResults.rocCoordinatesSensivity.Add(previousTPR);
            }

            foreach (var i in analyzedResults.specifityNumbers)
            {
                previousFPR += (sumFpr == 0 ? 0 : i / sumFpr);
                analyzedResults.rocCoordinatesSpecifity.Add(previousFPR);
            }

            return analyzedResults;
        }

    }
}