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

namespace Voting_System.Views.Components
{
    public partial class OrganizeVoteItem : UserControl
    {

        private int voteEventId;
        private VoteEvent voteEvent;
        private List<Candidate> candidates = new List<Candidate>();

        public OrganizeVoteItem(int voteEventId)
        {
            InitializeComponent();

            this.voteEventId = voteEventId;
        }

        public async Task Render()
        {
            voteEvent = await GetInfoController.GetVoteEvent(voteEventId);
            List<int> candidateIds = await GetInfoController.GetCandidateIds(voteEventId);

            foreach (int candidateId in candidateIds)
                candidates.Add(await GetInfoController.GetCandidate(candidateId));

            int totalNumVotes = 0;
            foreach (Candidate candidate in candidates)
                totalNumVotes += candidate.NUM_VOTES;

            lbTitle.Text = voteEvent.TITLE;
            lbNumVoteTurns.Text = $"{totalNumVotes} phiếu";
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            this.Cursor = Cursors.Hand;
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#EEEEEE");
            this.Cursor = Cursors.Default;
        }

        private async void pnlOrganizeVoteItem_Click(object sender, EventArgs e)
        {
            ManageVote manageVote = new ManageVote(voteEvent);
            Program.Navigate(manageVote);
            await manageVote.Render();
        }
    }
}
