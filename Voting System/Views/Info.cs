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
    public partial class Info : Form
    {
        private Candidate candidate;
        private ManageVote manageVote;
        private string type;

        public const string ADD_NEW_CANDIDATE = "ADD_NEW_CANDIDATE";
        public const string UPDATE_CANDIDATE = "UPDATE_CANDIDATE";

        public Info(Candidate candidate, string type = UPDATE_CANDIDATE, ManageVote manageVote = null)
        {
            InitializeComponent();

            this.candidate = candidate;
            this.manageVote = manageVote;
            this.type = type;
        }

        public void Render()
        {
            switch (type) {
                case ADD_NEW_CANDIDATE:
                    lbTitle.Text = "Thêm ứng cử viên mới";
                    break;
                case UPDATE_CANDIDATE:
                    lbTitle.Text = "Cập nhật thông tin ứng cử viên";
                    break;
            }

            tbName.Text = candidate.NAME;
            tbAge.Text = candidate.AGE.ToString();
            tbEmail.Text = candidate.EMAIL;
            tbRole.Text = candidate.ROLE;
            tbAvatar.Text = candidate.AVATAR;
            tbPhone.Text = candidate.PHONE;
            tbReason.Text = candidate.REASON;
        }

        private async void btnFinish_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnFinish.Text = "Đang xử lí";
            btnFinish.Enabled = false;

            candidate.NAME = tbName.Text;
            candidate.AGE = int.Parse(tbAge.Text);
            candidate.EMAIL = tbEmail.Text;
            candidate.AVATAR = tbAvatar.Text == "" ? null : tbAvatar.Text;
            candidate.ROLE = tbRole.Text;
            candidate.PHONE = tbPhone.Text;
            candidate.REASON = tbReason.Text;

            if (type == ADD_NEW_CANDIDATE)
                candidate.ID_VOTE_EVENT = manageVote.voteEvent.ID;

            switch (type)
            {
                case UPDATE_CANDIDATE:
                    await UpdateInfoController.UpdateCandidate(candidate);
                    break;
                case ADD_NEW_CANDIDATE:
                    int newCandidateId = await AddNewController.AddNewCandidate(candidate);
                    manageVote.AddNewCandidate(newCandidateId);
                    break;
            }

            Close();
        }

        private bool CheckInput()
        {
            bool result = true;

            if (tbName.Text.Length == 0) result = result && false;
            if (tbAge.Text.Length == 0) result = result && false;
            if (tbEmail.Text.Length == 0) result = result && false;
            if (tbRole.Text.Length == 0) result = result && false;
            if (tbPhone.Text.Length == 0) result = result && false;
            if (tbReason.Text.Length == 0) result = result && false;

            return result;
        }

        private void Info_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Back(sender);
        }
    }
}
