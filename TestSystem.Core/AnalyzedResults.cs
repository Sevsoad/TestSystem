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

        public List<float> TruePositiveNumber = new List<float>();
        public List<float> TrueNegativeNumber = new List<float>();
        public List<float> FalsePositiveNumber = new List<float>();
        public List<float> FalseNegativeNumber = new List<float>();
        public float ErrorRate = 0;

        public List<float[]> faultsNumbers = new List<float[]>();

        public List<float> rocCoordinatesSensivity = new List<float>();
        public List<float> rocCoordinatesSpecifity = new List<float>();       
    }
}
