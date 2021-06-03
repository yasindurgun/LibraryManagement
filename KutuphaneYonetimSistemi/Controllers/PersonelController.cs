using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneYonetimSistemi.Models.Entity;
using KutuphaneYonetimSistemi.Repository;

namespace KutuphaneYonetimSistemi.Controllers
{
    public class PersonelController : Controller
    {

        PersonelRepository repo = new PersonelRepository();
        public ActionResult Index()
        {

            var values = repo.List();
            return View(values);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PersonelEkle(TBLPERSONEL p)
        {
            if (!ModelState.IsValid)
            {
                return View("PersonelEkle");
            }
            repo.TAdd(p);
            
            return RedirectToAction("Index");
        }

        public ActionResult PersonelSil(int id)
        {
            TBLPERSONEL t = repo.Find(x => x.ID == id);
            repo.TDelete(t);
            return RedirectToAction("Index");
        }

        //public ActionResult PersonelGetir(int id)
        //{
        //    var prs = db.TBLPERSONEL.Find(id);
        //    return View("PersonelGetir", prs);
        //}

        //public ActionResult PersonelGuncelle (TBLPERSONEL p)
        //{
        //    var prs = db.TBLPERSONEL.Find(p.ID);
        //    prs.PERSONEL = p.PERSONEL;
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }
}