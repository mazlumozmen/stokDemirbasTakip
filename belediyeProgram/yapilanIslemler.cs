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
    public partial class yapilanIslemler : Form
    {
        public static string sorgu="select * from islemler";
        public yapilanIslemler()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Columns[0].Width = 120;
            listView1.Columns[1].Width = 120;
            listView1.Columns[2].Width = 120;
            listView1.Columns[3].Width = 270;

            int kayit = 0;
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "")
            {
                sorgu = "select * from islemler";
            }
            else if (textBox1.Text == "" && textBox2.Text == "")
            {
                sorgu = "select * from islemler where kullanici like '%" + textBox3.Text + "%'";
            }
            else if (textBox1.Text == "" && textBox3.Text == "")
            {
                sorgu = "select * from islemler where islemTarihi like '%" + textBox2.Text + "%'";
            }
            else if (textBox2.Text == "" && textBox3.Text == "")
            {
                sorgu = "select * from islemler where islem like '%" + textBox1.Text + "%'";
            }
            else if (textBox1.Text == "")
            {
                sorgu = "select * from islemler where islemTarihi like '%" + textBox2.Text + "%' and kullanici like '%" + textBox3.Text + "%'";
            }
            else if (textBox2.Text == "")
            {
                sorgu = "select * from islemler where islem like '%" + textBox1.Text + "%' and kullanici like '%" + textBox3.Text + "%'";
            }
            else if (textBox3.Text == "")
            {
                sorgu = "select * from islemler where islem like '%" + textBox1.Text + "%' and islemTarihi like '%" + textBox2.Text + "%'";
            }
            else
            {
                sorgu = "select * from islemler where islem='" + textBox1.Text + "'% and islemTarihi='" + textBox2.Text + "' and kullanici='" + textBox3.Text + "'";
            }

            listView1.Items.Clear();
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {

                ListViewItem item = new ListViewItem(dr["islem"].ToString());
                item.SubItems.Add(dr["islemTarihi"].ToString());
                item.SubItems.Add(dr["kullanici"].ToString());
                item.SubItems.Add(dr["detay"].ToString());
                listView1.Items.Add(item);
                kayit++;

            }
            dr.Close();
            baglanti.Close();
            label10.Text = kayit.ToString();
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

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Black;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button11_MouseEnter(object sender, EventArgs e)
        {
            button11.BackColor = Color.FromArgb(192, 0, 0);
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("İşlem seçmediniz!", "UYARI",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            }
            else
            {
                DialogResult secenek = MessageBox.Show("Bu kaydı silmek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (secenek == DialogResult.Yes)
                {
                    OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
                    baglanti.Open();
                    OleDbCommand sil = new OleDbCommand("delete from islemler where islemTarihi='" + islemTarihi + "' and kullanici='" + kullanici + "' and islem='" + islem + "'", baglanti);
                    sil.ExecuteNonQuery();
                    MessageBox.Show("Kayıt silindi.","Yapılan İşlemler",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    baglanti.Close();
                }
            }
        }
        string islem, kullanici, islemTarihi, detay;

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = Color.Black;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void yapilanIslemler_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            raporYI ac = new raporYI();
            ac.ShowDialog();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                islem = listView1.SelectedItems[0].SubItems[0].Text;
                islemTarihi = listView1.SelectedItems[0].SubItems[1].Text;
                kullanici = listView1.SelectedItems[0].SubItems[2].Text;
                detay= listView1.SelectedItems[0].SubItems[3].Text;
                sorgu = "select * from islemler where islem='" + listView1.SelectedItems[0].SubItems[0].Text + "' and islemTarihi='"+ listView1.SelectedItems[0].SubItems[1].Text + "' and kullanici='"+ listView1.SelectedItems[0].SubItems[2].Text + "' and detay='"+ listView1.SelectedItems[0].SubItems[3].Text +"'";

            }
        }
    }
}
