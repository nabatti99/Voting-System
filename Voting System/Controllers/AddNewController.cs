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
    public class AddNewController
    {
        // Action Types
        public const string ADD_NEW_CANDIDATE = "ADD_NEW_CANDIDATE";
        public const string ADD_NEW_VOTE = "ADD_NEW_VOTE";
        public const string ADD_NEW_VOTE_EVENT = "ADD_NEW_VOTE_EVENT";

        // States
        public const string VOTE_EXISTED = "VOTE_EXISTED";

        public static Task<int> AddNewCandidate(Candidate candidate)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = ADD_NEW_CANDIDATE;

            request["name"] = candidate.NAME;
            request["age"] = candidate.AGE.ToString();
            request["phone"] = candidate.PHONE;
            request["email"] = candidate.EMAIL;
            request["avatar"] = candidate.AVATAR;
            request["role"] = candidate.ROLE;
            request["reason"] = candidate.REASON;
            request["voteEventId"] = candidate.ID_VOTE_EVENT.ToString();

            return AppConnector.Request(request)
                .ContinueWith<int>(response =>
                {
                    var data = response.Result;

                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                            throw new Exception(data["message"]);

                        case AppConnector.SUCCESS:
                        case VOTE_EXISTED:
                            return int.Parse(data["id"]);

                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }

        public static Task<int> AddNewVote(int voteEventId)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = ADD_NEW_VOTE;

            request["voteEventId"] = voteEventId.ToString();

            return AppConnector.Request(request)
                .ContinueWith<int>(response =>
                {
                    var data = response.Result;

                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                            throw new Exception(data["message"]);

                        case AppConnector.SUCCESS:
                        case VOTE_EXISTED:
                            return int.Parse(data["id"]);

                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }

        public static Task<int> AddNewVoteEvent(VoteEvent newVoteEvent)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = ADD_NEW_VOTE_EVENT;

            request["title"] = newVoteEvent.TITLE;
            request["maxVoteTurn"] = newVoteEvent.MAX_VOTE_TURN.ToString();
            request["beginDate"] = newVoteEvent.BEGIN_DATE.ToString();
            request["endDate"] = newVoteEvent.END_DATE.ToString();

            return AppConnector.Request(request)
                .ContinueWith<int>(response =>
                {
                    var data = response.Result;

                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                            throw new Exception(data["message"]);

                        case AppConnector.SUCCESS:
                            return int.Parse(data["id"]);

                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }
    }
}
