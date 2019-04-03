using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using AspNetCoreMvcIdentity.Models;

namespace AspNetCoreMvcIdentity.Models.ViewModels
{
  public class Cizelge
  {
    public virtual Dictionary<Oturum, Dictionary<Salon, Sinav>> OturumTekil { get; set; }
  }
}
