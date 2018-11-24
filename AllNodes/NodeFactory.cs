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
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;
            var node = (_Node)Activator.CreateInstance(GetAlgorithmType(algorithm), flags, null, new object[] { id, nodeHolder, GetInitialState(algorithm), predefinedState }, null);
            
            node.Visualizer = nodeVisualizer;

            if (selfStab)
                node.UserDefined_SingleInitiatorProcedure();

            return node;
        }

        private static Type GetAlgorithmType(string algorithm)
        {
            var typeProps = algorithm.Split('_');
            var algorithmName = typeProps[0];

            return Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == algorithmName);
        }

        private static InitialState GetInitialState(string algorithm)
        {
            var typeProps = algorithm.Split('_');
            var state = typeProps[1];

            if (state == "rand")
            {
                return InitialState.Random;
            }

            if (state == "AllIn")
            {
                return InitialState.AllIn;
            }

            if (state == "AllWait")
            {
                return InitialState.AllWait;
            }

            return InitialState.Random;
        }
    }
}
