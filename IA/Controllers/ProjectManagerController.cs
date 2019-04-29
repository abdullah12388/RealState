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
            ViewBag.Id = new SelectList(db.project, "Id", "pName");
            Comments com = new Comments();
            return PartialView("_comment", com);
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
            ViewBag.proj = db.project.Where(x => x.pmId == ID && x.progress == 2).ToList();
            return PartialView("curProj");
        }
        public ActionResult pre_project()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            ViewBag.c_proj = db.project.Where(x => x.pmId == ID && x.progress == 3).ToList();
            return PartialView("prevProj");
        }
        public ActionResult cur_projects_list()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            var project = db.project.Where(p => p.pmId == ID).Include(p => p.Customer).Include(p => p.Pm).Include(p => p.Team);
            return PartialView("_Control_project", project.ToList());
        }
        public ActionResult Edit(int id)
        {
            projects projects = db.project.Find(id);
            if (projects == null) { return HttpNotFound(); }
            ViewBag.customerId = new SelectList(db.users, "Id", "firstName", projects.customerId);
            ViewBag.pmId = new SelectList(db.users, "Id", "firstName", projects.pmId);
            ViewBag.pTeam = new SelectList(db.team, "Id", "Id", projects.pTeam);
            return PartialView("_EditProj", projects);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,pName,pSalary,pDescription,pArea,pmId,customerId,pTeam")] projects projects, string statue)
        {
            if (ModelState.IsValid)
            {
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

    }
}