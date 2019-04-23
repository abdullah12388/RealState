namespace IA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Report")]
    public partial class report
    {
        [Key]
        public int Id { get; set; }
        
        public string rdesc { get; set; }

        public int? rUser { get; set; }
        public virtual users user { get; set; }
    }
}
