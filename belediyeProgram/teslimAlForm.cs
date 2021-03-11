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
    public partial class teslimAlForm : Form
    {
        public static string pAd;
        public static string pSoyad;
        public static string departman;
        public static string teslimTarihi;
        public static string urunAdi;
        public static string seriNo;
        public static string telNo;
        public static string teslimEden;
        public teslimAlForm()
        {
            InitializeComponent();
        }

        public string kayitNo;
        public string sorgu="select * from teslimEdilen";

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Columns[0].Width = 100;
            listView1.Columns[1].Width = 100;
            listView1.Columns[2].Width = 100;
            listView1.Columns[3].Width = 120;
            listView1.Columns[4].Width = 100;
            listView1.Columns[5].Width = 80;
            listView1.Columns[6].Width = 100;
            listView1.Columns[7].Width = 70;
            int kayit = 0;
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "")
            {
                sorgu = "select * from teslimEdilen";
            }
            else if (textBox1.Text == "" && textBox2.Text == "")
            {
                sorgu = "select * from teslimEdilen where telefonNo like '%" + textBox3.Text + "%'";
            }
            else if (textBox1.Text == "" && textBox3.Text == "")
            {
                sorgu = "select * from teslimEdilen where personelSoyAdi like '%" + textBox2.Text + "%'";
            }
            else if (textBox2.Text == "" && textBox3.Text == "")
            {
                sorgu = "select * from teslimEdilen where personelAdi like '%" + textBox1.Text + "%'";
            }
            else if (textBox1.Text == "")
            {
                sorgu = "select * from teslimEdilen where personelSoyAdi like '%" + textBox2.Text + "%' and telefonNo like '%" + textBox3.Text + "%'";
            }
            else if (textBox2.Text == "")
            {
                sorgu = "select * from teslimEdilen where personelAdi like '%" + textBox1.Text + "%' and telefonNo like '%" + textBox3.Text + "%'";
            }
            else if (textBox3.Text == "")
            {
                sorgu = "select * from urunler where personelAdi like '%" + textBox1.Text + "%' and personelSoyAdi like '%" + textBox2.Text + "%'";
            }
            else
            {
                sorgu = "select * from teslimEdilen where personelAdi like '%" + textBox1.Text + "%' and personelSoyAdi like '%" + textBox2.Text + "%' and telefonNo like '%" + textBox3.Text + "%'";
            }



            listView1.Items.Clear();
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            komut.ExecuteNonQuery();
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                detay = dr["personelAdi"].ToString() + " " + dr["personelSoyAdi"].ToString() + ", " + dr["urunAdi"].ToString() + " ürününü teslim aldı.";
                ListViewItem item = new ListViewItem(dr["personelAdi"].ToString());
                item.SubItems.Add(dr["personelSoyAdi"].ToString());
                item.SubItems.Add(dr["calistigiDepartman"].ToString());
                item.SubItems.Add(dr["teslimTarihi"].ToString());
                item.SubItems.Add(dr["urunAdi"].ToString());
                item.SubItems.Add(dr["seriNo"].ToString());
                item.SubItems.Add(dr["telefonNo"].ToString());
                item.SubItems.Add(dr["teslimEdenKulAdi"].ToString());
                item.SubItems.Add(dr["kayitNo"].ToString());
                listView1.Items.Add(item);
                kayit++;
            }
            dr.Close();
            baglanti.Close();
            label10.Text = kayit.ToString();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                pAd = listView1.SelectedItems[0].SubItems[0].Text;
                pSoyad = listView1.SelectedItems[0].SubItems[1].Text;
                departman = listView1.SelectedItems[0].SubItems[2].Text;
                teslimTarihi = listView1.SelectedItems[0].SubItems[3].Text;
                urunAdi = listView1.SelectedItems[0].SubItems[4].Text;
                seriNo = listView1.SelectedItems[0].SubItems[5].Text;
                telNo = listView1.SelectedItems[0].SubItems[6].Text;
                teslimEden = listView1.SelectedItems[0].SubItems[7].Text;
                kayitNo = listView1.SelectedItems[0].SubItems[8].Text;
            }
        }

        string detay;
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
                baglanti.Open();
                OleDbCommand guncelle = new OleDbCommand("update urunler set teslimEdilenAdet=teslimEdilenAdet-1 where kayitNo='" + kayitNo + "'", baglanti);
                guncelle.ExecuteNonQuery();

                OleDbCommand teslimAl = new OleDbCommand("insert into teslimAlinanlar(personelAdi, personelSoyAdi, calistigiDepartman, teslimTarihi, urunAdi, seriNo,telefonNo,teslimEdenKulAdi,teslimAlmaTarihi,kayitNo) values('" + pAd + "','" + pSoyad + "','" + departman + "','" + teslimTarihi + "','" + urunAdi + "','" + seriNo + "','" + telNo + "','" + teslimEden + "','" + DateTime.Now.ToString() + "','" + kayitNo + "')", baglanti);
                teslimAl.ExecuteNonQuery();

                OleDbCommand islemEkle = new OleDbCommand("insert into islemler(islem, islemTarihi, kullanici,detay) values('Teslim alma','" + DateTime.Now + "','" + girisForm.kulAdi + "','" + detay + "')", baglanti);
                islemEkle.ExecuteNonQuery();

                OleDbCommand sil = new OleDbCommand("delete from teslimEdilen where personelAdi='" + pAd + "' and personelSoyAdi='" + pSoyad + "' and teslimTarihi='" + teslimTarihi + "' and teslimEdenKulAdi='" + teslimEden + "' and kayitNo='" + kayitNo + "'", baglanti);
                sil.ExecuteNonQuery();


                int kayit = 0;
                OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
                komut.ExecuteNonQuery();
                OleDbDataReader dr = komut.ExecuteReader();
                listView1.Items.Clear();
                while (dr.Read())
                {
                    detay = dr["personelAdi"].ToString() + " " + dr["personelSoyAdi"].ToString() + ", " + dr["urunAdi"].ToString() + " ürününü teslim aldı.";
                    ListViewItem item = new ListViewItem(dr["personelAdi"].ToString());
                    item.SubItems.Add(dr["personelSoyAdi"].ToString());
                    item.SubItems.Add(dr["calistigiDepartman"].ToString());
                    item.SubItems.Add(dr["teslimTarihi"].ToString());
                    item.SubItems.Add(dr["urunAdi"].ToString());
                    item.SubItems.Add(dr["seriNo"].ToString());
                    item.SubItems.Add(dr["telefonNo"].ToString());
                    item.SubItems.Add(dr["teslimEdenKulAdi"].ToString());
                    item.SubItems.Add(dr["kayitNo"].ToString());
                    listView1.Items.Add(item);
                    kayit++;
                }
                dr.Close();
                baglanti.Close();

                MessageBox.Show("Ürün teslim alındı.", "Bildiri", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.BackColor = Color.Black;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackColor = Color.FromArgb(64, 64, 64);
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
