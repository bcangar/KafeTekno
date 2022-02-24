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
    public partial class GecmisSiparislerForm : Form
    {
        private readonly KafeVeri _db;//1 
        public GecmisSiparislerForm(KafeVeri db) //2
        {
            _db = db;
            InitializeComponent();
            dgvSiparisler.DataSource = _db.GecmisSiparisler;//3
        }

        private void dgvSiparisler_SelectionChanged(object sender, EventArgs e)//4
        {
           if(dgvSiparisler.SelectedRows.Count == 0)
            {
                dgvSiparisDetaylar.DataSource = null; //Seçim yoksa, altta gösterilecek de yoktur.
            }
           else
            {
                DataGridViewRow seciliSatir = dgvSiparisler.SelectedRows[0];
                Siparis siparis = (Siparis)seciliSatir.DataBoundItem;
                dgvSiparisDetaylar.DataSource = siparis.SiparisDetaylar;
            }

        }
    }
}
