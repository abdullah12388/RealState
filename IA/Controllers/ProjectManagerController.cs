using IA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IA.Controllers
{
    public class ProjectManagerController : Controller
    {
        // GET: ProjectManager
        private RealEstateContext db = new RealEstateContext();
        public ActionResult comment()
        {
            //ViewBag.Id = new SelectList(db.project, "Id", "pName");
            ViewBag.Id = db.project.Where(x=> x.Id > 0).ToList();
            Comments com = new Comments();
            return PartialView("_comment", com);
        }
        public ActionResult AddComment(Comments com,string pid)
        {
            int id = int.Parse(pid);
            com.project_Id = id;
            db.Comments.Add(com);
            db.SaveChanges();
            return RedirectToAction("Index","Profile");
        }
        //ended
        public ActionResult Exp_chart()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            ViewBag.Exp = db.Experience.Where(x => x.userId == ID).ToList();
            return PartialView("chart");
        }
        public ActionResult cur_projects()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            ViewBag.proj = db.project.Where(x => x.pmId == ID && x.progress == 1).ToList();
            return PartialView("curProj");
        }
        public ActionResult pre_project()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            ViewBag.c_proj = db.project.Where(x => x.pmId == ID && x.progress == 2).ToList();
            return PartialView("prevProj");
        }
        public ActionResult cur_projects_list()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            var project = db.project.Where(p => p.pmId == ID).Include(p => p.Customer).Include(p => p.Pm).Include(p => p.Team);
            ///////////////////////////////////////////////////////////////////
            List<schedule> sch = new List<schedule>();
            foreach (var item in project.ToList())
            {
                sch.Add(db.schedule.Where(x => x.pId == item.Id).FirstOrDefault());
            }
            ViewBag.sc = sch;
            ///////////////////////////////////////////////////////////////////
            return PartialView("_Control_project", project.ToList());
        }
        public ActionResult Edit(int id)
        {
            projects projects = db.project.Find(id);
            if (projects == null) { return HttpNotFound(); }
            ViewBag.customerId = new SelectList(db.users, "Id", "firstName", projects.customerId);
            ViewBag.pmId = new SelectList(db.users, "Id", "firstName", projects.pmId);
            ViewBag.pTeam = new SelectList(db.team, "Id", "Id", projects.pTeam);
            ///////////////////////////////////////////////////////////
            ViewBag.sch = db.schedule.Where(x=> x.pId == id && x.Progress == 1).ToList();
            ///////////////////////////////////////////////////////////
            return PartialView("_EditProj", projects);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,pName,pSalary,pDescription,pArea,pmId,customerId,pStatus,pTeam")] projects projects, string statue,string start,string end,string pId)
        {
            if (ModelState.IsValid)
            {
                /////////////////////////////////////////////////
                int pid = int.Parse(pId);
                var sc = db.schedule.Single(x=> x.pId == pid);
                sc.Start = start;
                sc.End = end;
                db.SaveChanges();
                /////////////////////////////////////////////////
                projects.progress = int.Parse(statue);
                db.Entry(projects).State = EntityState.Modified;
                projects.pmId = Convert.ToInt32(Session["ID"]);
                db.SaveChanges();
                Session["Valid_edit"] = 0;
                return RedirectToAction("Index", "Profile");
            }
            ViewBag.customerId = new SelectList(db.users, "Id", "firstName", projects.customerId);
            ViewBag.pmId = new SelectList(db.users, "Id", "firstName", projects.pmId);
            ViewBag.pTeam = new SelectList(db.team, "Id", "Id", projects.pTeam);
            Session["Valid_edit"] = 1;
            return RedirectToAction("Index", "Profile");
        }
        public ActionResult cancel(int? id)
        {
            //if (id == null){ return HttpNotFound(); }
            projects project = db.project.Find(id);
            //if (project == null){return HttpNotFound();}
            project.pmId = null;
            project.pStatus = 1;
            Session["Valid_edit"] = 0;
            db.SaveChanges();
            int ID = Convert.ToInt32(Session["ID"]);
            var projects = db.project.Where(p => p.pmId == ID).Include(p => p.Customer).Include(p => p.Pm).Include(p => p.Team);
            return PartialView("_Control_project", projects.ToList());
        }
        public ActionResult RequestView()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            var req = db.Req_proj.Where(x => x.rPM == ID && x.rStatue == 0).ToList();
            return PartialView("Requests",req);
        }
        public ActionResult Accept(int id)
        {
            int ID = Convert.ToInt32(Session["ID"]);
            var req = db.Req_proj.Where(x => x.Id.Equals(id)).FirstOrDefault();
            var pro = db.project.Where(x => x.Id.Equals(req.rProject)).FirstOrDefault();
            req.rStatue = 1;
            pro.pStatus = 2;
            pro.pmId = ID;
            pro.progress = 1;
            db.SaveChanges();
            return RedirectToAction("Index", "Profile");
        }
        public ActionResult Reject(int id)
        {
            var req = db.Req_proj.Where(x => x.Id.Equals(id)).FirstOrDefault();
            req.rStatue = 2;
            db.SaveChanges();
            return RedirectToAction("Index", "Profile");
        }
        public ActionResult TeamView()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            TeamView tv = new TeamView();
            tv.ourteam = db.team.Where(x=> x.pmId == ID).ToList();
            tv.users.AddRange(db.users.Where(x => x.userTypeId == 4).ToList());
            tv.users.AddRange(db.users.Where(x => x.userTypeId == 5).ToList());
            foreach (var item in tv.ourteam)
            {
                tv.tm.AddRange(db.teamMember.Where(x=>x.teamId == item.Id).ToList());
            }
            return PartialView("_TeamView",tv);
        }
        public ActionResult DeleteMember(int id)
        {
            var mem = db.teamMember.Where(x=>x.Id == id).FirstOrDefault();
            if (mem != null)
            {
                db.teamMember.Remove(mem);
                db.SaveChanges();
                return RedirectToAction("Index","Profile");
            }
            else
            {
                return HttpNotFound("Member Not found!");
            }
        }
        public ActionResult AddTeam()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            team t = new team();
            t.pmId = ID;
            db.team.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index", "Profile");
        }
        public ActionResult DeleteTeam(string teamid)
        {
            try
            {
                int tmid = int.Parse(teamid);
                var team = db.team.Single(x => x.Id == tmid);
                if (team != null)
                {
                    db.team.Remove(team);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    return HttpNotFound("Team Not found!");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Profile");
            }
        }
        public ActionResult TeamRequest(string userid,string teamid)
        {
            int ID = Convert.ToInt32(Session["ID"]);
            int uid = int.Parse(userid);
            int tmid = int.Parse(teamid);
            Req_Team rt = new Req_Team();
            rt.rPM = ID;
            rt.rTL = uid;
            rt.tId = tmid;
            rt.proId = null;
            rt.rStatue = 0;
            db.Req_Team.Add(rt);
            db.SaveChanges();
            return RedirectToAction("Index", "Profile");
        }
        public ActionResult ReportView()
        {
            var users = db.users.Where(x=> x.userTypeId == 2).ToList();
            return PartialView("ReportView",users);
        }
        public ActionResult Report(string custid,string report)
        {
            int cid = int.Parse(custid);
            report rp = new report();
            rp.rUser = cid;
            rp.rdesc = report;
            db.report.Add(rp);
            db.SaveChanges();
            return RedirectToAction("Index", "Profile");
        }
        public ActionResult ScheduleView()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            var projects = db.project.Where(x => x.pmId == ID && x.pStatus == 2).ToList();
            ViewBag.teams = db.team.Where(x => x.pmId == ID).ToList();
            return PartialView("ScheduleView", projects);
        }
        public ActionResult Schedule(string proid,string teamid,string start,string end)
        {
            int pid = int.Parse(proid);
            int tid = int.Parse(teamid);
            schedule sch = new schedule();
            sch.pId = pid;
            sch.teamId = tid;
            sch.Start = start;
            sch.End = end;
            sch.Progress = 1;
            db.schedule.Add(sch);
            db.SaveChanges();
            return RedirectToAction("Index", "Profile");
        }
    }
}