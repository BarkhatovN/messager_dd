using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Messager.WinForms.Controls
{
    public partial class StartControl : UserControl
    {
        public StartControl()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            var parentForm = (ParentForm as MainForm);
            parentForm.SetState(MainForm.States.Login);

        }

        private void StartControl_Load(object sender, EventArgs e)
        {

        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            var parentForm = (ParentForm as MainForm);
            parentForm.SetState(MainForm.States.Registration);
        }
    }
}
