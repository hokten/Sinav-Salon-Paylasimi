using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvcIdentity.Models
{
  public class Oturum {
    public int OturumId { get; set; }
    [Display(Name = "Oturum Tarih ve Saati")]
    [DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0: dd/MM/yyyy HH:mm:ss}", HtmlEncode = true, NullDisplayText = "-")]
    public virtual DateTime OturumTarihveSaati { get; set; }
    public virtual ICollection<Sinav> Sinav  { get; set; }
  }
}
