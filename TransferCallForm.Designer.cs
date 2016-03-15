namespace SIPSample
{
    partial class TransferCallForm
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
            this.TextBoxLineNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.TextBoxTranferNumber = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextBoxLineNum
            // 
            this.TextBoxLineNum.Location = new System.Drawing.Point(67, 139);
            this.TextBoxLineNum.Name = "TextBoxLineNum";
            this.TextBoxLineNum.Size = new System.Drawing.Size(76, 20);
            this.TextBoxLineNum.TabIndex = 55;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "Line No.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = "line would you want to replace";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "For attended transfer,please enter which";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(18, 51);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(176, 13);
            this.Label2.TabIndex = 51;
            this.Label2.Text = "(Likes: sip:number@sip.portsip.com)";
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(103, 73);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(75, 21);
            this.Button1.TabIndex = 50;
            this.Button1.Text = "Transfer";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TextBoxTranferNumber
            // 
            this.TextBoxTranferNumber.Location = new System.Drawing.Point(14, 25);
            this.TextBoxTranferNumber.Name = "TextBoxTranferNumber";
            this.TextBoxTranferNumber.Size = new System.Drawing.Size(249, 20);
            this.TextBoxTranferNumber.TabIndex = 49;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(114, 13);
            this.Label1.TabIndex = 48;
            this.Label1.Text = "Enter Transfer Number";
            // 
            // TransferCallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 176);
            this.Controls.Add(this.TextBoxLineNum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.TextBoxTranferNumber);
            this.Controls.Add(this.Label1);
            this.MaximizeBox = false;
            this.Name = "TransferCallForm";
            this.Text = "TransferCallForm";
            this.Load += new System.EventHandler(this.TransferCallForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxLineNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.TextBox TextBoxTranferNumber;
        internal System.Windows.Forms.Label Label1;
    }
}