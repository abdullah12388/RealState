using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA.Models;
namespace IA.ViewModel
{
    public class ListPro
    {
        public IEnumerable<projects> ListProject { get; set; }
        public projects project { get; set; }

        public users user { get; set; }
    }
}