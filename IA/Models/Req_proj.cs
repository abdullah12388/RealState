namespace RealEstate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Req_proj
    {
        [Key]
        public int R_ID { get; set; }

        public int R_PM { get; set; }

        public int R_PROJ { get; set; }

        public int? R_statue { get; set; }

        public virtual Project Project { get; set; }

        public virtual user user { get; set; }
    }
}
