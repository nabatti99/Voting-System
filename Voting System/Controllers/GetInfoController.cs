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
    public static class GetInfoController
    {

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

        public static Task GetVoteIds()
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = GET_VOTE_IDS;
            request["userId"] = Program.State.User.ID.ToString();

            return AppConnector.Request(request)
                .ContinueWith(response =>
                {
                    var data = response.Result;

                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                            throw new Exception(data["message"]);
                        case AppConnector.SUCCESS:
                            List<int> voteIds;
                            voteIds = JsonSerializer.Deserialize<List<int>>(data["ids"]);
                            Program.State.VoteIds = voteIds;
                            return;
                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }

        public static Task GetVoteEventIds()
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = GET_VOTE_EVENT_IDS;
            request["userId"] = Program.State.User.ID.ToString();

            return AppConnector.Request(request)
                .ContinueWith(response =>
                {
                    var data = response.Result;

                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                            throw new Exception(data["message"]);
                        case AppConnector.SUCCESS:
                            List<int> voteEventIds;
                            voteEventIds = JsonSerializer.Deserialize<List<int>>(data["ids"]);
                            Program.State.voteEventIds = voteEventIds;
                            return;
                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }

        public static Task<List<int>> GetCandidateIds(int voteEventId)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = GET_CANDIDATE_IDS;
            request["voteEventId"] = voteEventId.ToString();

            return AppConnector.Request(request)
                .ContinueWith<List<int>>(response =>
                {
                    var data = response.Result;

                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                            throw new Exception(data["message"]);
                        case AppConnector.SUCCESS:
                            List<int> candidateIds;
                            candidateIds = JsonSerializer.Deserialize<List<int>>(data["ids"]);
                            return candidateIds;
                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }

        public static Task<List<int>> GetVoteTurnIdsFromVote(int voteId)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = GET_VOTE_TURN_IDS_FROM_VOTE;
            request["voteId"] = voteId.ToString();

            return AppConnector.Request(request)
                .ContinueWith<List<int>>(response =>
                {
                    var data = response.Result;

                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                            throw new Exception(data["message"]);
                        case AppConnector.SUCCESS:
                            List<int> voteTurnIds;
                            voteTurnIds = JsonSerializer.Deserialize<List<int>>(data["ids"]);
                            return voteTurnIds;
                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }

        public static Task<List<int>> GetVoteTurnIdsFromCandidate(int candidateId)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = GET_VOTE_TURN_IDS_FROM_VOTE;
            request["candidateId"] = candidateId.ToString();

            return AppConnector.Request(request)
                .ContinueWith<List<int>>(response =>
                {
                    var data = response.Result;

                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                            throw new Exception(data["message"]);
                        case AppConnector.SUCCESS:
                            List<int> voteTurnIds;
                            voteTurnIds = JsonSerializer.Deserialize<List<int>>(data["ids"]);
                            return voteTurnIds;
                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }

        public static Task<Vote> GetVote(int voteId)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = GET_VOTE;
            request["voteId"] = voteId.ToString();

            return AppConnector.Request(request)
                .ContinueWith<Vote>(response =>
                {
                    var data = response.Result;

                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                            throw new Exception(data["message"]);
                        case AppConnector.SUCCESS:
                            Vote vote = new Vote();
                            vote.ID = int.Parse(data["id"]);
                            vote.ID_USER = int.Parse(data["idUser"]);
                            vote.ID_VOTE_EVENT = int.Parse(data["idVoteEvent"]);
                            return vote;
                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }

        public static Task<VoteTurn> GetVoteTurn(int voteTurnId)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = GET_VOTE_TURN;
            request["voteTurnId"] = voteTurnId.ToString();

            return AppConnector.Request(request)
                .ContinueWith<VoteTurn>(response =>
                {
                    var data = response.Result;

                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                            throw new Exception(data["message"]);
                        case AppConnector.SUCCESS:
                            VoteTurn voteTurn = new VoteTurn();
                            voteTurn.ID = int.Parse(data["id"]);
                            voteTurn.ID_CANDIDATE = int.Parse(data["idCandidate"]);
                            voteTurn.ID_VOTE = int.Parse(data["idVote"]);
                            return voteTurn;
                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }

        public static Task<VoteEvent> GetVoteEvent(int voteEventId)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = GET_VOTE_EVENT;
            request["voteEventId"] = voteEventId.ToString();

            return AppConnector.Request(request)
                .ContinueWith<VoteEvent>(response =>
                {
                    var data = response.Result;

                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                            throw new Exception(data["message"]);
                        case AppConnector.SUCCESS:
                            VoteEvent voteEvent = new VoteEvent();
                            voteEvent.ID = int.Parse(data["id"]);
                            voteEvent.ID_HOST = int.Parse(data["idHost"]);
                            voteEvent.TITLE = data["title"];
                            voteEvent.BEGIN_DATE = DateTime.Parse(data["beginDate"]);
                            voteEvent.END_DATE = DateTime.Parse(data["endDate"]);
                            voteEvent.MAX_VOTE_TURN = int.Parse(data["maxVoteTurn"]);
                            return voteEvent;
                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }

        public static Task<Candidate> GetCandidate(int candidateId)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = GET_CANDIDATE;
            request["candidateId"] = candidateId.ToString();

            return AppConnector.Request(request)
                .ContinueWith<Candidate>(response =>
                {
                    var data = response.Result;

                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                            throw new Exception(data["message"]);
                        case AppConnector.SUCCESS:
                            Candidate candidate = new Candidate();
                            candidate.ID = int.Parse(data["id"]);
                            candidate.ID_VOTE_EVENT = int.Parse(data["idVoteEvent"]);
                            candidate.AGE = int.Parse(data["age"]);
                            candidate.EMAIL = data["email"];
                            candidate.NAME = data["name"];
                            candidate.NUM_VOTES = int.Parse(data["numVotes"]);
                            candidate.PHONE = data["phone"];
                            candidate.AVATAR = data["avatar"];
                            candidate.ROLE = data["role"];
                            candidate.REASON = data["reason"];

                            return candidate;
                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }
    }
}
