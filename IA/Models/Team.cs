namespace IA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Team")]
    public partial class team
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("user")]
        public int? pmId{ get; set; }
        public virtual users user { get; set; }

        public virtual ICollection<schedule> Schedules { get; set; }
        
        public virtual ICollection<teamMember> Team_member { get; set; }

        public virtual ICollection<projects> Projects { get; set; }
        public virtual ICollection<Req_Team> tId { get; set; }
    }
}
