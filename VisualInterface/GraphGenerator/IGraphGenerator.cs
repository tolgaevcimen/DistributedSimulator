using AsyncSimulator;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VisualInterface.GraphGenerator
{
    public interface IGraphGenerator
    {
        void Generate(int nodeCount, Presenter parentForm, Panel drawing_panel, List<_Node> AllNodes, List<VisualEdge> AllEdges, string SelectedAlgorithm);
    }
}
