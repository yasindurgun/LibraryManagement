using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneYonetimSistemi.Models.Entity;
namespace KutuphaneYonetimSistemi.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index(string p)
        {
            var kitaplar = from k in db.TBLKITAP select k;
            if(!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(m => m.AD.Contains(p));
            }
            //var kitaplar = db.TBLKITAP.ToList();
            return View(kitaplar.ToList());
        }

        public ActionResult yazarara(string x)
        {
            var kitaplar = from k in db.TBLKITAP select k;
            if (!string.IsNullOrEmpty(x))
            {
                kitaplar = kitaplar.Where(m => m.TBLYAZAR.AD.Contains(x));

            }

            //var kitaplar = db.TBLKITAP.ToList();
            return View("Index", kitaplar.ToList());

        }

        public ActionResult barkodara(string y)
        {
            var kitaplar = from k in db.TBLKITAP select k;
            if (!string.IsNullOrEmpty(y))
            {
                kitaplar = kitaplar.Where(m => m.BARKODNO.ToString().Contains(y));

            }

            //var kitaplar = db.TBLKITAP.ToList();
            return View("Index", kitaplar.ToList());

        }

        public ActionResult ozetara(string z)
        {
            var kitaplar = from k in db.TBLKITAP select k;
            if (!string.IsNullOrEmpty(z))
            {
                kitaplar = kitaplar.Where(m => m.SUMMARY.ToString().Contains(z));

            }

            //var kitaplar = db.TBLKITAP.ToList();
            return View("Index", kitaplar.ToList());

        }

        [HttpGet]
        public ActionResult KitapEkle()
        {
            List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD +' '+ i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;

            List<SelectListItem> deger3 = (from i in db.Libraries.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.LibraryName,
                                               Value = i.LibrariesID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            return View();
        }

        [HttpPost]

        public ActionResult KitapEkle(TBLKITAP p)
        {
            var ktg = db.TBLKATEGORI.Where(k => k.ID == p.TBLKATEGORI.ID).FirstOrDefault();
            var yzr = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();
            var lbrr = db.Libraries.Where(y => y.LibrariesID == p.Libraries.LibrariesID).FirstOrDefault();
            p.TBLKATEGORI = ktg;
            p.TBLYAZAR = yzr;
            p.Libraries = lbrr;

            db.TBLKITAP.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KitapSil(int id)
        {
            var kitap = db.TBLKITAP.Find(id);
            db.TBLKITAP.Remove(kitap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KitapGetir(int id)
        {
            var ktp = db.TBLKITAP.Find(id);

            List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + ' ' + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;

            List<SelectListItem> deger3 = (from i in db.Libraries.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.LibraryName,
                                               Value = i.LibrariesID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;

            return View("KitapGetir", ktp);
        }

        public ActionResult KitapGuncelle(TBLKITAP p)
        {
            var kitap = db.TBLKITAP.Find(p.ID);
            kitap.AD = p.AD;
            kitap.BASIMYIL = p.BASIMYIL;
            kitap.SAYFA = p.SAYFA;
            kitap.YAYINEVI = p.YAYINEVI;

            var ktg = db.TBLKATEGORI.Where(k => k.ID == p.TBLKATEGORI.ID).FirstOrDefault();
            var yzr = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();
            var lbrr = db.Libraries.Where(y => y.LibrariesID == p.Libraries.LibrariesID).FirstOrDefault();


            kitap.KATEGORI = ktg.ID;
            kitap.YAZAR = yzr.ID;
            kitap.LIBRARYID = lbrr.LibrariesID;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}