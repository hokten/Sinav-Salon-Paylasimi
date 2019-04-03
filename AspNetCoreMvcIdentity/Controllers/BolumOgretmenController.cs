using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreMvcIdentity.Data;
using AspNetCoreMvcIdentity.Models;

namespace AspNetCoreMvcIdentity.Controllers
{
    public class BolumOgretmenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BolumOgretmenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BolumOgretmen
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BolumOgretmen.Include(b => b.Bolum).Include(b => b.OgretimElemani);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BolumOgretmen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolumOgretmen = await _context.BolumOgretmen
                .Include(b => b.Bolum)
                .Include(b => b.OgretimElemani)
                .FirstOrDefaultAsync(m => m.BolumId == id);
            if (bolumOgretmen == null)
            {
                return NotFound();
            }

            return View(bolumOgretmen);
        }

        // GET: BolumOgretmen/Create
        public IActionResult Create()
        {
            ViewData["BolumId"] = new SelectList(_context.Bolum, "BolumId", "BolumId");
            ViewData["OgretimElemaniId"] = new SelectList(_context.OgretimElemani, "OgretimElemaniId", "OgretimElemaniId");
            return View();
        }

        // POST: BolumOgretmen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BolumId,OgretimElemaniId")] BolumOgretmen bolumOgretmen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bolumOgretmen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BolumId"] = new SelectList(_context.Bolum, "BolumId", "BolumId", bolumOgretmen.BolumId);
            ViewData["OgretimElemaniId"] = new SelectList(_context.OgretimElemani, "OgretimElemaniId", "OgretimElemaniId", bolumOgretmen.OgretimElemaniId);
            return View(bolumOgretmen);
        }

        // GET: BolumOgretmen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolumOgretmen = await _context.BolumOgretmen.FindAsync(id);
            if (bolumOgretmen == null)
            {
                return NotFound();
            }
            ViewData["BolumId"] = new SelectList(_context.Bolum, "BolumId", "BolumId", bolumOgretmen.BolumId);
            ViewData["OgretimElemaniId"] = new SelectList(_context.OgretimElemani, "OgretimElemaniId", "OgretimElemaniId", bolumOgretmen.OgretimElemaniId);
            return View(bolumOgretmen);
        }

        // POST: BolumOgretmen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BolumId,OgretimElemaniId")] BolumOgretmen bolumOgretmen)
        {
            if (id != bolumOgretmen.BolumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bolumOgretmen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BolumOgretmenExists(bolumOgretmen.BolumId))
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
            ViewData["BolumId"] = new SelectList(_context.Bolum, "BolumId", "BolumId", bolumOgretmen.BolumId);
            ViewData["OgretimElemaniId"] = new SelectList(_context.OgretimElemani, "OgretimElemaniId", "OgretimElemaniId", bolumOgretmen.OgretimElemaniId);
            return View(bolumOgretmen);
        }

        // GET: BolumOgretmen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolumOgretmen = await _context.BolumOgretmen
                .Include(b => b.Bolum)
                .Include(b => b.OgretimElemani)
                .FirstOrDefaultAsync(m => m.BolumId == id);
            if (bolumOgretmen == null)
            {
                return NotFound();
            }

            return View(bolumOgretmen);
        }

        // POST: BolumOgretmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bolumOgretmen = await _context.BolumOgretmen.FindAsync(id);
            _context.BolumOgretmen.Remove(bolumOgretmen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BolumOgretmenExists(int id)
        {
            return _context.BolumOgretmen.Any(e => e.BolumId == id);
        }
    }
}
