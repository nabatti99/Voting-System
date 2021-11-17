using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting_System_Server.Models
{
    public class AppState
    {
        public AppDatabaseContext Db { get; set; }

        public AppUser User { get; set; }

        public List<int> VoteIds { get; set; }

        public List<int> voteEventIds { get; set; }
    }
}
