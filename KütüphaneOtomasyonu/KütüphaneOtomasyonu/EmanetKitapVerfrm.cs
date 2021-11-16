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
    public partial class EmanetKitapVerfrm : Form
    {
        public EmanetKitapVerfrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=desktop-4m20v96;Initial Catalog=Kutuphane;Integrated Security=True");
        DataSet daset = new DataSet();
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
        private void sepetlistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from sepet", baglanti);
            adtr.Fill(daset, "sepet");
            dataGridView1.DataSource = daset.Tables["sepet"];
            baglanti.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into sepet(barkodno,kitapadi,yazari,yayinevi,sayfasayisi,kitapsayisi,teslimtarihi,iadetarihi) values(@barkodno,@kitapadi,@yazari,@yayinevi,@sayfasayisi,@kitapsayisi,@teslimtarihi,@iadetarihi)", baglanti);
            komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@kitapadi", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@yazari", txtYazari.Text);
            komut.Parameters.AddWithValue("@yayinevi", txtYayinEvi.Text);
            komut.Parameters.AddWithValue("@sayfasayisi", txtSayfaSayisi.Text);
            komut.Parameters.AddWithValue("@kitapsayisi", int.Parse(txtKitapSayisi.Text));
            komut.Parameters.AddWithValue("@teslimtarihi", dateTimePicker1.Text);
            komut.Parameters.AddWithValue("@iadetarihi", dateTimePicker2.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitaplar Sepete Eklendi"); 
            daset.Tables["sepet"].Clear();
            sepetlistele();
            lblKitapSayi.Text = "";
            kitapsayisi();
            foreach (Control item in grpKitapBilgi.Controls)
            {
                if (item is TextBox)
                {
                  if(item!=txtKitapSayisi)
                    {
                        item.Text = " ";
                    }
                }
            }
        }
        private void kitapsayisi()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(kitapsayisi) from sepet ", baglanti);
            lblKitapSayi.Text = komut.ExecuteScalar().ToString();
            baglanti.Close();
        }


        private void EmanetKitapVerfrm_Load(object sender, EventArgs e)
        {
            sepetlistele();
            kitapsayisi();
        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            baglanti.Close();
            baglanti.Open();
            try
            {
                SqlCommand komut = new SqlCommand("select *from uyeekle where tc ='" + txtTcAra.Text + "'", baglanti);
                SqlDataReader read = komut.ExecuteReader();
                while (read.Read())
                {
                    txtAdSoyad.Text = read["adsoyad"].ToString();
                    txtYas.Text = read["yas"].ToString();
                    txtTelefon.Text = read["telefon"].ToString();

                }
                baglanti.Close();

            }
            catch (Exception)
            {

                throw;
            } 
            baglanti.Open();
            SqlCommand komut2 =new SqlCommand("select sum(kitapsayisi) from emanetkitaplar", baglanti);
            lblKayitliKitapSayisi.Text = komut2.ExecuteScalar().ToString();
            baglanti.Close(); 
            if(txtTcAra.Text=="")
                {
                foreach(Control item in grpUyeBilgi.Controls)
                {
                    if(item is TextBox)

                        {
                        item.Text = "";
                        lblKayitliKitapSayisi.Text = "";
                    }
                  
                       
                }

            }
        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from [table] where barkodno like'" + txtBarkodNo.Text + "' ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while(read.Read())
            {
                txtKitapAdi.Text = read["kitapadi"].ToString();
                txtYazari.Text = read["yazari"].ToString();
                txtYayinEvi.Text = read["yayinevi"].ToString();
                txtSayfaSayisi.Text = read["sayfasayisi"].ToString();
           
            }
            baglanti.Close();
            if(txtBarkodNo.Text=="")
            {
                foreach (Control item in grpKitapBilgi.Controls)

                {
                    if (item is TextBox)
                    {
                        if (item != txtKitapSayisi)
                        {
                            item.Text = "";
                        }
                    }
                }
            }
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet where barkodno='"+dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString()+"'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme işlemi yapıldı");
            daset.Tables["sepet"].Clear();
            sepetlistele();
            lblKitapSayi.Text = "";
            kitapsayisi();

        }

        private void button2_Click(object sender, EventArgs e)
        {
        if(lblKitapSayi.Text!="")
            {
             if(lblKayitliKitapSayisi.Text=="" && int.Parse(lblKitapSayi.Text)<= 3 || lblKayitliKitapSayisi.Text!="" && int.Parse(lblKayitliKitapSayisi.Text)+int.Parse(lblKitapSayi.Text)<=3  )
                {
                    if(txtTcAra.Text!="" && txtAdSoyad.Text!="" && txtYas.Text!="" && txtTelefon.Text!="" )
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                        {
                            baglanti.Open();
                            SqlCommand komut = new SqlCommand("insert into emanetkitaplar(tc,adsoyad,yas,telefon,barkodno,kitapadi,yazari,yayinevi,sayfasayisi,kitapsayisi,teslimtarihi,iadetarihi values(@tc,@adsoyad,@yas,@telefon,@barkodno,@kitapadi,@yazari,@yayinevi,@sayfasayisi,@kitapsayisi,@teslimtarihi,@iadetarihi)", baglanti);
                            komut.Parameters.AddWithValue("@tc", txtTcAra.Text);
                            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                            komut.Parameters.AddWithValue("@yas", txtYas.Text);
                            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                            komut.Parameters.AddWithValue("barkodno",dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                            komut.Parameters.AddWithValue("kitapadi", dataGridView1.Rows[i].Cells["kitapadi"].Value.ToString());
                            komut.Parameters.AddWithValue("yazari", dataGridView1.Rows[i].Cells["yazari"].Value.ToString());
                            komut.Parameters.AddWithValue("yayinevi", dataGridView1.Rows[i].Cells["yayinevi"].Value.ToString());
                            komut.Parameters.AddWithValue("sayfasayisi", dataGridView1.Rows[i].Cells["sayfasayisi"].Value.ToString());
                            komut.Parameters.AddWithValue("kitapsayisi",int.Parse(dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString() ));
                            komut.Parameters.AddWithValue("teslimtarihi", dataGridView1.Rows[i].Cells["teslimtarihi"].Value.ToString());
                            komut.Parameters.AddWithValue("iadetarihi", dataGridView1.Rows[i].Cells["iadetarihi"].Value.ToString());
                            komut.ExecuteNonQuery();
                            SqlCommand komut2 = new SqlCommand("update uyeekle set okunankitap=okunankitap+'"+int.Parse(dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString())+"'where tc='"+txtTcAra.Text+"'", baglanti);
                            komut2.ExecuteNonQuery();
                            SqlCommand komut3 = new SqlCommand("update [table] set stoksayisi=stoksayisi-'" + int.Parse(dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString()) + "'where barkodno='" +dataGridView1.Rows[i].Cells["barkono"].Value.ToString() + "'", baglanti);
                            komut3.ExecuteNonQuery();
                            baglanti.Close();
                        }
                        baglanti.Open();
                        SqlCommand komut4 = new SqlCommand("delete from sepet", baglanti);
                        komut4.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Kitap(lar) Emanet Edildi");
                         daset.Tables["sepet"].Clear();
                        sepetlistele();
                        txtTcAra.Text = "";
                        lblKitapSayi.Text = "";
                        kitapsayisi();
                        lblKayitliKitapSayisi.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Önce Üye İsmi Seçmeniz Gerekir!!!", "Uyarı");

                    }
                 
                }
              else
                {
                    MessageBox.Show("Emanet Kitap Sayısı 3 ten fazla olamaz!!!", "uyarı");
                }
            }
        else
            {
                MessageBox.Show("Önce Sepete Kitap Eklenmelidir!!!", "Uyarı");
            }





           
        }
    }
}
