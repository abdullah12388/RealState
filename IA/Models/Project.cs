namespace RealEstate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            Req_proj = new HashSet<Req_proj>();
            Schedules = new HashSet<Schedule>();
        }

        [Key]
        public int P_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string p_name { get; set; }

        [Column(TypeName = "money")]
        public decimal p_salary { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string p_desc { get; set; }

        [Required]
        [StringLength(50)]
        public string p_area { get; set; }

        public int? p_pm { get; set; }

        public int p_customer { get; set; }

        [Column(TypeName = "text")]
        public string p_comment { get; set; }

        public int p_status { get; set; }

        public int? p_team { get; set; }

        public virtual user user { get; set; }

        public virtual user user1 { get; set; }

        public virtual Team Team { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Req_proj> Req_proj { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
