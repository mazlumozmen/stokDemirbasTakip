using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace belediyeProgram
{
    public partial class urunEkle : Form
    {
        public urunEkle()
        {
            InitializeComponent();
        }

        string detay;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox6.Text == "" || richTextBox1.Text == "")
            {
                MessageBox.Show("Alanlar boş bırakılamaz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Random sayi = new Random();
                int kayitNo = sayi.Next(1000, 9999);

                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
                baglanti.Open();

                OleDbCommand kontrol = new OleDbCommand("select * from urunler where urunAdi='" + textBox1.Text + "' and marka='" + textBox2.Text + "' and seriNo='" + textBox6.Text + "'", baglanti);
                kontrol.ExecuteNonQuery();
                OleDbDataReader dr = kontrol.ExecuteReader();

                if (dr.Read())
                {
                    OleDbCommand arttir = new OleDbCommand("update urunler set adet=adet+'" + Convert.ToInt32(textBox3.Text) + "' where urunAdi='" + textBox1.Text + "' and marka='" + textBox2.Text + "' and seriNo='" + textBox6.Text + "'", baglanti);
                    arttir.ExecuteNonQuery();
                    textBox1.Items.Clear();
                    textBox2.Items.Clear();
                    textBox3.Clear();
                    MessageBox.Show("Ürün eklendi.", "Bildiri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    OleDbCommand sec = new OleDbCommand("select count(kayitNo) from urunler where kayitNo='" + kayitNo + "'", baglanti);
                    int sayi2 = (int)sec.ExecuteScalar();
                    while (true)
                    {
                        if (sayi2 != 0)
                        {

                            kayitNo = sayi.Next(1000, 9999);
                        }
                        else
                        {
                            break;
                        }

                    }
                    detay = girisForm.kulAdi + ", " + textBox1.Text + " ürününü ekledi.";


                    OleDbCommand ekle = new OleDbCommand("insert into urunler(urunAdi,marka,adet,islemTarihi,seriNo,detay,kayitNo) values('" + textBox1.Text + "','" + textBox2.Text + "','" + Convert.ToInt32(textBox3.Text) + "','" + DateTime.Now + "','" + textBox6.Text + "', '" + richTextBox1.Text + "','" + kayitNo.ToString() + "')", baglanti);
                    OleDbCommand islemEkle = new OleDbCommand("insert into islemler(islem, islemTarihi, kullanici,detay) values('Ürün ekleme','" + DateTime.Now + "','" + girisForm.kulAdi + "','" + detay + "')", baglanti);
                    ekle.ExecuteNonQuery();
                    islemEkle.ExecuteNonQuery();
                    baglanti.Close();
                    textBox1.Items.Clear();
                    textBox2.Items.Clear();
                    textBox3.Clear();
                    textBox6.Clear();
                    MessageBox.Show("Ürün eklendi.", "Bildiri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Items.Clear();
            textBox2.Items.Clear();
            textBox3.Clear();
            textBox6.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsPunctuation(e.KeyChar);
        }

        bool formTasiniyor = false;
        Point baslangicNoktasi = new Point(0, 0);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            formTasiniyor = true;
            baslangicNoktasi = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (formTasiniyor)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.baslangicNoktasi.X, p.Y - this.baslangicNoktasi.Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            formTasiniyor = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button11_MouseEnter(object sender, EventArgs e)
        {
            button11.BackColor = Color.FromArgb(196, 0, 0);
        }

        private void button11_MouseLeave(object sender, EventArgs e)
        {
            button11.BackColor = Color.White;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.Black;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Black;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(64, 64, 64);
        }
    }
}
