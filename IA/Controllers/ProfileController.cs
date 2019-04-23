using IA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IA.Controllers
{
    public class ProfileController : Controller
    {
        private RealEstateContext db = new RealEstateContext();

        // GET: Profile
        public ActionResult Index()
        {
            
            if (Session["ID"] != null)
            {
                users users = db.users.Find(Session["ID"]);
                return View(users);
            }
            else
            {
                return HttpNotFound();
            }
        
    }
    }
}