namespace DemoForAIA
{
    partial class frmDoPrintCheque
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmbBatchNo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.colBatchNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTX_ref_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChequeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCCY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAssignBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAssginTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmbBatchNo);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.btnPrint);
            this.splitContainer1.Panel1.Controls.Add(this.btnRefresh);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvResult);
            this.splitContainer1.Size = new System.Drawing.Size(592, 266);
            this.splitContainer1.SplitterDistance = 33;
            this.splitContainer1.TabIndex = 0;
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
            this.colTX_ref_no,
            this.colChequeNo,
            this.colAmount,
            this.colCCY,
            this.colAssignBy,
            this.colAssginTime});
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
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(493, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Do Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // colBatchNO
            // 
            this.colBatchNO.DataPropertyName = "C_BatchNo";
            this.colBatchNO.HeaderText = "BatchNO";
            this.colBatchNO.Name = "colBatchNO";
            this.colBatchNO.ReadOnly = true;
            this.colBatchNO.Width = 76;
            // 
            // colTX_ref_no
            // 
            this.colTX_ref_no.DataPropertyName = "TX_ref_no";
            this.colTX_ref_no.HeaderText = "TX_ref_no";
            this.colTX_ref_no.Name = "colTX_ref_no";
            this.colTX_ref_no.ReadOnly = true;
            this.colTX_ref_no.Width = 82;
            // 
            // colChequeNo
            // 
            this.colChequeNo.DataPropertyName = "ChequeNo";
            this.colChequeNo.HeaderText = "ChequeNo";
            this.colChequeNo.Name = "colChequeNo";
            this.colChequeNo.ReadOnly = true;
            this.colChequeNo.Width = 83;
            // 
            // colAmount
            // 
            this.colAmount.DataPropertyName = "Amount";
            this.colAmount.HeaderText = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            this.colAmount.Width = 68;
            // 
            // colCCY
            // 
            this.colCCY.DataPropertyName = "CCY";
            this.colCCY.HeaderText = "CCY";
            this.colCCY.Name = "colCCY";
            this.colCCY.ReadOnly = true;
            this.colCCY.Width = 53;
            // 
            // colAssignBy
            // 
            this.colAssignBy.DataPropertyName = "AssignBy";
            this.colAssignBy.HeaderText = "AssignBy";
            this.colAssignBy.Name = "colAssignBy";
            this.colAssignBy.ReadOnly = true;
            this.colAssignBy.Width = 75;
            // 
            // colAssginTime
            // 
            this.colAssginTime.DataPropertyName = "AssginTime";
            this.colAssginTime.HeaderText = "AssginTime";
            this.colAssginTime.Name = "colAssginTime";
            this.colAssginTime.ReadOnly = true;
            this.colAssginTime.Width = 86;
            // 
            // frmDoPrintCheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 266);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmDoPrintCheque";
            this.Text = "Do Print Cheque";
            this.Load += new System.EventHandler(this.frmRptResult_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cmbBatchNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBatchNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTX_ref_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChequeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCCY;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAssignBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAssginTime;
    }
}