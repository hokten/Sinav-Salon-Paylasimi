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
    public class SinavlarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SinavlarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sinavlar
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sinav.Include(s => s.Ders).Include(s => s.DersSorumlusu).Include(s => s.Gozetmen).Include(s => s.Oturum).Include(s => s.Salon);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sinavlar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinav = await _context.Sinav
                .Include(s => s.Ders)
                .Include(s => s.DersSorumlusu)
                .Include(s => s.Gozetmen)
                .Include(s => s.Oturum)
                .Include(s => s.Salon)
                .FirstOrDefaultAsync(m => m.SinavId == id);
            if (sinav == null)
            {
                return NotFound();
            }

            return View(sinav);
        }

        // GET: Sinavlar/Create
        public IActionResult Create()
        {
            ViewData["DersId"] = new SelectList(_context.Ders, "DersId", "DersId");
            ViewData["DersSorumlusuId"] = new SelectList(_context.OgretimElemani, "OgretimElemaniId", "OgretimElemaniId");
            ViewData["GozetmenId"] = new SelectList(_context.OgretimElemani, "OgretimElemaniId", "OgretimElemaniId");
            ViewData["OturumId"] = new SelectList(_context.Oturum, "OturumId", "OturumId");
            ViewData["SalonId"] = new SelectList(_context.Salon, "SalonId", "SalonId");
            return View();
        }

        // POST: Sinavlar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SinavId,OturumId,SalonId,DersId,DersSorumlusuId,GozetmenId")] Sinav sinav)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sinav);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DersId"] = new SelectList(_context.Ders, "DersId", "DersId", sinav.DersId);
            ViewData["DersSorumlusuId"] = new SelectList(_context.OgretimElemani, "OgretimElemaniId", "OgretimElemaniId", sinav.DersSorumlusuId);
            ViewData["GozetmenId"] = new SelectList(_context.OgretimElemani, "OgretimElemaniId", "OgretimElemaniId", sinav.GozetmenId);
            ViewData["OturumId"] = new SelectList(_context.Oturum, "OturumId", "OturumId", sinav.OturumId);
            ViewData["SalonId"] = new SelectList(_context.Salon, "SalonId", "SalonId", sinav.SalonId);
            return View(sinav);
        }

        // GET: Sinavlar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinav = await _context.Sinav.FindAsync(id);
            if (sinav == null)
            {
                return NotFound();
            }
            ViewData["DersId"] = new SelectList(_context.Ders, "DersId", "DersId", sinav.DersId);
            ViewData["DersSorumlusuId"] = new SelectList(_context.OgretimElemani, "OgretimElemaniId", "OgretimElemaniId", sinav.DersSorumlusuId);
            ViewData["GozetmenId"] = new SelectList(_context.OgretimElemani, "OgretimElemaniId", "OgretimElemaniId", sinav.GozetmenId);
            ViewData["OturumId"] = new SelectList(_context.Oturum, "OturumId", "OturumId", sinav.OturumId);
            ViewData["SalonId"] = new SelectList(_context.Salon, "SalonId", "SalonId", sinav.SalonId);
            return View(sinav);
        }

        // POST: Sinavlar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SinavId,OturumId,SalonId,DersId,DersSorumlusuId,GozetmenId")] Sinav sinav)
        {
            if (id != sinav.SinavId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sinav);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinavExists(sinav.SinavId))
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
            ViewData["DersId"] = new SelectList(_context.Ders, "DersId", "DersId", sinav.DersId);
            ViewData["DersSorumlusuId"] = new SelectList(_context.OgretimElemani, "OgretimElemaniId", "OgretimElemaniId", sinav.DersSorumlusuId);
            ViewData["GozetmenId"] = new SelectList(_context.OgretimElemani, "OgretimElemaniId", "OgretimElemaniId", sinav.GozetmenId);
            ViewData["OturumId"] = new SelectList(_context.Oturum, "OturumId", "OturumId", sinav.OturumId);
            ViewData["SalonId"] = new SelectList(_context.Salon, "SalonId", "SalonId", sinav.SalonId);
            return View(sinav);
        }

        // GET: Sinavlar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinav = await _context.Sinav
                .Include(s => s.Ders)
                .Include(s => s.DersSorumlusu)
                .Include(s => s.Gozetmen)
                .Include(s => s.Oturum)
                .Include(s => s.Salon)
                .FirstOrDefaultAsync(m => m.SinavId == id);
            if (sinav == null)
            {
                return NotFound();
            }

            return View(sinav);
        }

        // POST: Sinavlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sinav = await _context.Sinav.FindAsync(id);
            _context.Sinav.Remove(sinav);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SinavExists(int id)
        {
            return _context.Sinav.Any(e => e.SinavId == id);
        }
    }
}
