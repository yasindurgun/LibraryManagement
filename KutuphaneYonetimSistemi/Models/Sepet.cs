using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KutuphaneYonetimSistemi.Models
{
    public class Sepet
    {
        public static Sepet AktifSepet
        {
            get
            {
                HttpContext ctx = HttpContext.Current;
                if (ctx.Session["AktifSepet"] == null)
                    ctx.Session["AktifSepet"] = new Sepet(); 
                return (Sepet)ctx.Session["AktifSepet"]; 
            } 
        }

        private List<SepetItem> urunler = new List<SepetItem>(); 
        public List<SepetItem> Urunler
        {
            get { return urunler; }
            set { urunler = value; }
        }


        public void SepeteEkle(SepetItem si)
        {
            if (HttpContext.Current.Session["AktifSepet"] != null)
            {
                Sepet s = (Sepet)HttpContext.Current.Session["AktifSepet"];
                s.Urunler.Add(si);

            }
            else
            {
                Sepet s = new Sepet();
                s.Urunler.Add(si);

                HttpContext.Current.Session["AktifSepet"] = s;
            }

        }

        public decimal ToplamTutar
        {
            get
            {
                return Urunler.Sum(x => x.Tutar);
            }
        }
    }

    public class SepetItem
    {
        [Key]
        public int ID { get; set; }
        public string Kitap_Ad { get; set; }
        public string Yazar_Ad_Soyad { get; set; }
        public string Kategori_Ad { get; set; }
        public decimal Tutar { get; set; }

    }
}