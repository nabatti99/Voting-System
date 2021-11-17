using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System_Server.Models;
using System.Text.Json;

namespace Voting_System_Server.Controllers
{
    public class GetInfoController
    {
        private Client client;

        public GetInfoController(Client client)
        {
            this.client = client;
        }

        // Action Types
        public const string GET_VOTE_IDS = "GET_VOTE_IDS";
        public const string GET_VOTE_EVENT_IDS = "GET_VOTE_EVENT_IDS";
        public const string GET_CANDIDATE_IDS = "GET_CANDIDATE_IDS";
        public const string GET_VOTE_TURN_IDS_FROM_VOTE = "GET_VOTE_TURN_IDS_FROM_VOTE";
        public const string GET_VOTE_TURN_IDS_FROM_CANDIDATE = "GET_VOTE_TURN_IDS_FROM_CANDIDATE";
        public const string GET_VOTE = "GET_VOTE";
        public const string GET_VOTE_TURN = "GET_VOTE_TURN";
        public const string GET_VOTE_EVENT = "GET_VOTE_EVENT";
        public const string GET_CANDIDATE = "GET_CANDIDATE";

        // Reducer
        public void Handle(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = null;

            try
            {
                switch (data["action"])
                {
                    case GET_VOTE_IDS:
                        response = GetVoteIds(data);
                        return;
                    case GET_VOTE_EVENT_IDS:
                        response = GetVoteEventIds(data);
                        return;
                    case GET_CANDIDATE_IDS:
                        response = GetCandidateIds(data);
                        return;
                    case GET_VOTE_TURN_IDS_FROM_VOTE:
                        response = GetVoteTurnIdsFromVote(data);
                        return;
                    case GET_VOTE_TURN_IDS_FROM_CANDIDATE:
                        response = GetVoteTurnIdsFromCandidate(data);
                        return;
                    case GET_VOTE:
                        response = GetVote(data);
                        return;
                    case GET_VOTE_TURN:
                        response = GetVoteTurn(data);
                        return;
                    case GET_VOTE_EVENT:
                        response = GetVoteEvent(data);
                        return;
                    case GET_CANDIDATE:
                        response = GetCandidate(data);
                        return;
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

        private Dictionary<string, string> GetVoteIds(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            LoginController.Authenticate(client);

            int id;
            if (!int.TryParse(data["userId"], out id))
                throw new Exception("Thông tin không hợp lệ");

            var Db = AppDatabaseContext.GetInstance();

            var query = from vote in Db.Votes
                        join user in Db.AppUsers on vote.AppUser equals user
                        where user.ID == id
                        select vote.ID;

            client.state.VoteIds = query.ToList();

            response["status"] = Client.SUCCESS;
            response["ids"] = JsonSerializer.Serialize(client.state.VoteIds);

            return response;
        }

        private Dictionary<string, string> GetVoteEventIds(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            LoginController.Authenticate(client);

            int id;
            if (!int.TryParse(data["userId"], out id))
                throw new Exception("Thông tin không hợp lệ");

            var Db = AppDatabaseContext.GetInstance();

            var query = from voteEvent in Db.VoteEvents
                        join user in Db.AppUsers on voteEvent.AppUser equals user
                        where user.ID == id
                        select voteEvent.ID;

            client.state.VoteIds = query.ToList();

            response["status"] = Client.SUCCESS;
            response["ids"] = JsonSerializer.Serialize(client.state.VoteIds);

            return response;
        }

        private Dictionary<string, string> GetCandidateIds(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            LoginController.Authenticate(client);

            int id;
            if (!int.TryParse(data["voteEventId"], out id))
                throw new Exception("Thông tin không hợp lệ");

            var Db = AppDatabaseContext.GetInstance();

                var query = from candidate in Db.Candidates
                        join voteEvent in Db.VoteEvents on candidate.VoteEvent equals voteEvent
                        where voteEvent.ID == id
                        select candidate.ID;

            List<int> candidateIds = query.ToList();

            response["status"] = Client.SUCCESS;
            response["ids"] = JsonSerializer.Serialize(candidateIds);

            return response;
        }

        private Dictionary<string, string> GetVoteTurnIdsFromVote(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            LoginController.Authenticate(client);

            int id;
            if (!int.TryParse(data["voteId"], out id))
                throw new Exception("Thông tin không hợp lệ");

            var Db = AppDatabaseContext.GetInstance();

            var query = from voteTurn in Db.VoteTurns
                        join vote in Db.Votes on voteTurn.Vote equals vote
                        where vote.ID == id
                        select voteTurn.ID;

            List<int> voteTurnIds = query.ToList();

            response["status"] = Client.SUCCESS;
            response["ids"] = JsonSerializer.Serialize(voteTurnIds);

            return response;
        }

        private Dictionary<string, string> GetVoteTurnIdsFromCandidate(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            LoginController.Authenticate(client);

            int id;
            if (!int.TryParse(data["candidateId"], out id))
                throw new Exception("Thông tin không hợp lệ");

            var Db = AppDatabaseContext.GetInstance();

            var query = from voteTurn in Db.VoteTurns
                        join candidate in Db.Candidates on voteTurn.Candidate equals candidate
                        where candidate.ID == id
                        select voteTurn.ID;

            List<int> voteTurnIds = query.ToList();

            response["status"] = Client.SUCCESS;
            response["ids"] = JsonSerializer.Serialize(voteTurnIds);

            return response;
        }

        private Dictionary<string, string> GetVote(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            LoginController.Authenticate(client);

            int id;
            if (!int.TryParse(data["voteId"], out id))
                throw new Exception("Thông tin không hợp lệ");

            var Db = AppDatabaseContext.GetInstance();

            var voteFounded = (
                    from vote in Db.Votes
                    where vote.ID == id
                    select vote
                ).FirstOrDefault();

            if (voteFounded == null)
                throw new Exception("Không tìm thấy sự bỏ phiếu này");

            response["status"] = Client.SUCCESS;
            response["id"] = voteFounded.ID.ToString();
            response["idUser"] = voteFounded.ID_USER.ToString();
            response["idVoteEvent"] = voteFounded.ID_VOTE_EVENT.ToString();

            return response;
        }

        private Dictionary<string, string> GetVoteTurn(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            LoginController.Authenticate(client);

            int id;
            if (!int.TryParse(data["voteTurnId"], out id))
                throw new Exception("Thông tin không hợp lệ");

            var Db = AppDatabaseContext.GetInstance();

            var voteTurnFounded = (
                    from voteTurn in Db.VoteTurns
                    where voteTurn.ID == id
                    select voteTurn
                ).FirstOrDefault();

            if (voteTurnFounded == null)
                throw new Exception("Không tìm thấy lần chọn ứng cử viên này");

            response["status"] = Client.SUCCESS;
            response["id"] = voteTurnFounded.ID.ToString();
            response["idCandidate"] = voteTurnFounded.ID_CANDIDATE.ToString();
            response["idVote"] = voteTurnFounded.ID_VOTE.ToString();

            return response;
        }

        private Dictionary<string, string> GetCandidate(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            LoginController.Authenticate(client);

            int id;
            if (!int.TryParse(data["candidateId"], out id))
                throw new Exception("Thông tin không hợp lệ");

            var Db = AppDatabaseContext.GetInstance();

            var candidateFounded = (
                    from candidate in Db.Candidates
                    where candidate.ID == id
                    select candidate
                ).FirstOrDefault();

            if (candidateFounded == null)
                throw new Exception("Không tìm thấy sự bỏ phiếu này");

            response["status"] = Client.SUCCESS;
            response["id"] = candidateFounded.ID.ToString();
            response["idVoteEvent"] = candidateFounded.ID_VOTE_EVENT.ToString();
            response["age"] = candidateFounded.AGE.ToString();
            response["email"] = candidateFounded.EMAIL;
            response["avatar"] = candidateFounded.AVATAR;
            response["name"] = candidateFounded.NAME;
            response["numVotes"] = candidateFounded.NUM_VOTES.ToString();
            response["phone"] = candidateFounded.PHONE;
            response["role"] = candidateFounded.ROLE;
            response["reason"] = candidateFounded.REASON;

            return response;
        }

        private Dictionary<string, string> GetVoteEvent(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            LoginController.Authenticate(client);

            int id;
            if (!int.TryParse(data["voteEventId"], out id))
                throw new Exception("Thông tin không hợp lệ");

            var Db = AppDatabaseContext.GetInstance();

            var voteEventFounded = (
                    from voteEvent in Db.VoteEvents
                    where voteEvent.ID == id
                    select voteEvent
                ).FirstOrDefault();

            if (voteEventFounded == null)
                throw new Exception("Không tìm thấy cuộc bầu cử này");

            response["status"] = Client.SUCCESS;
            response["id"] = voteEventFounded.ID.ToString();
            response["idHost"] = voteEventFounded.ID_HOST.ToString();
            response["title"] = voteEventFounded.TITLE;
            response["beginDate"] = voteEventFounded.BEGIN_DATE.ToString();
            response["endDate"] = voteEventFounded.END_DATE.ToString();
            response["maxVoteTurn"] = voteEventFounded.MAX_VOTE_TURN.ToString();

            return response;
        }
    }
}
