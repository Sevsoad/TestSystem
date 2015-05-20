using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestSystem.Core
{
    public class TestResultsAnalyzer
    {
        /// <summary>
        /// Calculates number of items that was right classified
        /// </summary>
        /// <returns>Returns number of right classified items</returns>
        public float[] CalcTrueFalseNumbers(string expectedIn, string actualIn, string trueClassIndicator)
        {
            float truePositiveNumber = 0;
            float falsePositiveNumber = 0;
            float falseNegativeNumber = 0;
            float trueNegativeNumber = 0;

            var expectedCollection = Regex.Matches(expectedIn, @"[\S]+");
            var actualCollection = Regex.Matches(actualIn, @"[\S]+");
            var checker = new FormatChecker();
            if (!checker.CheckTestFormat(expectedIn)
                || checker.CheckTestFormat(actualIn))
            {
                throw new ArgumentException();
            }

            for (var i = 0; i < expectedCollection.Count; i++)
            {
                if ((expectedCollection[i].ToString() == actualCollection[i].ToString()) &&
                    actualCollection[i].ToString() == trueClassIndicator)
                {
                    truePositiveNumber++;
                    continue;
                }

                if (actualCollection[i].ToString() == trueClassIndicator &&
                    (expectedCollection[i].ToString() != actualCollection[i].ToString()))
                {
                    falsePositiveNumber++;
                    continue;
                }

                if (expectedCollection[i].ToString() == trueClassIndicator &&
                    (actualCollection[i].ToString() != expectedCollection[i].ToString()))
                {
                    falseNegativeNumber++;
                    continue;
                }

                if (expectedCollection[i].ToString() != trueClassIndicator &&
                    (actualCollection[i].ToString() == expectedCollection[i].ToString()))
                {
                    trueNegativeNumber++;
                    continue;
                }
            }

            return new[] { truePositiveNumber, falsePositiveNumber, falseNegativeNumber, trueNegativeNumber };
        }
    }
}
