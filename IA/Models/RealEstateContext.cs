using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IA.Models
{
    public class RealEstateContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public RealEstateContext() : base("name=RealEstateContext")
        {
        }

        public System.Data.Entity.DbSet<IA.Models.users> users { get; set; }

        public System.Data.Entity.DbSet<IA.Models.userType> userType { get; set; }

        public System.Data.Entity.DbSet<IA.Models.projects> project{ get; set; }
        public System.Data.Entity.DbSet<IA.Models.report> report{ get; set; }
        public System.Data.Entity.DbSet<IA.Models.Req_proj> Req_proj{ get; set; }
        public System.Data.Entity.DbSet<IA.Models.Req_Team> Req_Team { get; set; }
        public System.Data.Entity.DbSet<IA.Models.schedule> schedule { get; set; }
        public System.Data.Entity.DbSet<IA.Models.teamMember> teamMember{ get; set; }
        public System.Data.Entity.DbSet<IA.Models.team> team{ get; set; }

    }
}
