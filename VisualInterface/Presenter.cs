using AllNodes;
using AsyncSimulator;
using SupportedAlgorithmAndGraphTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualInterface.GraphGenerator;
using VisualInterface.GraphPersistancy;
using VisualInterface.PerformanceAnalyserSection;

namespace VisualInterface
{
    public partial class Presenter : Form
    {
        public EdgeHolder EdgeHolder { get; set; }
        public NodeHolder NodeHolder { get; set; }

        public DrawingPanelHelper DrawingPanelHelper { get; set; }

        public GraphPersister GraphPersister { get; set; }

        public PerformanceAnalyserFormOperations PerformanceAnalyserFormOperations { get; set; }

        /// <summary>
        /// Initiates the presenter form.
        /// </summary>
        public Presenter()
        {
            InitializeComponent();

            cb_choose_alg.Items.AddRange(Algorithms.ToArray());
            cb_choose_alg.SelectedIndex = 0;
            SelectedAlgorithm = Algorithms[cb_choose_alg.SelectedIndex];

            cb_graph_type.Items.AddRange(Enum.GetNames(typeof(GraphType)));
            cb_graph_type.SelectedIndex = 0;

            DrawingPanelHelper = new DrawingPanelHelper(this, drawing_panel, SelectedAlgorithm);
            EdgeHolder = new EdgeHolder();
            NodeHolder = new NodeHolder();
            NodeHolder.Terminated += TerminationDetected;

            clb_algorithmTypes.Items.AddRange(Algorithms.ToArray());
            clb_graphTypes.Items.AddRange(Enum.GetNames(typeof(GraphType)));

            visualSimulatorPanel.Dock = DockStyle.Fill; // remove this

            PerformanceAnalyserFormOperations = new PerformanceAnalyserFormOperations(this);
            btn_cancel.Click += new EventHandler(PerformanceAnalyserFormOperations.btn_cancel_Click);
            btn_runPerformanceAnalysis.Click += new EventHandler(PerformanceAnalyserFormOperations.btn_runPerformanceAnalysis_Click);

            GraphPersister = new GraphPersister(this);
            saveTopologyToolStripMenuItem.Click += GraphPersister.SaveTopology;
			importTopologyToolStripMenuItem.Click += GraphPersister.ImportTopology;
        }

        private void TerminationDetected(object sender, EventArgs e)
        {
            btn_proof_Click(null, null);
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
        private List<string> Algorithms = NodeTypeGatherer.AlgorithmTypes();

        /// <summary>
        /// Holds the selected algorithms name.
        /// </summary>
        public string SelectedAlgorithm { get; set; }

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
            foreach (var node in NodeHolder.GetCopyList().AsParallel())
            {
                Task.Run(() =>
                {
                    node.UserDefined_SingleInitiatorProcedure();
                });
            };
            NodeHolder.StartTerminationDetection();
        }

        public void DisableAlgorthmChange()
        {
            cb_choose_alg.Enabled = false;
        }

        private void ShowPerformanceAnalyser(object sender, EventArgs e)
        {
            performanceAnalyserPanel.Dock = DockStyle.Fill; // remove this
            visualSimulatorPanel.Dock = DockStyle.Fill; // remove this

            visualSimulatorPanel.Visible = false;
            performanceAnalyserPanel.Visible = true;
        }

        private void ShowVisualSimulator(object sender, EventArgs e)
        {
            performanceAnalyserPanel.Dock = DockStyle.Fill; // remove this
            visualSimulatorPanel.Dock = DockStyle.Fill; // remove this

            performanceAnalyserPanel.Visible = false;
            visualSimulatorPanel.Visible = true;

            NodeHolder.RedrawAllNodes();
        }
    }
}
