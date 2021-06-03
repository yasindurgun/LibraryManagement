using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using KutuphaneYonetimSistemi.Models.Entity;

namespace KutuphaneYonetimSistemi.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        public class Kritik_Stok
        {
            public Kritik_Stok(int id, string kategori)
            {
                ID = id;
                Kategori = kategori;

            }
            public int ID { get; set; }
            public string Kategori { get; set; }

        }
        public class DataSource
        {
            public DBKUTUPHANEEntities CC = new DBKUTUPHANEEntities();
            public List<Kritik_Stok> Kritik_Stok_List = new List<Kritik_Stok>();

            public DataSource()
            {
                var deger = CC.TBLKATEGORI.ToList();
                foreach (var item in deger)
                {

                    Kritik_Stok_List.Add(new Kritik_Stok(item.ID, item.AD));

                }

            }
        }
        // GET: Kategori
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

        public ActionResult Index()
        {
            DateTime bugündate = DateTime.Now;
            string bugüntarih = bugündate.ToString("dd/MM/yyyy");
            var deger99 = db.TBL_KritikStok.OrderByDescending(x => x.Dosya_Kayit).FirstOrDefault();
            if (deger99 == null)
            {
                var source = new DataSource();
                var degerler = db.TBLKATEGORI.ToList();
                var degerler2 = db.TBLKITAP.ToList();
                var Kritik_Stok = source.Kritik_Stok_List;
                var workbook = new XLWorkbook();
                var PersonWorksheet = workbook.Worksheets.Add("Kritik Kategori");
                PersonWorksheet.Cell("A1").Value = "ID";
                PersonWorksheet.Cell("B1").Value = "AD";
                var row = 2;
                for (int i = 0; i < degerler.Count; i++)
                {
                    Kritik_Stok person = Kritik_Stok[i];

                    PersonWorksheet.Cell(row, "A").Value = person.ID;
                    PersonWorksheet.Cell(row, "B").Value = person.Kategori; 
                    row++;
                }
                var rnd = new Random();
                string filePath = string.Format("/Content/Kritik_Stok_{0}_ex.xlsx", rnd.Next(0, 9999));
                TBL_KritikStok cC = new TBL_KritikStok();
                cC.Dosya_Kayit = DateTime.Now;
                cC.Dosya_Yolu = filePath;
                db.TBL_KritikStok.Add(cC);
                db.SaveChanges();
                workbook.SaveAs(HttpContext.Server.MapPath(filePath));

                SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);
                //Gönderici Mail Adresi
                NetworkCredential cred = new NetworkCredential("ebote944@gmail.com", "patates1998");

                mailClient.Credentials = cred;
                MailMessage contact = new MailMessage();
                //Giden Mail Adresi
                contact.From = new MailAddress("emincelik6105@gmail.com");
                contact.Subject = "Dosya Yüklendi.";
                contact.IsBodyHtml = true;
                contact.Body = "Stok'Ta Azalan Ürünler Listesi";
                mailClient.EnableSsl = true;

                Attachment a = new Attachment(HttpContext.Server.MapPath(filePath));
                contact.Attachments.Add(a);
                contact.To.Add("emincelik6105@gmail.com");
                mailClient.Send(contact);
                return View(degerler);
            }
            else if (true)
            {
                var degerler = db.TBLKATEGORI.ToList();

                return View(degerler);
            }
        }



        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(TBLKATEGORI p)
        {
            db.TBLKATEGORI.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriSil (int id)
        {
            var kategori = db.TBLKATEGORI.Find(id);
            db.TBLKATEGORI.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir (int id)
        {
            var ktg = db.TBLKATEGORI.Find(id);
            return View("KategoriGetir", ktg);
        }

        public ActionResult KategoriGuncelle (TBLKATEGORI p)
        {
            var ktg = db.TBLKATEGORI.Find(p.ID);
            ktg.AD = p.AD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}