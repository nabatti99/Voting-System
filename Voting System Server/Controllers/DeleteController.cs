using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System_Server.Models;
using System.Text.Json;

namespace Voting_System_Server.Controllers
{
    public class DeleteController
    {
        private Client client;

        public DeleteController(Client client)
        {
            this.client = client;
        }

        // Action Types
        public const string DELETE_CANDIDATE = "DELETE_CANDIDATE";

        public void Handle(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = null;

            try
            {
                switch (data["action"])
                {
                    case DELETE_CANDIDATE:
                        response = DeleteCandidate(data);
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

        private Dictionary<string, string> DeleteCandidate(Dictionary<string, string> data)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            LoginController.Authenticate(client);

            int id;
            if (!int.TryParse(data["candidateId"], out id))
                throw new Exception("Mã ứng viên không hợp lệ");

            var Db = AppDatabaseContext.GetInstance();

            var voteTurns = (
                    from voteTurn in Db.VoteTurns
                    where voteTurn.ID_CANDIDATE == id
                    select voteTurn
                );

            Db.VoteTurns.RemoveRange(voteTurns);

            Candidate candidateFounded = (
                    from candidate in Db.Candidates
                    where candidate.ID == id
                    select candidate
                ).FirstOrDefault();

            if (candidateFounded == null)
                throw new Exception("Không tìm thấy ứng viên này");

            Db.Candidates.Remove(candidateFounded);
            Db.SaveChanges();

            response["status"] = Client.SUCCESS;
            return response;
        }
    }
}
