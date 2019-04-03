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
    public class OturumlarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OturumlarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Oturumlar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Oturum.ToListAsync());
        }

        // GET: Oturumlar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oturum = await _context.Oturum
                .FirstOrDefaultAsync(m => m.OturumId == id);
            if (oturum == null)
            {
                return NotFound();
            }

            return View(oturum);
        }

        // GET: Oturumlar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Oturumlar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OturumId,OturumTarihveSaati")] Oturum oturum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oturum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(oturum);
        }

        // GET: Oturumlar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oturum = await _context.Oturum.FindAsync(id);
            if (oturum == null)
            {
                return NotFound();
            }
            return View(oturum);
        }

        // POST: Oturumlar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OturumId,OturumTarihveSaati")] Oturum oturum)
        {
            if (id != oturum.OturumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oturum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OturumExists(oturum.OturumId))
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
            return View(oturum);
        }

        // GET: Oturumlar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oturum = await _context.Oturum
                .FirstOrDefaultAsync(m => m.OturumId == id);
            if (oturum == null)
            {
                return NotFound();
            }

            return View(oturum);
        }

        // POST: Oturumlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oturum = await _context.Oturum.FindAsync(id);
            _context.Oturum.Remove(oturum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OturumExists(int id)
        {
            return _context.Oturum.Any(e => e.OturumId == id);
        }
    }
}
