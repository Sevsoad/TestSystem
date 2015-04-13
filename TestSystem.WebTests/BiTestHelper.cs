using Lab02_MultilayerPerceptron;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.WebTests
{


    static class BiTestHelper
    {
        public static VectorsContainer GetVectorContainer(String pathA, String pathB, String pathC, NeuronHelper neuronHelper)
        {
            var vectorA = neuronHelper.ConvertToVector(new PictureContainer(pathA, 10));
            var vectorB = neuronHelper.ConvertToVector(new PictureContainer(pathB, 10));
            var vectorC = neuronHelper.ConvertToVector(new PictureContainer(pathC, 10));

            return new VectorsContainer(vectorA, vectorB, vectorC);
        }

        public static string RunAlgorithmAnalyze(string testsPath, string outPath, float alpha)
        {
            var testingResults = string.Empty;
            var neuronHelper = new NeuronHelper();
            var expectedResults = string.Empty;
            var container = BiTestHelper.GetVectorContainer(PicturesPath.PathToOriginalA, PicturesPath.PathToOriginalB,
                PicturesPath.PathToOriginalC, new NeuronHelper());
            var error = 0.05;
            var beta = 0.01;
            var timeout = 10000;

            var perceptron = new Perceptron(
               alpha,
               beta,
               error,
               timeout,
               container);

            var helper = new PerceptronHelper(perceptron, 100, 3);

            helper.Teach();

            using (StreamReader file = new System.IO.StreamReader(testsPath, true))
            {

                while (!file.EndOfStream)
                {
                    var testLine = file.ReadLine();
                    expectedResults += testLine.ToCharArray()[0] + " ";
                    testLine = testLine.Remove(0, 2);
                    var byteVector = neuronHelper.ConvertTestInputToVector(testLine);

                    var percentage = helper.Recognize(byteVector);

                    var classNumber = Array.IndexOf<double>(percentage, percentage.Max()) + 1;

                    testingResults += classNumber.ToString() + " ";
                }
            }

            using (StreamWriter file = new System.IO.StreamWriter(@"D:\DPtests\results.txt", true))
            {
                file.WriteLine(testingResults);
            }
            
            return testingResults;
        }


    }
}
