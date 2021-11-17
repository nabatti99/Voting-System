namespace Voting_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AppUser
    {
        public AppUser()
        {
        }

        public int ID { get; set; }

        public string EMAIL { get; set; }

        public string PASSWORD { get; set; }
    }
}
