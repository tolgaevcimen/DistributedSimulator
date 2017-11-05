using System;
using System.Drawing;
using System.Windows.Forms;
using AsyncSimulator;
using NodeGenerator;

namespace VisualInterface.GraphGenerator
{
    class CircleGraphGenerator : AbstractGraphGenerator
    {
        public CircleGraphGenerator(Presenter parentForm, Panel drawing_panel) : base(parentForm, drawing_panel)
        {
        }

        public override void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm)
        {
            var randomizer = new Random();
            var arg = new PaintEventArgs(drawing_panel.CreateGraphics(), new Rectangle());

            var radius = (Math.Min(drawing_panel.Height, drawing_panel.Width) - 80) / 2;
            var origin = new Point(drawing_panel.Width / 2, drawing_panel.Height / 2);

            for (int i = 0; i < nodeCount; i++)
            {
                var angle = 360 / (float)nodeCount * i;
                                
                var p = PointOnCircle(radius, angle, origin);

                if (!nodeHolder.AnyIntersecting(p))
                {
                    var node = NodeFactory.Create(SelectedAlgorithm, nodeHolder.NodeCount, new WinformsNodeVisualiser(arg, p.X, p.Y, nodeHolder.NodeCount, parentForm), parentForm.cb_selfStab.Checked);

                    nodeHolder.AddNode(node);
                }
                else
                {
                    i--;
                }
            };

            for (int i = 0; i < nodeCount; i++)
            {
                if (i != nodeCount - 1)
                {
                    var node1 =  nodeHolder.GetNodeAt(i);
                    var node2 =  nodeHolder.GetNodeAt(i + 1);

                    edgeHolder.AddEgde(new WinformsEdge(arg, node1, node2));
                }
                else
                {
                    var node1 = nodeHolder.GetNodeAt(i);
                    var node2 = nodeHolder.GetNodeAt(0);

                    edgeHolder.AddEgde(new WinformsEdge(arg, node1, node2));
                }
            }
        }

        PointF PointOnCircle(float radius, float angleInDegrees, PointF origin)
        {
            // Convert from degrees to radians via multiplication by PI/180        
            float x = (float)(radius * Math.Cos(angleInDegrees * Math.PI / 180F)) + origin.X;
            float y = (float)(radius * Math.Sin(angleInDegrees * Math.PI / 180F)) + origin.Y;

            return new PointF(x, y);
        }
    }
}
