using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreMvcIdentity.Models
{
  // Add profile data for application users by adding properties to the ApplicationUser class
  public class ApplicationUser : IdentityUser<long>
  {
    public virtual int BolumId { get; set;}
    public virtual Bolum Bolum { get; set;}
  }
}
