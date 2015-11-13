namespace DemoForAIA
{
    partial class frmVoidCheque
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.mtxtReAssignChequeNo = new System.Windows.Forms.MaskedTextBox();
            this.mtxtVoidChequeNo = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Re-Assign ChequeNo :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Void ChequeNo :";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(298, 114);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Batch No. :";
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(133, 8);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(262, 20);
            this.txtBatchNo.TabIndex = 9;
            // 
            // mtxtReAssignChequeNo
            // 
            this.mtxtReAssignChequeNo.Location = new System.Drawing.Point(133, 77);
            this.mtxtReAssignChequeNo.Mask = "0000000";
            this.mtxtReAssignChequeNo.Name = "mtxtReAssignChequeNo";
            this.mtxtReAssignChequeNo.Size = new System.Drawing.Size(100, 20);
            this.mtxtReAssignChequeNo.TabIndex = 10;
            // 
            // mtxtVoidChequeNo
            // 
            this.mtxtVoidChequeNo.Location = new System.Drawing.Point(133, 46);
            this.mtxtVoidChequeNo.Mask = "0000000";
            this.mtxtVoidChequeNo.Name = "mtxtVoidChequeNo";
            this.mtxtVoidChequeNo.Size = new System.Drawing.Size(100, 20);
            this.mtxtVoidChequeNo.TabIndex = 10;
            // 
            // frmVoidCheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 149);
            this.Controls.Add(this.mtxtVoidChequeNo);
            this.Controls.Add(this.mtxtReAssignChequeNo);
            this.Controls.Add(this.txtBatchNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "frmVoidCheque";
            this.Text = "Void and Re-Assign Cheque";
            this.Load += new System.EventHandler(this.frmVoidCheque_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.MaskedTextBox mtxtReAssignChequeNo;
        private System.Windows.Forms.MaskedTextBox mtxtVoidChequeNo;
    }
}