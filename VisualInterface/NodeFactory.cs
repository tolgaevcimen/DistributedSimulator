using AsyncSimulator;
using FloodingNode;
using FloodSTNode;
using System;
using UpdateBfsNode;
using NeighDfsNode;
using ChiuDominatingSet;
using GoddardMDSNode;

namespace VisualInterface
{
    /// <summary>
    /// Factory class for creating nodes.
    /// </summary>
    public class NodeFactory
    {
        public static _Node Create(string algorithmType, int id, IVisualizer nodeVisualizer)
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
                        node = new ChiuNode(id, ChiuNode.InitialState.Random, Program.Randomizer);
                        
                        break;
                    }
                case "ChiuDS_allIn":
                    {
                        node = new ChiuNode(id, ChiuNode.InitialState.AllIn);

                        break;
                    }
                case "ChiuDS_allWait":
                    {
                        node = new ChiuNode(id, ChiuNode.InitialState.AllWait);

                        break;
                    }
                case "GoddardMDS":
                    {
                        node = new GoddardNode(id);

                        break;
                    }
                default: throw new Exception("Unknown algorithm");
            }

            node.Visualizer = nodeVisualizer;

            if (Program.Presenter.cb_selfStab.Checked)
                node.UserDefined_SingleInitiatorProcedure(node);

            return node;
        }
    }
}
