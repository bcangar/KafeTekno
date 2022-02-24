using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KafeTekno.DATA;
using KafeTekno.DATA.Models;
using KafeTekno.UI.Properties;

namespace KafeTekno.UI
{
    public partial class AnaForm : Form
    {
        KafeVeri db = new KafeVeri(); //0
        public AnaForm()
        {
            InitializeComponent();
            OrnekUrunleriYukle();//1
            MasalariOlustur(); //2
        }

        private void OrnekUrunleriYukle() //1
        {
            db.Urunler.Add(new Urun() { UrunAd = "Kola", BirimFiyat = 7.00m });
            db.Urunler.Add(new Urun() { UrunAd = "Ayran", BirimFiyat = 5.00m });
        }
        private void MasalariOlustur() //2
        {

            for (int i = 1; i <= db.MasaAdet; i++) //HATIRLATMA: Masa adeti, KafeVeri class ında default olarak 20 girildi.
            {
                ListViewItem lvi = new ListViewItem("Masa " + i); //Listview de yan yana "Masa 1,Masa2,.....,Masa 20 ye kadar yazdırır.
                lvi.ImageKey = "bos"; //Başlangıçta görseller bos.png olarak ayarlandı.
                lvi.Tag = i; //4     i nin değerini koruma amaçlı yazdık. (Satır 59 a git)
                lvwMasalar.Items.Add(lvi); //Item lar listview a eklenmiştir.
            }
            lvwMasalar.LargeImageList = BuyukImajListesi(); //3
        }
        private ImageList BuyukImajListesi() //3
        {//Bu kısımda, Resources dosyasında bulunan bos ve dolu görselleri, Satır 41 de bakarsan listeye yerleştirilmek üzere ayarlanmıştır.

            ImageList il = new ImageList();
            il.ImageSize = new Size(64, 64);
            il.Images.Add("bos", Resources.bos);
            il.Images.Add("dolu", Resources.dolu);
            return il;
        }

        private void lvwMasalar_DoubleClick(object sender, EventArgs e) //4
        {
            /*MessageBox.Show("Şaka");*/ //(DENEME) Listview de herhangi bir görsele tıklayınca, ekrana Şaka yazdırıyor.

            //Bu kısımda, Anaform ekranında seçili item ın ( masa görseli) masa numarası,satır 33'teki kodumuz sayesinde mb ile ekrana bildirilmiştir.

            ListViewItem lvi = lvwMasalar.SelectedItems[0];

            //////////////////////////////////////////////////////////////////////////////////////////
            
            lvi.ImageKey = "dolu"; //7 SiparisForm a geçtik 1-2-3 yazdık sonra buraya geldi satır 75 ayarladı

            //////////////////////////////////////////////////////////////////////////////////////////
            int masaNo = (int)lvi.Tag;
            //MessageBox.Show(masaNo.ToString());

            //////////////////////////////////////////////////////////////////////////////////////////

            Siparis siparis = SiparisBulYaDaOlustur(masaNo);//5    Satır 75 e git

            //////////////////////////////////////////////////////////////////////////////////////////
           
            new SiparisForm(db,siparis).ShowDialog(); //6 SiparisForm a geç

            //////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////
            //SiparisForm 14-15 adımları yazdım geldim
            //Masadan ödemeyi yapıp kalkan müşterinin masa görüntüsü, boş ayarlanmıştır.
            if (siparis.Durum != SiparisDurum.Aktif)
                lvi.ImageKey = "bos";

            //////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////


        }



        private Siparis SiparisBulYaDaOlustur(int masaNo)//5
        {
            //Sipariş varsa olanı döndür.

           Siparis siparis = db.AktifSiparisler.FirstOrDefault(x => x.MasaNo == masaNo);
            //FirstOrDefault: Liste üzerinde belirtilen şarta uygun durum var mı? Yoksa, default olarak null döndür.

            if (siparis == null) //Aktif sipariş yoksa bile, biri masaaya oturmuştur olarak kabul edip gene de oluşturuyoruz.
            {/* public DateTime? AcilisZamani { get; set; } = DateTime.Now;*/ //Siparis class ında default olarak tanımlandığı için burda tekrar yazmadık.
               siparis = new Siparis() { MasaNo = masaNo };
                db.AktifSiparisler.Add(siparis);

            }
            //Sipariş varsa , mevcut siparişi direkt döndür.
            return siparis;
        }
    }
}
