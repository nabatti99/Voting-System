using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voting_System.Models;
using Voting_System.Controllers;

namespace Voting_System.Views
{
    public partial class EnterCode : Form
    {
        private AppUser appUser;

        public EnterCode(AppUser appUser)
        {
            InitializeComponent();

            this.appUser = appUser;
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            btnSubmit.Text = "Đang xử lí";
            btnSubmit.Enabled = false;

            int voteEventId;
            if (!int.TryParse(tbCode.Text, out voteEventId))
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
