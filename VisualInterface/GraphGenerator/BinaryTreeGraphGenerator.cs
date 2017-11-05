using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AsyncSimulator;
using NodeGenerator;

namespace VisualInterface.GraphGenerator
{
    class BinaryTreeGraphGenerator : AbstractGraphGenerator
    {
        public BinaryTreeGraphGenerator(Presenter parentForm, Panel drawing_panel) : base(parentForm, drawing_panel)
        {
        }

        public override void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm)
        {
            var arg = new PaintEventArgs(drawing_panel.CreateGraphics(), new Rectangle());

            var queue = new Queue<int>();

            var totalHeight = Math.Floor(Math.Log(nodeCount, 2)) + 1;

            var verticalInterval = (int)((drawing_panel.Height - 80) / (totalHeight - 1));

            for (int i = 0; i < nodeCount; i++)
            {
                var currentDepth = (int)Math.Floor(Math.Log(i + 1, 2));

                var horizontalInterval = (int)((drawing_panel.Width) / (Math.Pow(2, currentDepth) + 1));

                var currentIndex = (int)(i - (Math.Pow(2, currentDepth) - 1) + 1);

                Console.WriteLine("i: {0}, depth: {1}, currentIndex: {2}", i, currentDepth, currentIndex);

                var p = new Point((currentIndex) * horizontalInterval, (currentDepth) * verticalInterval + 40);

                if (!nodeHolder.AnyIntersecting(p))
                {
                    var node = NodeFactory.Create(SelectedAlgorithm, nodeHolder.NodeCount, new WinformsNodeVisualiser(arg, p.X, p.Y, nodeHolder.NodeCount, parentForm), parentForm.cb_selfStab.Checked);

                    nodeHolder.AddNode(node);
                }
                else
                {
                    throw new Exception("Reached to screen limits");
                }
            };

            for (int i = 1; i < nodeCount; i++)
            {
                queue.Enqueue(i);
            }

            for (int i = 0; i < nodeCount; i++)
            {
                var parent = nodeHolder.GetNodeAt(i);

                for (int j = 0; j < 2; j++)
                {
                    if (!queue.Any()) break;

                    var firstChildId = queue.Dequeue();
                    var child = nodeHolder.GetNodeAt(firstChildId);
                    
                    edgeHolder.AddEgde(new WinformsEdge(arg, parent, child));
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
