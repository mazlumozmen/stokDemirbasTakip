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
    public partial class girisForm : Form
    {
        public static string kullanici;
        public static string kulAdi;
        public static string sifre;
        public static string yetki;
        public girisForm()
        {
            InitializeComponent();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void girisForm_Load(object sender, EventArgs e)
        {
            
            
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            bool res = base.ProcessCmdKey(ref msg, keyData);
            return res;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * from kullanicilar where kuladi='" + textBox1.Text + "' and sifre='" + textBox2.Text + "'", baglanti);
            komut.ExecuteNonQuery();
            OleDbDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                if (dr["yetki"].ToString() == "admin")
                {
                    int deger = String.Compare(textBox2.Text, dr["sifre"].ToString());
                    if (deger == 0)
                    {
                        kullanici = dr["ad"].ToString() + " " + dr["soyad"].ToString();
                        kulAdi = dr["kulAdi"].ToString();
                        sifre = textBox2.Text;

                        baglanti.Close();
                        Form1 ac = new Form1();
                        this.Hide();
                        ac.Show();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    yetki = "kullanici";

                    int deger = String.Compare(textBox2.Text, dr["sifre"].ToString());
                    if (deger == 0)
                    {
                        kullanici = dr["ad"].ToString() + " " + dr["soyad"].ToString();
                        kulAdi = dr["kulAdi"].ToString();
                        sifre = textBox2.Text;

                        baglanti.Close();
                        Form1 ac = new Form1();
                        this.Hide();
                        ac.Show();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dr.Close();
            baglanti.Close();

            baglanti.Dispose();
            komut.Dispose();
            
        }

        bool formTasiniyor2 = false;
        Point baslangicNoktasi2 = new Point(0, 0);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            formTasiniyor2 = true;
            baslangicNoktasi2 = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (formTasiniyor2)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.baslangicNoktasi2.X, p.Y - this.baslangicNoktasi2.Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            formTasiniyor2 = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button11_MouseEnter(object sender, EventArgs e)
        {
            button11.BackColor = Color.FromArgb(192,0,0);
        }

        private void button11_MouseLeave(object sender, EventArgs e)
        {
            button11.BackColor = Color.White;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Black;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(64,64,64);
        }
    }
}
