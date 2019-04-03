using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;  

namespace AspNetCoreMvcIdentity.Models
{
  public class Ders
  {
    public virtual int DersId { get; set; }
    public string DersKodu { get; set; }
    public string DersAdi { get; set; }
    public virtual int ProgramId { get; set; }
    public virtual int? ParentDersId { get; set; }
    [ForeignKey("ParentDersId")]
    public virtual Ders Parent { get; set; }
    public virtual Program Program { get; set; }
    public virtual ICollection<Ders> Children { get; set; }
  }
}
