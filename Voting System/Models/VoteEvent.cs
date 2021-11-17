namespace Voting_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VoteEvent
    {
        public VoteEvent()
        {
        }

        public int ID { get; set; }

        public string TITLE { get; set; }

        public DateTime BEGIN_DATE { get; set; }

        public DateTime END_DATE { get; set; }

        public int ID_HOST { get; set; }

        public int MAX_VOTE_TURN { get; set; }
    }
}
