using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KütüphaneOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UyeEklefrm uyeekle = new UyeEklefrm();
            uyeekle.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UyeListelemefrm uyeliste = new UyeListelemefrm();
            uyeliste.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KitapEklefrm kitapekle = new KitapEklefrm();
            kitapekle.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KitapListelefrm kitapliste = new KitapListelefrm();
            kitapliste.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EmanetKitapVerfrm emanetkitapver = new EmanetKitapVerfrm();
            emanetkitapver.ShowDialog(); 
        }
    }
}
