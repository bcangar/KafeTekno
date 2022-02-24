using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeTekno.DATA.Models
{
    public class Urun
    {
     /*   Urun
          * UrunAd: string
          * BirimFiyat: decimal
          - ToString() : string */

        public string UrunAd { get; set; }

        public decimal BirimFiyat { get; set; }

        public override string ToString()
        {
            return $" {UrunAd}  {BirimFiyat:n2}";
        }


    }
}
