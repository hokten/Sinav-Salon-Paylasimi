using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreMvcIdentity.Models
{
  [DisplayColumn("BolumAdi")]
  public class Bolum
  {
    public int BolumId { get; set; }
    public string BolumAdi { get; set; }
    public virtual ICollection<Program> Programlar { get; set; }
    public virtual ICollection<BolumOgretmen> BolumOgretimElemanlari { get; set; }
  }
}
