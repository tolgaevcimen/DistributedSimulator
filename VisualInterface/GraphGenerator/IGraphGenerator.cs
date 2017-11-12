using AsyncSimulator;
using System.Windows.Forms;

namespace VisualInterface.GraphGenerator
{
    internal interface IGraphGenerator
    {
        void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, AlgorithmType SelectedAlgorithm);
    }
}
