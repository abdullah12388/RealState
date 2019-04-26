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
                    int i = 0;
                    foreach (var item in urtl.rtl)
                    {
                        var pro = db.project.Where(x => x.Id.Equals((int)item.proId)).FirstOrDefault();
                        urtl.proName.Add(pro.pName);
                        var pm = db.users.Where(x => x.Id.Equals((int)item.rPM)).FirstOrDefault();
                        urtl.pmName.Add(pm.firstName);
                        i++;
                    }
                    return View("teamLeader", urtl);
                }
                return View(users);
            }
            else
            {
                return HttpNotFound();
            }
    }
        public ActionResult Accept(int id)
        {
            var reqTeam = db.Req_Team.Where(x => x.Id.Equals(id)).FirstOrDefault();
            reqTeam.rStatue = 1;
            db.SaveChanges();
            return Content(id.ToString());
        }
        public ActionResult Reject(int id)
        {
            var reqTeam = db.Req_Team.Where(x => x.Id.Equals(id)).FirstOrDefault();
            reqTeam.rStatue = 2;
            db.SaveChanges();
            return Content(id.ToString());
        }
    }
}