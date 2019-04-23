namespace IA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Req_proj
    {
        [Key]
        public int Id { get; set; }
        public int? rStatue { get; set; }

        public int rProject { get; set; }
        public virtual projects Project { get; set; }

        public int rPM { get; set; }
        public virtual users user { get; set; }
    }
}
