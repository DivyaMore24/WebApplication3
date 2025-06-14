using Microsoft.AspNetCore.Mvc;
using WebApplication3.DataContext;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication3.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly NewGenWebSoftechDb db;

        public EmployeeController(NewGenWebSoftechDb dbContext)
        {
            this.db = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var empList = await db.Employee
                                  .Include(e => e.Department) // 🔥 Load related department
                                  .ToListAsync();
            return View(empList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.DeptId = new SelectList(db.Department, "DeptId", "DeptName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Re-populate dropdown if form submission fails
            ViewBag.DeptId = new SelectList(db.Department, "DeptId", "DeptName", employee.DeptId);
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id > 0)
            {
                var employee = await db.Employee.FirstOrDefaultAsync(d => d.EmpId == id);
                if (employee != null)
                {
                    // ✅ Populate dropdown before returning view
                    ViewBag.DeptId = new SelectList(db.Department, "DeptId", "DeptName", employee.DeptId);
                    return View(employee);
                } 
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employee.Update(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DeptId = new SelectList(db.Department, "DeptId", "DeptName", employee.DeptId);
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id > 0)
            {
                var employee = await db.Employee.FirstOrDefaultAsync(d => d.EmpId == id);

                if (employee != null)
                {
                    return View(employee);
                }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                var employee = await db.Employee.FirstOrDefaultAsync(d => d.EmpId == id);

                if (employee != null)
                {
                    return View(employee);
                }
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteEmp(int id)
        {
            if (id > 0)
            {
                var employee = await db.Employee.FirstOrDefaultAsync(d => d.EmpId == id);

                if (employee != null)
                {
                    db.Employee.Remove(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
