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
    public partial class teslimEtForm : Form
    {
        public string kayitNo;
        public int teslimEdilenAdet;
        public int adet;
        public teslimEtForm()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Columns[0].Width = 100;
            listView1.Columns[1].Width = 80;
            listView1.Columns[2].Width = 50;
            listView1.Columns[3].Width = 100;
            listView1.Columns[4].Width = 220;
            listView1.Columns[5].Width = 65;


            string sorgu = "";
            if (textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "")
            {
                sorgu = "select * from urunler";
            }
            else if (textBox4.Text == "" && textBox5.Text == "")
            {
                sorgu = "select * from urunler where seriNo like '%" + textBox6.Text + "%'";
            }
            else if (textBox4.Text == "" && textBox6.Text == "")
            {
                sorgu = "select * from urunler where marka like '%" + textBox5.Text + "%'";
            }
            else if (textBox5.Text == "" && textBox6.Text == "")
            {
                sorgu = "select * from urunler where urunAdi like '%" + textBox4.Text + "%'";
            }
            else if (textBox4.Text == "")
            {
                sorgu = "select * from urunler where marka like '%" + textBox5.Text + "%' and seriNo like '%" + textBox6.Text + "%'";
            }
            else if (textBox5.Text == "")
            {
                sorgu = "select * from urunler where urunAdi like '%" + textBox4.Text + "%' and seriNo like '%" + textBox6.Text + "%'";
            }
            else if (textBox6.Text == "")
            {
                sorgu = "select * from urunler where urunAdi like '%" + textBox4.Text + "%' and marka like '%" + textBox5.Text + "%'";
            }
            else
            {
                sorgu = "select * from urunler where urunAdi like '%" + textBox4.Text + "%' and marka like '%" + textBox5.Text + "%' and seriNo like '%" + textBox6.Text + "%'";
            }




            listView1.Items.Clear();
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            komut.ExecuteNonQuery();
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem item = new ListViewItem(dr["urunAdi"].ToString());
                item.SubItems.Add(dr["marka"].ToString());
                item.SubItems.Add(dr["adet"].ToString());
                item.SubItems.Add(dr["seriNo"].ToString());
                item.SubItems.Add(dr["detay"].ToString());
                item.SubItems.Add(dr["teslimEdilenAdet"].ToString());
                item.SubItems.Add(dr["kayitNo"].ToString());
                listView1.Items.Add(item);
            }
            dr.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                textBox4.Text = listView1.SelectedItems[0].SubItems[0].Text;
                textBox5.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox6.Text = listView1.SelectedItems[0].SubItems[3].Text;
                label11.Text = listView1.SelectedItems[0].SubItems[0].Text;
                label12.Text = listView1.SelectedItems[0].SubItems[3].Text;
                label13.Text = listView1.SelectedItems[0].SubItems[1].Text;
                kayitNo = listView1.SelectedItems[0].SubItems[6].Text;
                teslimEdilenAdet = Convert.ToInt32(listView1.SelectedItems[0].SubItems[5].Text);
                adet = Convert.ToInt32(listView1.SelectedItems[0].SubItems[2].Text);
            }
        }

        string detay;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox7.Text == "")
            {
                MessageBox.Show("Alanlar boş geçilemez!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (teslimEdilenAdet == adet)
                {
                    MessageBox.Show("Bu ürünlerin hepsi kullanımda.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    DialogResult secenek = MessageBox.Show("Ürünü teslim etmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (secenek == DialogResult.Yes)
                    {
                        OleDbConnection baglanti2 = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
                        baglanti2.Open();
                        OleDbCommand guncelle = new OleDbCommand("update urunler set teslimEdilenAdet='" + (teslimEdilenAdet + 1) + "' where kayitNo='" + kayitNo + "'", baglanti2);
                        guncelle.ExecuteNonQuery();
                        OleDbCommand komut2 = new OleDbCommand("insert into teslimEdilen(personelAdi, personelSoyAdi, calistigiDepartman, teslimTarihi, urunAdi, seriNo,telefonNo,teslimEdenKulAdi,kayitNo) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + DateTime.Now + "','" + label11.Text + "','" + label12.Text + "','" + textBox7.Text + "','" + girisForm.kulAdi + "','" + kayitNo + "')", baglanti2);
                        komut2.ExecuteNonQuery();

                        detay = textBox1.Text + " " + textBox2.Text + ", " + label11.Text + " teslim edildi";

                        OleDbCommand islemEkle = new OleDbCommand("insert into islemler(islem, islemTarihi, kullanici,detay) values('Teslim etme','" + DateTime.Now + "','" + girisForm.kulAdi + "','" + detay + "')", baglanti2);
                        islemEkle.ExecuteNonQuery();
                        baglanti2.Close();
                        MessageBox.Show("Ürün teslim edildi.", "Bildiri", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
        }


        bool formTasiniyor;
        Point baslangicNoktasi = new Point(0, 0);
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            formTasiniyor = true;
            baslangicNoktasi = new Point(e.X, e.Y);
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            formTasiniyor = false;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (formTasiniyor)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.baslangicNoktasi.X, p.Y - this.baslangicNoktasi.Y);
            }
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
