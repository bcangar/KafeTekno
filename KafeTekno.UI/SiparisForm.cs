using KafeTekno.DATA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafeTekno.UI
{
    public partial class SiparisForm : Form
    {

        private readonly KafeVeri _db; //1
        private readonly Siparis _siparis; //2

        //private readonly: bir daha değişemesin
        private readonly BindingList<SiparisDetay> _blSiparisDetaylar; //7

        public SiparisForm(KafeVeri db, Siparis siparis) //3
        {   //3
            _db = db;
            _siparis = siparis;

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            _blSiparisDetaylar = new BindingList<SiparisDetay>(_siparis.SiparisDetaylar);//8
            InitializeComponent();
            cboUrun.DataSource = _db.Urunler; //4            Ürün cmb ına Kola ve Ayran girilmiştir.
            dgvSiparisDetaylar.DataSource = _blSiparisDetaylar; //9
            _blSiparisDetaylar.ListChanged += _blSiparisDetaylar_ListChanged; //11
            MasaNoGuncelle(); //5
            OdemeTutariGuncelle(); //6
        }

        private void _blSiparisDetaylar_ListChanged(object sender, ListChangedEventArgs e) //11
        {
            //Bu kısımda, dgvSiparisDetaylar e sipariş eklerken liste değişim eventi tetiklendiğinde,
            //ödeme tutarı kendini anlık olarak,BindingList sağolsun, kendini güncelliyor.

            OdemeTutariGuncelle();
        }

        private void MasaNoGuncelle() //5
        {
            Text = $"Masa {_siparis.MasaNo:00} (Açılış Zamanı: {_siparis.AcilisZamani})"; //Formun text ine açılış zamanı yazdırıldı.
            lblMasaNo.Text = _siparis.MasaNo.ToString(); //Domates rengindeki label text ine çift tıkladığımız masanın numarası yazdırılmıştır.
        }
        private void OdemeTutariGuncelle() //6
        {
            lblOdemeTutari.Text = _siparis.ToplamTutarTL;
        }

        private void btnEkle_Click(object sender, EventArgs e)//10
        {
            //Eğer cmb ta seçili bir ürün yoksa,tokadı basıyoruz
            if (cboUrun.SelectedIndex == -1) return;

            Urun urun = cboUrun.SelectedItem as Urun;

            SiparisDetay sd = new SiparisDetay() { UrunAd = urun.UrunAd, BirimFiyat = urun.BirimFiyat, Adet = (int)nudAdet.Value };
            _blSiparisDetaylar.Add(sd);
            EkleFormunuSifirla(); //13


        }

        private void EkleFormunuSifirla() //13
        {
            cboUrun.SelectedIndex = 0;
            nudAdet.Value = 1;
        }

        private void dgvSiparisDetaylar_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)//12
        {
            //Kullanıcı, silmeye yeltendiği an işlem gerçekleştiriyoruz.

            DialogResult dr = MessageBox.Show(
                text: "Silmek istediğinize emin misiniz?",
                caption: "Detay Silme Onayı",
                buttons: MessageBoxButtons.YesNo,
                icon: MessageBoxIcon.Question,
                defaultButton: MessageBoxDefaultButton.Button2);//9 of 21 bak bi

            //Ne zaman siparişi iptal etmeliyiz?
            e.Cancel = dr == DialogResult.No;
        }

        private void btnAnasayfa_Click(object sender, EventArgs e) //13
        {
            Close();
        }

        private void btnOdemeAl_Click(object sender, EventArgs e) //14
        {
            MasayiKapat(SiparisDurum.Odendi, _siparis.ToplamTutar()); //Satır 108 git
        }

        private void btnSiparisIptal_Click(object sender, EventArgs e)//15
        {
            MasayiKapat(SiparisDurum.Iptal); //Satır 108 git
           
        }

        private void MasayiKapat(SiparisDurum durum, decimal odenenTutar = 0) //14 ve 15
        {
            _siparis.Durum = durum;
            _siparis.KapanisZamani = DateTime.Now;
            _siparis.OdenenTutar =odenenTutar;
            _db.AktifSiparisler.Remove(_siparis);
            _db.GecmisSiparisler.Add(_siparis);
            Close();
            //Ana form satır 79 a git
        }
    }
}
