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

namespace Voting_System.Views
{
    public partial class CreateNewVote : Form
    {
        public CreateNewVote()
        {
            InitializeComponent();

            dtpBeginTime.Format = DateTimePickerFormat.Time;
            dtpBeginTime.ShowUpDown = true;

            dtpEndTime.Format = DateTimePickerFormat.Time;
            dtpEndTime.ShowUpDown = true;
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            VoteEvent newVoteEvent = GetInformation();

            if (newVoteEvent == null) return;

            try
            {
                int newVoteEventId = await AddNewController.AddNewVoteEvent(newVoteEvent);
                newVoteEvent = await GetInfoController.GetVoteEvent(newVoteEventId);

                ManageVote manageVote = new ManageVote(newVoteEvent);
                Program.Navigate(manageVote, isReplacing: true);
                await manageVote.Render();

                return;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Console.WriteLine(error.StackTrace);

                MessageBox.Show(error.Message, "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private VoteEvent GetInformation()
        {
            VoteEvent newVoteEvent = new VoteEvent();

            try
            {
                if (
                    tbTitle.Text.Length == 0 ||
                    tbNumVotePerPerson.Text.Length == 0
                ) throw new Exception("Chưa đủ thông tin");

                int maxVoteTurn;
                if (!int.TryParse(tbNumVotePerPerson.Text, out maxVoteTurn) || maxVoteTurn <= 0)
                {
                    tbNumVotePerPerson.Focus();
                    throw new Exception("Thông tin không hợp lệ");
                }

                DateTime beginDate = dtpBeginDate.Value.Date + dtpBeginTime.Value.TimeOfDay;
                DateTime endDate = dtpEndDate.Value.Date + dtpEndTime.Value.TimeOfDay;

                if (beginDate < DateTime.Now)
                {
                    dtpBeginDate.Focus();
                    throw new Exception("Thời gian bắt đầu bầu cử phải là tương lai");
                }

                if (endDate <= beginDate)
                {
                    dtpEndDate.Focus();
                    throw new Exception("Thời gian kết thúc bầu cử phải lớn hơn thời gian bắt đầu bầu cử");
                }

                newVoteEvent.TITLE = tbTitle.Text;
                newVoteEvent.MAX_VOTE_TURN = maxVoteTurn;
                newVoteEvent.BEGIN_DATE = beginDate;
                newVoteEvent.END_DATE = endDate;

                return newVoteEvent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                MessageBox.Show(e.Message, "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void CreateNewVote_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Back(sender);
        }
    }
}
