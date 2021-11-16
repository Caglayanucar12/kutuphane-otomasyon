using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KütüphaneOtomasyonu
{
    public partial class KitapEklefrm : Form
    {
        public KitapEklefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=desktop-4m20v96;Initial Catalog=Kutuphane;Integrated Security=True");

        private void KitapEklefrm_Load(object sender, EventArgs e)
        {
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into [Table](barkodno,kitapadi,yazari,yayinevi,sayfasayisi,turu,stoksayisi,rafno,aciklama,kayittarihi) values(@barkodno,@kitapadi,@yazari,@yayinevi,@sayfasayisi,@turu,@stoksayisi,@rafno,@aciklama,@kayittarihi) ", baglanti);
            komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@kitapadi", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@yazari", txtYazari.Text);
            komut.Parameters.AddWithValue("@yayinevi", txtYayinEvi.Text);
            komut.Parameters.AddWithValue("@sayfasayisi", txtSayfaSayisi.Text);
            komut.Parameters.AddWithValue("@turu", comboTuru.Text);
            komut.Parameters.AddWithValue("@stoksayisi", txtStokSayisi.Text);
            komut.Parameters.AddWithValue("@rafno", txtRafNo.Text);
            komut.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
            komut.Parameters.AddWithValue("@kayittarihi",DateTime.Now.ToShortDateString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap kaydı Yapıldı");
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = " ";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
