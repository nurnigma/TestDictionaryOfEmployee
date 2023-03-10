using DictionaryOfEmployee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DictionaryOfEmployee.Controllers
{
    public class OrganizationController : Controller, ICrud<Organization>
    {
        Context db;
        public OrganizationController(Context context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Organizations.ToListAsync());
        }
        public IActionResult Add()
        {
            return View();
        }
        public async Task<IActionResult> GetById(int id)
        {
            return RedirectToAction("Index");

        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckName(string name)
        {
            if (db.Organizations.Any(x => x.Name == name))
            {
                return Json(false);
            }
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Organization organization)
        {
            if (ModelState.IsValid)
            {
                db.Organizations.Add(organization);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Organization organization)
        {
            if (ModelState.IsValid)
            {
                db.Organizations.Update(organization);
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
                Organization organization = await db.Organizations.FirstOrDefaultAsync(p => p.Id == id);
                if (organization != null)
                {
                    db.Organizations.Remove(organization);
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

                Organization organization = await db.Organizations.FirstOrDefaultAsync(p => p.Id == id);
                if (organization != null) return View(organization);
            }
            return NotFound();
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
