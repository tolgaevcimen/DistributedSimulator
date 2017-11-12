using AsyncSimulator;

namespace ConsoleEnvironment.GraphGenerator
{
    internal interface IGraphGenerator
    {
        void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, AlgorithmType SelectedAlgorithm);
    }
}
