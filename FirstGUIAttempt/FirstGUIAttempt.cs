using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstGUIAttempt
{
    public partial class FirstGUIAttempt : Form
    {
        public FirstGUIAttempt()
        {
            InitializeComponent();
        }

        private void signIn_Click(object sender, EventArgs e)
        {
            var goToSignIn = new SignInPage();
            goToSignIn.Show();
        }

        private void signUp_Click(object sender, EventArgs e)
        {
            var goToSignUp = new SignUpForm();
            goToSignUp.Show();

        }

    }
}
