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
    public class ProgramlarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProgramlarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Programlar
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Program.Include(p => p.Bolum);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Programlar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.Program
                .Include(p => p.Bolum)
                .FirstOrDefaultAsync(m => m.ProgramId == id);
            if (program == null)
            {
                return NotFound();
            }

            return View(program);
        }

        // GET: Programlar/Create
        public IActionResult Create()
        {
            ViewData["BolumId"] = new SelectList(_context.Bolum, "BolumId", "BolumId");
            return View();
        }

        // POST: Programlar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProgramId,ProgramAdi,BolumId")] AspNetCoreMvcIdentity.Models.Program program)
        {
            if (ModelState.IsValid)
            {
                _context.Add(program);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BolumId"] = new SelectList(_context.Bolum, "BolumId", "BolumId", program.BolumId);
            return View(program);
        }

        // GET: Programlar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.Program.FindAsync(id);
            if (program == null)
            {
                return NotFound();
            }
            ViewData["BolumId"] = new SelectList(_context.Bolum, "BolumId", "BolumId", program.BolumId);
            return View(program);
        }

        // POST: Programlar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProgramId,ProgramAdi,BolumId")] AspNetCoreMvcIdentity.Models.Program  program)
        {
            if (id != program.ProgramId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(program);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramExists(program.ProgramId))
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
            ViewData["BolumId"] = new SelectList(_context.Bolum, "BolumId", "BolumId", program.BolumId);
            return View(program);
        }

        // GET: Programlar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.Program
                .Include(p => p.Bolum)
                .FirstOrDefaultAsync(m => m.ProgramId == id);
            if (program == null)
            {
                return NotFound();
            }

            return View(program);
        }

        // POST: Programlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var program = await _context.Program.FindAsync(id);
            _context.Program.Remove(program);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramExists(int id)
        {
            return _context.Program.Any(e => e.ProgramId == id);
        }
    }
}
