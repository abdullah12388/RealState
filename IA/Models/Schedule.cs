namespace IA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Schedule")]
    public partial class schedule
    {
        [Key]
        public int Id { get; set; }
        
        public string Start { get; set; }

        public string End { get; set; }

        public int Progress { get; set; }
        [ForeignKey("Project")]
        public int pId { get; set; }
        public virtual projects Project { get; set; }
        [ForeignKey("Team")]
        public int teamId { get; set; }
        public virtual team Team { get; set; }
    }
}
