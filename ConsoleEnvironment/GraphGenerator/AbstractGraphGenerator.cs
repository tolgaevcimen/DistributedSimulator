using AsyncSimulator;
using NodeGenerator;

namespace ConsoleEnvironment.GraphGenerator
{
    public abstract class AbstractGraphGenerator : IGraphGenerator
    {
        public bool SelfStab { get; set; }
        public virtual void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, AlgorithmType SelectedAlgorithm)
        {
            for (int i = 0; i < nodeCount; i++)
            {
                var node = NodeFactory.Create(SelectedAlgorithm, nodeHolder.NodeCount, new ConsoleNodeVisualizer(nodeHolder.NodeCount, edgeHolder), SelfStab, nodeHolder);
                nodeHolder.AddNode(node);
            }
        }
    }
}
