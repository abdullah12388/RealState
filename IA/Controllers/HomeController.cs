using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using IA.Models;
using System.Net;
using System.IO;

namespace IA.Controllers
{
    public class HomeController : Controller
    {
        private RealEstateContext db = new RealEstateContext();
        // GET: Home
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                if (Session["type"].Equals(2))
                {
                    var ID = Convert.ToInt32(Session["ID"]);
                    var proList_pro = new ListPro()
                    {

                        ListProject = db.project.Where(p => p.pStatus == 0),
                        project = new projects(),
                        user = db.users.SingleOrDefault(u => u.Id == ID)

                    };
                    return View("Customer", proList_pro);
                }
                ViewBag.projects = db.project.Where(x=>x.pStatus == 1).ToList();
                return View("Users");
            }
            else
            {
                users us = new users();
                ViewBag.User_Types = db.userType.Where(x => x.userTypeId > 1).ToList();

                return View(us);
            }
        }

        [HttpPost]
        public object Register(users usr, string type)
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
                Session["ID"] = usr.Id;
                Session["type"] = usr.userTypeId;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.User_Types = db.userType.Where(x => x.userTypeId > 1).ToList();
                return View("Index", usr);
            }
        }

        [HttpPost]
        public ActionResult Login(users usr)
        {
            try
            {
                var user = db.users.Where(u => u.email.Equals(usr.email) && u.password.Equals(usr.password)).FirstOrDefault();
                if (user != null)
                {
                    Session["ID"] = user.Id;
                    Session["firstName"] = user.firstName;
                    Session["type"] = user.userTypeId;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Wrong email or password. Please try again";
                    return RedirectToAction("Index");
                } 
            }
            catch
            {
                TempData["Message"] = "Wrong email or password. Please try again";
                return RedirectToAction("Index");
            }

        }
        public ActionResult Logout(users usr)
        {
            Session.RemoveAll();
            return RedirectToAction("Index");
        }
        public ActionResult test()
        {
            var pro = db.project.Where(x => x.Id.Equals(3)).FirstOrDefault();
            int pmid = (int) pro.pmId;
            var u = db.users.Where(y => y.Id.Equals(pmid)).FirstOrDefault();
            return Content(u.firstName.ToString());
        }
    }
}