namespace Kicker
{
    partial class HotKeyControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Col1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCtrl = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColAlt = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColShf = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColWin = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.dataGridView);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Padding = new System.Windows.Forms.Padding(9);
            this.groupBox.Size = new System.Drawing.Size(651, 267);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "groupBox1";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col1,
            this.ColKey,
            this.ColCtrl,
            this.ColAlt,
            this.ColShf,
            this.ColWin,
            this.Column,
            this.lastCol});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(9, 24);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(633, 234);
            this.dataGridView.TabIndex = 1;
            // 
            // Col1
            // 
            this.Col1.DataPropertyName = "Enabled";
            this.Col1.HeaderText = "Enabled";
            this.Col1.Name = "Col1";
            this.Col1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Col1.Width = 62;
            // 
            // ColKey
            // 
            this.ColKey.DataPropertyName = "Display";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColKey.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColKey.HeaderText = "HotKey";
            this.ColKey.Name = "ColKey";
            this.ColKey.ReadOnly = true;
            this.ColKey.Width = 74;
            // 
            // ColCtrl
            // 
            this.ColCtrl.DataPropertyName = "Control";
            this.ColCtrl.HeaderText = "CONTROL";
            this.ColCtrl.Name = "ColCtrl";
            this.ColCtrl.ReadOnly = true;
            this.ColCtrl.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColCtrl.Width = 62;
            // 
            // ColAlt
            // 
            this.ColAlt.DataPropertyName = "Alt";
            this.ColAlt.HeaderText = "ALT";
            this.ColAlt.Name = "ColAlt";
            this.ColAlt.ReadOnly = true;
            this.ColAlt.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColAlt.Width = 34;
            // 
            // ColShf
            // 
            this.ColShf.DataPropertyName = "Shift";
            this.ColShf.HeaderText = "SHIFT";
            this.ColShf.Name = "ColShf";
            this.ColShf.ReadOnly = true;
            this.ColShf.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColShf.Width = 48;
            // 
            // ColWin
            // 
            this.ColWin.DataPropertyName = "Win";
            this.ColWin.HeaderText = "WIN";
            this.ColWin.Name = "ColWin";
            this.ColWin.ReadOnly = true;
            this.ColWin.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColWin.Width = 34;
            // 
            // Column
            // 
            this.Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column.DataPropertyName = "Action";
            this.Column.HeaderText = "Action";
            this.Column.MinimumWidth = 280;
            this.Column.Name = "Column";
            // 
            // lastCol
            // 
            this.lastCol.HeaderText = "";
            this.lastCol.MinimumWidth = 24;
            this.lastCol.Name = "lastCol";
            this.lastCol.ReadOnly = true;
            this.lastCol.Width = 24;
            // 
            // HotKeyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "HotKeyControl";
            this.Size = new System.Drawing.Size(651, 267);
            this.groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Col1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColKey;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColCtrl;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColAlt;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColShf;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColWin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastCol;
    }
}
