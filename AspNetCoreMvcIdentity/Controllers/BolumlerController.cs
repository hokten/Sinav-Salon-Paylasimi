using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreMvcIdentity.Data;
using AspNetCoreMvcIdentity.Models;
using Newtonsoft.Json;  
using Newtonsoft.Json.Linq;
namespace AspNetCoreMvcIdentity.Controllers
{
    public class BolumlerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BolumlerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bolumler
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bolum.ToListAsync());
        }

        // GET: Bolumler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolum = await _context.Bolum.Include(m => m.BolumOgretimElemanlari).ThenInclude(s => s.OgretimElemani)
                .FirstOrDefaultAsync(m => m.BolumId == id);
          var personsDump = ObjectDumper.Dump(bolum);
                    Console.WriteLine(personsDump);
                    string json = JsonConvert.SerializeObject(personsDump);
                        string jsonFormatted = JValue.Parse(json).ToString(Formatting.Indented);
                            Console.WriteLine(jsonFormatted);





            if (bolum == null)
            {
                return NotFound();
            }

            return View(bolum);
        }

        // GET: Bolumler/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bolumler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BolumId,BolumAdi")] Bolum bolum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bolum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bolum);
        }

        // GET: Bolumler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolum = await _context.Bolum.FindAsync(id);
            if (bolum == null)
            {
                return NotFound();
            }
            return View(bolum);
        }

        // POST: Bolumler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BolumId,BolumAdi")] Bolum bolum)
        {
            if (id != bolum.BolumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bolum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BolumExists(bolum.BolumId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bolum);
        }

        // GET: Bolumler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolum = await _context.Bolum
                .FirstOrDefaultAsync(m => m.BolumId == id);
            if (bolum == null)
            {
                return NotFound();
            }

            return View(bolum);
        }

        // POST: Bolumler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bolum = await _context.Bolum.FindAsync(id);
            _context.Bolum.Remove(bolum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BolumExists(int id)
        {
            return _context.Bolum.Any(e => e.BolumId == id);
        }
    }
}
