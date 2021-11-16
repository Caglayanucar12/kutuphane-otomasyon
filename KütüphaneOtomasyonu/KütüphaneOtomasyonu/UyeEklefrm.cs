using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace KütüphaneOtomasyonu
{
    public partial class UyeEklefrm : Form
    {
        public UyeEklefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=desktop-4m20v96;Initial Catalog=Kutuphane;Integrated Security=True");

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into UyeEkle(tc,adsoyad,yas,telefon,adres,okunankitap)values(@tc,@adsoyad,@yas,@telefon,@adres,@okunankitap) ", baglanti);
            komut.Parameters.AddWithValue("@tc", textBox1.Text);
            komut.Parameters.AddWithValue("@adsoyad", textBox2.Text);
            komut.Parameters.AddWithValue("@yas", textBox4.Text);
            komut.Parameters.AddWithValue("@telefon", textBox3.Text);
            komut.Parameters.AddWithValue("@adres", textBox6.Text);
            komut.Parameters.AddWithValue("@okunankitap", textBox5.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Üye kaydı Yapıldı");
            foreach(Control item in Controls)
            {
                if(item is TextBox)
                {
                    item.Text = " ";
                }
            }

        }

        private void UyeEklefrm_Load(object sender, EventArgs e)
        {

        }
    }
}
