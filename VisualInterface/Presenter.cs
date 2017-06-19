using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AsyncSimulator;
using UpdateBfsNode;
using VisualInterface.GraphGenerator;
using System.Threading.Tasks;

namespace VisualInterface
{
    public partial class Presenter : Form
    {
        /// <summary>
        /// Holds list of all currently drawn edges.
        /// </summary>
        public List<VisualEdge> AllEdges
        {
            get
            {
                lock (AllEdgesLock)
                    return _AllEdges;
            }
        }

        /// <summary>
        /// Holds list of all currently drawn nodes.
        /// </summary>
        public List<_Node> AllNodes
        {
            get
            {
                lock (AllNodesLock)
                    return _AllNodes;
            }
        }


        /// <summary>
        /// Holds list of all currently drawn edges.
        /// </summary>
        List<VisualEdge> _AllEdges { get; set; }

        /// <summary>
        /// Holds list of all currently drawn nodes.
        /// </summary>
        List<_Node> _AllNodes { get; set; }

        /// <summary>
        /// Holds the edge that just has been started to be drawn.
        /// </summary>
        private VisualEdge CurrentEdge { get; set; }

        public object AllNodesLock { get; set; }
        public object AllEdgesLock { get; set; }

        /// <summary>
        /// Initiates the presenter form.
        /// </summary>
        public Presenter()
        {
            InitializeComponent();
            _AllNodes = new List<_Node>();
            _AllEdges = new List<VisualEdge>();
            AllEdgesLock = new object();
            AllNodesLock = new object();

            cb_choose_alg.Items.AddRange(Algorithms.ToArray());
            cb_choose_alg.SelectedIndex = 7;
            SelectedAlgorithm = "GoddardMDS";

            cb_graph_type.Items.AddRange(Enum.GetNames(typeof(GraphType)));
            cb_graph_type.SelectedIndex = 1;
        }

        #region mouse events for Creating Nodes and Edges

        /// <summary>
        /// Previous location that mouse was clicked.
        /// </summary>
        private Point MouseStartPos { get; set; }

        /// <summary>
        /// Mouse down event. Tries to decide whether the mouse was clicked on a node or not, and behaves accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawing_panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;

            MouseStartPos = drawing_panel.PointToScreen(e.Location);
            var arg = new PaintEventArgs(drawing_panel.CreateGraphics(), new Rectangle());

            var startingNode = AllNodes.FirstOrDefault(n => n.Visualizer.OnIt(e.Location));

            if (startingNode != null)
                CurrentEdge = new VisualEdge(arg, MouseStartPos, MouseStartPos, startingNode);
        }

        /// <summary>
        /// Mouse up event. Checks if a node should be drawn or an edge (or nothing).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawing_panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                HandleNodeRemoval(e);
                HandleEdgeRemoval(e);
            }
            else if (e.Button == MouseButtons.Left)
            {
                HandleNodeOrEdgeCreation(e);
            }
        }

        void HandleNodeOrEdgeCreation(MouseEventArgs e)
        {
            var arg = new PaintEventArgs(drawing_panel.CreateGraphics(), new Rectangle());

            int x = e.X;
            int y = e.Y;

            var pos2 = drawing_panel.PointToScreen(e.Location);

            if (MouseStartPos == pos2)
            {
                if (!AllNodes.Any(n => n.Visualizer.Intersects(e.Location)))
                {
                    var node = NodeFactory.Create(SelectedAlgorithm, AllNodes.Count, new NodeVisualizer(arg, x, y, AllNodes.Count, this));
                    node.Visualizer.Draw(node.Selected());
                    AllNodes.Add(node);

                    cb_choose_alg.Enabled = false;
                }
            }
            else if (CurrentEdge != null)
            {
                var endingNode = AllNodes.FirstOrDefault(n => n.Visualizer.OnIt(e.Location) && n != CurrentEdge.Node1);

                if (endingNode != null)
                {
                    CurrentEdge.Solidify(drawing_panel.PointToClient(MouseStartPos), drawing_panel.PointToClient(pos2), endingNode, true);
                    AllEdges.Add(CurrentEdge);
                }
                else
                {
                    CurrentEdge.Vanish(arg);
                }
            }

            CurrentEdge = null;
        }

        void HandleNodeRemoval(MouseEventArgs e)
        {
            var clickedNode = AllNodes.FirstOrDefault(node => node.Visualizer.OnIt(e.Location));
            if (clickedNode != null)
            {
                var edgesToBeRemoved = new List<VisualEdge>();
                var nodesToBePoked = new List<_Node>();

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

                foreach (var edge in edgesToBeRemoved)
                {
                    edge.Delete();
                }

                clickedNode.Visualizer.Delete();
                AllEdges.ForEach(edge => edge.Draw(null));

                if (cb_selfStab.Checked)
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
        }

        void HandleEdgeRemoval(MouseEventArgs e)
        {
            var clickedEdge = AllEdges.FirstOrDefault(edge => edge.Path.IsOutlineVisible(e.Location, edge.SolidPen));
            if (clickedEdge != null)
            {
                clickedEdge.Delete();
            }
        }

        /// <summary>
        /// Gives the feel of creating an edge interactively.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawing_panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentEdge != null)
            {
                var arg = new PaintEventArgs(drawing_panel.CreateGraphics(), new Rectangle());

                var newPos = drawing_panel.PointToScreen(e.Location);
                CurrentEdge.Refresh(arg, newPos);
            }
        }

        /// <summary>
        /// Creates randomly positioned nodes and ties them to each other in a given percentage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_random_nodes_Click(object sender, EventArgs e)
        {
            var nodeCount = 0;
            if (!int.TryParse(tbNodeCount.Text, out nodeCount)) return;

            var graphGenerator = GraphFactory.GetGraphGenerator((GraphType)Enum.Parse(typeof(GraphType), (string)cb_graph_type.SelectedItem));

            graphGenerator.Generate(nodeCount, this, drawing_panel, AllNodes, AllEdges, SelectedAlgorithm);
        }

        #endregion

        #region mouse events for clearing the canvas

        /// <summary>
        /// Clears the drawing panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            var arg = new PaintEventArgs(drawing_panel.CreateGraphics(), new Rectangle());
            arg.Graphics.Clear(Color.White);
            
            AllNodes.Clear();
            AllEdges.Clear();

            tb_console.Clear();

            cb_choose_alg.Enabled = true;
            if (cb_choose_alg.SelectedIndex == -1) cb_choose_alg.SelectedIndex = 1;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_proof_Click(object sender, EventArgs e)
        {
            var invalidNodes = AllNodes.Where(n => !n.IsValid());

            if (!invalidNodes.Any())
            {
                MessageBox.Show("Each node is valid");
            }
            else
            {
                MessageBox.Show(string.Format("Some nodes are invalid: {0}", string.Join(",", invalidNodes.Select(n => n.Id.ToString()))));
            }
        }

        #endregion

        #region algorithms

        /// <summary>
        /// Holds the list of algorithms. When new ones are added here, please add them to the factory implementation too.
        /// </summary>
        private List<string> Algorithms = new List<string> { "Flooding", "FloodST", "UpdateBFS", "NeighDFS", "ChiuDS_allWait", "ChiuDS_allIn", "ChiuDS_rand", "GoddardMDS" };

        /// <summary>
        /// Holds the selected algorithms name.
        /// </summary>
        private string SelectedAlgorithm { get; set; }

        /// <summary>
        /// Event for setting the current algorithm. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_choose_alg_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedAlgorithm = cb_choose_alg.SelectedItem.ToString();
        }

        #endregion

        /// <summary>
        /// Starts the simulation of the algorithm run.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_run_Click(object sender, EventArgs e)
        {
            var firstNode = AllNodes.FirstOrDefault(fn => fn.Id == AllNodes.Min(n => n.Id));
            if (firstNode == null) return;

            var initiator = NodeFactory.Create(SelectedAlgorithm, -1, null);
            firstNode.UserDefined_SingleInitiatorProcedure(firstNode);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AllEdges.FirstOrDefault().Delete();
        }

    }
}
