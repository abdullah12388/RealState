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
                else if (users.userTypeId.Equals(4))
                {
                    user_req_team urtobject = URT(users);
                    return View("teamLeader", urtobject);
                }

                else if (Session["type"].Equals(2))
                {
                    return RedirectToAction("customerProfile" , "Customer");
                }
                return View(users);
            }
            else
            {
                return HttpNotFound();
            }
    }
        //team leader Functions
        public ActionResult Accept(int id)
        {
            var reqTeam = db.Req_Team.Where(x => x.Id.Equals(id)).FirstOrDefault();
            reqTeam.rStatue = 1;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Reject(int id)
        {
            var reqTeam = db.Req_Team.Where(x => x.Id.Equals(id)).FirstOrDefault();
            reqTeam.rStatue = 2;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private user_req_team URT(users users) {
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
            urtl.je = db.users.Where(x => x.userTypeId == 5).ToList();
            return urtl;
        }
        public ActionResult ReqJE(string proId , string jeId)
        {
            int pid = int.Parse(proId);
            int jid = int.Parse(jeId);
            int pm = (int)Session["ID"];
            var req = db.Req_Team.Where(x => x.rPM == pm && x.proId == pid && x.rTL == jid).FirstOrDefault();
            //Console.Write(req.Id);
            if (req != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                int i = int.Parse(proId);
                var p = db.project.Where(x=> x.Id == i).FirstOrDefault();
                Req_Team rt = new Req_Team();
                rt.rPM = (int)Session["ID"];
                rt.rTL = int.Parse(jeId);
                rt.tId = p.pTeam;
                rt.proId = p.Id;
                rt.rStatue = 0;
                db.Req_Team.Add(rt);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult DelJE(string proId, string jeId)
        {
            int pid = int.Parse(proId);
            int jid = int.Parse(jeId);
            int pm = (int)Session["ID"];
            var req = db.Req_Team.Where(x => x.rPM == pm && x.proId == pid && x.rTL == jid && x.rStatue == 1).FirstOrDefault();
            //Console.Write(req.Id);
            if (req != null)
            {
                db.Req_Team.Remove(req);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
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