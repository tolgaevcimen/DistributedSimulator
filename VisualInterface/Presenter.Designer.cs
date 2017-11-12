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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tbNodeCount = new System.Windows.Forms.TextBox();
            this.tb_console = new System.Windows.Forms.TextBox();
            this.btn_proof = new System.Windows.Forms.Button();
            this.cb_choose_alg = new System.Windows.Forms.ComboBox();
            this.btn_run_update_bfs = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_random_nodes = new System.Windows.Forms.Button();
            this.cb_graph_type = new System.Windows.Forms.ComboBox();
            this.cb_selfStab = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.drawing_panel.Size = new System.Drawing.Size(546, 549);
            this.drawing_panel.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.Controls.Add(this.drawing_panel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(854, 565);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.68595F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.31405F));
            this.tableLayoutPanel2.Controls.Add(this.tbNodeCount, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.tb_console, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_proof, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.cb_choose_alg, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_run_update_bfs, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btn_clear, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.btn_random_nodes, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.cb_graph_type, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.cb_selfStab, 0, 7);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(558, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(292, 557);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tbNodeCount
            // 
            this.tbNodeCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNodeCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tbNodeCount.Location = new System.Drawing.Point(226, 339);
            this.tbNodeCount.Margin = new System.Windows.Forms.Padding(0, 7, 4, 11);
            this.tbNodeCount.Name = "tbNodeCount";
            this.tbNodeCount.Size = new System.Drawing.Size(62, 31);
            this.tbNodeCount.TabIndex = 12;
            this.tbNodeCount.Text = "10";
            // 
            // tb_console
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.tb_console, 2);
            this.tb_console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_console.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tb_console.Location = new System.Drawing.Point(4, 4);
            this.tb_console.Margin = new System.Windows.Forms.Padding(4);
            this.tb_console.Multiline = true;
            this.tb_console.Name = "tb_console";
            this.tb_console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_console.Size = new System.Drawing.Size(284, 234);
            this.tb_console.TabIndex = 3;
            // 
            // btn_proof
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.btn_proof, 2);
            this.btn_proof.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_proof.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_proof.Location = new System.Drawing.Point(3, 470);
            this.btn_proof.Name = "btn_proof";
            this.btn_proof.Size = new System.Drawing.Size(286, 39);
            this.btn_proof.TabIndex = 6;
            this.btn_proof.Text = "Check Correctness";
            this.btn_proof.UseVisualStyleBackColor = true;
            this.btn_proof.Click += new System.EventHandler(this.btn_proof_Click);
            // 
            // cb_choose_alg
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.cb_choose_alg, 2);
            this.cb_choose_alg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_choose_alg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cb_choose_alg.FormattingEnabled = true;
            this.cb_choose_alg.Location = new System.Drawing.Point(3, 245);
            this.cb_choose_alg.Name = "cb_choose_alg";
            this.cb_choose_alg.Size = new System.Drawing.Size(286, 39);
            this.cb_choose_alg.TabIndex = 10;
            this.cb_choose_alg.SelectedIndexChanged += new System.EventHandler(this.cb_choose_alg_SelectedIndexChanged);
            // 
            // btn_run_update_bfs
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.btn_run_update_bfs, 2);
            this.btn_run_update_bfs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_run_update_bfs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_run_update_bfs.Location = new System.Drawing.Point(3, 290);
            this.btn_run_update_bfs.Name = "btn_run_update_bfs";
            this.btn_run_update_bfs.Size = new System.Drawing.Size(286, 39);
            this.btn_run_update_bfs.TabIndex = 9;
            this.btn_run_update_bfs.Text = "Run";
            this.btn_run_update_bfs.UseVisualStyleBackColor = true;
            this.btn_run_update_bfs.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // btn_clear
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.btn_clear, 2);
            this.btn_clear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_clear.Location = new System.Drawing.Point(4, 426);
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
            this.tableLayoutPanel2.SetColumnSpan(this.btn_random_nodes, 2);
            this.btn_random_nodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_random_nodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_random_nodes.Location = new System.Drawing.Point(4, 381);
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
            this.cb_graph_type.Location = new System.Drawing.Point(3, 335);
            this.cb_graph_type.Name = "cb_graph_type";
            this.cb_graph_type.Size = new System.Drawing.Size(220, 39);
            this.cb_graph_type.TabIndex = 11;
            // 
            // cb_selfStab
            // 
            this.cb_selfStab.AutoSize = true;
            this.cb_selfStab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_selfStab.Location = new System.Drawing.Point(3, 515);
            this.cb_selfStab.Name = "cb_selfStab";
            this.cb_selfStab.Size = new System.Drawing.Size(220, 39);
            this.cb_selfStab.TabIndex = 13;
            this.cb_selfStab.Text = "Self Stab Mode";
            this.cb_selfStab.UseVisualStyleBackColor = true;
            // 
            // Presenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 565);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Presenter";
            this.Text = "Presenter";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel drawing_panel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_clear;
        public System.Windows.Forms.TextBox tb_console;
        private System.Windows.Forms.Button btn_random_nodes;
        private System.Windows.Forms.Button btn_proof;
        private System.Windows.Forms.Button btn_run_update_bfs;
        private System.Windows.Forms.ComboBox cb_choose_alg;
        private System.Windows.Forms.TextBox tbNodeCount;
        private System.Windows.Forms.ComboBox cb_graph_type;
        public System.Windows.Forms.CheckBox cb_selfStab;
    }
}

