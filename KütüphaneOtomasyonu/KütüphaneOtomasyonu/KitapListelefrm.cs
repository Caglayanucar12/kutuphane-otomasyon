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
    public partial class KitapListelefrm : Form
    {
        public KitapListelefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=desktop-4m20v96;Initial Catalog=Kutuphane;Integrated Security=True");
        DataSet daset = new DataSet();
        private void kitaplistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from [table]", baglanti);
            adtr.Fill(daset, "[table]");
            dataGridView1.DataSource = daset.Tables["[table]"];
            baglanti.Close();

        }
        private void KitapListelefrm_Load(object sender, EventArgs e)
        {
            kitaplistele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu kaydı Silmek Mi İstiyorsunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from [table] where barkodno=@barkodno", baglanti);
                komut.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Silme İşlemi Yapıldı");
                daset.Tables["[table]"].Clear();
                kitaplistele();
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }

            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update [table] set kitapadi=@kitapadi,yazari=@yazari,yayinevi=@yayinevi,sayfasayisi=@sayfasayisi,turu=@turu,stoksayisi=@stoksayisi,rafno=@rafno,aciklama=@aciklama where barkodno=@barkodno", baglanti);
            komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@kitapadi", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@yazari", txtYazari.Text);
            komut.Parameters.AddWithValue("@yayinevi", txtYayinEvi.Text);
            komut.Parameters.AddWithValue("@sayfasayisi", txtSayfaSayisi.Text);
            komut.Parameters.AddWithValue("@turu", comboTuru.Text);
            komut.Parameters.AddWithValue("@stoksayisi", txtStokSayisi.Text);
            komut.Parameters.AddWithValue("@rafno", txtRafNo.Text);
            komut.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme İşlemi Yapıldı");
            daset.Tables["[tablo]"].Clear();
            kitaplistele();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            daset.Tables["[table]"].Clear();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from [table] where barkodno like '%" + txtBarkodNo.Text + "&' ", baglanti);
            adtr.Fill(daset, "[table]");
            dataGridView1.DataSource = daset.Tables["[table]"];
            baglanti.Close();

        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from [table] where barkodno like '" + txtBarkodNo.Text + "' ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtKitapAdi.Text = read["kitapadi"].ToString();
                txtYazari.Text = read["yazari"].ToString();
                txtYayinEvi.Text = read["yayinevi"].ToString();
                txtSayfaSayisi.Text = read["sayfasayisi"].ToString();
                comboTuru.Text = read["turu"].ToString();
                txtStokSayisi.Text = read["stoksayisi"].ToString();
                txtRafNo.Text = read["rafno"].ToString();
                txtAciklama.Text = read["aciklama"].ToString();
            }
            baglanti.Close();
        }
    }
}
