namespace Voting_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vote
    {
        public Vote()
        {
        }

        public int ID { get; set; }

        public int ID_USER { get; set; }

        public int ID_VOTE_EVENT { get; set; }
    }
}
