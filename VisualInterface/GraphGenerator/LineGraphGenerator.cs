using System.Drawing;
using System.Windows.Forms;
using AsyncSimulator;
using NodeGenerator;

namespace VisualInterface.GraphGenerator
{
    class LineGraphGenerator : AbstractGraphGenerator
    {
        public LineGraphGenerator(Presenter parentForm, Panel drawing_panel) : base(parentForm, drawing_panel)
        {
        }

        public override void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm)
        {
            var arg = new PaintEventArgs(Drawing_panel.CreateGraphics(), new Rectangle());

            for (int i = 0; i < nodeCount; i++)
            {
                var interval = (Drawing_panel.Width - 80) / (nodeCount - 1);

                var p = new Point(40 + (interval * i), Drawing_panel.Height / 2);

                if (!nodeHolder.AnyIntersecting(p))
                {
                    var node = NodeFactory.Create(SelectedAlgorithm, nodeHolder.NodeCount, new WinformsNodeVisualiser(arg, p.X, p.Y, nodeHolder.NodeCount, ParentForm), ParentForm.cb_selfStab.Checked, nodeHolder);

                    nodeHolder.AddNode(node);
                }
                else
                {
                    MessageBox.Show("Nodes do not fit in screen");
                    break;
                }
            };

            for (int i = 0; i < nodeCount; i++)
            {
                if (i != nodeCount - 1)
                {
                    var node1 = nodeHolder.GetNodeAt(i);
                    var node2 = nodeHolder.GetNodeAt(i + 1);
                    
                    edgeHolder.AddEgde(new WinformsEdge(arg, node1, node2));
                }
            }
        }
    }
}
