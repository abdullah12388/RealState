namespace RealEstate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Schedule")]
    public partial class Schedule
    {
        [Key]
        public int S_ID { get; set; }

        public int P_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Start { get; set; }

        [Column(TypeName = "date")]
        public DateTime End { get; set; }

        public int Progress { get; set; }

        public int team_ID { get; set; }

        public virtual Project Project { get; set; }

        public virtual Team Team { get; set; }
    }
}
