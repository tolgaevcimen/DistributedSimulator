using AsyncSimulator;

namespace ConsoleEnvironment.GraphGenerator
{
    class CircleGraphGenerator : AbstractGraphGenerator
    {
        public override void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm)
        {
            base.Generate(nodeCount, nodeHolder, edgeHolder, SelectedAlgorithm);

            for (int i = 0; i < nodeCount; i++)
            {
                if (i != nodeCount - 1)
                {
                    var node1 =  nodeHolder.GetNodeAt(i);
                    var node2 =  nodeHolder.GetNodeAt(i + 1);

                    edgeHolder.AddEgde(new ConsoleEdge(node1, node2));
                }
                else
                {
                    var node1 = nodeHolder.GetNodeAt(i);
                    var node2 = nodeHolder.GetNodeAt(0);

                    edgeHolder.AddEgde(new ConsoleEdge(node1, node2));
                }
            }
        }
    }
}
