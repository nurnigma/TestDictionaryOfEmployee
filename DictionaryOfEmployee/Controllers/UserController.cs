using DictionaryOfEmployee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DictionaryOfEmployee.Controllers
{
    public class UserController : Controller, ICrud<User>
    {
        
            Context db;
            public UserController(Context context)
            {
                db = context;
            }
            public async Task<IActionResult> Index()
            {
                return View(await db.Users.ToListAsync());
            }
            public IActionResult Add()
            {
                return View();
            }
            public async Task<IActionResult> GetById(int id)
            {
                return RedirectToAction("Index");

            }

            [HttpPost]
            public async Task<IActionResult> Add(User user)
            {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);

            }

            [HttpPost]
            public async Task<IActionResult> Edit(User user)
            {
            if (ModelState.IsValid)
            {
                db.Users.Update(user);
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
                    User? user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                    if (user != null)
                    {
                        db.Users.Remove(user);
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

                    User? user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                    if (user != null) return View(user);
                }
                return NotFound();
            }
   
        }
    

}
