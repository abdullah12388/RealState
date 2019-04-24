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
            users us = new users();
            ViewBag.User_Types = db.userType.Where(x => x.userTypeId > 1).ToList();
            
            return View(us);
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
                FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

                
                usr.photo = Path.Combine(Server.MapPath("~/Content/Images/"), FileName);
                usr.photoFile.SaveAs(usr.photo);
                db.users.Add(usr);
                db.SaveChanges();
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
    }
}