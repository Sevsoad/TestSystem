using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Data.Entity;
using TestSystem.DataAccess;

namespace TestSystem.Core
{
    public class RocCurveCreator 
    {
        public AnalyzedResults GenerateRocCurveCoordinates(string expectedResults, List<string> testingResults, string trueClassNumber)
        {           
            var analyzedResults = new AnalyzedResults();
            var dbacc = new DataAccess.Users();
            dbacc.Password = "12345678";
            var analyzer = new TestResultsAnalyzer();

            foreach (var testResult in testingResults)
            {
                var result = analyzer.CalcTrueFalseNumbers(expectedResults, testResult, trueClassNumber);
                analyzedResults.faultsNumbers.Add(result);
                var sensivityItem = result[0] / (result[0] + result[2]);
                var specifityItem = (result[1] / (result[1] + result[3]));
                analyzedResults.sensivityNumbers.Add(sensivityItem);
                analyzedResults.specifityNumbers.Add(specifityItem);
            }

            var sumTpr = analyzedResults.sensivityNumbers.Sum();
            var sumFpr = analyzedResults.specifityNumbers.Sum();
            var previousTPR = 0f;
            var previousFPR = 0f;

            foreach (var i in analyzedResults.sensivityNumbers)
            {
                previousTPR += i / sumTpr;
                analyzedResults.rocCoordinatesSensivity.Add(previousTPR);
            }

            foreach (var i in analyzedResults.specifityNumbers)
            {
                previousFPR += i / sumFpr;
                analyzedResults.rocCoordinatesSpecifity.Add(previousFPR);
            }

            return analyzedResults;
        }
          

        private int GetTotalClassifiedItemsNumber(string expected)
        {
	        MatchCollection collection = Regex.Matches(expected, @"[\S]+");
            return collection.Count;
        }

    }
}