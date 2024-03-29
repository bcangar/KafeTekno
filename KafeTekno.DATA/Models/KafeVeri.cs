﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeTekno.DATA.Models
{
    public class KafeVeri
    {
        /*
        KafeVeri
        * MasaAdet: int
        * Urunler: List<Urun>
        * AktifSiparisler: List<Siparis>
        * GecmisSiparisler: List<Siparis>
        */

        public int MasaAdet { get; set; } = 20;

        public List<Urun> Urunler { get; set; } = new List<Urun>();

        public List<Siparis> AktifSiparisler { get; set; } = new List<Siparis>(); 
        //Şu anda aktif olarak, hangi masalardan sipariş verildiğinin bilgisini tutuyor.

        public List<Siparis> GecmisSiparisler { get; set; } = new List<Siparis>();


    }
}
