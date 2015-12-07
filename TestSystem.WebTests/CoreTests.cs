using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Core;

namespace TestSystem.WebTests
{
    [TestClass]
    public class CoreTests
    {
        [TestMethod]
        public void RocCurveCreator_CalculateAUC_NoErrorThrown()
        {
            //Arrange
            var creator = new RocCurveCreator();
            var xVals = "0 0,5 1";
            var yVals = "0 0,5 1";

            //Act
            var auc = creator.CalculateAUC(xVals, yVals);

            //Assert

            Assert.AreEqual(auc.ToString(), "0,5");
        }

        [TestMethod]
        public void GenerateTestData_ValidInput_ValidOutput()
        {
            var formatter = new TestRatioFormatter();
            var result = formatter.GenerateTestData(1, "10");

        }

        [TestMethod]
        public void ResultsAnalyzer_RightFormatInput_NoErrorThrown()
        {
            //Arrange
            var analyzer = new TestResultsAnalyzer();
            var expcetedStringRightFormat = "1 1 1 1 1 2 2 2 2 2 3 3 3 3 3";
            var actualStringRightFormat = "1 2 1 2 1 3 2 2 3 1 2 1 3 3 3";
            var classNumber = "1";

            //Act
            var results = analyzer.CalcTrueFalseNumbers(expcetedStringRightFormat,
                actualStringRightFormat, classNumber);

            //Assert
            
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void ResultsAnalyzer_WrongFormatInput_ErrorThrown()
        //{
        //    //Arrange
        //    var analyzer = new TestResultsAnalyzer();
        //    var expcetedStringRightFormat = "1-asddaf2 3 3 3 3 3";
        //    var actualStringRightFormat = "1 2 1 2 1 3 2 2 3 1 2 1 3 3 3";
        //    var classNumber = "1";

        //    //Act
        //    var results = analyzer.CalcTrueFalseNumbers(expcetedStringRightFormat,
        //        actualStringRightFormat, classNumber);

        //    //Assert
        //}

        [TestMethod]
        public void ResultsAnalyzer_RightFormatInput_RightCalculation()
        {
            //Arrange
            var analyzer = new TestResultsAnalyzer();
            var expcetedStringRightFormat = "1 1 1 1 1 2 2 2 2 2 3 3 3 3 3";
            var actualStringRightFormat = "1 2 1 2 1 3 2 2 3 1 2 1 3 3 3";
            var classNumber = "1";

            //Act
            var results = analyzer.CalcTrueFalseNumbers(expcetedStringRightFormat,
                actualStringRightFormat, classNumber);

            //Assert
            Assert.AreEqual(3, results[0]);
            Assert.AreEqual(2, results[1]);
            Assert.AreEqual(2, results[2]);
            Assert.AreEqual(5, results[3]);
        }

        //public void RocCurveCreator_RightFormatInput_NoExceptonThrown()
        //{
        //    //Arrange
        //    var rocCurveCreator = new TestSystem.Core.RocCurveCreator();
        //    var testResultsForRocCurve = new List<string>();
        //    var testPath20percent = @"~\test20percNoise.txt";

        //    var expectedResults = formatConverter.GetResultsFromTestSet(testPath20percent);

        //    for (var i = 0f; i < 1; i += 0.05f)
        //    {
        //        testingResults.Add(
        //            CoreTestHelper.RunAlgorithmAnalyze
        //            (testPath20percent, i));
        //    }

        //    //Act
        //    var result = rocCurveCreator.
        //        GenerateRocCurveCoordinates(
        //        expectedResults, testingResults, "2");

        //    //Assert
        //}

    }
}
