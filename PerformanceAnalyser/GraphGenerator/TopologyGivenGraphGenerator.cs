using AsyncSimulator;
using NodeGenerator;

namespace PerformanceAnalyserLibrary.GraphGenerator
{
    public class TopologyGivenGraphGenerator
    {
        public Topology Topology { get; set; }
        public string AlgorithmType { get; set; }

        public TopologyGivenGraphGenerator(Topology topology, string algorithmType)
        {
            Topology = topology;
            AlgorithmType = algorithmType;
        }

        public void GenerateGraph(NodeHolder nodeHolder, EdgeHolder edgeHolder)
        {
            foreach (var tuple in Topology.Neighbourhoods)
            {
                var node1 = nodeHolder.GetNodeById(tuple.Item1);
                var node2 = nodeHolder.GetNodeById(tuple.Item2);

                if (node1 == null)
                {
                    node1 = Topology.States != null ?
                        NodeFactory.Create(AlgorithmType, tuple.Item1, new ConsoleNodeVisualizer(tuple.Item1, edgeHolder), false, nodeHolder, Topology.States[tuple.Item1]) :
                        NodeFactory.Create(AlgorithmType, tuple.Item1, new ConsoleNodeVisualizer(tuple.Item1, edgeHolder), false, nodeHolder);
                    nodeHolder.AddNode(node1);
                }

                if (node2 == null)
                {
                    node2 = Topology.States != null ?
                        NodeFactory.Create(AlgorithmType, tuple.Item2, new ConsoleNodeVisualizer(tuple.Item2, edgeHolder), false, nodeHolder, Topology.States[tuple.Item2]) :
                        NodeFactory.Create(AlgorithmType, tuple.Item2, new ConsoleNodeVisualizer(tuple.Item2, edgeHolder), false, nodeHolder);
                    nodeHolder.AddNode(node2);
                }

                if (!node1.IsNeigbourOf(node2.Id))
                {
                    edgeHolder.AddEgde(new ConsoleEdge(node1, node2));
                }
            }
        }
    }
}