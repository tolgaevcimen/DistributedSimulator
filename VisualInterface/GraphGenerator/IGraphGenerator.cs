using AsyncSimulator;
using SupportedAlgorithmAndGraphTypes;

namespace VisualInterface.GraphGenerator
{
    internal interface IGraphGenerator
    {
        void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, AlgorithmType SelectedAlgorithm);
    }
}
