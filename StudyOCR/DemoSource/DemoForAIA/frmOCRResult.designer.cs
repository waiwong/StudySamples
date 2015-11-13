namespace DemoForAIA
{
    partial class frmOCRResult
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
            if (disposing && (components != null))
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
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitConMain = new System.Windows.Forms.SplitContainer();
            this.cmbBatchNo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.colBatchNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitConMain.Panel1.SuspendLayout();
            this.splitConMain.Panel2.SuspendLayout();
            this.splitConMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // splitConMain
            // 
            this.splitConMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitConMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitConMain.Location = new System.Drawing.Point(0, 0);
            this.splitConMain.Name = "splitConMain";
            this.splitConMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitConMain.Panel1
            // 
            this.splitConMain.Panel1.Controls.Add(this.cmbBatchNo);
            this.splitConMain.Panel1.Controls.Add(this.label5);
            this.splitConMain.Panel1.Controls.Add(this.btnRefresh);
            // 
            // splitConMain.Panel2
            // 
            this.splitConMain.Panel2.Controls.Add(this.dgvResult);
            this.splitConMain.Size = new System.Drawing.Size(592, 266);
            this.splitConMain.SplitterDistance = 33;
            this.splitConMain.TabIndex = 0;
            // 
            // cmbBatchNo
            // 
            this.cmbBatchNo.FormattingEnabled = true;
            this.cmbBatchNo.Items.AddRange(new object[] {
            "",
            "HKD",
            "MOP"});
            this.cmbBatchNo.Location = new System.Drawing.Point(70, 5);
            this.cmbBatchNo.Name = "cmbBatchNo";
            this.cmbBatchNo.Size = new System.Drawing.Size(336, 21);
            this.cmbBatchNo.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Batch No. :";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(412, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToOrderColumns = true;
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvResult.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBatchNO,
            this.colPID,
            this.colCID,
            this.colUserID});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(0, 0);
            this.dgvResult.MultiSelect = false;
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(592, 229);
            this.dgvResult.TabIndex = 0;
            // 
            // colBatchNO
            // 
            this.colBatchNO.DataPropertyName = "C_BatchNO";
            this.colBatchNO.HeaderText = "BatchNO";
            this.colBatchNO.Name = "colBatchNO";
            this.colBatchNO.ReadOnly = true;
            this.colBatchNO.Width = 76;
            // 
            // colPID
            // 
            this.colPID.DataPropertyName = "PID";
            this.colPID.HeaderText = "PID";
            this.colPID.Name = "colPID";
            this.colPID.ReadOnly = true;
            this.colPID.Width = 50;
            // 
            // colCID
            // 
            this.colCID.DataPropertyName = "CID";
            this.colCID.HeaderText = "CID";
            this.colCID.Name = "colCID";
            this.colCID.ReadOnly = true;
            this.colCID.Width = 50;
            // 
            // colUserID
            // 
            this.colUserID.DataPropertyName = "C_UserID";
            this.colUserID.HeaderText = "UserID";
            this.colUserID.Name = "colUserID";
            this.colUserID.ReadOnly = true;
            this.colUserID.Width = 65;
            // 
            // frmOCRResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 266);
            this.Controls.Add(this.splitConMain);
            this.Name = "frmOCRResult";
            this.Text = "OCR Result";
            this.Load += new System.EventHandler(this.frmRptResult_Load);
            this.splitConMain.Panel1.ResumeLayout(false);
            this.splitConMain.Panel1.PerformLayout();
            this.splitConMain.Panel2.ResumeLayout(false);
            this.splitConMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitConMain;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cmbBatchNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBatchNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserID;
    }
}