using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IA.Models
{
    public class Req_Team
    {
        [Key]
        public int Id { get; set; }
        public int? rStatue { get; set; }
        [ForeignKey("teamleader")]
        public int rTL { get; set; }
        public virtual users teamleader { get; set; }
        [ForeignKey("user")]
        public int? rPM { get; set; }
        public virtual users user { get; set; }
    }
}