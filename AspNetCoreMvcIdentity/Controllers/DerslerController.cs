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
    public class DerslerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DerslerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dersler
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ders.Include(d => d.Parent).Include(d => d.Program);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Dersler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ders = await _context.Ders
                .Include(d => d.Parent)
                .Include(d => d.Program)
                .FirstOrDefaultAsync(m => m.DersId == id);
            if (ders == null)
            {
                return NotFound();
            }

            return View(ders);
        }

        // GET: Dersler/Create
        public IActionResult Create()
        {
            ViewData["ParentDersId"] = new SelectList(_context.Ders, "DersId", "DersId");
            ViewData["ProgramId"] = new SelectList(_context.Program, "ProgramId", "ProgramId");
            return View();
        }

        // POST: Dersler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DersId,DersKodu,DersAdi,ProgramId,ParentDersId")] Ders ders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentDersId"] = new SelectList(_context.Ders, "DersId", "DersId", ders.ParentDersId);
            ViewData["ProgramId"] = new SelectList(_context.Program, "ProgramId", "ProgramId", ders.ProgramId);
            return View(ders);
        }

        // GET: Dersler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ders = await _context.Ders.FindAsync(id);
            if (ders == null)
            {
                return NotFound();
            }
            ViewData["ParentDersId"] = new SelectList(_context.Ders, "DersId", "DersId", ders.ParentDersId);
            ViewData["ProgramId"] = new SelectList(_context.Program, "ProgramId", "ProgramId", ders.ProgramId);
            return View(ders);
        }

        // POST: Dersler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DersId,DersKodu,DersAdi,ProgramId,ParentDersId")] Ders ders)
        {
            if (id != ders.DersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DersExists(ders.DersId))
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
            ViewData["ParentDersId"] = new SelectList(_context.Ders, "DersId", "DersId", ders.ParentDersId);
            ViewData["ProgramId"] = new SelectList(_context.Program, "ProgramId", "ProgramId", ders.ProgramId);
            return View(ders);
        }

        // GET: Dersler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ders = await _context.Ders
                .Include(d => d.Parent)
                .Include(d => d.Program)
                .FirstOrDefaultAsync(m => m.DersId == id);
            if (ders == null)
            {
                return NotFound();
            }

            return View(ders);
        }

        // POST: Dersler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ders = await _context.Ders.FindAsync(id);
            _context.Ders.Remove(ders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DersExists(int id)
        {
            return _context.Ders.Any(e => e.DersId == id);
        }
    }
}
