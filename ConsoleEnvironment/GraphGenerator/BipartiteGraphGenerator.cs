using AsyncSimulator;

namespace ConsoleEnvironment.GraphGenerator
{
    class BipartiteGraphGenerator : AbstractGraphGenerator
    {
        public override void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, AlgorithmType SelectedAlgorithm)
        {
            base.Generate(nodeCount, nodeHolder, edgeHolder, SelectedAlgorithm);

            for (int i = 0; i < nodeCount / 2; i++)
            {
                for (int j = nodeCount / 2; j < nodeCount; j++)
                {
                    var node1 = nodeHolder.GetNodeAt(i);
                    var node2 = nodeHolder.GetNodeAt(j);
                    
                    edgeHolder.AddEgde(new ConsoleEdge(node1, node2));
                }
            }
        }
    }
}
