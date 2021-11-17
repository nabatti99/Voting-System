namespace Voting_System_Server.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vote")]
    public partial class Vote
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vote()
        {
            VoteTurns = new HashSet<VoteTurn>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        public int ID_USER { get; set; }

        public int ID_VOTE_EVENT { get; set; }

        public virtual AppUser AppUser { get; set; }

        public virtual VoteEvent VoteEvent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VoteTurn> VoteTurns { get; set; }
    }
}
