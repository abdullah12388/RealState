using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IA.Models
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("project")]
        public int project_Id { get; set; }
        public virtual projects project { get; set; }
        public string comment { get; set; }
    }
}