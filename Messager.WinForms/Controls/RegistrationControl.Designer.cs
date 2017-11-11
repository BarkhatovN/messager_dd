namespace Messager.WinForms.Controls
{
    partial class RegistrationControl
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
            this.loginTxt = new System.Windows.Forms.TextBox();
            this.passwordTxt = new System.Windows.Forms.TextBox();
            this.firstNameTxt = new System.Windows.Forms.TextBox();
            this.lastNameTxt = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.photoTxt = new System.Windows.Forms.TextBox();
            this.CancelLoginBtn = new System.Windows.Forms.Button();
            this.cancelRegistration = new System.Windows.Forms.Button();
            this.registrationOkBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loginTxt
            // 
            this.loginTxt.BackColor = System.Drawing.Color.LightBlue;
            this.loginTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.loginTxt.Font = new System.Drawing.Font("Fira Code", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loginTxt.ForeColor = System.Drawing.Color.DarkCyan;
            this.loginTxt.Location = new System.Drawing.Point(23, 37);
            this.loginTxt.MaxLength = 80;
            this.loginTxt.Multiline = true;
            this.loginTxt.Name = "loginTxt";
            this.loginTxt.Size = new System.Drawing.Size(269, 30);
            this.loginTxt.TabIndex = 1;
            this.loginTxt.Text = "Логин ";
            this.loginTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.loginTxt.Enter += new System.EventHandler(this.LoginTxt_Enter);
            this.loginTxt.Leave += new System.EventHandler(this.LoginTxt_Leave);
            // 
            // passwordTxt
            // 
            this.passwordTxt.BackColor = System.Drawing.Color.LightBlue;
            this.passwordTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.passwordTxt.Font = new System.Drawing.Font("Fira Code", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.passwordTxt.ForeColor = System.Drawing.Color.DarkCyan;
            this.passwordTxt.Location = new System.Drawing.Point(23, 82);
            this.passwordTxt.MaxLength = 80;
            this.passwordTxt.Multiline = true;
            this.passwordTxt.Name = "passwordTxt";
            this.passwordTxt.Size = new System.Drawing.Size(269, 30);
            this.passwordTxt.TabIndex = 1;
            this.passwordTxt.Text = "Пароль";
            this.passwordTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.passwordTxt.Enter += new System.EventHandler(this.PasswordTxt_Enter);
            this.passwordTxt.Leave += new System.EventHandler(this.PasswordTxt_Leave);
            // 
            // firstNameTxt
            // 
            this.firstNameTxt.BackColor = System.Drawing.Color.LightBlue;
            this.firstNameTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.firstNameTxt.Font = new System.Drawing.Font("Fira Code", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.firstNameTxt.ForeColor = System.Drawing.Color.DarkCyan;
            this.firstNameTxt.Location = new System.Drawing.Point(23, 127);
            this.firstNameTxt.MaxLength = 80;
            this.firstNameTxt.Multiline = true;
            this.firstNameTxt.Name = "firstNameTxt";
            this.firstNameTxt.Size = new System.Drawing.Size(269, 30);
            this.firstNameTxt.TabIndex = 1;
            this.firstNameTxt.Text = "Имя";
            this.firstNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.firstNameTxt.Enter += new System.EventHandler(this.FirstNameTxt_Enter);
            this.firstNameTxt.Leave += new System.EventHandler(this.FirstNameTxt_Leave);
            // 
            // lastNameTxt
            // 
            this.lastNameTxt.BackColor = System.Drawing.Color.LightBlue;
            this.lastNameTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lastNameTxt.Font = new System.Drawing.Font("Fira Code", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lastNameTxt.ForeColor = System.Drawing.Color.DarkCyan;
            this.lastNameTxt.Location = new System.Drawing.Point(23, 172);
            this.lastNameTxt.MaxLength = 80;
            this.lastNameTxt.Multiline = true;
            this.lastNameTxt.Name = "lastNameTxt";
            this.lastNameTxt.Size = new System.Drawing.Size(269, 30);
            this.lastNameTxt.TabIndex = 1;
            this.lastNameTxt.Text = "Фамилия";
            this.lastNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lastNameTxt.Enter += new System.EventHandler(this.LastNameTxt_Enter);
            this.lastNameTxt.Leave += new System.EventHandler(this.LastNameTxt_Leave);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // photoTxt
            // 
            this.photoTxt.BackColor = System.Drawing.Color.LightBlue;
            this.photoTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.photoTxt.Font = new System.Drawing.Font("Fira Code", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.photoTxt.ForeColor = System.Drawing.Color.DarkCyan;
            this.photoTxt.Location = new System.Drawing.Point(23, 217);
            this.photoTxt.MaxLength = 80;
            this.photoTxt.Multiline = true;
            this.photoTxt.Name = "photoTxt";
            this.photoTxt.Size = new System.Drawing.Size(269, 30);
            this.photoTxt.TabIndex = 1;
            this.photoTxt.Text = "Путь к фотографии";
            this.photoTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CancelLoginBtn
            // 
            this.CancelLoginBtn.BackColor = System.Drawing.Color.Snow;
            this.CancelLoginBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelLoginBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelLoginBtn.Font = new System.Drawing.Font("Fira Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelLoginBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.CancelLoginBtn.Location = new System.Drawing.Point(270, 217);
            this.CancelLoginBtn.Name = "CancelLoginBtn";
            this.CancelLoginBtn.Size = new System.Drawing.Size(22, 30);
            this.CancelLoginBtn.TabIndex = 2;
            this.CancelLoginBtn.UseVisualStyleBackColor = false;
            this.CancelLoginBtn.Click += new System.EventHandler(this.CancelLoginBtn_Click);
            // 
            // cancelRegistration
            // 
            this.cancelRegistration.BackColor = System.Drawing.Color.Snow;
            this.cancelRegistration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelRegistration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelRegistration.Font = new System.Drawing.Font("Fira Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancelRegistration.ForeColor = System.Drawing.Color.DarkCyan;
            this.cancelRegistration.Location = new System.Drawing.Point(181, 268);
            this.cancelRegistration.Name = "cancelRegistration";
            this.cancelRegistration.Size = new System.Drawing.Size(111, 34);
            this.cancelRegistration.TabIndex = 3;
            this.cancelRegistration.Text = "Отмена";
            this.cancelRegistration.UseVisualStyleBackColor = false;
            this.cancelRegistration.Click += new System.EventHandler(this.cancelRegistration_Click);
            // 
            // registrationOkBtn
            // 
            this.registrationOkBtn.BackColor = System.Drawing.Color.Snow;
            this.registrationOkBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.registrationOkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.registrationOkBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.registrationOkBtn.Font = new System.Drawing.Font("Fira Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.registrationOkBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.registrationOkBtn.Location = new System.Drawing.Point(23, 268);
            this.registrationOkBtn.Name = "registrationOkBtn";
            this.registrationOkBtn.Size = new System.Drawing.Size(111, 34);
            this.registrationOkBtn.TabIndex = 4;
            this.registrationOkBtn.Text = "Дальше";
            this.registrationOkBtn.UseVisualStyleBackColor = false;
            this.registrationOkBtn.Click += new System.EventHandler(this.LoginOkBtn_Click);
            // 
            // RegistrationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.cancelRegistration);
            this.Controls.Add(this.registrationOkBtn);
            this.Controls.Add(this.CancelLoginBtn);
            this.Controls.Add(this.photoTxt);
            this.Controls.Add(this.lastNameTxt);
            this.Controls.Add(this.firstNameTxt);
            this.Controls.Add(this.passwordTxt);
            this.Controls.Add(this.loginTxt);
            this.Name = "RegistrationControl";
            this.Size = new System.Drawing.Size(315, 329);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox loginTxt;
        private System.Windows.Forms.TextBox passwordTxt;
        private System.Windows.Forms.TextBox firstNameTxt;
        private System.Windows.Forms.TextBox lastNameTxt;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox photoTxt;
        private System.Windows.Forms.Button CancelLoginBtn;
        private System.Windows.Forms.Button cancelRegistration;
        private System.Windows.Forms.Button registrationOkBtn;
    }
}
