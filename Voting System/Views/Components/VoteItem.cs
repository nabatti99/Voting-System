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
    public partial class VoteItem : UserControl
    {        
        private int voteId;
        private Vote vote;
        private VoteEvent voteEvent;
        private List<int> voteTurnIds;

        public VoteItem(int voteId)
        {
            InitializeComponent();

            this.voteId = voteId;
        }

        public async Task Render()
        {
            vote = await GetInfoController.GetVote(voteId);
            voteEvent = await GetInfoController.GetVoteEvent(vote.ID_VOTE_EVENT);
            voteTurnIds = await GetInfoController.GetVoteTurnIdsFromVote(vote.ID);

            lbTitle.Text = voteEvent.TITLE;
            lbDate.Text = voteEvent.END_DATE.ToShortDateString();

            if (voteTurnIds.Count == voteEvent.MAX_VOTE_TURN)
            {
                lbState.Text = "Đã vote";
                lbState.ForeColor = Color.Green;
            }
            else if (voteTurnIds.Count == 0)
            {
                lbState.Text = "Chưa vote";
                lbState.ForeColor = Color.Red;
            }
            else
            {
                lbState.Text = $"{voteTurnIds.Count}/{voteEvent.MAX_VOTE_TURN} phiếu";
                lbState.ForeColor = Color.Orange;
            }

            if (voteEvent.END_DATE < DateTime.Now)
            {
                lbState.Text = "Hết hạn";
                lbState.ForeColor = Color.DarkGray;
            }
        }

        private void VoteItem_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            this.Cursor = Cursors.Hand;
        }

        private void VoteItem_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#EEEEEE");
            this.Cursor = Cursors.Default;
        }

        private async void pnlVoteItem_Click(object sender, EventArgs e)
        {
            Voting voting = new Voting(vote, voteEvent, voteTurnIds);
            Program.Navigate(voting);
            await voting.Render();
        }
    }
}
