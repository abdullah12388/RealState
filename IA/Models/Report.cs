namespace RealEstate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Report")]
    public partial class Report
    {
        [Key]
        public int R_ID { get; set; }

        public int? R_user { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string desc { get; set; }

        public virtual user user { get; set; }
    }
}
