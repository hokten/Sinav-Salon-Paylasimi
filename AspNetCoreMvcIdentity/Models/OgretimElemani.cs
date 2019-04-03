using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvcIdentity.Models
{
  [DisplayColumn("Adisoyadi")]
  public class OgretimElemani
  {
    public int OgretimElemaniId { get; set; }
    public string OgretimElemaniAdiSoyadi { get; set; }
    public string OgretimElemaniKisaltmaa { get; set; }
    public virtual ICollection<BolumOgretmen> OgretimElemanininBolumleri  { get; set; }
  }
}
