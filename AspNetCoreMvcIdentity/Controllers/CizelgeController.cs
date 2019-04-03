using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AspNetCoreMvcIdentity.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AspNetCoreMvcIdentity.Models;
using AspNetCoreMvcIdentity.Models.ManageViewModels;
using AspNetCoreMvcIdentity.Models.ViewModels;
using AspNetCoreMvcIdentity.Services;
using AspNetCoreMvcIdentity.Data;

namespace AspNetCoreMvcIdentity.Controllers
{
  [Authorize]
//  [Route("[controller]/[action]")]
  public class CizelgeController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailSender _emailSender;
    private readonly ILogger _logger;
    private readonly UrlEncoder _urlEncoder;
    private readonly ApplicationDbContext _context;

    public CizelgeController(
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
    public async Task<IActionResult> Index()
    {
      ViewBag.UyariMesaji = TempData["UyariMesaji"] ?? "";
      var user = await _userManager.Users.Include(x => x.Bolum).SingleAsync(x => x.Id == Convert.ToInt32(_userManager.GetUserId(User)));
      if (user == null)
      {
        throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
      }
       Cizelge Cizelge =  new Cizelge();
      Cizelge.OturumTekil = new Dictionary<Oturum, Dictionary<Salon, Sinav>>();


      IEnumerable<Oturum> TumOturumlarSirali = _context.Oturum.OrderBy(c => c.OturumTarihveSaati);
      IEnumerable<Salon> TumSalonlarSirali  = _context.Salon.OrderBy(c => c.SalonAdi);
      IEnumerable<Sinav> TumSinavlar = _context.Sinav.Include(m => m.Ders).Include(x => x.Salon).Include(s => s.Ders.Program).Include(s => s.Ders.Program.Bolum);


      foreach(Oturum oturum in TumOturumlarSirali) {
        Dictionary<Salon, Sinav> OturumSatiri = new Dictionary<Salon, Sinav>();
        foreach(Salon salon in TumSalonlarSirali) {
          if(TumSinavlar.Where(s => s.OturumId == oturum.OturumId).Where(s => s.SalonId == salon.SalonId).Any()) {
            Sinav HucreyeAitSinav = TumSinavlar.Where(s => s.OturumId == oturum.OturumId).Where(s => s.SalonId == salon.SalonId).Single();
            OturumSatiri.Add(salon, HucreyeAitSinav);
          }
          else {
            OturumSatiri.Add(salon, null);
          }
        }
        Cizelge.OturumTekil.Add(oturum, OturumSatiri);
      }
                var personsDump = ObjectDumper.Dump(Cizelge);
                    Console.WriteLine(personsDump);



      return View("~/Views/Cizelge/Index.cshtml", Cizelge);
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
