namespace VisualInterface
{
    partial class Presenter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.drawing_panel = new System.Windows.Forms.Panel();
            this.visualSimulatorPanel = new System.Windows.Forms.TableLayoutPanel();
            this.menuPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tbNodeCount = new System.Windows.Forms.TextBox();
            this.tb_console = new System.Windows.Forms.TextBox();
            this.btn_proof = new System.Windows.Forms.Button();
            this.cb_choose_alg = new System.Windows.Forms.ComboBox();
            this.btn_run_update_bfs = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_random_nodes = new System.Windows.Forms.Button();
            this.cb_graph_type = new System.Windows.Forms.ComboBox();
            this.cb_selfStab = new System.Windows.Forms.CheckBox();
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTopologyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTopologyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTopologyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.performanceAnalyserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualSimulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.performanceAnalyserPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btn_runPerformanceAnalysis = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_topologyCount = new System.Windows.Forms.TextBox();
            this.tb_numberToIncreaseNodeCount = new System.Windows.Forms.TextBox();
            this.tb_nodeCountFold = new System.Windows.Forms.TextBox();
            this.clb_graphTypes = new System.Windows.Forms.CheckedListBox();
            this.clb_algorithmTypes = new System.Windows.Forms.CheckedListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_currentAlgorithm = new System.Windows.Forms.Label();
            this.lbl_currentTopologyType = new System.Windows.Forms.Label();
            this.lbl_currentNodeCount = new System.Windows.Forms.Label();
            this.lbl_currentTopologyIndex = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_sessionName = new System.Windows.Forms.TextBox();
            this.visualSimulatorPanel.SuspendLayout();
            this.menuPanel.SuspendLayout();
            this.menuBar.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.performanceAnalyserPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawing_panel
            // 
            this.drawing_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawing_panel.BackColor = System.Drawing.Color.White;
            this.drawing_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawing_panel.Location = new System.Drawing.Point(8, 8);
            this.drawing_panel.Margin = new System.Windows.Forms.Padding(8, 8, 0, 8);
            this.drawing_panel.Name = "drawing_panel";
            this.drawing_panel.Size = new System.Drawing.Size(303, 665);
            this.drawing_panel.TabIndex = 0;
            // 
            // visualSimulatorPanel
            // 
            this.visualSimulatorPanel.ColumnCount = 2;
            this.visualSimulatorPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.visualSimulatorPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.visualSimulatorPanel.Controls.Add(this.drawing_panel, 0, 0);
            this.visualSimulatorPanel.Controls.Add(this.menuPanel, 1, 0);
            this.visualSimulatorPanel.Location = new System.Drawing.Point(0, 0);
            this.visualSimulatorPanel.Margin = new System.Windows.Forms.Padding(4);
            this.visualSimulatorPanel.Name = "visualSimulatorPanel";
            this.visualSimulatorPanel.RowCount = 1;
            this.visualSimulatorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.visualSimulatorPanel.Size = new System.Drawing.Size(611, 681);
            this.visualSimulatorPanel.TabIndex = 1;
            // 
            // menuPanel
            // 
            this.menuPanel.ColumnCount = 2;
            this.menuPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.68595F));
            this.menuPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.31405F));
            this.menuPanel.Controls.Add(this.tbNodeCount, 1, 3);
            this.menuPanel.Controls.Add(this.tb_console, 0, 0);
            this.menuPanel.Controls.Add(this.btn_proof, 0, 6);
            this.menuPanel.Controls.Add(this.cb_choose_alg, 0, 1);
            this.menuPanel.Controls.Add(this.btn_run_update_bfs, 0, 2);
            this.menuPanel.Controls.Add(this.btn_clear, 0, 5);
            this.menuPanel.Controls.Add(this.btn_random_nodes, 0, 4);
            this.menuPanel.Controls.Add(this.cb_graph_type, 0, 3);
            this.menuPanel.Controls.Add(this.cb_selfStab, 0, 7);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuPanel.Location = new System.Drawing.Point(315, 4);
            this.menuPanel.Margin = new System.Windows.Forms.Padding(4);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.RowCount = 8;
            this.menuPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.menuPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.menuPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.menuPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.menuPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.menuPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.menuPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.menuPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.menuPanel.Size = new System.Drawing.Size(292, 673);
            this.menuPanel.TabIndex = 1;
            // 
            // tbNodeCount
            // 
            this.tbNodeCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNodeCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tbNodeCount.Location = new System.Drawing.Point(226, 455);
            this.tbNodeCount.Margin = new System.Windows.Forms.Padding(0, 7, 4, 11);
            this.tbNodeCount.Name = "tbNodeCount";
            this.tbNodeCount.Size = new System.Drawing.Size(62, 31);
            this.tbNodeCount.TabIndex = 12;
            this.tbNodeCount.Text = "10";
            // 
            // tb_console
            // 
            this.menuPanel.SetColumnSpan(this.tb_console, 2);
            this.tb_console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_console.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tb_console.Location = new System.Drawing.Point(4, 4);
            this.tb_console.Margin = new System.Windows.Forms.Padding(4);
            this.tb_console.Multiline = true;
            this.tb_console.Name = "tb_console";
            this.tb_console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_console.Size = new System.Drawing.Size(284, 350);
            this.tb_console.TabIndex = 3;
            // 
            // btn_proof
            // 
            this.menuPanel.SetColumnSpan(this.btn_proof, 2);
            this.btn_proof.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_proof.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_proof.Location = new System.Drawing.Point(3, 586);
            this.btn_proof.Name = "btn_proof";
            this.btn_proof.Size = new System.Drawing.Size(286, 39);
            this.btn_proof.TabIndex = 6;
            this.btn_proof.Text = "Check Correctness";
            this.btn_proof.UseVisualStyleBackColor = true;
            this.btn_proof.Click += new System.EventHandler(this.btn_proof_Click);
            // 
            // cb_choose_alg
            // 
            this.menuPanel.SetColumnSpan(this.cb_choose_alg, 2);
            this.cb_choose_alg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_choose_alg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cb_choose_alg.FormattingEnabled = true;
            this.cb_choose_alg.Location = new System.Drawing.Point(3, 361);
            this.cb_choose_alg.Name = "cb_choose_alg";
            this.cb_choose_alg.Size = new System.Drawing.Size(286, 39);
            this.cb_choose_alg.TabIndex = 10;
            this.cb_choose_alg.SelectedIndexChanged += new System.EventHandler(this.cb_choose_alg_SelectedIndexChanged);
            // 
            // btn_run_update_bfs
            // 
            this.menuPanel.SetColumnSpan(this.btn_run_update_bfs, 2);
            this.btn_run_update_bfs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_run_update_bfs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_run_update_bfs.Location = new System.Drawing.Point(3, 406);
            this.btn_run_update_bfs.Name = "btn_run_update_bfs";
            this.btn_run_update_bfs.Size = new System.Drawing.Size(286, 39);
            this.btn_run_update_bfs.TabIndex = 9;
            this.btn_run_update_bfs.Text = "Run";
            this.btn_run_update_bfs.UseVisualStyleBackColor = true;
            this.btn_run_update_bfs.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // btn_clear
            // 
            this.menuPanel.SetColumnSpan(this.btn_clear, 2);
            this.btn_clear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_clear.Location = new System.Drawing.Point(4, 542);
            this.btn_clear.Margin = new System.Windows.Forms.Padding(4);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(284, 37);
            this.btn_clear.TabIndex = 0;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_random_nodes
            // 
            this.menuPanel.SetColumnSpan(this.btn_random_nodes, 2);
            this.btn_random_nodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_random_nodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_random_nodes.Location = new System.Drawing.Point(4, 497);
            this.btn_random_nodes.Margin = new System.Windows.Forms.Padding(4);
            this.btn_random_nodes.Name = "btn_random_nodes";
            this.btn_random_nodes.Size = new System.Drawing.Size(284, 37);
            this.btn_random_nodes.TabIndex = 4;
            this.btn_random_nodes.Text = "Generate Nodes";
            this.btn_random_nodes.UseVisualStyleBackColor = true;
            this.btn_random_nodes.Click += new System.EventHandler(this.btn_generate_nodes_Click);
            // 
            // cb_graph_type
            // 
            this.cb_graph_type.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_graph_type.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cb_graph_type.FormattingEnabled = true;
            this.cb_graph_type.Location = new System.Drawing.Point(3, 451);
            this.cb_graph_type.Name = "cb_graph_type";
            this.cb_graph_type.Size = new System.Drawing.Size(220, 39);
            this.cb_graph_type.TabIndex = 11;
            // 
            // cb_selfStab
            // 
            this.cb_selfStab.AutoSize = true;
            this.cb_selfStab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_selfStab.Location = new System.Drawing.Point(3, 631);
            this.cb_selfStab.Name = "cb_selfStab";
            this.cb_selfStab.Size = new System.Drawing.Size(220, 39);
            this.cb_selfStab.TabIndex = 13;
            this.cb_selfStab.Text = "Self Stab Mode";
            this.cb_selfStab.UseVisualStyleBackColor = true;
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.performanceAnalyserToolStripMenuItem,
            this.visualSimulatorToolStripMenuItem});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(1661, 24);
            this.menuBar.TabIndex = 2;
            this.menuBar.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveTopologyToolStripMenuItem,
            this.exportTopologyToolStripMenuItem,
            this.importTopologyToolStripMenuItem,
            this.exportLogsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveTopologyToolStripMenuItem
            // 
            this.saveTopologyToolStripMenuItem.Name = "saveTopologyToolStripMenuItem";
            this.saveTopologyToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.saveTopologyToolStripMenuItem.Text = "Save Topology";
            // 
            // exportTopologyToolStripMenuItem
            // 
            this.exportTopologyToolStripMenuItem.Name = "exportTopologyToolStripMenuItem";
            this.exportTopologyToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.exportTopologyToolStripMenuItem.Text = "Export Topology";
            // 
            // importTopologyToolStripMenuItem
            // 
            this.importTopologyToolStripMenuItem.Name = "importTopologyToolStripMenuItem";
            this.importTopologyToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.importTopologyToolStripMenuItem.Text = "Import Topology";
            // 
            // exportLogsToolStripMenuItem
            // 
            this.exportLogsToolStripMenuItem.Name = "exportLogsToolStripMenuItem";
            this.exportLogsToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.exportLogsToolStripMenuItem.Text = "Export Logs";
            // 
            // performanceAnalyserToolStripMenuItem
            // 
            this.performanceAnalyserToolStripMenuItem.Name = "performanceAnalyserToolStripMenuItem";
            this.performanceAnalyserToolStripMenuItem.Size = new System.Drawing.Size(135, 20);
            this.performanceAnalyserToolStripMenuItem.Text = "Performance Analyser";
            this.performanceAnalyserToolStripMenuItem.Click += new System.EventHandler(this.TogglePanels);
            // 
            // visualSimulatorToolStripMenuItem
            // 
            this.visualSimulatorToolStripMenuItem.Name = "visualSimulatorToolStripMenuItem";
            this.visualSimulatorToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.visualSimulatorToolStripMenuItem.Text = "Visual Simulator";
            this.visualSimulatorToolStripMenuItem.Click += new System.EventHandler(this.TogglePanels);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.performanceAnalyserPanel);
            this.mainPanel.Controls.Add(this.visualSimulatorPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 24);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1661, 694);
            this.mainPanel.TabIndex = 3;
            // 
            // performanceAnalyserPanel
            // 
            this.performanceAnalyserPanel.ColumnCount = 3;
            this.performanceAnalyserPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.performanceAnalyserPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.performanceAnalyserPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.performanceAnalyserPanel.Controls.Add(this.btn_runPerformanceAnalysis, 1, 6);
            this.performanceAnalyserPanel.Controls.Add(this.label1, 0, 1);
            this.performanceAnalyserPanel.Controls.Add(this.label2, 0, 2);
            this.performanceAnalyserPanel.Controls.Add(this.label3, 0, 3);
            this.performanceAnalyserPanel.Controls.Add(this.label4, 0, 4);
            this.performanceAnalyserPanel.Controls.Add(this.label5, 0, 5);
            this.performanceAnalyserPanel.Controls.Add(this.tb_topologyCount, 1, 1);
            this.performanceAnalyserPanel.Controls.Add(this.tb_numberToIncreaseNodeCount, 1, 2);
            this.performanceAnalyserPanel.Controls.Add(this.tb_nodeCountFold, 1, 3);
            this.performanceAnalyserPanel.Controls.Add(this.clb_graphTypes, 1, 4);
            this.performanceAnalyserPanel.Controls.Add(this.clb_algorithmTypes, 1, 5);
            this.performanceAnalyserPanel.Controls.Add(this.label6, 0, 10);
            this.performanceAnalyserPanel.Controls.Add(this.label7, 0, 11);
            this.performanceAnalyserPanel.Controls.Add(this.label8, 0, 12);
            this.performanceAnalyserPanel.Controls.Add(this.label9, 0, 13);
            this.performanceAnalyserPanel.Controls.Add(this.lbl_currentAlgorithm, 1, 10);
            this.performanceAnalyserPanel.Controls.Add(this.lbl_currentTopologyType, 1, 11);
            this.performanceAnalyserPanel.Controls.Add(this.lbl_currentNodeCount, 1, 12);
            this.performanceAnalyserPanel.Controls.Add(this.lbl_currentTopologyIndex, 1, 13);
            this.performanceAnalyserPanel.Controls.Add(this.progressBar, 0, 14);
            this.performanceAnalyserPanel.Controls.Add(this.btn_cancel, 1, 7);
            this.performanceAnalyserPanel.Controls.Add(this.label10, 0, 0);
            this.performanceAnalyserPanel.Controls.Add(this.tb_sessionName, 1, 0);
            this.performanceAnalyserPanel.Location = new System.Drawing.Point(636, 23);
            this.performanceAnalyserPanel.Name = "performanceAnalyserPanel";
            this.performanceAnalyserPanel.RowCount = 17;
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.performanceAnalyserPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.performanceAnalyserPanel.Size = new System.Drawing.Size(869, 585);
            this.performanceAnalyserPanel.TabIndex = 2;
            this.performanceAnalyserPanel.Visible = false;
            // 
            // btn_runPerformanceAnalysis
            // 
            this.btn_runPerformanceAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_runPerformanceAnalysis.Location = new System.Drawing.Point(303, 323);
            this.btn_runPerformanceAnalysis.Name = "btn_runPerformanceAnalysis";
            this.btn_runPerformanceAnalysis.Size = new System.Drawing.Size(194, 24);
            this.btn_runPerformanceAnalysis.TabIndex = 5;
            this.btn_runPerformanceAnalysis.Text = "Start";
            this.btn_runPerformanceAnalysis.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Topology Count for Number of Node";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(294, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "Number to Increase Node Count";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Node Count Fold";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(294, 100);
            this.label4.TabIndex = 3;
            this.label4.Text = "Graph Types to Compare";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(294, 100);
            this.label5.TabIndex = 4;
            this.label5.Text = "Algorithm Types to Compare";
            // 
            // tb_topologyCount
            // 
            this.tb_topologyCount.AccessibleName = "Topology Count for Number of Node";
            this.tb_topologyCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_topologyCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_topologyCount.Location = new System.Drawing.Point(303, 33);
            this.tb_topologyCount.Name = "tb_topologyCount";
            this.tb_topologyCount.Size = new System.Drawing.Size(194, 23);
            this.tb_topologyCount.TabIndex = 6;
            this.tb_topologyCount.Text = "5";
            // 
            // tb_numberToIncreaseNodeCount
            // 
            this.tb_numberToIncreaseNodeCount.AccessibleName = "Number to Increase Node Count";
            this.tb_numberToIncreaseNodeCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_numberToIncreaseNodeCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_numberToIncreaseNodeCount.Location = new System.Drawing.Point(303, 63);
            this.tb_numberToIncreaseNodeCount.Name = "tb_numberToIncreaseNodeCount";
            this.tb_numberToIncreaseNodeCount.Size = new System.Drawing.Size(194, 23);
            this.tb_numberToIncreaseNodeCount.TabIndex = 7;
            this.tb_numberToIncreaseNodeCount.Text = "5";
            // 
            // tb_nodeCountFold
            // 
            this.tb_nodeCountFold.AccessibleName = "Node Count Fold";
            this.tb_nodeCountFold.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_nodeCountFold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_nodeCountFold.Location = new System.Drawing.Point(303, 93);
            this.tb_nodeCountFold.Name = "tb_nodeCountFold";
            this.tb_nodeCountFold.Size = new System.Drawing.Size(194, 23);
            this.tb_nodeCountFold.TabIndex = 8;
            this.tb_nodeCountFold.Text = "10";
            // 
            // clb_graphTypes
            // 
            this.clb_graphTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clb_graphTypes.FormattingEnabled = true;
            this.clb_graphTypes.Location = new System.Drawing.Point(303, 123);
            this.clb_graphTypes.Name = "clb_graphTypes";
            this.clb_graphTypes.Size = new System.Drawing.Size(194, 94);
            this.clb_graphTypes.TabIndex = 9;
            // 
            // clb_algorithmTypes
            // 
            this.clb_algorithmTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clb_algorithmTypes.FormattingEnabled = true;
            this.clb_algorithmTypes.Location = new System.Drawing.Point(303, 223);
            this.clb_algorithmTypes.Name = "clb_algorithmTypes";
            this.clb_algorithmTypes.Size = new System.Drawing.Size(194, 94);
            this.clb_algorithmTypes.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 380);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(294, 30);
            this.label6.TabIndex = 11;
            this.label6.Text = "Current Algorithm";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 410);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(294, 30);
            this.label7.TabIndex = 12;
            this.label7.Text = "Current Topology Type";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 440);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(294, 30);
            this.label8.TabIndex = 13;
            this.label8.Text = "Current Node Count";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 470);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(294, 30);
            this.label9.TabIndex = 14;
            this.label9.Text = "Current Topology Index";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_currentAlgorithm
            // 
            this.lbl_currentAlgorithm.AccessibleName = "";
            this.lbl_currentAlgorithm.AutoSize = true;
            this.lbl_currentAlgorithm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_currentAlgorithm.Location = new System.Drawing.Point(303, 380);
            this.lbl_currentAlgorithm.Name = "lbl_currentAlgorithm";
            this.lbl_currentAlgorithm.Size = new System.Drawing.Size(194, 30);
            this.lbl_currentAlgorithm.TabIndex = 15;
            this.lbl_currentAlgorithm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_currentTopologyType
            // 
            this.lbl_currentTopologyType.AccessibleName = "";
            this.lbl_currentTopologyType.AutoSize = true;
            this.lbl_currentTopologyType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_currentTopologyType.Location = new System.Drawing.Point(303, 410);
            this.lbl_currentTopologyType.Name = "lbl_currentTopologyType";
            this.lbl_currentTopologyType.Size = new System.Drawing.Size(194, 30);
            this.lbl_currentTopologyType.TabIndex = 16;
            this.lbl_currentTopologyType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_currentNodeCount
            // 
            this.lbl_currentNodeCount.AccessibleName = "";
            this.lbl_currentNodeCount.AutoSize = true;
            this.lbl_currentNodeCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_currentNodeCount.Location = new System.Drawing.Point(303, 440);
            this.lbl_currentNodeCount.Name = "lbl_currentNodeCount";
            this.lbl_currentNodeCount.Size = new System.Drawing.Size(194, 30);
            this.lbl_currentNodeCount.TabIndex = 17;
            this.lbl_currentNodeCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_currentTopologyIndex
            // 
            this.lbl_currentTopologyIndex.AccessibleName = "";
            this.lbl_currentTopologyIndex.AutoSize = true;
            this.lbl_currentTopologyIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_currentTopologyIndex.Location = new System.Drawing.Point(303, 470);
            this.lbl_currentTopologyIndex.Name = "lbl_currentTopologyIndex";
            this.lbl_currentTopologyIndex.Size = new System.Drawing.Size(194, 30);
            this.lbl_currentTopologyIndex.TabIndex = 18;
            this.lbl_currentTopologyIndex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.performanceAnalyserPanel.SetColumnSpan(this.progressBar, 3);
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(3, 503);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(863, 48);
            this.progressBar.TabIndex = 19;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_cancel.Location = new System.Drawing.Point(303, 353);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(194, 24);
            this.btn_cancel.TabIndex = 20;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(294, 30);
            this.label10.TabIndex = 21;
            this.label10.Text = "Session Name";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_sessionName
            // 
            this.tb_sessionName.AccessibleName = "Session Name";
            this.tb_sessionName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_sessionName.Location = new System.Drawing.Point(303, 3);
            this.tb_sessionName.Name = "tb_sessionName";
            this.tb_sessionName.Size = new System.Drawing.Size(194, 23);
            this.tb_sessionName.TabIndex = 22;
            // 
            // Presenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1661, 718);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuBar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.MainMenuStrip = this.menuBar;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Presenter";
            this.Text = "Self Stabilizing Distributed Simulator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.visualSimulatorPanel.ResumeLayout(false);
            this.menuPanel.ResumeLayout(false);
            this.menuPanel.PerformLayout();
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.performanceAnalyserPanel.ResumeLayout(false);
            this.performanceAnalyserPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel drawing_panel;
        private System.Windows.Forms.TableLayoutPanel visualSimulatorPanel;
        private System.Windows.Forms.TableLayoutPanel menuPanel;
        private System.Windows.Forms.Button btn_clear;
        public System.Windows.Forms.TextBox tb_console;
        private System.Windows.Forms.Button btn_random_nodes;
        private System.Windows.Forms.Button btn_proof;
        private System.Windows.Forms.Button btn_run_update_bfs;
        private System.Windows.Forms.ComboBox cb_choose_alg;
        private System.Windows.Forms.TextBox tbNodeCount;
        private System.Windows.Forms.ComboBox cb_graph_type;
        public System.Windows.Forms.CheckBox cb_selfStab;
        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem performanceAnalyserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTopologyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportTopologyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importTopologyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportLogsToolStripMenuItem;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TableLayoutPanel performanceAnalyserPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem visualSimulatorToolStripMenuItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Button btn_runPerformanceAnalysis;
        internal System.Windows.Forms.TextBox tb_topologyCount;
        internal System.Windows.Forms.TextBox tb_numberToIncreaseNodeCount;
        internal System.Windows.Forms.TextBox tb_nodeCountFold;
        internal System.Windows.Forms.CheckedListBox clb_graphTypes;
        internal System.Windows.Forms.CheckedListBox clb_algorithmTypes;
        internal System.Windows.Forms.Label lbl_currentAlgorithm;
        internal System.Windows.Forms.Label lbl_currentTopologyType;
        internal System.Windows.Forms.Label lbl_currentNodeCount;
        internal System.Windows.Forms.Label lbl_currentTopologyIndex;
        internal System.Windows.Forms.ProgressBar progressBar;
        internal System.Windows.Forms.Button btn_cancel;
        internal System.Windows.Forms.TextBox tb_sessionName;
    }
}

