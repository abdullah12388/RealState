using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IA.Models
{
    public class user_req_team
    {
        public users tl = new users();
        public List<Req_Team> rtl = new List<Req_Team>();
        public List<string> pmName = new List<string>();
        public List<string> proName = new List<string>();
    }
}