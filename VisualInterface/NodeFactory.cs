using AsyncSimulator;
using FloodingNode;
using FloodSTNode;
using System;
using UpdateBfsNode;
using NeighDfsNode;

namespace VisualInterface
{
    /// <summary>
    /// Factory class for creating nodes.
    /// </summary>
    public class NodeFactory
    {
        public static _Node Create ( string algorithmType, int id )
        {
            _Node node = null;
            switch ( algorithmType )
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
                default: throw new Exception("Unknown algorithm");
            }

            return node;
        }
    }
}
