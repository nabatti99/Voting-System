using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Voting_System.Models;
using Voting_System.Utilities;

namespace Voting_System.Controllers
{
    public class UpdateInfoController
    {
        // Action Types
        public const string UPDATE_VOTE_TURN = "UPDATE_VOTE_TURN";
        public const string UPDATE_CANDIDATE = "UPDATE_CANDIDATE";

        // Status
        public const string VOTE_TURN_CREATED = "VOTE_TURN_CREATED";
        public const string VOTE_TURN_DELETED = "VOTE_TURN_DELETED";

        public static Task<VoteTurn> UpdateVoteTurn(int voteId, int candidateId)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = UPDATE_VOTE_TURN;
            request["voteId"] = voteId.ToString();
            request["candidateId"] = candidateId.ToString();

            return AppConnector.Request(request)
                .ContinueWith<VoteTurn>(response =>
                {
                    var data = response.Result;

                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                            throw new Exception(data["message"]);
                        case VOTE_TURN_CREATED:
                            VoteTurn voteTurn = new VoteTurn();
                            voteTurn.ID = int.Parse(data["id"]);
                            voteTurn.ID_CANDIDATE = int.Parse(data["candidateId"]);
                            voteTurn.ID_VOTE = int.Parse(data["voteId"]);
                            return voteTurn;
                        case VOTE_TURN_DELETED:
                            return null;
                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }

        public static Task UpdateCandidate(Candidate candidate)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = UPDATE_CANDIDATE;
            request["candidateId"] = candidate.ID.ToString();
            request["name"] = candidate.NAME;
            request["age"] = candidate.AGE.ToString();
            request["phone"] = candidate.PHONE;
            request["role"] = candidate.ROLE;
            request["reason"] = candidate.REASON;

            return AppConnector.Request(request)
                .ContinueWith(response =>
                {
                    var data = response.Result;

                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                            throw new Exception(data["message"]);
                        case AppConnector.SUCCESS:
                            return;
                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }
    }
}
