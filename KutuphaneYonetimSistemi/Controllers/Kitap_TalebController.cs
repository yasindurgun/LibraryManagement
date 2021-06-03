using KutuphaneYonetimSistemi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KutuphaneYonetimSistemi.Controllers
{
    public class RequestBookController : Controller
    {
        // GET: Kitap_Taleb 
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var deger = db.TBL_Kitap_Talep.ToList();
            return View(deger);
        }
        [HttpGet]
        public ActionResult Talep_Tarih_Kısıtla(string Tarih2, string Tarih3, DateTime? enddate, int? Secenek)
        {

            DateTime Tarih = Convert.ToDateTime(Tarih2);
            ViewBag.Datetime = DateTime.UtcNow;
            ViewBag.startdate = Convert.ToDateTime(Tarih);
            if (Tarih3 == null)
            {
                enddate = DateTime.Now;
            }
            else
            {
                enddate = Convert.ToDateTime(Tarih3);
            }
            ViewBag.enddate = DateTime.Now;
            IQueryable<TBL_Kitap_Talep> trips = db.TBL_Kitap_Talep;
            if (Tarih != null)
            {
                trips = trips.Where(x => x.İstenilme_Tarihi > Tarih);
            }
            if (enddate.HasValue)
            {
                trips = trips.Where(x => x.İstenilme_Tarihi < enddate.Value);
            }
            // At this point the query has generated a SQL statement based on the conditions above,
            // but it will not be executed until the until the next line - i.e. when calling .ToList()
            if (Secenek == 1)
            {
                return View(trips.Where(x => x.Temin_Edildi == true).ToList());
            }
            else if (Secenek == 2)
            {
                return View(trips.ToList());
            }
            else if (Secenek == 0)
            {
                return View(trips.Where(x => x.Bekleniyor == false).ToList());
            }
            else
            {
                return View(trips.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TBL_Kitap_Talep cc)
        {
            var deger = db.TBL_Kitap_Talep.FirstOrDefault(x => x.Kitap_Ad == cc.Kitap_Ad || x.Kategori_Ad == cc.Kategori_Ad || x.Yazar_Ad == cc.Yazar_Ad);
            if (deger == null)
            {
                cc.İstenilme_Tarihi = DateTime.Now;
                cc.Bekleniyor = true;
                cc.Temin_Edildi = false;
                db.TBL_Kitap_Talep.Add(cc);
                db.SaveChanges();
            }
            else
            {
                ViewBag.X = "It's not appropriate for record.Already exist this book";
                return View(cc);
            }
            ViewBag.X = "It is requested. Success...";
            return View(cc);
        }
        public ActionResult Temin_Et(int? id)
        {
            var deger = db.TBL_Kitap_Talep.FirstOrDefault(x => x.ID == id);
            deger.Temin_Edildi = true;
            deger.Bekleniyor = false;
            db.SaveChanges();
            var deger2 = db.TBLKATEGORI.FirstOrDefault(x => x.AD == deger.Kategori_Ad);
            if (deger2 == null)
            {
                TBLKATEGORI cc = new TBLKATEGORI();
                cc.AD = deger.Kategori_Ad;
                db.TBLKATEGORI.Add(cc);
                db.SaveChanges();

            }
            var deger22 = db.TBLKATEGORI.FirstOrDefault(x => x.AD == deger.Kategori_Ad);
            string[] a = deger.Yazar_Ad.Split();
            if (a.Length>=2)
            {
                string Ad = a[0];
                string Soyad = a[1];
                var deger3 = db.TBLYAZAR.FirstOrDefault(x => x.AD == Ad || x.SOYAD == Soyad);
                if (deger3 == null)
                {
                    TBLYAZAR dd = new TBLYAZAR();
                    dd.AD = Ad;
                    dd.SOYAD = Soyad;
                    dd.DETAY = "Kitap Talepden Gelmiştir";
                    db.TBLYAZAR.Add(dd);
                    db.SaveChanges();
                }
                var deger33 = db.TBLYAZAR.FirstOrDefault(x => x.AD == Ad || x.SOYAD == Soyad);
                TBLKITAP ff = new TBLKITAP();
                ff.AD = deger.Kitap_Ad;
                ff.TBLKATEGORI = deger22;
                ff.TBLYAZAR = deger33;
                // Tablo bağlı ise ff.Yazaar=deger33; değilse aşşağısı
                ////ff.YAZAR = deger33.ID;
                //ff.KATEGORI = deger22.ID;
                db.TBLKITAP.Add(ff);
                db.SaveChanges();
            }
            else
            {
                string Ad = a[0];
                var deger3 = db.TBLYAZAR.FirstOrDefault(x => x.AD == Ad);
                if (deger3 == null)
                {
                    TBLYAZAR dd = new TBLYAZAR();
                    dd.AD = Ad;
                    dd.DETAY = "It came from the request table";
                    db.TBLYAZAR.Add(dd);
                    db.SaveChanges();
                }
                var deger33 = db.TBLYAZAR.FirstOrDefault(x => x.AD == Ad);
                TBLKITAP ff = new TBLKITAP();
                ff.AD = deger.Kitap_Ad;
                ff.TBLKATEGORI = deger22;
                ff.TBLYAZAR = deger33;
                // Tablo bağlı ise ff.Yazaar=deger33; değilse aşşağısı
                ////ff.YAZAR = deger33.ID;
                //ff.KATEGORI = deger22.ID;
                db.TBLKITAP.Add(ff);
                db.SaveChanges();
            }
           
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            var deger = db.TBL_Kitap_Talep.Find(id);
            if (deger != null)
            {
                db.TBL_Kitap_Talep.Remove(deger);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Y = "It's not appropriate for record. There isn't this book.";
            }
            return RedirectToAction("Index");
        }
    }
}