using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AspNetCoreMvcIdentity.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AspNetCoreMvcIdentity.Models;
using AspNetCoreMvcIdentity.Models.ManageViewModels;
using AspNetCoreMvcIdentity.Models.ViewModels;
using AspNetCoreMvcIdentity.Services;
using AspNetCoreMvcIdentity.Data;
using Omu.ValueInjecter;


namespace AspNetCoreMvcIdentity.Controllers
{
  [Authorize]
//  [Route("[controller]/[action]")]
  public class Cizelge1Controller
   : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailSender _emailSender;
    private readonly ILogger _logger;
    private readonly UrlEncoder _urlEncoder;
    private readonly ApplicationDbContext _context;

    public Cizelge1Controller(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailSender emailSender,
        ILogger<ManageController> logger,
        UrlEncoder urlEncoder,
        ApplicationDbContext context)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _emailSender = emailSender;
      _logger = logger;
      _urlEncoder = urlEncoder;
      _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> SinavEkle(Sinav sinav)
    {
      if (!ModelState.IsValid)
      {
        // TODO : Uyari mesajları HTML Helper ile verilecek.
        TempData["UyariMesaji"] = "<div class=\"alert alert-danger\" role=\"alert\">Oturum zamanı veya salon bilgisi uygunsuz.</div>";
        return RedirectToAction(nameof(Index));
      }
      if(!_context.Oturum.Any(m => m.OturumId == sinav.OturumId) || !_context.Salon.Any(m => m.SalonId == sinav.SalonId)) {
        TempData["UyariMesaji"] = "<div class=\"alert alert-danger\" role=\"alert\">Oturum zamanı veya salon bulunamadı.</div>";
        return RedirectToAction(nameof(Index));
      }
      if (_context.Sinav.Any(o => o.OturumId == sinav.OturumId && o.SalonId == sinav.SalonId)) {
         TempData["UyariMesaji"] = "<div class=\"alert alert-danger\" role=\"alert\">Belirtilen oturum zamanı için ilgili salonda zaten sınav var.</div>";
        return RedirectToAction(nameof(Index));
      }


      ViewData["DersId"] = new SelectList(_context.Ders, "DersId", "DersId");
      ViewData["DersSorumlusuId"] = new SelectList(_context.OgretimElemani, "OgretimElemaniId", "OgretimElemaniId");
      ViewData["GozetmenId"] = new SelectList(_context.OgretimElemani, "OgretimElemaniId", "OgretimElemaniId");
      ViewData["OturumId"] = new SelectList(_context.Oturum.Select( m => new {OturumId = m.OturumId, OturumZamani = m.OturumTarihveSaati.ToString("dd MMMM yyyy dddd HH:mm", new CultureInfo("tr-TR"))}).ToList(), "OturumId", "OturumZamani", sinav.OturumId);
      ViewData["SalonId"] = new SelectList(_context.Salon, "SalonId", "SalonAdi", sinav.SalonId);
      
      return View("~/Views/Cizelge/Create.cshtml", sinav);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      ViewBag.UyariMesaji = TempData["UyariMesaji"] ?? "";
      var user = await _userManager.Users.Include(x => x.Bolum).SingleAsync(x => x.Id == Convert.ToInt32(_userManager.GetUserId(User)));
      if (user == null)
      {
        throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
      }
      ICollection<SinavDTO> tumsinavlarDTO = new List<SinavDTO>();
      
      

      IEnumerable<Oturum> TumOturumlarSirali = _context.Oturum.OrderBy(c => c.OturumTarihveSaati);
      IEnumerable<Salon> TumSalonlarSirali  = _context.Salon.OrderBy(c => c.SalonAdi);
      IEnumerable<Sinav> TumSinavlar = _context.Sinav
      .Include(m => m.Oturum)
      .Include(m => m.DersSorumlusu)
      .Include(m => m.Gozetmen)
      .Include(m => m.Ders)
      .Include(x => x.Salon).Include(s => s.Ders.Program).Include(s => s.Ders.Program.Bolum);


      foreach(Oturum oturum in TumOturumlarSirali) {
        foreach(Salon salon in TumSalonlarSirali) {
          if(TumSinavlar.Where(s => s.OturumId == oturum.OturumId).Where(s => s.SalonId == salon.SalonId).Any()) {
            Sinav HucreyeAitSinav = TumSinavlar.Where(s => s.OturumId == oturum.OturumId).Where(s => s.SalonId == salon.SalonId).Single();
            SinavDTO temp_sinavDTO = Mapper.Map<SinavDTO>(HucreyeAitSinav); 
            tumsinavlarDTO.Add(temp_sinavDTO);

          }
          else {
            SinavDTO temp_sinavDTO = new SinavDTO();
            temp_sinavDTO.OturumId = oturum.OturumId;
            temp_sinavDTO.Oturum = oturum;
            temp_sinavDTO.SalonId = salon.SalonId;
            temp_sinavDTO.Salon= salon;
            tumsinavlarDTO.Add(temp_sinavDTO);

          }
        }
      }


      IEnumerable<IGrouping<DateTime, SinavDTO>> ff = tumsinavlarDTO
      .OrderBy(m => m.Oturum.OturumTarihveSaati)
      .OrderBy(x => x.Salon.SalonAdi)
      .GroupBy(m => m.Oturum.OturumTarihveSaati);

      var gg = ff.GroupBy(x => x.Key.Date);

      var cc = tumsinavlarDTO
      .OrderBy(m => m.Oturum.OturumTarihveSaati)
      .GroupBy(
        m => m.Oturum.OturumTarihveSaati,
        m => m,
        (kriter, nesne) => new {
          Key = kriter,
          Nesne = nesne.OrderBy(x => x.Salon.SalonAdi)
        }
        ).ToDictionary(g => g.Key, g => g.Nesne);
   
      return View("~/Views/Cizelge/Deneme.cshtml", gg);
    }








    [HttpPost]
    [ValidateAntiForgeryToken]

      public async Task<IActionResult> Index([Bind("SinavId,OturumId,SalonId,DersId")] Sinav sinav)
      {
        var user = await _userManager.Users.Include(x => x.Bolum).SingleAsync(x => x.Id == Convert.ToInt32(_userManager.GetUserId(User)));
        if (user == null)
        {
          throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }
        _context.Sinav.Remove(sinav);
        TempData["UyariMesaji"] = "<div class=\"alert alert-success\" role=\"alert\">Sınav silindi</div>";
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

        /*
           return await Task.Run<IActionResult>(() =>
           {
            if (true)
            {
            return RedirectToAction("Index");
            }
            else
            {
            return NotFound();
            }
            });
            */

      }
  }
}
