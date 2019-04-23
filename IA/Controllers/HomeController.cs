using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using IA.Models;
using System.Net;

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
            
            if (ModelState.IsValid)
            {
                usr.userTypeId = int.Parse(type);
                db.users.Add(usr);
                db.SaveChanges();
                Session["ID"] = usr.Id;
                Session["type"] = usr.userTypeId;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.User_Types = db.userType.Where(x => x.userTypeId > 1).ToList();
                return View("Index",usr);
                //return Content("Eroor");
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
                    return RedirectToAction("Index");
                } 
            }
            catch
            {
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