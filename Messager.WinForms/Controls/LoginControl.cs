using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Messager.Model;

namespace Messager.WinForms.Controls
{
    public partial class LoginControl : UserControl
    {
        private readonly string _placeLoginHolder = "Логин";
        private readonly string _placePasswordHolder = "Пароль";
        public User user;

        public LoginControl()
        {
            InitializeComponent();

        }

        private void LoginTxt_Enter(object sender, EventArgs e)
        {
            LoginTxt.Text = String.Empty;
        }

        private void LoginTxt_Leave(object sender, EventArgs e)
        {
            if (LoginTxt.Text == String.Empty)
                LoginTxt.Text = _placeLoginHolder;
        }

        private void PasswordTxt_Enter(object sender, EventArgs e)
        {
            PasswordTxt.Text = String.Empty;
            PasswordTxt.PasswordChar = '☺';
        }

        private void PasswordTxt_Leave(object sender, EventArgs e)
        {
            if (PasswordTxt.Text == String.Empty)
                PasswordTxt.Text = _placePasswordHolder;
        }

        private void CancelRegistratioBtn_Click(object sender, EventArgs e)
        {
            var parentForm = (ParentForm as MainForm);
            parentForm.SetState(MainForm.States.Start);
        }

        private  async void RegistrationOkBtn_Click(object sender, EventArgs e)
        {
            //user = await  ServiceClient.Authorize(LoginTxt.Text.Trim(), PasswordTxt.Text.Trim());
            var parentForm = (ParentForm as MainForm);
            parentForm.SetState(MainForm.States.Working);
        }
    }
}
