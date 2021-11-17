namespace Voting_System_Server.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VoteTurn")]
    public partial class VoteTurn
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        public int ID_VOTE { get; set; }

        public int ID_CANDIDATE { get; set; }

        public virtual Candidate Candidate { get; set; }

        public virtual Vote Vote { get; set; }
    }
}
