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
    public partial class RegistrationControl : UserControl
    {
        private readonly string _loginPlaceHolder = "Логин";
        private readonly string _passwordPlaceHolder = "Пароль";
        private readonly string _firstNamePlaceHolder = "Имя";
        private readonly string _lastNamePlaceHolder = "Фамилия";
        private readonly string _photoPlaceHolder = "Путь к фото";
        public User _user;
        private byte[] _photo;

        public RegistrationControl()
        {
            InitializeComponent();
        }

        private void LoginTxt_Enter(object sender, EventArgs e)
        {
            loginTxt.Text = String.Empty;
        }

        private void LoginTxt_Leave(object sender, EventArgs e)
        {
            if (loginTxt.Text == String.Empty)
                loginTxt.Text = _loginPlaceHolder;
        }

        private void PasswordTxt_Enter(object sender, EventArgs e)
        {
            passwordTxt.Text = String.Empty;
            passwordTxt.PasswordChar = '☺';
        }

        private void PasswordTxt_Leave(object sender, EventArgs e)
        {
            if (passwordTxt.Text == String.Empty)
                passwordTxt.Text = _passwordPlaceHolder;
        }

        private void FirstNameTxt_Enter(object sender, EventArgs e)
        {
            firstNameTxt.Text = String.Empty;
        }

        private void FirstNameTxt_Leave(object sender, EventArgs e)
        {
            if (firstNameTxt.Text == String.Empty)
                firstNameTxt.Text = _firstNamePlaceHolder;
        }

        private void LastNameTxt_Enter(object sender, EventArgs e)
        {
            lastNameTxt.Text = String.Empty;
        }

        private void LastNameTxt_Leave(object sender, EventArgs e)
        {
            if (lastNameTxt.Text == String.Empty)
                lastNameTxt.Text = _lastNamePlaceHolder;
        }

        private void CancelLoginBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Image";
            openFileDialog1.Filter = "jpg files (*.jpeg)|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            var bitmap = new Bitmap(openFileDialog1.FileName);
            ImageConverter converter = new ImageConverter();
            _photo = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
        }

        private void LoginOkBtn_Click(object sender, EventArgs e)
        {
            registrationOkBtn.Enabled = false;

            if (loginTxt.Text == _loginPlaceHolder)
            {
                MessageBox.Show("Ошибка, введите корректные данные");
                registrationOkBtn.Enabled = true;
                return;
            }
            _user = ServiceClient.RegisterUser(new User
            {
                Login = loginTxt.Text.Trim(),
                Password = passwordTxt.Text.Trim(),
                FirstName = firstNameTxt.Text.Trim(),
                LastName = lastNameTxt.Text.Trim(),
                ProfilePhoto = _photo
            });

            
            var parentForm = (ParentForm as MainForm);
            parentForm.user = _user;
            parentForm.SetState(MainForm.States.Working);
        }

        private void cancelRegistration_Click(object sender, EventArgs e)
        {
            var parentForm = (ParentForm as MainForm);
            parentForm.SetState(MainForm.States.Start);
        }
    }
}
