using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AsyncSimulator;

namespace ConsoleEnvironment.GraphGenerator
{
    class BinaryTreeGraphGenerator : AbstractGraphGenerator
    {
        public override void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm)
        {
            base.Generate(nodeCount, nodeHolder, edgeHolder, SelectedAlgorithm);

            var queue = new Queue<int>();
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
                    
                    edgeHolder.AddEgde(new ConsoleEdge(parent, child));
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
