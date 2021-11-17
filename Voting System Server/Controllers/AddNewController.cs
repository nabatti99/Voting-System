using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System_Server.Models;
using System.Text.Json;

namespace Voting_System_Server.Controllers
{
    public class AddNewController
    {
        private Client client;

        public AddNewController(Client client)
        {
            this.client = client;
        }

        // Action Types
        public const string ADD_NEW_CANDIDATE = "ADD_NEW_CANDIDATE";
        public const string ADD_NEW_VOTE = "ADD_NEW_VOTE";
        public const string ADD_NEW_VOTE_EVENT = "ADD_NEW_VOTE_EVENT";

        // States
        public const string VOTE_EXISTED = "VOTE_EXISTED";

        public void Handle(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = null;

            try
            {
                switch (data["action"])
                {
                    case ADD_NEW_CANDIDATE:
                        response = AddNewCandidate(data);
                        break;
                    case ADD_NEW_VOTE:
                        response = AddNewVote(data);
                        break;
                    case ADD_NEW_VOTE_EVENT:
                        response = AddNewVoteEvent(data);
                        break;
                    default:
                        return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                response = new Dictionary<string, string>();
                response["status"] = Client.ERROR;
                response["message"] = e.Message;
            }
            finally
            {
                if (response != null)
                    client.Send(response);
            }
        }

        private Dictionary<string, string> AddNewCandidate(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            LoginController.Authenticate(client);

            var Db = AppDatabaseContext.GetInstance();

            Candidate newCandidate = new Candidate();

            newCandidate.NAME = data["name"];
            newCandidate.AGE = int.Parse(data["age"]);
            newCandidate.PHONE = data["phone"];
            newCandidate.EMAIL = data["email"];
            newCandidate.AVATAR = data["avatar"];
            newCandidate.ROLE = data["role"];
            newCandidate.REASON = data["reason"];
            newCandidate.ID_VOTE_EVENT = int.Parse(data["voteEventId"]);

            newCandidate = Db.Candidates.Add(newCandidate);
            Db.SaveChanges();

            response["status"] = Client.SUCCESS;
            response["id"] = newCandidate.ID.ToString();
            return response;
        }

        private Dictionary<string, string> AddNewVote(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            LoginController.Authenticate(client);

            var Db = AppDatabaseContext.GetInstance();

            int voteEventId;
            if (!int.TryParse(data["voteEventId"], out voteEventId))
                throw new Exception("Thông tin không hợp lệ");

            VoteEvent voteEventFounded = (
                    from voteEvent in Db.VoteEvents
                    where voteEvent.ID == voteEventId
                    select voteEvent
                ).FirstOrDefault();

            if (voteEventFounded == null)
            {
                response["status"] = Client.ERROR;
                response["message"] = "Không tìm thấy cuộc bầu cử này";
                return response;
            }

            Vote voteFounded = (
                    from vote in Db.Votes
                    where vote.ID_USER == client.state.User.ID && vote.ID_VOTE_EVENT == voteEventId
                    select vote
                ).FirstOrDefault();

            if (voteFounded != null)
            {
                response["status"] = VOTE_EXISTED;
                response["id"] = voteFounded.ID.ToString();
                return response;
            }

            Vote newVote = new Vote();
            newVote.ID_USER = client.state.User.ID;
            newVote.ID_VOTE_EVENT = voteEventFounded.ID;

            newVote = Db.Votes.Add(newVote);
            Db.SaveChanges();

            response["status"] = Client.SUCCESS;
            response["id"] = newVote.ID.ToString();
            return response;
        }

        private Dictionary<string, string> AddNewVoteEvent(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            LoginController.Authenticate(client);

            var Db = AppDatabaseContext.GetInstance();

            int maxVoteTurn;
            DateTime beginDate, endDate;
            if (
                !int.TryParse(data["maxVoteTurn"], out maxVoteTurn) ||
                !DateTime.TryParse(data["beginDate"], out beginDate) ||
                !DateTime.TryParse(data["endDate"], out endDate)
            ) throw new Exception("Thông tin không hợp lệ");

            VoteEvent newVoteEvent = new VoteEvent();
            newVoteEvent.TITLE = data["title"];
            newVoteEvent.MAX_VOTE_TURN = maxVoteTurn;
            newVoteEvent.BEGIN_DATE = beginDate;
            newVoteEvent.END_DATE = endDate;
            newVoteEvent.ID_HOST = client.state.User.ID;

            newVoteEvent = Db.VoteEvents.Add(newVoteEvent);
            Db.SaveChanges();

            response["status"] = Client.SUCCESS;
            response["id"] = newVoteEvent.ID.ToString();
            return response;
        }
    }
}