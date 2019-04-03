using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvcIdentity.Data;
using AspNetCoreMvcIdentity.Models;
using AspNetCoreMvcIdentity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace AspNetCoreMvcIdentity.Controllers {
  public class OgretmenController : Controller {
    private readonly ApplicationDbContext _context;

    public OgretmenController (ApplicationDbContext context) {
      _context = context;
    }

    // GET: OgretimElemani/Details/5
    public async Task<IActionResult> Edit (int? id) {
      var ogretimElemani = await _context.OgretimElemani.Include (m => m.OgretimElemanininBolumleri).ThenInclude (m => m.Bolum)

        .FirstOrDefaultAsync (m => m.OgretimElemaniId == id);
      /*

         ogretimElemani.OgretimElemanininBolumleri.Clear();
         BolumOgretmen ddd = new BolumOgretmen {BolumId=2, OgretimElemaniId=1};
         ogretimElemani.OgretimElemanininBolumleri.Add(ddd);
         _context.SaveChanges();
         */

      var tumBolumler = await _context.Bolum.ToListAsync ();

      if (ogretimElemani == null) {
        return NotFound ();
      }
      Ogretmen ogretmen = new Ogretmen (ogretimElemani, tumBolumler);

      
      return View ("~/Views/Ogretmen/Edit.cshtml", ogretmen);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit (int id, [Bind ("OgretimElemaniId,OgretimElemaniAdiSoyadi,OgretimElemaniKisaltmaa,SecilenBolumler")] Ogretmen ogretimElemani) {
      if (id != ogretimElemani.OgretimElemaniId) {
        return NotFound ();
      }
      ogretimElemani.TumBolumler = await _context.Bolum.ToListAsync ();

      var bolumOgretmen = await _context.OgretimElemani
        .Include(b => b.OgretimElemanininBolumleri)
        .FirstOrDefaultAsync(m => m.OgretimElemaniId == id);
        Mapper.AddMap<Ogretmen, OgretimElemani>((from, resp) =>
        {
          var existing = resp as OgretimElemani;
          existing.InjectFrom(from);
          return existing;
        });
        Mapper.Map<OgretimElemani>(ogretimElemani, bolumOgretmen);
        _context.Update(bolumOgretmen);
                    await _context.SaveChangesAsync();
/*
      foreach (var bogr in bolumOgretmen.OgretimElemanininBolumleri.ToList ()) {
        bolumOgretmen.OgretimElemanininBolumleri.Remove (bogr);
      }

      foreach (var yeni in ogretimElemani.SecilenBolumler) {
        bolumOgretmen.OgretimElemanininBolumleri.Add (new BolumOgretmen { BolumId = yeni, OgretimElemaniId = ogretimElemani.OgretimElemaniId });
      }
      bolumOgretmen.OgretimElemaniAdiSoyadi = ogretimElemani.OgretimElemaniAdiSoyadi;
      bolumOgretmen.OgretimElemaniKisaltmaa = ogretimElemani.OgretimElemaniKisaltmaa;
      ogretimElemani.TumBolumler = await _context.Bolum.ToListAsync ();

      _context.Update (bolumOgretmen);
      await _context.SaveChangesAsync ();

      var personsump = ObjectDumper.Dump (ogretimElemani);
      Console.WriteLine (personsump);
      */

      return View (ogretimElemani);
    }

  }
}