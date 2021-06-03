using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneYonetimSistemi.Models.Entity;
using KutuphaneYonetimSistemi.Models.Classes;
namespace KutuphaneYonetimSistemi.Controllers
{
    public class VitrinController : Controller
    {
        // GET: Vitrin
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        
        
        [HttpGet]
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            cs.Variable1 = db.TBLKITAP.ToList();
            cs.Variable2 = db.TBLHAKKIMIZDA.ToList();
            //var degerler = db.TBLKITAP.ToList();
            return View(cs);
        }

        [HttpPost]
        public ActionResult Index(TBLILETISIM t)
        {
            db.TBLILETISIM.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}