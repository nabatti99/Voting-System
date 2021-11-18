using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voting_System.Models;
using Voting_System.Controllers;

namespace Voting_System.Views
{
    public partial class EnterCode : Form
    {
        private AppUser appUser;

        private static byte[] hashKey;
        private static ICryptoTransform decryptor;

        static EnterCode()
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            hashKey = Encoding.UTF8.GetBytes(ManageVote.KEY);
            hashKey = md5.ComputeHash(hashKey);

            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = hashKey;
            tripleDES.Mode = CipherMode.ECB;

            decryptor = tripleDES.CreateDecryptor();
        }

        public EnterCode(AppUser appUser)
        {
            InitializeComponent();

            this.appUser = appUser;
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            btnSubmit.Text = "Đang xử lí";
            btnSubmit.Enabled = false;

            byte[] hashedData = Convert.FromBase64String(tbCode.Text);
            byte[] data = decryptor.TransformFinalBlock(hashedData, 0, hashedData.Length);
            string textDecrypted = Encoding.UTF8.GetString(data);

            int voteEventId;
            if (!int.TryParse(textDecrypted, out voteEventId))
            {
                MessageBox.Show("Mã bầu cử không hợp lệ");
                return;
            }

            try
            {
                VoteEvent voteEvent = await GetInfoController.GetVoteEvent(voteEventId);
                int voteId = await AddNewController.AddNewVote(voteEventId);
                Vote vote = await GetInfoController.GetVote(voteId);
                List<int> voteTurnIds = await GetInfoController.GetVoteTurnIdsFromVote(vote.ID);

                Voting voting = new Voting(vote, voteEvent, voteTurnIds);
                Program.Navigate(voting, isReplacing: true);
                await voting.Render();
            } 
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Lỗi server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
        }

        private void EnterCode_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Back(sender);
        }
    }
}
