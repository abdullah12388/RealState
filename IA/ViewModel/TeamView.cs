using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IA.Models
{
    public class TeamView
    {
        public List<team> ourteam = new List<team>();
        public List<users> users = new List<users>();
        public List<teamMember> tm = new List<teamMember>();
    }
}