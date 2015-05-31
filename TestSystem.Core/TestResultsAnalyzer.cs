using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestSystem.Core
{
    public class TestResultsAnalyzer
    {
        public float[] CalcTrueFalseNumbers(string expectedIn, string actualIn, string trueClassIndicator)
        {
            float truePositiveNumber = 0;
            float falsePositiveNumber = 0;
            float falseNegativeNumber = 0;
            float trueNegativeNumber = 0;

            var expectedCollection = Regex.Matches(expectedIn, @"[\S]+");
            var actualCollection = Regex.Matches(actualIn, @"[\S]+");
            //var checker = new FormatChecker();
            //if (!checker.CheckTestFormat(expectedIn)
            //    || checker.CheckTestFormat(actualIn))
            //{
            //    throw new ArgumentException();
            //}

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

        public float CalcErrorPercentage(string expected, MemoryStream msActual, int testRunsNumber)
        {
            var all = 0f;
            var right = 0f;
            string[] numbersActual = null;

            using (StreamReader file = new System.IO.StreamReader(msActual, true))
            {
                numbersActual = file.ReadToEnd().TrimStart().TrimEnd().Split(' ');
            }
            for (var i = 0; i < numbersActual.Count(); i++)
            {
                numbersActual[i] = Regex.Replace(numbersActual[i], "\r\n", string.Empty);
            }

            string[] numbersExpected = expected.TrimEnd().Split(' ');

            if (numbersExpected.Count() * testRunsNumber != numbersActual.Count())
            {
                throw new FormatException();
            }

            var j = 0;
            for (var i = 0; i < numbersActual.Count(); i++, j++)
            {
                all++;                

                if (numbersActual[i] == numbersExpected[j])
                {
                    right++;
                }
                if (j == numbersExpected.Count() - 1)
                {
                    j = -1;
                }
            }

            var result = right / all;

            return result;
        }
    }
}
