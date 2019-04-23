namespace IA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class userType
    {
        
        [Key]
        public int userTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string type { get; set; }

        public virtual ICollection<users> users { get; set; }
    }
}
