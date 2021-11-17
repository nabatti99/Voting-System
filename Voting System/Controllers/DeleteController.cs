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
    public class DeleteController
    {
        // Action Types
        public const string DELETE_CANDIDATE = "DELETE_CANDIDATE";

        public static Task<bool> DeleteCandidate(int candidateId)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = DELETE_CANDIDATE;

            request["candidateId"] = candidateId.ToString();

            return AppConnector.Request(request)
                .ContinueWith<bool>(response =>
                {
                    var data = response.Result;

                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                            throw new Exception(data["message"]);

                        case AppConnector.SUCCESS:
                            return true;

                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }
    }
}
