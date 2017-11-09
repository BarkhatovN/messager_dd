namespace Messager.WinForms.Controls
{
    partial class LoginControl
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
            this.LoginTxt = new System.Windows.Forms.TextBox();
            this.PasswordTxt = new System.Windows.Forms.TextBox();
            this.LoginOkBtn = new System.Windows.Forms.Button();
            this.CancelLoginBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LoginTxt
            // 
            this.LoginTxt.BackColor = System.Drawing.Color.LightCyan;
            this.LoginTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LoginTxt.Font = new System.Drawing.Font("Fira Code", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoginTxt.ForeColor = System.Drawing.Color.DarkCyan;
            this.LoginTxt.Location = new System.Drawing.Point(24, 34);
            this.LoginTxt.MaxLength = 80;
            this.LoginTxt.Multiline = true;
            this.LoginTxt.Name = "LoginTxt";
            this.LoginTxt.Size = new System.Drawing.Size(269, 30);
            this.LoginTxt.TabIndex = 0;
            this.LoginTxt.Text = "Введите Логин ";
            this.LoginTxt.Enter += new System.EventHandler(this.LoginTxt_Enter);
            this.LoginTxt.Leave += new System.EventHandler(this.LoginTxt_Leave);
            // 
            // PasswordTxt
            // 
            this.PasswordTxt.BackColor = System.Drawing.Color.LightCyan;
            this.PasswordTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PasswordTxt.Font = new System.Drawing.Font("Fira Code", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PasswordTxt.ForeColor = System.Drawing.Color.DarkCyan;
            this.PasswordTxt.Location = new System.Drawing.Point(24, 99);
            this.PasswordTxt.MaxLength = 80;
            this.PasswordTxt.Multiline = true;
            this.PasswordTxt.Name = "PasswordTxt";
            this.PasswordTxt.Size = new System.Drawing.Size(269, 30);
            this.PasswordTxt.TabIndex = 1;
            this.PasswordTxt.Text = "Введите пароль";
            this.PasswordTxt.Enter += new System.EventHandler(this.PasswordTxt_Enter);
            this.PasswordTxt.Leave += new System.EventHandler(this.PasswordTxt_Leave);
            // 
            // LoginOkBtn
            // 
            this.LoginOkBtn.BackColor = System.Drawing.Color.Snow;
            this.LoginOkBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.LoginOkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.LoginOkBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginOkBtn.Font = new System.Drawing.Font("Fira Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoginOkBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.LoginOkBtn.Location = new System.Drawing.Point(24, 174);
            this.LoginOkBtn.Name = "LoginOkBtn";
            this.LoginOkBtn.Size = new System.Drawing.Size(111, 34);
            this.LoginOkBtn.TabIndex = 1;
            this.LoginOkBtn.Text = "Дальше";
            this.LoginOkBtn.UseVisualStyleBackColor = false;
            // 
            // CancelLoginBtn
            // 
            this.CancelLoginBtn.BackColor = System.Drawing.Color.Snow;
            this.CancelLoginBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelLoginBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelLoginBtn.Font = new System.Drawing.Font("Fira Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelLoginBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.CancelLoginBtn.Location = new System.Drawing.Point(182, 174);
            this.CancelLoginBtn.Name = "CancelLoginBtn";
            this.CancelLoginBtn.Size = new System.Drawing.Size(111, 34);
            this.CancelLoginBtn.TabIndex = 1;
            this.CancelLoginBtn.Text = "Отмена";
            this.CancelLoginBtn.UseVisualStyleBackColor = false;
            // 
            // LoginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.CancelLoginBtn);
            this.Controls.Add(this.LoginOkBtn);
            this.Controls.Add(this.PasswordTxt);
            this.Controls.Add(this.LoginTxt);
            this.Name = "LoginControl";
            this.Size = new System.Drawing.Size(315, 220);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LoginTxt;
        private System.Windows.Forms.TextBox PasswordTxt;
        private System.Windows.Forms.Button LoginOkBtn;
        private System.Windows.Forms.Button CancelLoginBtn;
    }
}
