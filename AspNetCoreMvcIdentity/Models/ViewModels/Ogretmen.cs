using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreMvcIdentity.Models;

namespace AspNetCoreMvcIdentity.Models.ViewModels
{

  public class Ogretmen
  {
    public int OgretimElemaniId { get; set; }
    public string OgretimElemaniAdiSoyadi { get; set; }
    public string OgretimElemaniKisaltmaa { get; set; }
    public List<Bolum> TumBolumler { get; set; }
    private int[] secilenBolumler;
    public int[] SecilenBolumler {
      get {
        return this.secilenBolumler;
      }
      set {
        this.secilenBolumler = value;
        this.ogretimElemanininBolumleri = new List<BolumOgretmen>();
        foreach (var item in value) {
          BolumOgretmen ddd = new BolumOgretmen {BolumId=item, OgretimElemaniId=OgretimElemaniId};
          this.ogretimElemanininBolumleri.Add(ddd);
        }
      }
    }

    private ICollection<BolumOgretmen> ogretimElemanininBolumleri;
    public ICollection<BolumOgretmen> OgretimElemanininBolumleri  {
      get {
        return this.ogretimElemanininBolumleri;
      }
      set {
        this.ogretimElemanininBolumleri = value;
        this.secilenBolumler = this.OgretimElemanininBolumleri.Select (b => b.BolumId).ToArray ();
      }
    }

    
    public Ogretmen() {}


    public Ogretmen(OgretimElemani _ogretimElemani, List<Bolum> _bolumler) {
      OgretimElemaniId = _ogretimElemani.OgretimElemaniId;
      OgretimElemaniAdiSoyadi = _ogretimElemani.OgretimElemaniAdiSoyadi;
      OgretimElemaniKisaltmaa = _ogretimElemani.OgretimElemaniKisaltmaa;
      OgretimElemanininBolumleri = _ogretimElemani.OgretimElemanininBolumleri;
      TumBolumler = _bolumler;
    }
  }
}
