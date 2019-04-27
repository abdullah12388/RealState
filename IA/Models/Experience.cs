using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IA.Models
{
    public class Experience
    {
        [Key]
        public int Id { get; set; }
        public string E_Name { get; set; }
        public string year { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public virtual users User { get; set; }
    }
}