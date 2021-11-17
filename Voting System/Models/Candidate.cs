namespace Voting_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Candidate
    {
        public Candidate()
        {
        }

        public int ID { get; set; }

        public string NAME { get; set; }

        public int AGE { get; set; }

        public string PHONE { get; set; }

        public string EMAIL { get; set; }

        public string AVATAR { get; set; }

        public string ROLE { get; set; }

        public string REASON { get; set; }

        public int NUM_VOTES { get; set; }

        public int ID_VOTE_EVENT { get; set; }
    }
}
