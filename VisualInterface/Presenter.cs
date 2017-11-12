using AsyncSimulator;
using NodeGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualInterface.GraphGenerator;

namespace VisualInterface
{
    internal partial class Presenter : Form
    {
        public EdgeHolder EdgeHolder { get; set; }
        public NodeHolder NodeHolder { get; set; }

        public DrawingPanelHelper DrawingPanelHelper { get; set; }

        /// <summary>
        /// Initiates the presenter form.
        /// </summary>
        public Presenter()
        {
            InitializeComponent();

            cb_choose_alg.Items.AddRange(Enum.GetNames(typeof(AlgorithmType)));
            cb_choose_alg.SelectedIndex = 3;
            SelectedAlgorithm = Algorithms[cb_choose_alg.SelectedIndex];

            cb_graph_type.Items.AddRange(Enum.GetNames(typeof(GraphType)));
            cb_graph_type.SelectedIndex = 0;

            DrawingPanelHelper = new DrawingPanelHelper(this, drawing_panel, SelectedAlgorithm);
            EdgeHolder = new EdgeHolder();
            NodeHolder = new NodeHolder();
        }

        #region mouse events for Creating Nodes and Edges
                
        /// <summary>
        /// Creates randomly positioned nodes and ties them to each other in a given percentage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_generate_nodes_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(tbNodeCount.Text, out int nodeCount)) return;

            var graphGenerator = GraphFactory.GetGraphGenerator((GraphType)Enum.Parse(typeof(GraphType), (string)cb_graph_type.SelectedItem), this, drawing_panel);

            graphGenerator.Generate(nodeCount, NodeHolder, EdgeHolder, SelectedAlgorithm);
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
            DrawingPanelHelper.ClearPanel();

            NodeHolder.EmptyAllNodes();
            EdgeHolder.EmptyAllEdges();

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
            var invalidNodes = NodeHolder.GetCopyList().Where(n => !n.IsValid());

            var runCountReport = string.Join("\n", NodeHolder.GetCopyList().Select(n => string.Format("node: {0}\t runCount: {1}", n.Id, n.MoveCount)));
            runCountReport += string.Format("\ntotal run count: {0}", NodeHolder.GetCopyList().Sum(n => n.MoveCount));

            if (!invalidNodes.Any())
            {
                MessageBox.Show(string.Format("{0}\nRun count report:\n{1}", "Each node is valid", runCountReport));
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
        private List<AlgorithmType> Algorithms = Enum.GetNames(typeof(AlgorithmType)).Select(atn => (AlgorithmType)Enum.Parse(typeof(AlgorithmType), atn)).ToList();

        /// <summary>
        /// Holds the selected algorithms name.
        /// </summary>
        private AlgorithmType SelectedAlgorithm { get; set; }

        /// <summary>
        /// Event for setting the current algorithm. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_choose_alg_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedAlgorithm = Algorithms[cb_choose_alg.SelectedIndex];
        }

        #endregion

        /// <summary>
        /// Starts the simulation of the algorithm run.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_run_Click(object sender, EventArgs e)
        {
            //var firstNode = NodeHolder.GetMinimumNode();
            //if (firstNode == null) return;

            //var initiator = NodeFactory.Create(SelectedAlgorithm, -1, null, cb_selfStab.Checked);
            //firstNode.UserDefined_SingleInitiatorProcedure(firstNode);


            foreach (var node in NodeHolder.GetCopyList().AsParallel())
            {
                Task.Run(() =>
                {
                    node.UserDefined_SingleInitiatorProcedure(null);
                });
                //Console.WriteLine(node.Id);
            };
        }

        public void DisableAlgorthmChange()
        {
            cb_choose_alg.Enabled = false;
        }
    }
}
