namespace IA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class users
    {
        [Key]
        public int Id { get; set; }

        
        [StringLength(50)]
        [Required(ErrorMessage ="please enter el zft ")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50)]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50)]
        public string email { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50)]
        public string password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirm Password required")]
        [StringLength(50)]
        [CompareAttribute("password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassowrd { get; set; }


        [Required(ErrorMessage = "Required")]
        [StringLength(50)]
        public string address { get; set; }

        
        public string phone { get; set; }
        public string jobDescription { get; set; }
        public string qualification { get; set; }
        public string experience { get; set; }

        
        public int? userTypeId { get; set; }
        public virtual userType type { get; set; }

        public virtual ICollection<projects> pPM { get; set; }

        public virtual ICollection<projects> pCustomer { get; set; }

        public virtual ICollection<report> Reports { get; set; }
        
        public virtual ICollection<Req_proj> Req_proj { get; set; }
        // good
        public virtual ICollection<team> Teams { get; set; }

        public virtual ICollection<teamMember> Team_member { get; set; }

    }
}
