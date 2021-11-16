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
    public partial class UyeListelemefrm : Form
    {
        public UyeListelemefrm()
        {
            InitializeComponent();
        }
        private void uyelistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from UyeEkle", baglanti);
            adtr.Fill(daset, "uyeekle");
            dataGridView1.DataSource = daset.Tables["uyeekle"];
            baglanti.Close();

        }

        private void UyeListelemefrm_Load(object sender, EventArgs e)
        {
            uyelistele();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             txtTcAra.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();

        }
        SqlConnection baglanti = new SqlConnection("Data Source=desktop-4m20v96;Initial Catalog=Kutuphane;Integrated Security=True");
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
             baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from UyeEkle where tc ='" + txtTcAra.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while(read.Read())
            {
                textBox2.Text = read["adsoyad"].ToString();
                textBox4.Text = read["yas"].ToString();
                textBox3.Text = read["telefon"].ToString();
                textBox6.Text = read["adres"].ToString();
                textBox5.Text = read["okunankitap"].ToString();

            }
            baglanti.Close();
            
                
        }
        DataSet daset = new DataSet();
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            daset.Tables ["UyeEkle"].Clear();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from UyeEkle where tc like '%" + txtTcAra.Text + "%' ", baglanti);
            adtr.Fill(daset,"UyeEkle");
            dataGridView1.DataSource = daset.Tables["UyeEkle"];
            baglanti.Close();

      

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu kaydı Silmek Mi İstiyorsunuz?","Sil",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if(dialog==DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from UyeEkle where tc=@tc", baglanti);
                komut.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells["tc"].Value.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Silme İşlemi Yapıldı");
                daset.Tables["uyeekle"].Clear();
                uyelistele();
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }

            }


        }
    
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update UyeEkle set adsoyad=@adsoyad,yas=@yas,telefon=@telefon,adres=@adres,okunankitap=@okunankitap where tc=@tc", baglanti);
            komut.Parameters.AddWithValue("@tc", txtTcAra.Text);
            komut.Parameters.AddWithValue("@adsoyad", textBox2.Text);
            komut.Parameters.AddWithValue("@yas", textBox4.Text);
            komut.Parameters.AddWithValue("@telefon", textBox3.Text);
            komut.Parameters.AddWithValue("@adres", textBox6.Text);
            komut.Parameters.AddWithValue("@okunankitap",int.Parse(textBox5.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme İşlemi Yapıldı");
            daset.Tables["UyeEkle"].Clear();
            uyelistele();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

        }
    }
}
