using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voting_System;
using Voting_System.Models;
using Voting_System.Controllers;
using System.Text.RegularExpressions;

namespace Voting_System.Views
{
    public partial class Login : Form
    {
        private Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public Login()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Text = "Đang đăng nhập";
            btnLogin.Enabled = false;

            try
            {
                if (!emailRegex.IsMatch(tbEmail.Text))
                    throw new Exception("Hãy nhập đúng email");

                await LoginController.Login(tbEmail.Text, tbPassword.Text);
                Program.Navigate(new Home());
            } catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Console.WriteLine(error.StackTrace);

                MessageBox.Show(error.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnLogin.Text = "Đăng nhập";
            btnLogin.Enabled = true;
        }
    }
}
