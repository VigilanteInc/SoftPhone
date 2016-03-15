using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIPSample
{
    public partial class TransferCallForm : Form
    {
        public TransferCallForm()
        {
            InitializeComponent();
        }

        private string TransferNumber = string.Empty;

        public string GetTransferNumber()
        {
            return TransferNumber;
        }

        public int GetReplaceLineNum()
        {
            if (TextBoxLineNum.Text.Length <= 0)
            {
                return 0;
            }

            return int.Parse(TextBoxLineNum.Text);
        }

        private void TransferCallForm_Load(object sender, EventArgs e)
        {
            TransferNumber = "";
            TextBoxTranferNumber.Text = "";

            TextBoxLineNum.Text = "";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (TextBoxTranferNumber.Text.Length > 0)
            {
                TransferNumber = TextBoxTranferNumber.Text;
                TextBoxTranferNumber.Text = "";
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please enter the transfer number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
