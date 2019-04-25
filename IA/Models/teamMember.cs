namespace IA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class teamMember
    {
        public int Id { get; set; }
        [ForeignKey("Team")]
        public int teamId { get; set; }
        public virtual team Team { get; set; }
        [ForeignKey("user")]
        public int userId { get; set; }
        public virtual users user { get; set; }
        public int? Statue { get; set; }

        

        
    }
}
