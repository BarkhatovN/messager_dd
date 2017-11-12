namespace Messager.WinForms.Forms
{
    partial class NewChatDialog
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.chatNameTxt = new System.Windows.Forms.TextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.Color.LightCyan;
            this.checkedListBox1.Font = new System.Drawing.Font("Fira Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBox1.ForeColor = System.Drawing.Color.DarkCyan;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(25, 94);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(237, 224);
            this.checkedListBox1.TabIndex = 0;
            // 
            // chatNameTxt
            // 
            this.chatNameTxt.CausesValidation = false;
            this.chatNameTxt.Font = new System.Drawing.Font("Fira Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chatNameTxt.ForeColor = System.Drawing.Color.DarkCyan;
            this.chatNameTxt.Location = new System.Drawing.Point(12, 12);
            this.chatNameTxt.Name = "chatNameTxt";
            this.chatNameTxt.Size = new System.Drawing.Size(260, 30);
            this.chatNameTxt.TabIndex = 1;
            this.chatNameTxt.Text = "Название чата";
            this.chatNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sendBtn
            // 
            this.sendBtn.BackColor = System.Drawing.Color.Snow;
            this.sendBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.sendBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sendBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sendBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.sendBtn.Location = new System.Drawing.Point(25, 324);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(237, 32);
            this.sendBtn.TabIndex = 2;
            this.sendBtn.Text = "Добавить";
            this.sendBtn.UseVisualStyleBackColor = false;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // NewChatDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(284, 380);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.chatNameTxt);
            this.Controls.Add(this.checkedListBox1);
            this.Name = "NewChatDialog";
            this.Text = "NewChatDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TextBox chatNameTxt;
        private System.Windows.Forms.Button sendBtn;
    }
}