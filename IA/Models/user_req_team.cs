using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IA.Models
{
    public class user_req_team
    {
        public users tl = new users();
        public List<users> je = new List<users>();
        public List<Req_Team> rtl = new List<Req_Team>();
        public List<string> pmName = new List<string>();
        public List<string> proName = new List<string>();
        public List<projects> curPro = new List<projects>();
        public List<string> hisPro = new List<string>();
    }
}