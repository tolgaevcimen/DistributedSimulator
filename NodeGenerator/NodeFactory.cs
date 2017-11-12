using AsyncSimulator;
using System;
using ChiuDominatingSet;
using GoddardMDSNode;
using TurauDominatingSet;

namespace NodeGenerator
{
    /// <summary>
    /// Factory class for creating nodes.
    /// </summary>
    public class NodeFactory
    {
        public static Random Randomizer = new Random();
        public static _Node Create(AlgorithmType algorithmType, int id, IVisualizer nodeVisualizer, bool selfStab, NodeHolder nodeHolder)
        {
            _Node node = null;
            switch (algorithmType)
            {
                case AlgorithmType.ChiuMDS_rand:
                    node = new ChiuNode(id, nodeHolder, InitialState.Random, Randomizer);
                    break;
                case AlgorithmType.ChiuMDS_allIn:
                    node = new ChiuNode(id, nodeHolder, InitialState.AllIn);
                    break;
                case AlgorithmType.ChiuMDS_allWait:
                    node = new ChiuNode(id, nodeHolder, InitialState.AllWait);
                    break;
                case AlgorithmType.GoddardMDS_rand:
                    node = new GoddardNode(id, nodeHolder, InitialState.Random, Randomizer);
                    break;
                case AlgorithmType.GoddardMDS_allIn:
                    node = new GoddardNode(id, nodeHolder, InitialState.AllIn);
                    break;
                case AlgorithmType.GoddardMDS_allWait:
                    node = new GoddardNode(id, nodeHolder, InitialState.AllWait);
                    break;
                case AlgorithmType.TurauMDS_rand:
                    node = new TurauNode(id, nodeHolder, InitialState.Random, Randomizer);
                    break;
                case AlgorithmType.TurauMDS_allIn:
                    node = new TurauNode(id, nodeHolder, InitialState.AllIn);
                    break;
                case AlgorithmType.TurauMDS_allWait:
                    node = new TurauNode(id, nodeHolder, InitialState.AllWait);
                    break;
                default:
                    throw new Exception("Unknown algorithm");
            }

            node.Visualizer = nodeVisualizer;

            if (selfStab)
                node.UserDefined_SingleInitiatorProcedure(node);

            return node;
        }
    }
}
