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
                if (users.userTypeId.Equals(1))
                {
                    AdminView adminView = AV(users);
                    return View("admin", adminView);
                }
                else if(users.userTypeId.Equals(4))
                {
                    user_req_team urtobject = TL(users);
                    return View("teamLeader", urtobject);
                }
                else if (users.userTypeId.Equals(3))
                {
                    int ID = Convert.ToInt32(Session["ID"]);
                    ViewBag.Exp = db.Experience.Where(x => x.userId == ID).ToList();
                    return View("projectManager",users);
                }
                return View(users);
            }
            else
            {
                return HttpNotFound();
            }
    }
        
        //team leader Functions
        private user_req_team TL(users users) {
            user_req_team urtl = new user_req_team();
            urtl.tl = users;
            urtl.rtl = db.Req_Team.Where(x => x.rTL.Equals(users.Id) && x.rStatue == 0).ToList();
            foreach (var item in urtl.rtl)
            {
                var pro = db.project.Where(x => x.Id.Equals((int)item.proId)).FirstOrDefault();
                urtl.proName.Add(pro.pName);
                var pm = db.users.Where(x => x.Id.Equals((int)item.rPM)).FirstOrDefault();
                urtl.pmName.Add(pm.firstName);
            }
            var currentProject = db.Req_Team.Where(x => x.rTL.Equals(users.Id) && x.rStatue == 1).ToList();
            foreach (var item in currentProject)
            {
                var pro = db.project.Where(x => x.Id.Equals((int)item.proId)).FirstOrDefault();
                urtl.curPro.Add(pro);
            }
            var HistoryProject = db.Req_Team.Where(x => x.rTL.Equals(users.Id) && x.rStatue == 3).ToList();
            foreach (var item in HistoryProject)
            {
                var pro = db.project.Where(x => x.Id.Equals((int)item.proId)).FirstOrDefault();
                urtl.hisPro.Add(pro.pName);
            }
            int pmc = (int)Session["ID"];
            urtl.je = db.users.Where(x => x.userTypeId == 5).ToList();
            foreach (var item in urtl.je )
            {
                var j = db.Req_Team.Where(x => x.rPM == pmc && x.rTL == item.Id && x.rStatue == 1).FirstOrDefault();
                if(j != null)
                {
                    urtl.jepro.Add(item);
                }
            }
            return urtl;
        }
        
        // Admin Function
        private AdminView AV(users u)
        {
            AdminView adminView = new AdminView();
            adminView.user = u;
            adminView.AllUsers = db.users.Where(x => x.userTypeId != 1).ToList();
            adminView.AllProjects = db.project.Where(x => x.Id > 0).ToList();
            return adminView;
        }
    }
}