﻿namespace DemoForAIA
{
    partial class frmVoidReAssignCheque
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnVoidAll = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmbBatchNo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.colBatchNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTX_ref_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChequeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVoidChequeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCCY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVoid_By = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVoid_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Panel1.Controls.Add(this.btnVoidAll);
            this.splitContainer1.Panel1.Controls.Add(this.btnPrint);
            this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainer1.Panel1.Controls.Add(this.cmbBatchNo);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.btnRefresh);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvResult);
            this.splitContainer1.Size = new System.Drawing.Size(756, 266);
            this.splitContainer1.SplitterDistance = 33;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnVoidAll
            // 
            this.btnVoidAll.Location = new System.Drawing.Point(574, 4);
            this.btnVoidAll.Name = "btnVoidAll";
            this.btnVoidAll.Size = new System.Drawing.Size(75, 23);
            this.btnVoidAll.TabIndex = 10;
            this.btnVoidAll.Text = "Void All";
            this.btnVoidAll.UseVisualStyleBackColor = true;
            this.btnVoidAll.Click += new System.EventHandler(this.btnVoidAll_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(655, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 9;
            this.btnPrint.Text = "PrintReport";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(493, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.cmbBatchNo.SelectedIndexChanged += new System.EventHandler(this.cmbBatchNo_SelectedIndexChanged);
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
            this.colVoidChequeNo,
            this.colAmount,
            this.colCCY,
            this.colVoid_By,
            this.colVoid_Date});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(0, 0);
            this.dgvResult.MultiSelect = false;
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(756, 229);
            this.dgvResult.TabIndex = 0;
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
            this.colChequeNo.HeaderText = "ReAssignChequeNo";
            this.colChequeNo.Name = "colChequeNo";
            this.colChequeNo.ReadOnly = true;
            this.colChequeNo.Width = 128;
            // 
            // colVoidChequeNo
            // 
            this.colVoidChequeNo.DataPropertyName = "VoidChequeNo";
            this.colVoidChequeNo.HeaderText = "VoidChequeNo";
            this.colVoidChequeNo.Name = "colVoidChequeNo";
            this.colVoidChequeNo.ReadOnly = true;
            this.colVoidChequeNo.Width = 104;
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
            // colVoid_By
            // 
            this.colVoid_By.DataPropertyName = "Void_By";
            this.colVoid_By.HeaderText = "Void_By";
            this.colVoid_By.Name = "colVoid_By";
            this.colVoid_By.ReadOnly = true;
            this.colVoid_By.Width = 71;
            // 
            // colVoid_Date
            // 
            this.colVoid_Date.DataPropertyName = "Void_Date";
            this.colVoid_Date.HeaderText = "Void_Date";
            this.colVoid_Date.Name = "colVoid_Date";
            this.colVoid_Date.ReadOnly = true;
            this.colVoid_Date.Width = 82;
            // 
            // frmVoidReAssignCheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 266);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmVoidReAssignCheque";
            this.Text = "Void and Re-Assign Cheque";
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
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBatchNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTX_ref_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChequeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVoidChequeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCCY;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVoid_By;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVoid_Date;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnVoidAll;
    }
}