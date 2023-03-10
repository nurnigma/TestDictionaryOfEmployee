using DictionaryOfEmployee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DictionaryOfEmployee.Controllers
{
    public class PositionController : Controller, ICrud<Position>
    {
        Context db;
        public PositionController(Context context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Positions.ToListAsync());
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
            if (db.Positions.Any(x => x.Name == name))
            {
                return Json(false);
            }
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Position position)
        {
            if (ModelState.IsValid)
            {
                db.Positions.Add(position);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Position position)
        {
            if (ModelState.IsValid)
            {
                db.Positions.Update(position);
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
                Position? position = await db.Positions.FirstOrDefaultAsync(p => p.Id == id);
                if (position != null)
                {
                    db.Positions.Remove(position);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {

                Position? position = await db.Positions.FirstOrDefaultAsync(p => p.Id == id);
                if (position != null) return View(position);
            }
            return NotFound();
        }
       
    }
}
