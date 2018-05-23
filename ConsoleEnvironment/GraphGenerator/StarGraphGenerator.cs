using System;
using System.Drawing;
using AsyncSimulator;
using SupportedAlgorithmAndGraphTypes;

namespace ConsoleEnvironment.GraphGenerator
{
    class StarGraphGenerator : AbstractGraphGenerator
    {
        public override void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, AlgorithmType SelectedAlgorithm)
        {
            base.Generate(nodeCount, nodeHolder, edgeHolder, SelectedAlgorithm);

            for (int i = 0; i < nodeCount; i++)
            {
                var node1 = nodeHolder.GetNodeAt(0);
                var node2 = nodeHolder.GetNodeAt(i);

                edgeHolder.AddEgde(new ConsoleEdge(node1, node2));
            }
        }
    }
}
