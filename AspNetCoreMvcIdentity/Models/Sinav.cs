using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreMvcIdentity.Models
{
  public class Sinav
  {
    public int SinavId { get; set; }
    public int OturumId { get; set; }
    public virtual Oturum Oturum { get; set; }
    public int SalonId { get; set; }
    public virtual Salon Salon { get; set; }
    public int DersId { get; set; }
    public virtual Ders Ders { get; set; }
    public int DersSorumlusuId { get; set;}
    [Display(Name="Dersin Sorumlu Öğretim Elemanı")]
    [ForeignKey("DersSorumlusuId")]
    public virtual OgretimElemani DersSorumlusu { get; set;}
    public int GozetmenId { get; set;}
    [Display(Name="Gözetmen Öğretim Elemanı")]
    [ForeignKey("GozetmenId")]
    public virtual OgretimElemani Gozetmen { get; set;}

  }
}
