using AsyncSimulator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualInterface
{
    public class DrawingPanelHelper
    {
        Presenter PresenterForm { get; set; }
        Panel DrawingPanel { get; set; }

        /// <summary>
        /// Previous location that mouse was clicked.
        /// </summary>
        Point MouseStartPos { get; set; }
        /// <summary>
        /// Holds the selected algorithms name.
        /// </summary>
        string SelectedAlgorithm { get; set; }
        /// <summary>
        /// Holds the edge that just has been started to be drawn.
        /// </summary>
        VisualEdge EdgeBeingDrawn { get; set; }

        List<VisualEdge> AllEdges { get { return PresenterForm.AllEdges; } }
        List<_Node> AllNodes { get { return PresenterForm.AllNodes; } }

        public bool SelfStabModeEnabled { get; set; }

        public DrawingPanelHelper(Presenter presenterForm, Panel drawingPanel, string selectedAlgorithm)
        {
            PresenterForm = presenterForm;
            DrawingPanel = drawingPanel;
            SelectedAlgorithm = selectedAlgorithm;

            BindEvents();
        }

        void BindEvents()
        {
            DrawingPanel.MouseDown += DrawingPanel_MouseDown;
            DrawingPanel.MouseMove += DrawingPanel_MouseMove;
            DrawingPanel.MouseUp += DrawingPanel_MouseUp;
            DrawingPanel.Resize += DrawingPanel_Resize;
        }

        void DrawingPanel_Resize(object sender, EventArgs e)
        {
            AllEdges.ForEach(ed => ed.Restore());
            AllNodes.ForEach(n => n.Visualizer.Draw(n.Selected()));
        }

        /// <summary>
        /// Mouse down event. Tries to decide whether the mouse was clicked on a node or not, and behaves accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DrawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;

            MouseStartPos = GetCurrentMousePosition(e);
            var startingNode = GetNodeAt(e);

            if (startingNode != null)
                EdgeBeingDrawn = new VisualEdge(GetNewPaintEventArgs(), MouseStartPos, MouseStartPos, startingNode);
        }

        /// <summary>
        /// Gives the feel of creating an edge interactively.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DrawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (EdgeBeingDrawn == null) return;

            var newPos = GetCurrentMousePosition(e);
            EdgeBeingDrawn.Refresh(GetNewPaintEventArgs(), newPos);
        }

        /// <summary>
        /// Mouse up event. Checks if a node should be drawn or an edge (or nothing).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DrawingPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                HandleNodeOrEdgeCreation(e);
            }
            else if (e.Button == MouseButtons.Right)
            {
                HandleNodeRemoval(e);
                HandleEdgeRemoval(e);
            }
        }

        void HandleNodeOrEdgeCreation(MouseEventArgs e)
        {
            var pea = GetNewPaintEventArgs();
            var currentMousePosition = GetCurrentMousePosition(e);

            if (MouseStartPos == currentMousePosition)
            {
                if (!AllNodes.Any(n => n.Visualizer.Intersects(e.Location)))
                {
                    var node = NodeFactory.Create(SelectedAlgorithm, AllNodes.Count, GetNewNodeVisualizer(e, pea));
                    node.Visualizer.Draw(node.Selected());
                    AllNodes.Add(node);

                    PresenterForm.DisableAlgorthmChange();
                }
            }
            else if (EdgeBeingDrawn != null)
            {
                var endingNode = AllNodes.FirstOrDefault(n => n.Visualizer.OnIt(e.Location) && n != EdgeBeingDrawn.Node1);

                if (endingNode != null)
                {
                    EdgeBeingDrawn.Solidify(DrawingPanel.PointToClient(MouseStartPos), DrawingPanel.PointToClient(currentMousePosition), endingNode, true);
                    AllEdges.Add(EdgeBeingDrawn);
                }
                else
                {
                    EdgeBeingDrawn.Vanish(pea);
                }
            }

            EdgeBeingDrawn = null;
        }

        void HandleNodeRemoval(MouseEventArgs e)
        {
            var clickedNode = GetNodeAt(e);
            if (clickedNode == null) return;

            var edgesToBeRemoved = new List<VisualEdge>();
            var nodesToBePoked = new List<_Node>();

            // go through all edges, find the nodes and edges to be removed
            foreach (var edge in AllEdges)
            {
                if (edge.Node1 == clickedNode)
                {
                    edgesToBeRemoved.Add(edge);
                    nodesToBePoked.Add(edge.Node2);
                }
                else if (edge.Node2 == clickedNode)
                {
                    edgesToBeRemoved.Add(edge);
                    nodesToBePoked.Add(edge.Node1);
                }
            }

            // delete each edge one by one
            //edgesToBeRemoved.ForEach(edge => edge.Delete());
            edgesToBeRemoved.AsParallel().ForAll(edge => edge.Delete());

            clickedNode.Visualizer.Delete();
            AllEdges.ForEach(edge => edge.Draw(null));

            if (SelfStabModeEnabled)
            {
                foreach (var node in nodesToBePoked)
                {
                    Task.Run(() =>
                    {
                        node.UserDefined_SingleInitiatorProcedure(node);
                    });
                }
            }

            AllNodes.Remove(clickedNode);
            AllNodes.ForEach(node => node.Visualizer.Draw(node.Selected()));
        }

        void HandleEdgeRemoval(MouseEventArgs e)
        {
            var clickedEdge = AllEdges.FirstOrDefault(edge => edge.Path.IsOutlineVisible(e.Location, edge.SolidPen));
            if (clickedEdge != null)
            {
                clickedEdge.Delete();
            }
        }

        NodeVisualizer GetNewNodeVisualizer(MouseEventArgs e, PaintEventArgs pea)
        {
            return new NodeVisualizer(pea, e.X, e.Y, AllNodes.Count, PresenterForm);
        }

        PaintEventArgs GetNewPaintEventArgs()
        {
            return new PaintEventArgs(DrawingPanel.CreateGraphics(), new Rectangle());
        }

        Point GetCurrentMousePosition(MouseEventArgs e)
        {
            return DrawingPanel.PointToScreen(e.Location);
        }

        _Node GetNodeAt(MouseEventArgs e)
        {
            return AllNodes.FirstOrDefault(n => n.Visualizer.OnIt(e.Location));
        }
    }
}
