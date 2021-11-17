using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System_Server.Models;
using System.Text.Json;

namespace Voting_System_Server.Controllers
{
    public class UpdateInfoController
    {
        private Client client;

        public UpdateInfoController(Client client)
        {
            this.client = client;
        }

        // Action Types
        public const string UPDATE_VOTE_TURN = "UPDATE_VOTE_TURN";
        public const string UPDATE_CANDIDATE = "UPDATE_CANDIDATE";

        // Status
        public const string VOTE_TURN_CREATED = "VOTE_TURN_CREATED";
        public const string VOTE_TURN_DELETED = "VOTE_TURN_DELETED";

        // Reducer
        public void Handle(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = null;

            try
            {
                switch (data["action"])
                {
                    case UPDATE_VOTE_TURN:
                        response = UpdateVoteTurn(data);
                        return;
                    case UPDATE_CANDIDATE:
                        response = UpdateCandidate(data);
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

        private Dictionary<string, string> UpdateVoteTurn(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            LoginController.Authenticate(client);

            int voteId;
            if (!int.TryParse(data["voteId"], out voteId))
                throw new Exception("Thông tin không hợp lệ");

            int candidateId;
            if (!int.TryParse(data["candidateId"], out candidateId))
                throw new Exception("Thông tin không hợp lệ");

            var Db = AppDatabaseContext.GetInstance();

            var candidateFounded = (
                    from candidate in Db.Candidates
                    where candidate.ID == candidateId
                    select candidate
                ).FirstOrDefault();

            if (candidateFounded == null)
                throw new Exception("Không tìm thấy ứng viên này");

            var voteTurnsFounded = (
                    from voteTurn in Db.VoteTurns
                    where voteTurn.ID_CANDIDATE == candidateId
                    select voteTurn
                );

            int numVoteTurns = voteTurnsFounded.Count();

            var voteTurnFounded = (
                    from voteTurn in Db.VoteTurns
                    where voteTurn.ID_VOTE == voteId && voteTurn.ID_CANDIDATE == candidateId
                    select voteTurn
                ).FirstOrDefault();

            VoteTurn newVoteTurn = new VoteTurn();
            newVoteTurn.ID_VOTE = voteId;
            newVoteTurn.ID_CANDIDATE = candidateId;

            if (voteTurnFounded == null)
            {
                newVoteTurn = Db.VoteTurns.Add(newVoteTurn);
                response["status"] = VOTE_TURN_CREATED;
                response["id"] = newVoteTurn.ID.ToString();
                response["candidateId"] = newVoteTurn.ID_CANDIDATE.ToString();
                response["voteId"] = newVoteTurn.ID_VOTE.ToString();
                numVoteTurns += 1;
            }
            else
            {
                Db.VoteTurns.Remove(voteTurnFounded);
                response["status"] = VOTE_TURN_DELETED;
                numVoteTurns -= 1;
            }

            candidateFounded.NUM_VOTES = numVoteTurns;

            Db.SaveChanges();

            return response;
        }

        private Dictionary<string, string> UpdateCandidate(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            LoginController.Authenticate(client);

            int candidateId;
            if (!int.TryParse(data["candidateId"], out candidateId))
                throw new Exception("Thông tin không hợp lệ");

            var Db = AppDatabaseContext.GetInstance();

            var candidateFounded = (
                    from candidate in Db.Candidates
                    where candidate.ID == candidateId
                    select candidate
                ).FirstOrDefault();

            if (candidateFounded == null)
                throw new Exception("Không tồn tại ứng cử viên này");

            candidateFounded.NAME = data["name"];
            candidateFounded.AGE = int.Parse(data["age"]);
            candidateFounded.PHONE = data["phone"];
            candidateFounded.ROLE = data["role"];
            candidateFounded.REASON = data["reason"];

            Db.SaveChanges();

            response["status"] = Client.SUCCESS;
            return response;
        }
    }
}
