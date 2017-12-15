using Encog.ML.Data.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Training.Propagation.Quick;
using Encog.Neural.Networks.Training.Propagation;
using System.Diagnostics;
using Matches.Neural.Common;

namespace Matches.Neural.Learning
{
    public static class Training
    {
        public static BasicNetwork Train<T>(ILearningScenario<T> scenario,
            IList<T> dataSource,
            BasicNetwork network = null)
        {
            if (network == null)
            {
                network = NeuralNetwork
                    .CreateNetwork(scenario.InputParams.Count,
                        scenario.OutputParams.Count);
            }
            
            BasicMLDataSet trainingSet = scenario.GetSet(dataSource);
            var training = GetTraining(network, trainingSet);

            int epoch = 1;
            do
            {
                training.Iteration();
                Debug.WriteLine($"Iteration No: {epoch++}, Error: {training.Error}");
            }
            while (training.Error > scenario.MaxError || epoch < scenario.MaxIterations);
            return network;
        }

        private static Propagation GetTraining(BasicNetwork network, BasicMLDataSet trainingSet)
        {
            //var train = new ResilientPropagation(network, trainingSet);
            //var train = new Backpropagation(network, trainingSet, 0.7, 0.2);
            //var train = new ManhattanPropagation(network, trainingSet, 0.001);
            //var train = new ScaledConjugateGradient(network, trainingSet);
            //var train = new LevenbergMarquardtTraining(network, trainingSet);
            var training = new QuickPropagation(network, trainingSet, 2.0);
            return training;
        }
    }
}
