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
    public partial class Voting : Form
    {
        private Vote vote;
        private VoteEvent voteEvent;
        private List<int> voteTurnIds;

        private List<VoteTurn> voteTurns;
        private List<Candidate> candidates;
        private int currentCandidateId;

        private const string BTN_HAS_VOTED = "Bỏ bầu chọn";
        private const string BTN_HASNT_VOTED = "Bầu người này";

        public Voting(Vote vote, VoteEvent voteEvent, List<int> voteTurnIds)
        {
            InitializeComponent();

            this.vote = vote;
            this.voteEvent = voteEvent;
            this.voteTurnIds = voteTurnIds;
            this.currentCandidateId = -1;

            candidates = new List<Candidate>();
            voteTurns = new List<VoteTurn>();
        }

        public async Task Render()
        {
            dataGridView.Rows.Clear();
            ClearTableInformation();
            btnVote.Enabled = false;

            foreach (int voteTurnId in voteTurnIds)
            {
                VoteTurn voteTurn = await GetInfoController.GetVoteTurn(voteTurnId);
                voteTurns.Add(voteTurn);
            }

            List<int> candidateIds = await GetInfoController.GetCandidateIds(voteEvent.ID);
            foreach (int candidateId in candidateIds)
            {
                Candidate candidate = await GetInfoController.GetCandidate(candidateId);
                candidates.Add(candidate);
                int rowIndex = dataGridView.Rows.Add(candidate.ID, candidate.NAME, candidate.ROLE, candidate.NUM_VOTES);
               
                if (CandidateHasVoted(candidate))
                    dataGridView.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
            }

            currentCandidateId = candidates.Count > 0 ? 0 : -1;

            if (currentCandidateId > -1)
            {
                UpdateTableInformation(candidates[currentCandidateId]);
                UpdateRowInformation(candidates[currentCandidateId]);
            }
        }

        private bool CandidateHasVoted(Candidate candidate)
        {
            foreach (VoteTurn voteTurn in voteTurns)
                if (candidate.ID == voteTurn.ID_CANDIDATE)
                    return true;

            return false;
        }

        private int GetRowIndex(int candidateId)
        {
            for (int i = 0; i < dataGridView.Rows.Count; ++i)
                if ((int) dataGridView.Rows[i].Cells[0].Value == candidateId)
                    return i;

            return -1;
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Candidate lastCandidate = candidates[currentCandidateId];

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            if (row == null) return;

            int id = (int) row.Cells[0].Value;
            currentCandidateId = candidates.FindIndex(item => item.ID == id);

            UpdateRowInformation(lastCandidate);
            UpdateTableInformation(candidates[currentCandidateId]);
            UpdateRowInformation(candidates[currentCandidateId]);
        }

        private void UpdateTableInformation(Candidate candidate)
        {
            lbName.Text = candidate.NAME;
            lbAge.Text = candidate.AGE.ToString();
            lbEmail.Text = candidate.EMAIL;
            lbPhone.Text = candidate.PHONE;
            pbAvatar.LoadAsync(candidate.AVATAR == null ? $"https://ui-avatars.com/api/?size=320&name=${candidate.NAME}" : candidate.AVATAR);
            lbRole.Text = candidate.ROLE;
            lbNumVote.Text = candidate.NUM_VOTES.ToString();
            lbReason.Text = candidate.REASON;

            btnVote.Enabled = true;
            if (CandidateHasVoted(candidate))
            {
                btnVote.Text = BTN_HAS_VOTED;
            } else if (voteTurns.Count == voteEvent.MAX_VOTE_TURN)
            {
                btnVote.Text = $"Tối đa {voteEvent.MAX_VOTE_TURN} lượt bầu";
                btnVote.Enabled = false;
            } else
            {
                btnVote.Text = BTN_HASNT_VOTED;
            }
        }

        private void ClearTableInformation()
        {
            string content = "(Chưa chọn)";

            lbName.Text = content;
            lbAge.Text = content;
            lbEmail.Text = content;
            lbPhone.Text = content;
            pbAvatar.LoadAsync($"https://ui-avatars.com/api/?size=320&name=Avatar");
            lbRole.Text = content;
            lbNumVote.Text = content;
            lbReason.Text = content;
        }

        private void UpdateRowInformation(Candidate candidate)
        {
            int rowIndex = GetRowIndex(candidate.ID);
            DataGridViewRow row = dataGridView.Rows[rowIndex];

            row.Cells["id"].Value = candidate.ID;
            row.Cells["name"].Value = candidate.NAME;
            row.Cells["role"].Value = candidate.ROLE;
            row.Cells["numVotes"].Value = candidate.NUM_VOTES;

            if (CandidateHasVoted(candidate))
            {
                row.DefaultCellStyle.BackColor = Color.LightYellow;
                if (candidate == candidates[currentCandidateId])
                    row.DefaultCellStyle.BackColor = Color.Yellow;
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.White;
                if (candidate == candidates[currentCandidateId])
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
            }
        }

        private async void btnVote_Click(object sender, EventArgs e)
        {
            VoteTurn newVoteTurn = await UpdateInfoController.UpdateVoteTurn(vote.ID, candidates[currentCandidateId].ID);
            
            if (newVoteTurn == null)
            {
                int voteTurnId = voteTurns.FindIndex(item => item.ID_CANDIDATE == candidates[currentCandidateId].ID);
                voteTurns.RemoveAt(voteTurnId);
            } else
            {
                voteTurns.Add(newVoteTurn);
            }
            
            candidates[currentCandidateId] = await GetInfoController.GetCandidate(candidates[currentCandidateId].ID);

            UpdateTableInformation(candidates[currentCandidateId]);
            UpdateRowInformation(candidates[currentCandidateId]);
        }

        private void Voting_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Back(sender);
        }

        private void Voting_VisibleChanged(object sender, EventArgs e)
        {
            if (currentCandidateId > -1)
            {
                UpdateRowInformation(candidates[currentCandidateId]);
                UpdateTableInformation(candidates[currentCandidateId]);
            }
        }
    }
}
