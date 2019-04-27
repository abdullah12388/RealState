using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IA.Models;
using IA.ViewModels;
using System.Data.Entity;
namespace IA.Controllers
{ 
    public class CustomerController : Controller
    { 
    
        private RealEstateContext _context;

        public CustomerController()
        {
            _context = new RealEstateContext();

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Customer
        [HttpPost]
        public ActionResult SaveProject(projects project)
        {
            if (Session["ID"].Equals(2))
            {
                if (project.Id == 0)
                {
                    var ID = Convert.ToInt32(Session["ID"]);
                    project.pStatus = 0;
                    project.pCustomer = ID;

                    _context.project.Add(project);
                }
                else
                {

                    var projectindb = _context.project.Single(p => p.Id == project.Id);
                    projectindb.pName = project.pName;
                    projectindb.pSalary = project.pSalary;
                    projectindb.pArea = project.pArea;
                    projectindb.pDescription = project.pDescription;

                }
                _context.SaveChanges();

                return View();
            }
            else { 
            
                return HttpNotFound();
            }
        }

        public ActionResult DeleteProject(int id)
        {
            if (Session["ID"].Equals(2))
            {
                var project = _context.project.Single(p => p.Id == id && p.pPM == null);
                if (project == null)
                {
                    return HttpNotFound("There Is No project With That ID Or IT is Already Assigned To Project Manger");
                }
                _context.project.Remove(project);
                _context.SaveChanges();

                return View();
            }
            else
            {
                return HttpNotFound("Not A Customer");
            }
        }

        public ActionResult ALLProjects(int pagenum)
        {

            var project = _context.project.Where(p => p.pStatus == 0).ToList();

            return View();
        }

        public ActionResult CustomerProjects()
        {
            var customerID = Convert.ToInt32(Session["ID"]);

            var Assignedprojects = _context.project.Where(p => p.pCustomer == customerID && p.pStatus == 1).ToList();

            var NonAssignedprojects = _context.project.Where(p => p.pCustomer == customerID && p.pStatus == 0).ToList();

            var viewmodel = new AssignedandNotAssignedProjects
            {
                Assignedproject = Assignedprojects,
                NonAssignedproject = NonAssignedprojects
            };

            return View(viewmodel);
        }

    }
}