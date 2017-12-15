using Encog.ML.Data.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Training.Propagation.Quick;
using Encog.Neural.Networks.Training.Propagation;

namespace Matches.Neural.Learning
{
    public class LearningScenario<T> : ILearningScenario<T>
    {
        public List<IParamDefinition<T>> InputParams
        {
            get;
            set;
        }

        public double MaxError
        {
            get;set;
        }

        public int MaxIterations
        {
            get;set;
        }

        public List<IParamDefinition<T>> OutputParams
        {
            get;
            set;
        }        
    }
}
