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
    public partial class urunDuzenleForm : Form
    {
        public urunDuzenleForm()
        {
            InitializeComponent();
        }

        string detay;
        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Columns[0].Width = 80;
            listView1.Columns[1].Width = 80;
            listView1.Columns[2].Width = 40;
            listView1.Columns[3].Width = 120;
            listView1.Columns[4].Width = 60;
            listView1.Columns[5].Width = 207;
            listView1.Columns[6].Width = 65;

            string sorgu = "";
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "")
            {
                sorgu = "select * from urunler";
            }
            else if (textBox1.Text == "" && textBox2.Text == "")
            {
                sorgu = "select * from urunler where detay like '%" + textBox3.Text + "%'";
            }
            else if (textBox1.Text == "" && textBox3.Text == "")
            {
                sorgu = "select * from urunler where marka like '%" + textBox2.Text + "%'";
            }
            else if (textBox2.Text == "" && textBox3.Text == "")
            {
                sorgu = "select * from urunler where urunAdi like '%" + textBox1.Text + "%'";
            }
            else if (textBox1.Text == "")
            {
                sorgu = "select * from urunler where marka like '%" + textBox2.Text + "%' and detay like '%" + textBox3.Text + "%'";
            }
            else if (textBox2.Text == "")
            {
                sorgu = "select * from urunler where urunAdi like '%" + textBox1.Text + "%' and detay like '%" + textBox3.Text + "%'";
            }
            else if (textBox3.Text == "")
            {
                sorgu = "select * from urunler where urunAdi like '%" + textBox1.Text + "%' and marka like '%" + textBox2.Text + "%'";
            }
            else
            {
                sorgu = "select * from urunler where urunAdi='" + textBox1.Text + "'% and marka='" + textBox2.Text + "' and detay='" + textBox3.Text + "'";
            }

            int sayi = 0;
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
                item.SubItems.Add(dr["islemTarihi"].ToString());
                item.SubItems.Add(dr["seriNo"].ToString());
                item.SubItems.Add(dr["detay"].ToString());
                item.SubItems.Add(dr["teslimEdilenAdet"].ToString());
                item.SubItems.Add(dr["kayitNo"].ToString());
                item.SubItems.Add(sayi.ToString());
                listView1.Items.Add(item);
                sayi++;
            }
            label4.Text = sayi.ToString();
            dr.Close();
            baglanti.Close();
        }

        string kayitNo, teslimEdilen;

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
            baglanti.Open();

            OleDbCommand kontrol = new OleDbCommand("select teslimEdilenAdet from urunler where kayitNo='" + kayitNo + "'", baglanti);
            OleDbDataReader drkontrol = kontrol.ExecuteReader();

            if (drkontrol.Read() && Convert.ToInt32(textBox7.Text) < Convert.ToInt32(drkontrol["teslimEdilenAdet"]))
            {
                MessageBox.Show("Kullanımda olan ürünler var.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                OleDbCommand guncelle = new OleDbCommand("update urunler set urunAdi='" + textBox12.Text + "',marka='" + textBox11.Text + "',adet='" + Convert.ToInt32(textBox7.Text) + "',seriNo='" + textBox8.Text + "',detay='" + richTextBox1.Text + "' where kayitNo='" + kayitNo + "'", baglanti);
                guncelle.ExecuteNonQuery();

                OleDbCommand guncelleTE = new OleDbCommand("update teslimEdilen set urunAdi='" + textBox12.Text + "',seriNo='" + textBox8.Text + "' where kayitNo='" + kayitNo + "'", baglanti);
                guncelleTE.ExecuteNonQuery();

                OleDbCommand islemEkle = new OleDbCommand("insert into islemler(islem, islemTarihi, kullanici,detay) values('Ürün ekleme','" + DateTime.Now + "','" + girisForm.kulAdi + "','" + detay + "')", baglanti);
                islemEkle.ExecuteNonQuery();
                listView1.Items.Clear();

                OleDbCommand komut = new OleDbCommand("select * from urunler", baglanti);
                komut.ExecuteNonQuery();
                OleDbDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["urunAdi"].ToString());
                    item.SubItems.Add(dr["marka"].ToString());
                    item.SubItems.Add(dr["adet"].ToString());
                    item.SubItems.Add(dr["islemTarihi"].ToString());
                    item.SubItems.Add(dr["seriNo"].ToString());
                    item.SubItems.Add(dr["detay"].ToString());
                    item.SubItems.Add(dr["teslimEdilenAdet"].ToString());
                    item.SubItems.Add(dr["kayitNo"].ToString());
                    listView1.Items.Add(item);
                }
                dr.Close();

                baglanti.Close();
                MessageBox.Show("Ürün bilgileri güncellendi!", "GÜNCELLENDİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsPunctuation(e.KeyChar);
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsPunctuation(e.KeyChar);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
            baglanti.Open();
            OleDbCommand kontrol = new OleDbCommand("select teslimEdilenAdet from urunler where kayitNo='" + kayitNo + "'", baglanti);
            OleDbDataReader dr = kontrol.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr["teslimEdilenAdet"]) > 0)
                {
                    MessageBox.Show("Kullanımda olan ürünler var!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult secenek = MessageBox.Show("Ürünü silmek  istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo);
                    if (secenek == DialogResult.Yes)
                    {

                        OleDbCommand komut = new OleDbCommand("delete from urunler where kayitNo='" + kayitNo + "' ", baglanti);
                        komut.ExecuteNonQuery();

                        listView1.Items.Clear();
                        OleDbCommand listele = new OleDbCommand("select * from urunler", baglanti);
                        OleDbDataReader getir = listele.ExecuteReader();
                        while (getir.Read())
                        {
                            ListViewItem item = new ListViewItem(getir["urunAdi"].ToString());
                            item.SubItems.Add(getir["marka"].ToString());
                            item.SubItems.Add(getir["adet"].ToString());
                            item.SubItems.Add(getir["islemTarihi"].ToString());
                            item.SubItems.Add(getir["seriNo"].ToString());
                            item.SubItems.Add(getir["detay"].ToString());
                            item.SubItems.Add(getir["teslimEdilenAdet"].ToString());
                            item.SubItems.Add(getir["kayitNo"].ToString());
                            listView1.Items.Add(item);
                        }
                        getir.Close();
                        dr.Close();
                        baglanti.Close();
                        MessageBox.Show("Ürün silindi.", "Bildiri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (secenek == DialogResult.No)
                    {

                    }
                }
            }
        }

        bool formTasiniyor;
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                button2.Enabled = true;
                button1.Enabled = true;
                textBox12.Text = listView1.SelectedItems[0].SubItems[0].Text;
                textBox11.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox7.Text = listView1.SelectedItems[0].SubItems[2].Text;
                //textBox10.Text = listView1.SelectedItems[0].SubItems[3].Text;
                textBox8.Text = listView1.SelectedItems[0].SubItems[4].Text;
                richTextBox1.Text = listView1.SelectedItems[0].SubItems[5].Text;
                teslimEdilen = listView1.SelectedItems[0].SubItems[6].Text;
                kayitNo = listView1.SelectedItems[0].SubItems[7].Text;

                detay = listView1.SelectedItems[0].SubItems[0].Text + " ürünü düzenlendi.";
            }
        }
    }
}
