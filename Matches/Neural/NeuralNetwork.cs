using Encog.Engine.Network.Activation;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches.Neural
{
    public static class NeuralNetwork
    {
        public static BasicNetwork CreateNetwork(int inputSize, int outputSize)
        {
            var network = new BasicNetwork();
            network.AddLayer(new BasicLayer(null, true, inputSize));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, inputSize));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false, outputSize));
            network.Structure.FinalizeStructure();
            network.Reset();
            return network;
        }
    }
}
