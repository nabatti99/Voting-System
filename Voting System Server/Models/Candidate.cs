namespace Voting_System_Server.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Candidate")]
    public partial class Candidate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Candidate()
        {
            VoteTurns = new HashSet<VoteTurn>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string NAME { get; set; }

        public int AGE { get; set; }

        [Required]
        [StringLength(10)]
        public string PHONE { get; set; }

        [Required]
        [StringLength(50)]
        public string EMAIL { get; set; }

        [StringLength(200)]
        public string AVATAR { get; set; }

        [Required]
        [StringLength(50)]
        public string ROLE { get; set; }

        [Required]
        [StringLength(500)]
        public string REASON { get; set; }

        public int NUM_VOTES { get; set; }

        public int ID_VOTE_EVENT { get; set; }

        public virtual VoteEvent VoteEvent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VoteTurn> VoteTurns { get; set; }
    }
}
