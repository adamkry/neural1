using Encog.ML.Data.Basic;
using Encog.Util.Arrayutil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches.Neural.Common
{
    public static class ILearningScenarioExtensions
    {
        public static BasicMLDataSet GetSet<T>(this ILearningScenario<T> scenario,
            IList<T> dataSource)
        {
            double[][] input = scenario.InputParams.GetValues(dataSource);
            double[][] ideal = scenario.OutputParams.GetValues(dataSource);
            return new BasicMLDataSet(input, ideal);
        }        
    }
}
