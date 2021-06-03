using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneYonetimSistemi.Models.Entity;

namespace KutuphaneYonetimSistemi.Controllers
{
    public class MovementController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        // GET: Movement
        public ActionResult Index()
        {
            var values = db.TBLAction.Where(x => x.ActionCase == true).ToList();

            return View();
        }
    }
}