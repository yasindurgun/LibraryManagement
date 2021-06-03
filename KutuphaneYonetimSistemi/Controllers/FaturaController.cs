using KutuphaneYonetimSistemi.Models;
using KutuphaneYonetimSistemi.Models.Entity;
using KutuphaneYonetimSistemi.Models.Fatura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KutuphaneYonetimSistemi.Controllers
{
    public class BillController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

        public ActionResult Detail(int? id)
        {
            Fatura_View_Model vm = new Fatura_View_Model();
            vm.TBLFatura_Duzenle = db.TBLFatura.FirstOrDefault(x => x.ID == id);
            vm.TBLFaturaKalem = db.TBL_FaturaKalem.Where(x => x.Fatura_ID == vm.TBLFatura_Duzenle.ID).ToList();
            vm.TBLKITAP_List = db.TBLKITAP.ToList();
            vm.TBLYazar = db.TBLYAZAR.ToList();
            vm.TBL_Kategori = db.TBLKATEGORI.ToList();
            vm.TBLFatura = db.TBLFatura.ToList();
            vm.TBLFaturaKalem = db.TBL_FaturaKalem.ToList();
            vm.TBLUYELER = db.TBLUYELER.ToList();
            return View(vm);
        }
        // GET: Fatura
        public ActionResult Index()
        {
            Fatura_View_Model vm = new Fatura_View_Model();
            vm.TBLKITAP_List = db.TBLKITAP.ToList();
            vm.TBL_Kategori = db.TBLKATEGORI.ToList();
            vm.TBLFatura = db.TBLFatura.ToList();
            vm.TBLFaturaKalem = db.TBL_FaturaKalem.ToList();
            vm.TBLUYELER = db.TBLUYELER.ToList();
            return View(vm);
        }
        public ActionResult Fatura_Baslik()
        {
            Fatura_View_Model vm = new Fatura_View_Model();
            vm.TBLKITAP_List = db.TBLKITAP.ToList();
            vm.TBL_Kategori = db.TBLKATEGORI.ToList();
            vm.TBLFatura = db.TBLFatura.ToList();
            vm.TBLFaturaKalem = db.TBL_FaturaKalem.ToList();
            vm.TBLUYELER = db.TBLUYELER.ToList();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Fatura_Baslik(Fatura_View_Model cc, int UYE_ID, DateTime aldigitarih)
        {
            cc.TBLFatura_Duzenle.Aldığı_Tarih = aldigitarih;
            cc.TBLFatura_Duzenle.Uye_ID = UYE_ID;
            db.TBLFatura.Add(cc.TBLFatura_Duzenle);
            db.SaveChanges();
            var deger = db.TBLFatura.FirstOrDefault(x => x.Uye_ID == UYE_ID && x.Adress == cc.TBLFatura_Duzenle.Adress && x.Aldı == true && x.Verdi == false
            && x.Aldığı_Tarih == cc.TBLFatura_Duzenle.Aldığı_Tarih);
            Session["ID"] = deger.ID;
            return RedirectToAction("Fatura_Kalem");
        }
        public ActionResult Fatura_Kalem()
        {
            Fatura_View_Model vm = new Fatura_View_Model();
            vm.TBLKITAP_List = db.TBLKITAP.ToList();
            vm.TBL_Kategori = db.TBLKATEGORI.ToList();
            vm.TBLYazar = db.TBLYAZAR.ToList();
            vm.TBLFatura = db.TBLFatura.ToList();
            vm.TBLFaturaKalem = db.TBL_FaturaKalem.ToList();
            vm.TBLUYELER = db.TBLUYELER.ToList();
            return View(vm);
        }
        public ActionResult Siparis_Kaydet()
        {
            int a = Convert.ToInt32(Session["ID"].ToString());
            var deger = Sepet.AktifSepet.Urunler.ToList();
            foreach (var item in deger)
            {
                var deger2 = db.TBLKITAP.FirstOrDefault(x => x.AD == item.Kitap_Ad);
                var deger3 = db.TBLKATEGORI.FirstOrDefault(x => x.AD == item.Kategori_Ad);
                var deger4 = db.TBLYAZAR.FirstOrDefault(x => x.ID == deger2.YAZAR);
                TBL_FaturaKalem cc = new TBL_FaturaKalem();
                cc.Kitap_ID = deger2.ID;
                cc.Kategori_ID = deger3.ID;
                cc.Yazar_ID = deger4.ID;
                cc.Fatura_ID = a;
                db.TBL_FaturaKalem.Add(cc);
                db.SaveChanges();
            }

            return Redirect("Index");
        }
        public PartialViewResult Aldigi_Kitap()
        {

            if (HttpContext.Session["AktifSepet"] != null)
                return PartialView((Sepet)HttpContext.Session["AktifSepet"]);
            else
                return PartialView();
        }
        public ActionResult Sepet_Temizle()
        {
            var deger = Sepet.AktifSepet.Urunler.ToList();

            if (deger != null)
            {
                HttpContext.Session["AktifSepet"] = null;
            }
            else
            {

            }
            return RedirectToAction("Fatura_Kalem");
        }
        public ActionResult Sepet_Adet_Sil(int id)
        {
            var deger = Sepet.AktifSepet.Urunler.Where(X => X.ID == id).FirstOrDefault();
            Sepet.AktifSepet.Urunler.Remove(deger);
            return RedirectToAction("Fatura_Kalem");
        }
        public ActionResult Sepet_Ekle(int id)
        {
            var deger = db.TBLKITAP.FirstOrDefault(x => x.ID == id);
            var deger2 = db.TBLYAZAR.FirstOrDefault(x => x.ID == deger.YAZAR);
            var deger3 = db.TBLKATEGORI.FirstOrDefault(x => x.ID == deger.KATEGORI);
            var deger4 = Sepet.AktifSepet.Urunler.ToList();
            var deger5 = db.TBL_Kural.FirstOrDefault();
            if (deger4.Count >= 4)
            {

            }
            else
            {
                if (deger != null)
                {
                    SepetItem si = new SepetItem();
                    si.Kitap_Ad = deger.AD;
                    string a = deger2.AD + " " + deger2.SOYAD;
                    si.Yazar_Ad_Soyad = a;
                    si.Kategori_Ad = deger3.AD;
                    si.Tutar = Convert.ToDecimal(deger5.Günlük_Fiyat);
                    Sepet s = new Sepet();
                    s.SepeteEkle(si);
                }
                else
                {

                }
            }

            return RedirectToAction("Fatura_Kalem");
        }
    }
}