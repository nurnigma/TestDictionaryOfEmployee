using DictionaryOfEmployee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DictionaryOfEmployee.Controllers
{
    public class DepartmentController : Controller, ICrud<Department>
    {
        Context db;
        public DepartmentController(Context context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            var dep = db.Departments.Include(o => o.Organization).ToList();
            return View(dep);

        }

        public IActionResult Add()
        {
            
                ViewBag.Organization = new SelectList(db.Organizations, "Id", "Name"); // ресурс для выпадающего списка
                return View();
            
        }
        public async Task<IActionResult> GetById(int id)
        {
            return RedirectToAction("Index");

        } 

        [HttpPost]
        public async Task<IActionResult> Add(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ModelState.Values.SelectMany(v=>v.Errors));
        }
 
        [HttpPost]
        public async Task<IActionResult> Edit(Department department)
        {
            if (ModelState.IsValid)
            {

                db.Departments.Update(department);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            if (id != null)
            {
                Department? department = await db.Departments.FirstOrDefaultAsync(p => p.Id == id);
                if (department != null)
                {
                    db.Departments.Remove(department);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id != null)
            {
                ViewBag.Organization = new SelectList(db.Organizations, "Id", "Name");
                Department? department = await db.Departments.FirstOrDefaultAsync(p => p.Id == id);
                if (department != null) return View(department);
            }
            return NotFound();
        }
        
    }
}
