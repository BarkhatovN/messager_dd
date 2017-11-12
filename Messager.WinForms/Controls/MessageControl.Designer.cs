namespace Messager.WinForms.Controls
{
    partial class MessageControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.authorTxt = new System.Windows.Forms.TextBox();
            this.msgTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // authorTxt
            // 
            this.authorTxt.BackColor = System.Drawing.Color.LightBlue;
            this.authorTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.authorTxt.Font = new System.Drawing.Font("Fira Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.authorTxt.ForeColor = System.Drawing.Color.DarkCyan;
            this.authorTxt.Location = new System.Drawing.Point(274, 3);
            this.authorTxt.MaxLength = 80;
            this.authorTxt.Multiline = true;
            this.authorTxt.Name = "authorTxt";
            this.authorTxt.ReadOnly = true;
            this.authorTxt.Size = new System.Drawing.Size(200, 21);
            this.authorTxt.TabIndex = 2;
            this.authorTxt.Text = "Никита Бархатов";
            // 
            // msgTxt
            // 
            this.msgTxt.BackColor = System.Drawing.Color.LemonChiffon;
            this.msgTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.msgTxt.Font = new System.Drawing.Font("Fira Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.msgTxt.ForeColor = System.Drawing.Color.DarkCyan;
            this.msgTxt.Location = new System.Drawing.Point(6, 30);
            this.msgTxt.MaxLength = 80;
            this.msgTxt.Multiline = true;
            this.msgTxt.Name = "msgTxt";
            this.msgTxt.ReadOnly = true;
            this.msgTxt.Size = new System.Drawing.Size(468, 42);
            this.msgTxt.TabIndex = 3;
            this.msgTxt.Text = "Привет, как дела?\r\nЧто нового?\r\n";
            this.msgTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MessageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.msgTxt);
            this.Controls.Add(this.authorTxt);
            this.Name = "MessageControl";
            this.Size = new System.Drawing.Size(480, 75);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox authorTxt;
        public System.Windows.Forms.TextBox msgTxt;
    }
}
