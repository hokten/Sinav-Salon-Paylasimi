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
    public class OgretimElemaniController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OgretimElemaniController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OgretimElemani
        public async Task<IActionResult> Index()
        {
            return View(await _context.OgretimElemani.ToListAsync());
        }

        // GET: OgretimElemani/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogretimElemani = await _context.OgretimElemani
                .FirstOrDefaultAsync(m => m.OgretimElemaniId == id);
            if (ogretimElemani == null)
            {
                return NotFound();
            }

            return View(ogretimElemani);
        }

        // GET: OgretimElemani/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OgretimElemani/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OgretimElemaniId,OgretimElemaniAdiSoyadi,OgretimElemaniKisaltmaa")] OgretimElemani ogretimElemani)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ogretimElemani);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ogretimElemani);
        }

        // GET: OgretimElemani/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogretimElemani = await _context.OgretimElemani.FindAsync(id);
            if (ogretimElemani == null)
            {
                return NotFound();
            }
            return View(ogretimElemani);
        }

        // POST: OgretimElemani/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OgretimElemaniId,OgretimElemaniAdiSoyadi,OgretimElemaniKisaltmaa")] OgretimElemani ogretimElemani)
        {
            if (id != ogretimElemani.OgretimElemaniId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ogretimElemani);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OgretimElemaniExists(ogretimElemani.OgretimElemaniId))
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
            return View(ogretimElemani);
        }

        // GET: OgretimElemani/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogretimElemani = await _context.OgretimElemani
                .FirstOrDefaultAsync(m => m.OgretimElemaniId == id);
            if (ogretimElemani == null)
            {
                return NotFound();
            }

            return View(ogretimElemani);
        }

        // POST: OgretimElemani/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ogretimElemani = await _context.OgretimElemani.FindAsync(id);
            _context.OgretimElemani.Remove(ogretimElemani);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OgretimElemaniExists(int id)
        {
            return _context.OgretimElemani.Any(e => e.OgretimElemaniId == id);
        }
    }
}
