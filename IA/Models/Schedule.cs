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
        
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int Progress { get; set; }
        
        public int pId { get; set; }
        public virtual projects Project { get; set; }
        public int teamId { get; set; }
        public virtual team Team { get; set; }
    }
}
