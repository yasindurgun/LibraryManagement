using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneYonetimSistemi.Models.Entity;

namespace KutuphaneYonetimSistemi.Controllers
{
    public class statisticsController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        // GET: statistics
        public ActionResult Index()
        {
            var var1 = db.TBLUYELER.Count();
            var var2 = db.TBLKITAP.Count();
            var var3 = db.TBLKITAP.Where(x => x.DURUM == false).Count();
            var var4 = db.TBLCEZALAR.Sum(x => x.PARA);
            ViewBag.var1 = var1;
            ViewBag.var2 = var2;
            ViewBag.var3 = var3;
            ViewBag.var4 = var4;
            return View();
        }
        public ActionResult weather()
        {
            return View();
        }
        public ActionResult weatherCard()
        {
            return View();
        }
        public ActionResult gallery()
        {
            return View();
        }
        [HttpPost]
        public ActionResult resimyukle(HttpPostedFileBase dosya)
        {
            if (dosya.ContentLength > 0) { 

                string dosyayolu = System.IO.Path.Combine(Server.MapPath("~/web2/resimler"), System.IO.Path.GetFileName
                    (dosya.FileName));
            dosya.SaveAs(dosyayolu);
            }
        return RedirectToAction("Gallery");
        }

        public ActionResult LinqCard()
        {
            //Linq first line

            var variable1 = db.TBLKITAP.Count();
            var variable2 = db.TBLUYELER.Count();
            var variable3 = db.TBLCEZALAR.Sum(x => x.PARA);
            var variable4 = db.TBLKITAP.Where(x => x.DURUM == false).Count();
            ViewBag.variable1 = variable1;
            ViewBag.variable2 = variable2;
            ViewBag.variable3 = variable3;
            ViewBag.variable4 = variable4;

            //Linq second line

            var variable5 = db.TBLKATEGORI.Count();
            //var variable6 = db.TBLUYELER.Count();
            //var variable7 = db.TBLCEZALAR.Sum(x => x.PARA);
            //var variable8 = db.MostHaveBookAuthor().FirstOrDefault();
            ViewBag.variable5 = variable5;
           // ViewBag.variable6 = variable6;
            //ViewBag.variable7 = variable7;
            //ViewBag.variable8 = variable8;

            var variable9 = db.TBLKITAP.GroupBy(x => x.YAYINEVI).OrderByDescending(z => z.Count()).Select(y =>
                new { y.Key}).FirstOrDefault();
            ViewBag.variable9 = variable9;


            var variable11 = db.TBLILETISIM.Count();
            ViewBag.variable11 = variable11;


            return View();
        
        }


    }
}