using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using MVCProject.ViewModel;

namespace MVCProject.Controllers
{
    public class EmployeeController : Controller
    {
        ITIContext context = new ITIContext();
        public IActionResult Index()
        {
            List<Employee> employees = context.Employee.Include(e => e.Department).ToList();
            return View("Index", employees);
        }
        public IActionResult Edit(int id)
        {
            Employee empModel = context.Employee.Include(e => e.Department).FirstOrDefault(e => e.Id == id);
            List<Department> departments = context.Department.ToList();
            EmpWithDeptListViewModel empWithDeptListViewModel = new EmpWithDeptListViewModel
            {
                Id = empModel.Id,
                Name = empModel.Name,
                Salary = empModel.Salary,
                JobTitle = empModel.JobTitle,
                ImageUrl = empModel.ImageUrl,
                Address = empModel.Address,
                DepartmentId = empModel.DepartmentId,
                DepartmentList = departments
            };
           
            return View("Edit", empWithDeptListViewModel);
        }

        [HttpGet]
        public IActionResult New()
        {
            ViewData["Departments"] = context.Department.ToList();
            return View("New");
        }
        [HttpPost]
        public IActionResult SaveNew(Employee empFromReq)
        {
            if (ModelState.IsValid)
            {
                try
                { 
                    context.Employee.Add(empFromReq);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex) 
                {
                
                    ModelState.AddModelError("DepartmentId", ex.Message);
                }
            }
     
            ViewData["Departments"] = context.Department.ToList();
            return View("New", empFromReq);
        }

        [HttpPost]
        public IActionResult SaveEdit(EmpWithDeptListViewModel empFromRequest)
        {
            Employee empModel = context.Employee.Include(e => e.Department).FirstOrDefault(e => e.Id == empFromRequest.Id);
            //if (ModelState.IsValid)
            //{

                empModel.Name = empFromRequest.Name;
                empModel.Salary = empFromRequest.Salary;
                empModel.JobTitle = empFromRequest.JobTitle;
                empModel.ImageUrl = empFromRequest.ImageUrl;
                empModel.Address = empFromRequest.Address;
                empModel.DepartmentId = empFromRequest.DepartmentId;
                context.SaveChanges();
                return RedirectToAction("Index");
            //}
               
            //empFromRequest.DepartmentList = context.Department.ToList();
            //return View("Edit", empFromRequest);
  
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var emp = context.Employee.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                context.Employee.Remove(emp);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
