using Encog.ML.Data.Basic;
using Encog.Neural.Networks;
using Matches.Neural.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches.Neural.Computing
{
    public static class Prediction
    {
        public static void Compute<T>(BasicNetwork network, ILearningScenario<T> scenario,
            IList<T> dataSource)
        {
            var computingSet = scenario.GetSet(dataSource);
            foreach (var item in computingSet)
            {
                var output = network.Compute(item.Input);
                Console.WriteLine($"Input: {item.Input[0]}, {item.Input[1]} Ideal: {item.Ideal[0]} Actual: {output[0]}");
            }
        }
    }
}
