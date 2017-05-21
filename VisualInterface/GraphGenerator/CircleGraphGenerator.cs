using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AsyncSimulator;

namespace VisualInterface.GraphGenerator
{
    class CircleGraphGenerator : IGraphGenerator
    {
        public void Generate(int nodeCount, Presenter parentForm, Panel drawing_panel, List<_Node> AllNodes, List<VisualEdge> AllEdges, string SelectedAlgorithm)
        {
            var randomizer = new Random();
            var arg = new PaintEventArgs(drawing_panel.CreateGraphics(), new Rectangle());

            var radius = (Math.Min(drawing_panel.Height, drawing_panel.Width) - 80) / 2;
            var origin = new Point(drawing_panel.Width / 2, drawing_panel.Height / 2);

            for (int i = 0; i < nodeCount; i++)
            {
                var angle = 360 / (float)nodeCount * i;
                                
                var p = PointOnCircle(radius, angle, origin);

                if (!AllNodes.Any(n => n.Visualizer.Intersects(p)))
                {
                    var node = NodeFactory.Create(SelectedAlgorithm, AllNodes.Count, new NodeVisualizer(arg, p.X, p.Y, AllNodes.Count, parentForm));

                    AllNodes.Add(node);
                }
                else
                {
                    i--;
                }
            };

            for (int i = 0; i < nodeCount; i++)
            {
                if (i != 0)
                {
                    var node1 = AllNodes[i];
                    var node2 = AllNodes[i - 1];

                    //AllNodes[i].AddNeighbor(AllNodes[i - 1]);

                    var edge = new VisualEdge(arg, node1.Visualizer.Location, node2.Visualizer.Location, node1, ghost: true);
                    edge.Solidify(node1.Visualizer.Location, node2.Visualizer.Location, node2, true);
                    AllEdges.Add(edge);
                }
                if (i != nodeCount - 1)
                {
                    var node1 = AllNodes[i];
                    var node2 = AllNodes[i + 1];

                    //AllNodes[i].AddNeighbor(AllNodes[i + 1]);

                    var edge = new VisualEdge(arg, node1.Visualizer.Location, node2.Visualizer.Location, node1, ghost: true);
                    edge.Solidify(node1.Visualizer.Location, node2.Visualizer.Location, node2, true);
                    AllEdges.Add(edge);
                }
                else
                {
                    var node1 = AllNodes[0];
                    var node2 = AllNodes[i];

                    //AllNodes[i].AddNeighbor(AllNodes[i + 1]);

                    var edge = new VisualEdge(arg, node1.Visualizer.Location, node2.Visualizer.Location, node1, ghost: true);
                    edge.Solidify(node1.Visualizer.Location, node2.Visualizer.Location, node2, true);
                    AllEdges.Add(edge);
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
