using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System_Server.Models;

namespace Voting_System_Server.Controllers
{
    public class LoginController
    {
        private Client client;

        public LoginController(Client client)
        {
            this.client = client;
        }

        // Status
        public const string ACCOUNT_CREATED = "ACCOUNT_CREATED";
        public const string ACCOUNT_FOUNDED = "ACCOUNT_FOUNDED";
        public const string ACCOUNT_WRONG = "ACCOUNT_WRONG";

        // Action Types
        public const string LOGIN = "LOGIN";

        // Reducer
        public void Handle(Dictionary<string, string> data)
        {
            switch (data["action"])
            {
                case LOGIN:
                    Login(data);
                    break;
                default:
                    return;
            }
        }

        // Actions
        private void Login(Dictionary<string, string> data)
        {
            var Db = AppDatabaseContext.GetInstance();
            string email = data["email"];
            var appUser = (
                    from user in Db.AppUsers
                    where user.EMAIL == email
                    select user
                ).FirstOrDefault();

            // Send response
            Dictionary<string, string> response = new Dictionary<string, string>();
            try
            {
                if (appUser == null)
                {
                    appUser = new AppUser();
                    appUser.EMAIL = data["email"];
                    appUser.PASSWORD = data["password"];

                    Db.AppUsers.Add(appUser);
                    Db.SaveChanges();

                    client.state.User = appUser;
                    response["status"] = ACCOUNT_CREATED;
                    response["id"] = appUser.ID.ToString();
                    response["email"] = appUser.EMAIL;
                    response["password"] = appUser.PASSWORD;

                } else if (appUser.PASSWORD == data["password"])
                {
                    client.state.User = appUser;
                    response["status"] = ACCOUNT_FOUNDED;
                    response["id"] = appUser.ID.ToString();
                    response["email"] = appUser.EMAIL;
                    response["password"] = appUser.PASSWORD;
                } else
                {
                    response["status"] = ACCOUNT_WRONG;
                    response["message"] = "Sai thông tin đăng nhập";
                }
            } catch (Exception e)
            {
                response["status"] = Client.ERROR;
                response["message"] = e.Message;

                throw e;
            } finally
            {
                client.Send(response);
            }
        }

        public static void Authenticate(Client client)
        {
            if (client.state.User == null)
                throw new Exception("Chưa đăng nhập!");
        }
    }
}
