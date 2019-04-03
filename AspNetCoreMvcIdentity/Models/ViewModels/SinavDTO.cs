using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using AspNetCoreMvcIdentity.Models;

namespace AspNetCoreMvcIdentity.Models.ViewModels
{
  public class SinavDTO
  {
    public int SinavId { get; set; }
    public int OturumId { get; set; }
    public Oturum Oturum { get; set; }
    public int SalonId { get; set; }
    public Salon Salon { get; set; }
    public int DersId { get; set; }
    public Ders Ders { get; set; }
    public int DersSorumlusuId { get; set;}
    public OgretimElemani DersSorumlusu { get; set;}
    public int GozetmenId { get; set;}
    public OgretimElemani Gozetmen { get; set;}
  }
}
