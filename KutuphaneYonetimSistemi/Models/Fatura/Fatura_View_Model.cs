using KutuphaneYonetimSistemi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KutuphaneYonetimSistemi.Models.Fatura
{
    public class Fatura_View_Model
    {
        public IEnumerable<TBLKITAP> TBLKITAP_List { get; set; }
        public IEnumerable<TBLKATEGORI> TBL_Kategori { get; set; }
        public IEnumerable<TBLYAZAR> TBLYazar { get; set; }
        public IEnumerable<TBLFatura> TBLFatura { get; set; }
        public TBLFatura TBLFatura_Duzenle { get; set; }
        public TBL_FaturaKalem TBLFaturaKalem_Duzenle { get; set; }
        public IEnumerable<TBL_FaturaKalem> TBLFaturaKalem { get; set; }
        public IEnumerable<TBLUYELER> TBLUYELER { get; set; }
    }
}