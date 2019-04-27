using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA.Models;
namespace IA.ViewModel
{
    public class Customer_Assingnd_NotAssigned_Projects
    {
        public IEnumerable<projects> Assigned { get; set; }
        public IEnumerable<projects> NotAssigned { get; set; }
        public users CustomerData { get; set; }
    }
}