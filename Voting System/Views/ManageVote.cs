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
    public partial class ManageVote : Form
    {
        public VoteEvent voteEvent;

        private List<VoteTurn> voteTurns;
        private List<Candidate> candidates;
        private int currentCandidateId;

        public ManageVote(VoteEvent voteEvent)
        {
            InitializeComponent();

            this.voteEvent = voteEvent;
            candidates = new List<Candidate>();
            currentCandidateId = -1;
        }

        public async Task Render()
        {
            lbTitle.Text = voteEvent.TITLE;
            lbDate.Text = $"{voteEvent.BEGIN_DATE} - {voteEvent.END_DATE}";
            ClearTableInformation();

            List<int> candidateIds = await GetInfoController.GetCandidateIds(voteEvent.ID);
            foreach (int candidateId in candidateIds)
            {
                Candidate candidate = await GetInfoController.GetCandidate(candidateId);
                candidates.Add(candidate);
                int rowIndex = dataGridView.Rows.Add(candidate.ID, candidate.NAME, candidate.ROLE, candidate.NUM_VOTES);

                if (candidate.NUM_VOTES > 0)
                    dataGridView.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
            }

            currentCandidateId = candidates.Count > 0 ? 0 : -1;

            if (currentCandidateId > -1)
            {
                UpdateTableInformation(candidates[currentCandidateId]);
                UpdateRowInformation(candidates[currentCandidateId]);
            }
        }

        public async void AddNewCandidate(int newCandidateId)
        {
            Candidate lastCandidate;
            if (currentCandidateId > -1)
                lastCandidate = candidates[currentCandidateId];
            else
                lastCandidate = null;

            Candidate newCandidate = await GetInfoController.GetCandidate(newCandidateId);
            candidates.Add(newCandidate);
            dataGridView.Rows.Add(newCandidate.ID, newCandidate.NAME, newCandidate.ROLE, newCandidate.NUM_VOTES);

            currentCandidateId = candidates.Count - 1;

            UpdateRowInformation(lastCandidate);
            UpdateTableInformation(candidates[currentCandidateId]);
            UpdateRowInformation(candidates[currentCandidateId]);
        }

        private int GetRowIndex(int candidateId)
        {
            for (int i = 0; i < dataGridView.Rows.Count; ++i)
                if ((int)dataGridView.Rows[i].Cells[0].Value == candidateId)
                    return i;

            return -1;
        }

        private void UpdateTableInformation(Candidate candidate)
        {
            if (candidate == null)
                ClearTableInformation();

            lbName.Text = candidate.NAME;
            lbAge.Text = candidate.AGE.ToString();
            lbEmail.Text = candidate.EMAIL;
            lbPhone.Text = candidate.PHONE;
            pbAvatar.LoadAsync(candidate.AVATAR == null ? $"https://ui-avatars.com/api/?size=320&name=${candidate.NAME}" : candidate.AVATAR);
            lbRole.Text = candidate.ROLE;
            lbNumVote.Text = candidate.NUM_VOTES.ToString();
            lbReason.Text = candidate.REASON;
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
            if (candidate == null)
                return;

            int rowIndex = GetRowIndex(candidate.ID);
            DataGridViewRow row = dataGridView.Rows[rowIndex];

            row.Cells["id"].Value = candidate.ID;
            row.Cells["name"].Value = candidate.NAME;
            row.Cells["role"].Value = candidate.ROLE;
            row.Cells["numVotes"].Value = candidate.NUM_VOTES;

            if (candidate.NUM_VOTES > 0)
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

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (currentCandidateId == -1)
                currentCandidateId = 0;

            Candidate lastCandidate = candidates[currentCandidateId];

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            if (row == null) return;

            int id = (int)row.Cells[0].Value;
            currentCandidateId = candidates.FindIndex(item => item.ID == id);

            UpdateRowInformation(lastCandidate);
            UpdateTableInformation(candidates[currentCandidateId]);
            UpdateRowInformation(candidates[currentCandidateId]);
        }

        private void ManageVote_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Back(sender);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Info info = new Info(candidates[currentCandidateId]);
            Program.Navigate(info);
            info.Render();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Candidate newCandidate = new Candidate();
            Info info = new Info(newCandidate, Info.ADD_NEW_CANDIDATE, this);
            Program.Navigate(info);
            info.Render();
        }

        private async void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int candidateId = (int) e.Row.Cells["id"].Value;
            bool isSuccess = await DeleteController.DeleteCandidate(candidateId);

            if (!isSuccess)
            {
                MessageBox.Show("Không thể xoá ứng viên lúc này. Hãy thử lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            int candidateIndex = candidates.FindIndex(item => item.ID == candidateId);
            candidates.RemoveAt(candidateIndex);
            currentCandidateId = -1;

            ClearTableInformation();
        }

        private void ManageVote_VisibleChanged(object sender, EventArgs e)
        {
            if (currentCandidateId > -1)
            {
                UpdateRowInformation(candidates[currentCandidateId]);
                UpdateTableInformation(candidates[currentCandidateId]);
            }
        }

        private void btnGetCode_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(voteEvent.ID.ToString());
        }
    }
}
