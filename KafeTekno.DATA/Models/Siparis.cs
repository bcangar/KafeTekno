﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeTekno.DATA.Models
{
    public class Siparis
    {
        /*
         Siparis
         * MasaNo: int
         * Durum: SiparisDurum
         * OdenenTutar: decimal
         * AcilisZamani: DateTime?
         * KapanisZamani: DateTime?
         * SiparisDetaylar: List<SiparisDetay>
         * ToplamTutarTL: string-readonly
         - ToplamTutar(): decimal
        */

        public int MasaNo { get; set; }

        public SiparisDurum Durum { get; set; }
        public decimal OdenenTutar { get; set; }

        public DateTime? AcilisZamani { get; set; } = DateTime.Now;

        public DateTime? KapanisZamani { get; set; }

        public List<SiparisDetay> SiparisDetaylar { get; set; } = new List<SiparisDetay>(); //Yeni sipraiş verildiğinde elimizde hazır boş liste bulunsun diye tanımladık.

        public string ToplamTutarTL => $"₺{ToplamTutar():n2}"; 

        public decimal ToplamTutar()
        {
            return SiparisDetaylar.Sum(x=>x.Tutar());
        }


    }
}
