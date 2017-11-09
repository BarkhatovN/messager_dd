using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Messager.WinForms.Controls;

namespace Messager.WinForms
{
    public partial class MainForm : Form
    {
        private LoginControl loginControl;
        private StartControl startControl;

        public enum States
        {
            Start,
            Login,
            Registration,
            Working
        }

        private States currentState;
        private States prevState;

        public void SetState(States state)
        {
            switch (state)
            {
                case States.Start:
                    SetStartState();
                    break;

                case States.Login:
                    SetLoginState();
                    break;

                case States.Registration:
                    break;

                case States.Working:
                    break;

                default:
                   throw new NotImplementedException();
            }
        }

        private void SetStartState()
        {
            currentState = States.Start;
            // loc: 10, 80
            // h w: 315, 220
            startControl = new StartControl {Location = new Point(10, 20)};

            Controls.Add(startControl);
            prevState = States.Start;
        }

        private void SetLoginState()
        {
            if (prevState == States.Start)
            {
                Controls.Remove(startControl);
                startControl = null;
            }
            currentState = States.Login;

            loginControl = new LoginControl {Location = new Point(10, 20)};
            Controls.Add(loginControl);

            prevState = States.Login;
        }

        public MainForm()
        {
            InitializeComponent();
            SetState(States.Start);
        }


    }
}
