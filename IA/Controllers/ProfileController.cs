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
                if (users.userTypeId.Equals(1) || users.userTypeId.Equals(3))
                {
                    return View("admin", users);
                }
                else if(users.userTypeId.Equals(4))
                {
                    user_req_team urtl = new user_req_team();
                    urtl.tl = users;
                    urtl.rtl = db.Req_Team.Where(x => x.rTL.Equals(users.Id)).ToList();
                    return View("teamLeader", urtl);
                }
                return View(users);
            }
            else
            {
                return HttpNotFound();
            }
        
    }
    }
}