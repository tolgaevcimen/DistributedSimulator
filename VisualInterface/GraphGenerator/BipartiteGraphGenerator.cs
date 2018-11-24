using System;
using System.Drawing;
using System.Windows.Forms;
using AsyncSimulator;
using NodeGenerator;
using SupportedAlgorithmAndGraphTypes;

namespace VisualInterface.GraphGenerator
{
    class BipartiteGraphGenerator : AbstractGraphGenerator
    {
        public BipartiteGraphGenerator(Presenter parentForm, Panel drawing_panel) : base(parentForm, drawing_panel)
        {
        }

        public override void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm)
        {
            var arg = new PaintEventArgs(Drawing_panel.CreateGraphics(), new Rectangle());

            var horizontalInterval = (Drawing_panel.Width - 80) / 3;
            var verticalInterval = (Drawing_panel.Height - 80) / ((nodeCount / 2) - (nodeCount % 2 == 0 ? 1 : 0));

            for (int i = 0; i < nodeCount; i++)
            {
                var x = i < nodeCount / 2 ?
                    40 + horizontalInterval :
                    40 + (horizontalInterval * 2);

                var y = 40 + (verticalInterval * (i < (nodeCount / 2) ? 
                    i : 
                    i - (nodeCount / 2)));

                var p = new Point(x, y);

                if (!nodeHolder.AnyIntersecting(p))
                {
                    var node = NodeFactory.Create(SelectedAlgorithm, nodeHolder.NodeCount, new WinformsNodeVisualiser(arg, p.X, p.Y, nodeHolder.NodeCount, ParentForm), ParentForm.cb_selfStab.Checked, nodeHolder);

                    nodeHolder.AddNode(node);
                }
                else
                {
                    i--;
                }
            };

            for (int i = 0; i < nodeCount / 2; i++)
            {
                for (int j = nodeCount / 2; j < nodeCount; j++)
                {
                    var node1 = nodeHolder.GetNodeAt(i);
                    var node2 = nodeHolder.GetNodeAt(j);
                    
                    edgeHolder.AddEgde(new WinformsEdge(arg, node1, node2));
                }
            }
        }
    }
}
