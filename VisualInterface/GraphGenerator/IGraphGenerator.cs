using AsyncSimulator;
using System.Windows.Forms;

namespace VisualInterface.GraphGenerator
{
    internal interface IGraphGenerator
    {
        void Generate(int nodeCount, Presenter parentForm, Panel drawing_panel, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm);
    }
}
