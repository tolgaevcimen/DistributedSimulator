using System.Windows.Forms;
using AsyncSimulator;

namespace VisualInterface.GraphGenerator
{
    public abstract class AbstractGraphGenerator : IGraphGenerator
    {
        internal Presenter parentForm { get; set; }
        public Panel drawing_panel { get; set; }

        internal AbstractGraphGenerator(Presenter parentForm, Panel drawing_panel)
        {
            parentForm = parentForm;
            drawing_panel = drawing_panel;
        }

        public abstract void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm);
    }
}
