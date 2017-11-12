using System.Windows.Forms;
using AsyncSimulator;

namespace VisualInterface.GraphGenerator
{
    public abstract class AbstractGraphGenerator : IGraphGenerator
    {
        internal Presenter ParentForm { get; set; }
        public Panel Drawing_panel { get; set; }

        internal AbstractGraphGenerator(Presenter parentForm, Panel drawing_panel)
        {
            ParentForm = parentForm;
            Drawing_panel = drawing_panel;
        }

        public abstract void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, AlgorithmType SelectedAlgorithm);
    }
}
