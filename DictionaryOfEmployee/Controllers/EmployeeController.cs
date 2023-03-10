using DictionaryOfEmployee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DictionaryOfEmployee.Controllers
{
    public class EmployeeController : Controller, ICrud<Employee>
    {
        Context db;
        public EmployeeController(Context context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            var dep = db.Employees.Include(o => o.Department).Include(o => o.Position).Include(o => o.User).ToList();


            return View(dep);

        }
        [HttpPost]
        public async Task<IActionResult> Index(string value)
        {

            if (!string.IsNullOrEmpty(value))
            {
                value = value.Trim();
                var res = db.Employees
                    .Include(o => o.Department)
                    .Include(o => o.Position)
                    .Include(o => o.User)
                    .Where(o => 
                        o.User.LastName.Contains(value) ||
                        o.User.FirstName.Contains(value) || 
                        o.User.Patronymic.Contains(value) || 
                        o.Email.Contains(value) || 
                        o.Telephone.Contains(value) ||
                        o.Position.Name.Contains(value) ||
                        o.Department.Name.Contains(value)
                        )
                     .ToList();
                return View(res);
            }
            return RedirectToAction("Index");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckEmail(string email)
        {
            if (db.Employees.Any(x => x.Email == email))
            {
                return Json(false);
            }
            return Json(true);
        }
        public void SelectList()
        {
            ViewBag.Department = new SelectList(db.Departments, "Id", "Name"); // ресурс для выпадающего списка
            ViewBag.Position = new SelectList(db.Positions, "Id", "Name"); 
            var users = db.Users
                .Where(w => !db.Employees.Any(c => c.UserId == w.Id))
                .Select(s => new { s.Id, FIO = s.FirstName + " " + s.LastName + " " + s.Patronymic });
            ViewBag.User = new SelectList(users, "Id", "FIO"); 
        }
        public IActionResult Add()
        {
            SelectList();
            return View();
        }
        public async Task<IActionResult> GetById(int id)
        {
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Add(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Update(employee);
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
                Employee? employee = await db.Employees.FirstOrDefaultAsync(p => p.Id == id);
                if (employee != null)
                {
                    db.Employees.Remove(employee);
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
                SelectList();
                Employee? employee = await db.Employees.FirstOrDefaultAsync(p => p.Id == id);
                if (employee != null) 
                    return View(employee);
            }
            return NotFound();
        }
    }
}
