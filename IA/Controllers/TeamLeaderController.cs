using IA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IA.Controllers
{
    public class TeamLeaderController : Controller
    {
        private RealEstateContext db = new RealEstateContext();
        // GET: TeamLeader
        public ActionResult Accept(int id)
        {
            var reqTeam = db.Req_Team.Where(x => x.Id.Equals(id)).FirstOrDefault();
            reqTeam.rStatue = 1;
            db.SaveChanges();
            return RedirectToAction("Index", "Profile");
        }
        public ActionResult Reject(int id)
        {
            var reqTeam = db.Req_Team.Where(x => x.Id.Equals(id)).FirstOrDefault();
            reqTeam.rStatue = 2;
            db.SaveChanges();
            return RedirectToAction("Index", "Profile");
        }
        public ActionResult ReqJE(string proId, string jeId)
        {
            int pid = int.Parse(proId);
            int jid = int.Parse(jeId);
            int pm = (int)Session["ID"];
            var req = db.Req_Team.Where(x => x.rPM == pm && x.proId == pid && x.rTL == jid).FirstOrDefault();
            //Console.Write(req.Id);
            if (req != null)
            {
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                int i = int.Parse(proId);
                var p = db.project.Where(x => x.Id == i).FirstOrDefault();
                Req_Team rt = new Req_Team();
                rt.rPM = (int)Session["ID"];
                rt.rTL = int.Parse(jeId);
                rt.tId = p.pTeam;
                rt.proId = p.Id;
                rt.rStatue = 0;
                db.Req_Team.Add(rt);
                db.SaveChanges();
            }
            return RedirectToAction("Index","Profile");
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
                return RedirectToAction("Index", "Profile");
            }
            return RedirectToAction("Index", "Profile");
        }
    }
}