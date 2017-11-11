using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Messager.Model;
using Messager.WinForms.Controls;

namespace Messager.WinForms
{
    public partial class MainForm : Form
    {
        private readonly Size _startSize = new Size(350, 380);
        private readonly Size _workSize = new Size(725, 450);

        private User _user;
        private LoginControl _loginControl;
        private StartControl _startControl;
        private RegistrationControl _registrationControl;
        private ChatControl _chatControl;

        public enum States
        {
            Start,
            Login,
            Registration,
            Working
        }

        private States _currentState;
        private States _prevState;

        public void SetState(States state)
        {
            Size = _startSize;

            Controls.Clear();
            switch (state)
            {
                case States.Start:
                    SetStartState();
                    break;

                case States.Login:
                    SetLoginState();
                    break;

                case States.Registration:
                    SetRegistrationState();
                    break;

                case States.Working:
                    Size = _workSize;
                    SetWorkingState();
                    break;

                default:
                   throw new NotImplementedException();
            }
        }

        private void SetWorkingState()
        {
            _currentState = States.Working;

            _chatControl = new ChatControl(_user){Location = new Point(0,0)};
            Controls.Add(_chatControl);

            _prevState = States.Working;
        }

        private void SetStartState()
        {
            _currentState = States.Start;
            // loc: 10, 80
            _startControl = new StartControl {Location = new Point(10, 20)};

            Controls.Add(_startControl);
            _prevState = States.Start;
        }

        private void SetLoginState()
        {
            _currentState = States.Login;

            _loginControl = new LoginControl {Location = new Point(10, 20)};
            Controls.Add(_loginControl);

            _prevState = States.Login;
        }

        private void SetRegistrationState()
        {
            _currentState = States.Registration;

            _registrationControl = new RegistrationControl(_user) { Location = new Point(10, 0) };
            Controls.Add(_registrationControl);

            _prevState = States.Registration;
        }

        public MainForm()
        {
            InitializeComponent();
            SetState(States.Start);
        }
    }
}
