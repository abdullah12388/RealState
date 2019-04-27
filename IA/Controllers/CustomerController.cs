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

        [HttpPost]
        public ActionResult SaveProject(projects project)
        {
            if (Session["ID"] != null)
                {
                    if (Session["type"].Equals(2))
                    {
                        if (project.Id == 0)
                        {
                            var ID = Convert.ToInt32(Session["ID"]);
                            project.pStatus = 0;
                            project.customerId = ID;

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

                        return RedirectToAction("customerProfile");
                    }
                    else
                    {

                        return HttpNotFound("Not A Customer");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
      
        }

        public ActionResult Edit(int id)
        {
            var project = _context.project.SingleOrDefault(p =>p.Id == id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View("CustomerForm",project);
        }

        [HttpPost]
        public ActionResult SaveAssignProjectManger(ProjectMangerAssign request)
        {
            if (Session["ID"] != null)
            {
                if (Session["type"].Equals(2))
                {
                    request.Reqpro.rStatue = 0;
                    _context.Req_proj.Add(request.Reqpro);
                    _context.SaveChanges();

                    return RedirectToAction("customerProfile", "Customer");

                }
                else
                {
                    return HttpNotFound("You Dont Have Acess To this Page");
                }
            }
            else
            {
                return HttpNotFound("Not Registerd");
            }

        }
        public ActionResult AssignProjectManger()
        {
            if (Session["ID"] != null)
            {
                if (Session["type"].Equals(2))
                {
                    var ID = Convert.ToInt32(Session["ID"]);

                    var ProList = _context.project.Where(p => p.customerId == ID && p.pStatus == 0);

                    var Pmlist = _context.users.Where(p => p.userTypeId == 3);

                    var List_Pm_pro = new ProjectMangerAssign
                    {
                        ProjectList = ProList,
                        ProjectManagersList = Pmlist,
                        Reqpro = new Req_proj()
                    };
                    return View("AssignProjectManger", List_Pm_pro);
                }
                else
                {
                    return HttpNotFound("You Dont Have Permision");
                }
            }
            else
            {
                return HttpNotFound("Not Registered");
            }
        }
    }
}