using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspNetCoreMvcIdentity.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreMvcIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<AspNetCoreMvcIdentity.Models.Bolum> Bolum { get; set; }
        public DbSet<AspNetCoreMvcIdentity.Models.Program> Program { get; set; }
        public DbSet<AspNetCoreMvcIdentity.Models.Oturum> Oturum { get; set; }
        public DbSet<AspNetCoreMvcIdentity.Models.Salon> Salon { get; set; }
        public DbSet<AspNetCoreMvcIdentity.Models.Ders> Ders { get; set; }
        public DbSet<AspNetCoreMvcIdentity.Models.Sinav> Sinav { get; set; }
        public DbSet<AspNetCoreMvcIdentity.Models.OgretimElemani> OgretimElemani { get; set; }
        public DbSet<AspNetCoreMvcIdentity.Models.BolumOgretmen> BolumOgretmen { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //
            builder.Entity<BolumOgretmen>()
              .HasKey(x => new {x.BolumId, x.OgretimElemaniId});

            builder.Entity<BolumOgretmen>()
              .HasOne(pt => pt.Bolum)
              .WithMany(p => p.BolumOgretimElemanlari)
              .HasForeignKey(pt => pt.BolumId);

            builder.Entity<BolumOgretmen>()
              .HasOne(pt => pt.OgretimElemani)
              .WithMany(t => t.OgretimElemanininBolumleri)
              .HasForeignKey(pt => pt.OgretimElemaniId);
        }


    }
}
