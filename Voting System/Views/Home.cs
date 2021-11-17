using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voting_System.Controllers;
using Voting_System.Models;
using Voting_System.Views.Components;

namespace Voting_System.Views
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();

            lbEmail.Text = Program.State.User.EMAIL;
        }

        private async void Render()
        {
            flAttendingVotes.Controls.Clear();
            flHostingVotes.Controls.Clear();

            await GetInfoController.GetVoteIds();
            foreach (int voteId in Program.State.VoteIds) {
                VoteItem voteItem = new VoteItem(voteId);
                flAttendingVotes.Controls.Add(voteItem);
                await voteItem.Render();
            }

            await GetInfoController.GetVoteEventIds();
            foreach(int voteEventId in Program.State.voteEventIds) { 
                OrganizeVoteItem organizeVoteItem = new OrganizeVoteItem(voteEventId);
                flHostingVotes.Controls.Add(organizeVoteItem);
                await organizeVoteItem.Render();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVoting_Click(object sender, EventArgs e)
        {
            Program.Navigate(new EnterCode(Program.State.User), isDialog: true);
        }

        private void btnNewVote_Click(object sender, EventArgs e)
        {
            Program.Navigate(new CreateNewVote(), isDialog: true);
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Back(sender);
        }

        private void Home_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
                Render();
        }
    }
}
