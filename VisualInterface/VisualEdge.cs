using AsyncSimulator;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;

namespace VisualInterface
{
    public class VisualEdge : IEdge 
    {
        public _Node Node1 { get; set; }
        public _Node Node2 { get; set; }

        Color EdgeColor { get; set; }
        Color SelectedEdgeColor { get; set; }
        Color RevertedEdgeColor { get; set; }

        int Thickness = 7;
        GraphicsPath Path { get; set; }
        Pen SolidPen { get; set; }

        Pen LastUsedPen { get; set; }

        PaintEventArgs PaintArgs { get; set; }

        public VisualEdge(PaintEventArgs e, _Node node1, _Node node2)
        {
            EdgeColor = Color.Pink;
            SelectedEdgeColor = Color.LightBlue;
            RevertedEdgeColor = Color.Black;
            
            SolidPen = new Pen(EdgeColor, Thickness);
            LastUsedPen = SolidPen;
            
            Node1 = node1;
            Node2 = node2;

            PaintArgs = e;

            HandleVisualization();
            HandleNeighbourhood();
            HandleSelfStabilization();
        }

        void HandleVisualization()
        {
            _Draw(SolidPen);

            Path = new GraphicsPath();
            Path.AddLine(Node1.Visualizer.Location, Node2.Visualizer.Location);

            /// redraw the nodes
            Node1.Visualizer.Draw(Node1.Selected());
            Node2.Visualizer.Draw(Node2.Selected());
        }

        void HandleNeighbourhood()
        {
            /// set neighbourhood
            Node1.Neighbours.Add(Node2);
            Node2.Neighbours.Add(Node1);
        }

        void HandleSelfStabilization()
        {
            if (Program.Presenter.cb_selfStab.Checked)
            {
                Node1.UserDefined_SingleInitiatorProcedure(Node1);
                Node2.UserDefined_SingleInitiatorProcedure(Node2);
            }
        }

        public void Colorify(bool reverted)
        {
            if (!reverted)
                LastUsedPen = new Pen(SelectedEdgeColor, Thickness);
            else
                LastUsedPen = new Pen(RevertedEdgeColor, Thickness);

            _Draw(null);
        }

        public void Delete(bool onlyEdgeDeleted)
        {
            _Draw(new Pen(Color.White, Thickness + 1));

            // set neighbourhood
            Node1.Neighbours.Remove(Node2);
            Node2.Neighbours.Remove(Node1);

            // redraw the nodes
            Node1.Visualizer.Draw(Node1.Selected());
            Node2.Visualizer.Draw(Node2.Selected());

            Program.Presenter.EdgeHolder.RemoveEdge(this);
            Program.Presenter.EdgeHolder.RedrawAllEdges();
            Program.Presenter.NodeHolder.RedrawAllNodes();

            if (Program.Presenter.cb_selfStab.Checked && onlyEdgeDeleted)
            {
                Task.Run(() => Node1.UserDefined_SingleInitiatorProcedure(Node1));
                Task.Run(() => Node2.UserDefined_SingleInitiatorProcedure(Node2));
            }
        }

        public void Draw()
        {
            _Draw(null);
        }

        void _Draw(Pen pen)
        {
            if (pen == null)
                pen = LastUsedPen;

            bool drawn = false;
            while (!drawn)
                try
                {
                    /// solidify the edge
                    PaintArgs.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    PaintArgs.Graphics.DrawLine(pen, Node1.Visualizer.Location, Node2.Visualizer.Location);

                    drawn = true;
                }
                catch { }
        }

        public _Node GetNode1()
        {
            return Node1;
        }

        public _Node GetNode2()
        {
            return Node2;
        }
        
        public bool OnPoint(Point location)
        {
            return Path.IsOutlineVisible(location, SolidPen);
        }
    }
}
