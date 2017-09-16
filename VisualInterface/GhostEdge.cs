using AsyncSimulator;
using System.Drawing;
using System.Windows.Forms;

namespace VisualInterface
{
    class GhostEdge
    {
        public _Node Node1 { get; set; }

        PointF End1 { get; set; }
        PointF End2 { get; set; }
        Color EdgeColor { get; set; }

        private PaintEventArgs PaintArgs { get; set; }

        public GhostEdge(PaintEventArgs e, PointF end1, PointF end2, _Node node1)
        {
            EdgeColor = Color.Pink;

            End1 = end1;
            End2 = end2;

            Node1 = node1;
            PaintArgs = e;
        }

        public void Redirect(PaintEventArgs e, Point end2)
        {
            ControlPaint.DrawReversibleLine(Point.Round(End1), Point.Round(End2), EdgeColor);

            End2 = end2;

            ControlPaint.DrawReversibleLine(Point.Round(End1), Point.Round(End2), EdgeColor);
        }

        public void Vanish(PaintEventArgs e)
        {
            ControlPaint.DrawReversibleLine(Point.Round(End1), Point.Round(End2), EdgeColor);
        }

        public VisualEdge Solidify(_Node node2)
        {
            return new VisualEdge(PaintArgs, Node1, node2);
        }
    }
}
