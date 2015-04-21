using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Core
{
    public class AnalyzedResults
    {
        public List<float> sensivityNumbers = new List<float>();
        public List<float> specifityNumbers = new List<float>();

        public List<int> TruePositiveNumber = new List<int>();
        public List<int> TrueNegativeNumber = new List<int>();
        public List<int> FalsePositiveNumber = new List<int>();
        public List<int> FalseNegativeNumber = new List<int>();

        public List<float[]> faultsNumbers = new List<float[]>();

        public List<float> rocCoordinatesSensivity = new List<float>();
        public List<float> rocCoordinatesSpecifity = new List<float>();
    }
}
