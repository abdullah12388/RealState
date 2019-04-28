using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA.Models;
namespace IA.Models
{
    public class ProjectMangerAssign
    {
        public IEnumerable<users> ProjectManagersList { get; set; }
        public IEnumerable<projects> ProjectList { get; set; }

        public Req_proj Reqpro { get; set; }
    }
}