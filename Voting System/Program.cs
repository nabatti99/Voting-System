using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voting_System.Views;
using Voting_System.Utilities;
using Voting_System.Models;

namespace Voting_System
{
    static class Program
    {
        public static Stack<Form> StackNavigation;
        public static AppState State { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StartApp();
        }

        private static void StartApp()
        {
            try
            {
                State = new AppState();
                StackNavigation = new Stack<Form>();

                // Connect to Server
                AppConnector.GetInstance();

                Form initialScreen = new Login();
                StackNavigation.Push(initialScreen);

                Application.Run(StackNavigation.Peek());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                DialogResult userResponse = MessageBox.Show("Chạy lại app từ đầu", "Lỗi hệ thống", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                switch (userResponse)
                {
                    case DialogResult.OK:
                        StartApp();
                        break;
                    case DialogResult.Cancel:
                        Back(StackNavigation.Peek());
                        break;
                }
            }
        }

        public static void Navigate(Form form, bool isDialog = false, bool isHidePrevious = true, bool isReplacing = false)
        {
            if (isReplacing) 
                StackNavigation.Pop().Close();
            if (isHidePrevious && !isDialog)
                StackNavigation.Peek().Hide();

            StackNavigation.Push(form);

            if (isDialog)
                form.ShowDialog();
            else
                form.Show();
        }

        public static void Back(object sender)
        {
            if (StackNavigation.Peek() == sender)
            {
                StackNavigation.Pop().Close();
                StackNavigation.Peek().Show();
            }
        }
    }
}
