﻿namespace SiaNet.NN
{
    using CNTK;
    using SiaNet.Common;
    using System;

    /// <summary>
    /// A recurrent neural network (RNN) is a class of artificial neural network where connections between units form a directed cycle. This allows it to exhibit dynamic temporal behavior. Unlike feedforward neural networks, RNNs can use their internal memory to process arbitrary sequences of inputs.
    /// </summary>
    public class Recurrent
    {
        /// <summary>
        /// Long short-term memory (LSTM) is a recurrent neural network (RNN) architecture that remembers values over arbitrary intervals
        /// </summary>
        /// <param name="inputDim">The input dimension.</param>
        /// <param name="hiddenSize">Size of the hidden layer.</param>
        /// <param name="numLayers">The number layers.</param>
        /// <param name="bidirectional">If bidirectional RNN</param>
        /// <param name="weightInitializer">The weight initializer.</param>
        /// <returns></returns>
        public static Function LSTM(int inputDim, uint hiddenSize, uint numLayers, bool bidirectional = false, string weightInitializer = OptInitializers.Xavier)
        {
            return BuildRNN(inputDim, hiddenSize, numLayers, bidirectional, weightInitializer, "lstm");
        }

        /// <summary>
        /// Gated recurrent units (GRUs) are a gating mechanism in recurrent neural networks, introduced in 2014. Their performance on polyphonic music modeling and speech signal modeling was found to be similar to that of long short-term memory.[1] They have fewer parameters than LSTM, as they lack an output gate
        /// </summary>
        /// <param name="inputDim">The input dimension.</param>
        /// <param name="hiddenSize">Size of the hidden layer.</param>
        /// <param name="numLayers">The number layers.</param>
        /// <param name="bidirectional">If bidirectional RNN</param>
        /// <param name="weightInitializer">The weight initializer.</param>
        /// <returns></returns>
        public static Function GRU(int inputDim, uint hiddenSize, uint numLayers, bool bidirectional = false, string weightInitializer = OptInitializers.Xavier)
        {
            return BuildRNN(inputDim, hiddenSize, numLayers, bidirectional, weightInitializer, "gru");
        }

        /// <summary>
        /// Recurrent neural network defined with activation function
        /// </summary>
        /// <param name="inputDim">The input dim.</param>
        /// <param name="hiddenSize">Size of the hidden.</param>
        /// <param name="numLayers">The number layers.</param>
        /// <param name="activation">Activation function to use. Supported are ReLU and TanH <see cref="SiaNet.Common.OptActivations"/>.</param>
        /// <param name="bidirectional">If bidirectional RNN</param>
        /// <param name="weightInitializer">The weight initializer.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Supported activation for RNN is ReLU and Tanh</exception>
        public static Function RNN(int inputDim, uint hiddenSize, uint numLayers, string activation, bool bidirectional = false, string weightInitializer = OptInitializers.Xavier)
        {
            switch (activation)
            {
                case OptActivations.ReLU:
                    return BuildRNN(inputDim, hiddenSize, numLayers, bidirectional, weightInitializer, "rnnReLU");
                case OptActivations.Tanh:
                    return BuildRNN(inputDim, hiddenSize, numLayers, bidirectional, weightInitializer, "rnnTanh");
                default:
                    throw new Exception("Supported activation for RNN is ReLU and Tanh");
            }
        }

        /// <summary>
        /// Builds the RNN.
        /// </summary>
        /// <param name="inputDim">The input dim.</param>
        /// <param name="hiddenSize">Size of the hidden.</param>
        /// <param name="numLayers">The number layers.</param>
        /// <param name="bidirectional">if set to <c>true</c> [bidirectional].</param>
        /// <param name="weightInitializer">The weight initializer.</param>
        /// <param name="rnnName">Name of the RNN.</param>
        /// <returns></returns>
        private static Function BuildRNN(int inputDim, uint hiddenSize, uint numLayers, bool bidirectional = false, string weightInitializer = OptInitializers.Xavier, string rnnName = "")
        {
            int[] s = { inputDim };
            var weights = new Parameter(s, DataType.Float, Initializers.Get(weightInitializer), GlobalParameters.Device);

            return CNTKLib.OptimizedRNNStack(Variable.InputVariable(new int[] { inputDim }, DataType.Float), weights, hiddenSize, numLayers, bidirectional, rnnName);
        }
    }
}
