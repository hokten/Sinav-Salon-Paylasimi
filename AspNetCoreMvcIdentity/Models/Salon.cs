using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvcIdentity.Models
{
  public class Salon
  {
    public virtual int SalonId { get; set; }
    public string SalonAdi { get; set; }
    public virtual ICollection<Oturum> Oturum { get; set; }
  }
}
