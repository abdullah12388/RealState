using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IA.Models
{
    public class AdminView
    {
        public users user = new users();
        public List<users> AllUsers = new List<users>();
        public List<projects> AllProjects = new List<projects>();
    }
}