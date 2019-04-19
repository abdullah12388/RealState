namespace RealEstate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Team_member
    {
        public int ID { get; set; }

        public int T_ID { get; set; }

        public int Mem_ID { get; set; }

        public int? Statue { get; set; }

        public virtual Team Team { get; set; }

        public virtual user user { get; set; }
    }
}
