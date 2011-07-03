namespace NppHashMaker
{
    partial class HashResultDlg
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
            this.textBox_hashResult = new System.Windows.Forms.TextBox();
            this.button_close = new System.Windows.Forms.Button();
            this.label_hash = new System.Windows.Forms.Label();
            this.label_data = new System.Windows.Forms.Label();
            this.label_dataContent = new System.Windows.Forms.Label();
            this.checkBox_showBase64 = new System.Windows.Forms.CheckBox();
            this.textBox_base64 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox_hashResult
            // 
            this.textBox_hashResult.Location = new System.Drawing.Point(52, 62);
            this.textBox_hashResult.Name = "textBox_hashResult";
            this.textBox_hashResult.ReadOnly = true;
            this.textBox_hashResult.Size = new System.Drawing.Size(497, 20);
            this.textBox_hashResult.TabIndex = 0;
            // 
            // button_close
            // 
            this.button_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_close.Location = new System.Drawing.Point(240, 146);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 1;
            this.button_close.Text = "Close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // label_hash
            // 
            this.label_hash.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label_hash.AutoSize = true;
            this.label_hash.Location = new System.Drawing.Point(12, 65);
            this.label_hash.Name = "label_hash";
            this.label_hash.Size = new System.Drawing.Size(35, 13);
            this.label_hash.TabIndex = 3;
            this.label_hash.Text = "Hash:";
            // 
            // label_data
            // 
            this.label_data.AutoSize = true;
            this.label_data.Location = new System.Drawing.Point(12, 22);
            this.label_data.Name = "label_data";
            this.label_data.Size = new System.Drawing.Size(71, 13);
            this.label_data.TabIndex = 6;
            this.label_data.Text = "Data to hash:";
            // 
            // label_dataContent
            // 
            this.label_dataContent.AutoSize = true;
            this.label_dataContent.Location = new System.Drawing.Point(88, 22);
            this.label_dataContent.Name = "label_dataContent";
            this.label_dataContent.Size = new System.Drawing.Size(35, 13);
            this.label_dataContent.TabIndex = 7;
            this.label_dataContent.Text = "label5";
            // 
            // checkBox_showBase64
            // 
            this.checkBox_showBase64.AutoSize = true;
            this.checkBox_showBase64.Location = new System.Drawing.Point(15, 105);
            this.checkBox_showBase64.Name = "checkBox_showBase64";
            this.checkBox_showBase64.Size = new System.Drawing.Size(62, 17);
            this.checkBox_showBase64.TabIndex = 8;
            this.checkBox_showBase64.Text = "Base64";
            this.checkBox_showBase64.UseVisualStyleBackColor = true;
            this.checkBox_showBase64.CheckedChanged += new System.EventHandler(this.checkBox_showBase64_CheckedChanged);
            // 
            // textBox_base64
            // 
            this.textBox_base64.Location = new System.Drawing.Point(83, 103);
            this.textBox_base64.Name = "textBox_base64";
            this.textBox_base64.ReadOnly = true;
            this.textBox_base64.Size = new System.Drawing.Size(466, 20);
            this.textBox_base64.TabIndex = 0;
            this.textBox_base64.Hide();
            // 
            // HashResultDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_close;
            this.ClientSize = new System.Drawing.Size(561, 181);
            this.ControlBox = false;
            this.Controls.Add(this.checkBox_showBase64);
            this.Controls.Add(this.label_dataContent);
            this.Controls.Add(this.label_data);
            this.Controls.Add(this.label_hash);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.textBox_base64);
            this.Controls.Add(this.textBox_hashResult);
            this.Name = "HashResultDlg";
            this.Text = "Hash Result";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_hashResult;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Label label_hash;
        private System.Windows.Forms.Label label_data;
        private System.Windows.Forms.Label label_dataContent;
        private System.Windows.Forms.CheckBox checkBox_showBase64;
        private System.Windows.Forms.TextBox textBox_base64;
    }
}