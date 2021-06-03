using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneYonetimSistemi.Models.Entity;

namespace KutuphaneYonetimSistemi.Controllers
{
    //ÜyelerController
    public class UserController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        // GET: User
        public ActionResult Index()
        {
            var values = db.TBLUYELER.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddUser()
        {

            List<SelectListItem> values3 = (from i in db.Libraries.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.LibraryName,
                                                Value = i.LibrariesID.ToString()
                                            }).ToList();
            ViewBag.value3 = values3;


            return View();

        }
        [HttpPost]
        public ActionResult AddUser(TBLUYELER p)
        {
            
            var loc = db.Libraries.Where(a => a.LibrariesID == p.Libraries.LibrariesID).FirstOrDefault();
            p.Libraries = loc;
            db.TBLUYELER.Add(p);
            db.SaveChanges();
            return View("Index");
        }

        public ActionResult DeleteUser(int id)

        {
            var user = db.TBLUYELER.Find(id);
            db.TBLUYELER.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UpdateUser(int id)

        {
            var update = db.TBLUYELER.Find(id);


            List<SelectListItem> values3 = (from i in db.Libraries.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.LibraryName,
                                                Value = i.LibrariesID.ToString()
                                            }).ToList();
            ViewBag.value3 = values3;



            return View("UpdateUser", update);
        }
        public ActionResult SaveUser(TBLUYELER p)
        {
            var getir = db.TBLUYELER.Find(p.ID);
            getir.AD = p.AD;
            getir.SOYAD = p.SOYAD;
            getir.MAIL = p.MAIL;
            getir.KULLANICIADI = p.KULLANICIADI;
            getir.SIFRE = p.SIFRE;
            getir.FOTOGRAF = p.FOTOGRAF;
            getir.TELEFON = p.TELEFON;
            getir.OKUL = p.OKUL;
            getir.DETAY = p.DETAY;


            var loc = db.Libraries.Where(b => b.LibrariesID == p.Libraries.LibrariesID).FirstOrDefault();
            getir.LIBRARYID = loc.LibrariesID;

            db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}