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
    public partial class sifreDegisForm : Form
    {
        public sifreDegisForm()
        {
            InitializeComponent();
        }

        bool formTasiniyor;
        Point baslangicNoktasi = new Point(0, 0);
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            formTasiniyor = true;
            baslangicNoktasi = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (formTasiniyor)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.baslangicNoktasi.X, p.Y - this.baslangicNoktasi.Y);
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            formTasiniyor = false;
        }

        private void button11_MouseEnter(object sender, EventArgs e)
        {
            button11.BackColor = Color.FromArgb(196, 0, 0);
        }

        private void button11_MouseLeave(object sender, EventArgs e)
        {
            button11.BackColor = Color.White;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Black;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void sifreDegisForm_Load(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != textBox3.Text)
            {
                label4.ForeColor = Color.IndianRed;
                label4.Text = "Şifreler aynı değil";
            }
            else
            {
                label4.ForeColor = Color.ForestGreen;
                label4.Text = "Şifreler aynı";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != textBox3.Text)
            {
                label4.ForeColor = Color.IndianRed;
                label4.Text = "Şifreler aynı değil";
            }
            else
            {
                label4.ForeColor = Color.ForestGreen;
                label4.Text = "Şifreler aynı";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.PasswordChar = '\0';
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';
            }
            else if (checkBox1.Checked == false)
            {
                textBox1.PasswordChar = '*';
                textBox2.PasswordChar = '*';
                textBox3.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Alanlar boş bırakılamaz.","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                if (girisForm.sifre == textBox1.Text)
                {
                    OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
                    baglanti.Open();
                    OleDbCommand degis = new OleDbCommand("update kullanicilar set sifre='" + textBox2.Text + "' where kuladi='" + girisForm.kulAdi + "' and sifre='" + textBox1.Text + "'", baglanti);
                    degis.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Şifre Değiştirildi.", "BAŞARILI",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
                else
                {
                    MessageBox.Show("Eski şifre yanlış.", "Hata",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                }

            }
        }
    }
}
