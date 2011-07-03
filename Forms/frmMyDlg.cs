using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NppHashMaker
{
    public partial class HashResultDlg : Form
    {
        public HashResultDlg()
        {
            InitializeComponent();
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        
        public void SetDlgTitle(string title)
        {
            this.Text = title;
        }
        
        public void SetHashResult(string hashText)
        {
            this.textBox_hashResult.Text = hashText;
        }

        public void SetDataTipText(string dataText)
        {
            this.label_dataContent.Text = dataText;
        }

        public void SetHashB64Result(string hashB64Text)
        {
            this.textBox_base64.Text = hashB64Text;
        }

        private void checkBox_showBase64_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_showBase64.Checked)
                this.textBox_base64.Show();
            else
                this.textBox_base64.Hide();
        }

    }
}
