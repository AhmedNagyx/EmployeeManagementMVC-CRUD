using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using MVCProject.ViewModel;

namespace MVCProject.Controllers
{
    public class DepartmentController : Controller
    {
        ITIContext context = new ITIContext();
        public IActionResult Index()
        {
            List<Department> departments = context.Department.ToList();
            return View("Index",departments);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }
        [HttpPost]
        public IActionResult SaveAdd(Department depart)
        {
            if(depart.Name != null)
            {
                context.Department.Add(depart);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Add",depart);
        }

       

        public IActionResult Edit(int id)
        {
            Department department = context.Department.FirstOrDefault(d => d.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            return View("Edit", department);
        }
        [HttpPost]

        public IActionResult SaveEdit(Department departFromRequest)
        {
            var department = context.Department.FirstOrDefault(d => d.Id == departFromRequest.Id);
            if (ModelState.IsValid)
            {

                department.Name = departFromRequest.Name;
                department.ManagerName = departFromRequest.ManagerName;

                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Edit", department);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Department department = context.Department.FirstOrDefault(d => d.Id == id);

            if (department != null)
            {
                context.Department.Remove(department);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
