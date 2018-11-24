using AsyncSimulator;
using System;
using System.Linq;
using System.Reflection;

namespace NodeGenerator
{
    /// <summary>
    /// Factory class for creating nodes.
    /// </summary>
    public class NodeFactory
    {
        public static _Node Create(string algorithm, int id, IVisualizer nodeVisualizer, bool selfStab, NodeHolder nodeHolder, int predefinedState = 0)
        {
            var node = (_Node)Activator.CreateInstance(GetAlgorithmType(algorithm), id, nodeHolder, GetInitialState(algorithm), predefinedState);
            
            node.Visualizer = nodeVisualizer;

            if (selfStab)
                node.UserDefined_SingleInitiatorProcedure();

            return node;
        }

        public static Type GetAlgorithmType(string algorithm)
        {
            var typeProps = algorithm.Split('_');
            var algorithmName = typeProps[0];

            return Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == algorithmName);
        }

        public static InitialState GetInitialState(string algorithm)
        {
            var typeProps = algorithm.Split('_');
            var state = typeProps[1].ToLower();

            if (state == "rand")
            {
                return InitialState.Random;
            }

            if (state == "allin")
            {
                return InitialState.AllIn;
            }

            if (state == "allwait")
            {
                return InitialState.AllWait;
            }

            return InitialState.Random;
        }
    }
}
