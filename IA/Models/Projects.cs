namespace IA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class projects
    {
        
        [Key]
        public int Id { get; set; }
        public string pName { get; set; }
        public decimal pSalary { get; set; }
        public string pDescription { get; set; }
        public string pArea { get; set; }
        public int pStatus { get; set; }
        [ForeignKey("Pm")]
        public int? pmId { get; set; }
        public virtual users Pm{ get; set; }
        [ForeignKey("Customer")]
        public int customerId { get; set; }
        public virtual users Customer { get; set; }
        [ForeignKey("Team")]
        public int? pTeam { get; set; }
        public virtual team Team { get; set; }
        public virtual ICollection<Req_proj> Req_proj { get; set; }
        public virtual ICollection<schedule> Schedules { get; set; }
        public virtual ICollection<Req_Team> proId { get; set; }
    }
}
