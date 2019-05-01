using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IA.Models
{
    public class Feedback
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("tmld")]
        public int? tl { get; set; }
        public virtual users tmld { get; set; }

        [ForeignKey("juen")]
        public int? je { get; set; }
        public virtual users juen { get; set; }

        [ForeignKey("proj")]
        public int? pro { get; set; }
        public virtual users proj { get; set; }

        public int rate { get; set; }

        public string feedback { get; set; }
    }
}