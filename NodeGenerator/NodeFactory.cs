using AsyncSimulator;
using FloodingNode;
using FloodSTNode;
using System;
using UpdateBfsNode;
using NeighDfsNode;
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
        public static _Node Create(string algorithmType, int id, IVisualizer nodeVisualizer, bool selfStab)
        {
            _Node node = null;
            switch (algorithmType)
            {
                case "Flooding":
                    {
                        node = new _FloodingNode(id);

                        break;
                    }
                case "FloodST":
                    {
                        node = new _FloodSTNode(id);

                        break;
                    }
                case "UpdateBFS":
                    {
                        node = new _UpdateBfsNode(id);

                        break;
                    }
                case "NeighDFS":
                    {
                        node = new _NeighDfsNode(id);

                        break;
                    }
                case "ChiuDS_rand":
                    {
                        node = new ChiuNode(id, InitialState.Random, Randomizer);
                        
                        break;
                    }
                case "ChiuDS_allIn":
                    {
                        node = new ChiuNode(id, InitialState.AllIn);

                        break;
                    }
                case "ChiuDS_allWait":
                    {
                        node = new ChiuNode(id, InitialState.AllWait);

                        break;
                    }
                case "GoddardMDS_rand":
                    {
                        node = new GoddardNode(id, InitialState.Random, Randomizer);

                        break;
                    }
                case "GoddardMDS_allIn":
                    {
                        node = new GoddardNode(id, InitialState.AllIn);

                        break;
                    }
                case "GoddardMDS_allWait":
                    {
                        node = new GoddardNode(id, InitialState.AllWait);

                        break;
                    }
                case "TurauMDS_rand":
                    {
                        node = new TurauNode(id, InitialState.Random, Randomizer);

                        break;
                    }
                case "TurauMDS_allIn":
                    {
                        node = new TurauNode(id, InitialState.AllIn);

                        break;
                    }
                case "TurauMDS_allWait":
                    {
                        node = new TurauNode(id, InitialState.AllWait);

                        break;
                    }
                default: throw new Exception("Unknown algorithm");
            }

            node.Visualizer = nodeVisualizer;

            if (selfStab)
                node.UserDefined_SingleInitiatorProcedure(node);

            return node;
        }
    }
}
