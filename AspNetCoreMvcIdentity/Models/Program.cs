using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreMvcIdentity.Models
{
  public class Program
  {
    public int ProgramId { get; set; }
    public string ProgramAdi { get; set; }
    public virtual int BolumId { get; set; }
    public virtual Bolum Bolum { get; set; }
  }
}
