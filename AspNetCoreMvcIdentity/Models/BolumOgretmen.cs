using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvcIdentity.Models
{
  public class BolumOgretmen
  {
    public int BolumId { get; set; }
    public virtual Bolum Bolum { get; set; }
    public int OgretimElemaniId { get; set; }
    public virtual OgretimElemani OgretimElemani { get; set; }

  }
}
