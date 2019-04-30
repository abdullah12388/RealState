using IA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IA.Controllers
{
    public class AdminController : Controller
    {
        private RealEstateContext db = new RealEstateContext();
        // GET: Admin
        public ActionResult Delete(int id)
        {
            var user = db.users.Single(x => x.Id == id);
            if (user != null)
            {
                db.users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Profile");
        }

        public ActionResult DeleteView()
        {
            List<users> users = db.users.Where(x => x.userTypeId > 1).ToList();
            return PartialView("DeleteUser",users);
        }

        public ActionResult Add(users usr, string type)
        {
            try
            {
                usr.userTypeId = int.Parse(type);


                string FileName = Path.GetFileNameWithoutExtension(usr.photoFile.FileName);

                //To Get File Extension  
                string FileExtension = Path.GetExtension(usr.photoFile.FileName);

                //Add Current Date To Attached File Name  
                Random rnd = new Random();
                int r = rnd.Next();
                FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + r.ToString() + FileName.Trim() + FileExtension;

                string path = Path.Combine(Server.MapPath("~/Content/Images/"), FileName);
                usr.photo = "Content/Images/" + FileName;


                db.users.Add(usr);
                db.SaveChanges();
                usr.photoFile.SaveAs(path);
                
                return RedirectToAction("Index", "Profile");
            }
            catch (Exception)
            {
                ViewBag.User_Types = db.userType.Where(x => x.userTypeId > 1).ToList();
                return View("Index" , "Profile");
            }
        }

        public ActionResult AddView()
        {
            ViewBag.User_Types = db.userType.Where(x => x.userTypeId > 1).ToList();
            users user = new users();
            return PartialView("AddUser");
        }

        public ActionResult DeleteProject(int id)
        {
            var project = db.project.Single(x => x.Id == id);
            if (project != null)
            {
                db.project.Remove(project);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Profile");
        }

        public ActionResult ProjectView()
        {
            List<projects> projects = db.project.Where(x => x.Id > 0 && x.pStatus > 0).ToList();
            return PartialView("DeleteProject", projects);
        }

        public ActionResult AddProject(projects project,string id = "1")
        {
            project.customerId = int.Parse(id);
            project.pStatus = 0;
            db.project.Add(project);
            db.SaveChanges();
            return RedirectToAction("Index", "Profile");
        }

        public ActionResult AddProjectView()
        {
            projects project = new projects();
            ViewBag.cust = db.users.Where(x => x.userTypeId == 2).ToList();
            return PartialView("AddProject", project);
        }

        public ActionResult UpdateProjectView(int id)
        {
            var project = db.project.SingleOrDefault(p => p.Id == id);
            if (project == null){return HttpNotFound();}
            return View("UpdateProject",project);
        }

        public ActionResult UpdateProject(projects project)
        {
            var projectindb = db.project.Single(p => p.Id == project.Id);
            projectindb.pName = project.pName;
            projectindb.pSalary = project.pSalary;
            projectindb.pArea = project.pArea;
            projectindb.pDescription = project.pDescription;
            db.SaveChanges();
            return RedirectToAction("Index", "Profile");
        }

        public ActionResult PenddingProjectsView()
        {
            List<projects> projects = db.project.Where(x => x.Id > 0 && x.pStatus == 0).ToList();
            return PartialView("PenddingProjects", projects);
        }
        public ActionResult AcceptProject(int id)
        {
            var project = db.project.Single(x => x.Id == id);
            project.pStatus = 1;
            db.SaveChanges();
            return RedirectToAction("Index","Profile");
        }
        public ActionResult RejectProject(int id)
        {
            var project = db.project.Single(x => x.Id == id);
            db.project.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index", "Profile");
        }
    }
}