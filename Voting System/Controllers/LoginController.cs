using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System.Models;
using Voting_System.Utilities;
using System.Text.Json;

namespace Voting_System.Controllers
{
    public static class LoginController
    {
        // Action Types
        public const string LOGIN = "LOGIN";

        // Status
        public const string ACCOUNT_CREATED = "ACCOUNT_CREATED";
        public const string ACCOUNT_FOUNDED = "ACCOUNT_FOUNDED";
        public const string ACCOUNT_WRONG = "ACCOUNT_WRONG";

        public static Task Login(string email, string password)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request["action"] = LOGIN;
            request["email"] = email;
            request["password"] = password;
            
            return AppConnector.Request(request)            // Send request
                .ContinueWith(response =>          // Receive response
                {
                    var data = response.Result;
                    
                    switch (data["status"])
                    {
                        case AppConnector.ERROR:
                        case ACCOUNT_WRONG:
                            throw new Exception(data["message"]);
                        case ACCOUNT_FOUNDED:
                        case ACCOUNT_CREATED:
                            AppUser user = new AppUser();
                            user.ID = int.Parse(data["id"]);
                            user.EMAIL = data["email"];
                            user.PASSWORD = data["password"];
                            Program.State.User = user;
                            return;
                        default:
                            Console.WriteLine(JsonSerializer.Serialize(data));
                            throw new Exception("Lỗi không xác định");
                    }
                });
        }
    }
}
