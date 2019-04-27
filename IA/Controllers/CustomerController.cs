using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IA.Models;
using IA.ViewModel;
namespace IA.Controllers
{
    public class CustomerController : Controller
    {
        RealEstateContext _context;

        public CustomerController()
        {
            _context = new RealEstateContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customer
        public ActionResult customerProfile()
        {
            if (Session["ID"] != null)
            {
                if (Session["type"].Equals(2))
                {
                    var Customer_ID = Convert.ToInt32(Session["ID"]);

                    var Assigndprojects = _context.project.Where(p => p.customerId == Customer_ID && p.pStatus == 1);

                    var NotAssigndprojects = _context.project.Where(p => p.customerId == Customer_ID && p.pStatus == 0);

                    var Customer = _context.users.SingleOrDefault(c => c.Id == Customer_ID);

                    var viewmodel = new Customer_Assingnd_NotAssigned_Projects
                    {
                        Assigned = Assigndprojects,

                        NotAssigned = NotAssigndprojects,

                        CustomerData = Customer
                    };

                    return View(viewmodel);
                }
                else
                {
                    return HttpNotFound("Not A Customer");
                }

            }
            else
            {
                return HttpNotFound("Not Registered");
            }
        }

        public ActionResult DeleteProject(int id)
        {
            if (Session["ID"] != null)
            {
                if (Session["type"].Equals(2))
                {
                    var project = _context.project.Single(p => p.Id == id && p.pStatus == 0);

                    if (project == null)
                    {
                        return HttpNotFound("project Not Found OR Project Already Assigned To Project Manger");
                    }
                    else
                    {
                        _context.project.Remove(project);
                        _context.SaveChanges();

                        return RedirectToAction("customerProfile");
                    }
                }
                else
                {
                    return HttpNotFound("Not A Customer");
                }
            }
            else
            {
                return HttpNotFound("Not LogIn");
            }

        }
    }
}