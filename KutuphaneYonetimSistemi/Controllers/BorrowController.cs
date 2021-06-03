using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneYonetimSistemi.Models.Entity;

namespace KutuphaneYonetimSistemi.Controllers
{
    public class BorrowController : Controller
    {

        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities ();
        // GET: Borrow
        public ActionResult Index()
        {
            var values = db.TBLHAREKET.Where(x => x.ISLEMDURUM == false).ToList();


            return View(values);
        }

        [HttpGet]
        public ActionResult Borrow()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Borrow(TBLHAREKET p)
        {
            db.TBLHAREKET.Add(p);
            db.SaveChanges();
            return View();
        }
        public ActionResult Lending(TBLHAREKET P)
        {
            var lend = db.TBLHAREKET.Find(P.ID);
            DateTime d1 = DateTime.Parse(lend.IADETARIH.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;
            
            ViewBag.dgr = d3.TotalDays;
            

            return View("Lending",lend);
        }

        public ActionResult UpdateLending(TBLHAREKET p)
        {
            var update = db.TBLHAREKET.Find(p.ID);

            update.UYEGETIRTARIH = p.UYEGETIRTARIH;
            update.ISLEMDURUM = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}