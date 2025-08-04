using Microsoft.AspNetCore.Mvc;
using WebApplication3.DataContext;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Controllers
{
    [Route("deparment")]
    public class DepartmentController : Controller
    {

        private readonly NewGenWebSoftechDb db;

        public DepartmentController(NewGenWebSoftechDb dbContext)
        {
            this.db = dbContext;
        }

        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var deptList = await db.Department.ToListAsync();
            return View(deptList);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                var result = await db.Department.AddAsync(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }
        //Attribute routing
        [Route("edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id > 0)
            {
                var department = await db.Department.FirstOrDefaultAsync(d => d.DeptId == id);
                if (department != null)
                {
                    return View(department);
                }
            }
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Department.Update(department);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(department);
        }

       
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id > 0)
            {
                var department = await db.Department.FirstOrDefaultAsync(d => d.DeptId == id);

                if (department != null)
                {
                    return View(department);
                }
            }
            return NotFound();
        }

        [Route("delete")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                var department = await db.Department.FirstOrDefaultAsync(d => d.DeptId == id);

                if (department != null)
                {
                    return View(department);
                }
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteDept(int id)
        {
            if (id > 0)
            {
                var department = await db.Department.FirstOrDefaultAsync(d => d.DeptId == id);

                if (department != null)
                {
                    db.Department.Remove(department);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
