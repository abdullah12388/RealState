using IA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IA.Controllers
{
    public class ProjectManagerController : Controller
    {
        // GET: ProjectManager
        private RealEstateContext db = new RealEstateContext();

        
        
        public ActionResult pre_project()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            ViewBag.proj = db.project.Where(x => x.pmId == ID && x.progress == 2).ToList();
            ViewBag.c_proj = db.project.Where(x => x.pmId == ID && x.progress == 1).ToList();
            return PartialView("_pre_project");
        }

        public ActionResult Search_team()
        {
            return PartialView();
        }
        //ended
        public ActionResult Exp_chart()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            ViewBag.Exp = db.Experience.Where(x => x.userId == ID).ToList();
            return PartialView("chart");
        }
        public ActionResult PM_exp()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            //ViewBag.Exp = db.Experience.Where(x => x.userId == ID).ToList();
            ViewBag.proj = db.project.Where(x => x.pmId == ID && x.progress == 2).ToList();
            //ViewBag.c_proj = db.project.Where(x => x.pmId == ID && x.progress == 1).ToList();
            return PartialView("PM_Exp");
        }
    }
}