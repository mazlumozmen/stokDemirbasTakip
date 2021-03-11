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
    public partial class Form1 : Form
    {
        public string urunEkle, urunDuzenle, teslimEt, teslimAl, teslimEdilenler, islemler;

        public static string sorgu = "select * from urunler";

        private void button11_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            girisForm ac = new girisForm();
            this.Close();
            ac.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            urunDuzenleForm ac = new urunDuzenleForm();
            ac.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            teslimEtForm ac = new teslimEtForm();
            ac.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            teslimAlForm ac = new teslimAlForm();
            ac.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            teslimVerilenForm ac = new teslimVerilenForm();
            ac.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            yapilanIslemler ac = new yapilanIslemler();
            ac.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            sifreDegisForm ac = new sifreDegisForm();
            ac.ShowDialog();
        }

        public string urunAdi, marka, kayitNo;

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                teslimEt = "var";
            }
            else if (checkBox4.Checked == false)
            {
                teslimEt = "yok";
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                islemler = "var";
            }
            else if (checkBox5.Checked == false)
            {
                islemler = "yok";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                teslimAl = "var";
            }
            else if (checkBox3.Checked == false)
            {
                teslimAl = "yok";
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                teslimEdilenler = "var";
            }
            else if (checkBox6.Checked == false)
            {
                teslimEdilenler = "yok";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                urunEkle = "var";
            }
            else if (checkBox1.Checked == false)
            {
                urunEkle = "yok";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            seciliKullanici = comboBox1.Text;
            urunEkle = "yok";
            urunDuzenle = "yok";
            teslimAl = "yok";
            teslimEdilenler = "yok";
            teslimEt = "yok";
            islemler = "yok";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
            baglanti.Open();

            OleDbCommand kul = new OleDbCommand("select * from kullanicilar", baglanti);
            OleDbDataReader oku = kul.ExecuteReader();

            while (oku.Read())
            {

                if (oku["ad"].ToString() == comboBox1.Text)
                {
                    if (oku["urunEkle"].ToString() == "var")
                    {
                        checkBox1.Checked = true;
                    }
                    if (oku["urunDuzenle"].ToString() == "var")
                    {
                        checkBox2.Checked = true;
                    }
                    if (oku["teslimAl"].ToString() == "var")
                    {
                        checkBox3.Checked = true;
                    }
                    if (oku["teslimEt"].ToString() == "var")
                    {
                        checkBox4.Checked = true;
                    }
                    if (oku["islemler"].ToString() == "var")
                    {
                        checkBox5.Checked = true;
                    }
                    if (oku["teslimEdilenler"].ToString() == "var")
                    {
                        checkBox6.Checked = true;
                    }
                }

            }
            oku.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Kullanıcı seçmediniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                baglanti.Open();
                OleDbCommand guncelle = new OleDbCommand("update kullanicilar set urunEkle='" + urunEkle + "', urunDuzenle='" + urunDuzenle + "',teslimEt='" + teslimEt + "',teslimAl='" + teslimAl + "',teslimEdilenler='" + teslimEdilenler + "',islemler='" + islemler + "' where ad='" + seciliKullanici + "'", baglanti);
                guncelle.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Yetkiler güncellendi.", "BİLGİ",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip2.Show(Cursor.Position);
                }
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

        private void button13_Click(object sender, EventArgs e)
        {
            rapor ac = new rapor();
            ac.ShowDialog();
        }

        private void button11_MouseEnter(object sender, EventArgs e)
        {
            button11.BackColor = Color.FromArgb(192,0,0);
        }

        private void button11_MouseLeave(object sender, EventArgs e)
        {
            button11.BackColor = Color.White;
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            urunDuzenleForm ac = new urunDuzenleForm();


            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * from urunler where kayitNo='" + kayitNo + "'", baglanti);
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
                ac.listView1.Items.Add(item);
            }
            dr.Close();
            baglanti.Close();

            ac.ShowDialog();
        }

        private void teslimEtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            teslimEtForm ac = new teslimEtForm();

            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * from urunler where kayitNo='" + kayitNo + "'", baglanti);
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
                ac.listView1.Items.Add(item);
            }
            dr.Close();
            baglanti.Close();

            ac.ShowDialog();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            urunDuzenleForm ac = new urunDuzenleForm();

            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * from urunler where kayitNo='" + kayitNo + "'", baglanti);
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
                ac.listView1.Items.Add(item);
            }
            dr.Close();
            baglanti.Close();


            ac.ShowDialog();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Black;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(64,64,64);
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.BackColor = Color.Black;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.Black;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = Color.Black;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button8_MouseEnter(object sender, EventArgs e)
        {
            button8.BackColor = Color.Black;
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            button8.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {
            button7.BackColor = Color.Black;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            button7.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.BackColor = Color.Black;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackColor = Color.FromArgb(64, 64, 64);

        }

        private void button12_MouseEnter(object sender, EventArgs e)
        {
            button12.BackColor = Color.Black;
        }

        private void button12_MouseLeave(object sender, EventArgs e)
        {
            button12.BackColor = Color.FromArgb(64,64,64);
        }

        private void button9_MouseEnter(object sender, EventArgs e)
        {
            button9.BackColor = Color.Black;
        }

        private void button9_MouseLeave(object sender, EventArgs e)
        {
            button9.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button13_MouseEnter(object sender, EventArgs e)
        {
            button13.BackColor = Color.Black;
        }

        private void button13_MouseLeave(object sender, EventArgs e)
        {
            button13.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button8_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                urunDuzenle = "var";
            }
            else if (checkBox2.Checked == false)
            {
                urunDuzenle = "yok";
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                sorgu = "select * from urunler where kayitNo='" + listView1.SelectedItems[0].SubItems[7].Text + "'";
                kayitNo = listView1.SelectedItems[0].SubItems[7].Text;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            treeView2.Nodes.Clear();
            label2.Text = "Toplam Ürün Sayısı:";
            label3.Text = "Kullanımda Olan Ürün Sayısı:";

            listView1.Columns[0].Width = 91;
            listView1.Columns[1].Width = 80;
            listView1.Columns[2].Width = 40;
            listView1.Columns[3].Width = 120;
            listView1.Columns[4].Width = 70;
            listView1.Columns[5].Width = 249;
            listView1.Columns[6].Width = 97;


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

            int toplam = 0;
            int toplam2 = 0;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
            baglanti.Open();
            OleDbCommand say = new OleDbCommand("select adet,teslimEdilenAdet from urunler", baglanti);
            OleDbDataReader oku2 = say.ExecuteReader();
            while (oku2.Read())
            {

                toplam = toplam + Convert.ToInt32(oku2["adet"]);
                toplam2 = toplam2 + Convert.ToInt32(oku2["teslimEdilenAdet"]);
            }
            oku2.Close(); baglanti.Close();

            label2.Text = toplam.ToString();
            label3.Text = toplam2.ToString();


            listView1.Items.Clear();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            komut.ExecuteNonQuery();
            OleDbDataReader dr = komut.ExecuteReader();
            int kayit = 0;
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
                kayit++;
            }
            dr.Close();
            label10.Text = kayit.ToString();


            OleDbCommand sorgu1 = new OleDbCommand("select distinct calistigiDepartman from teslimEdilen", baglanti);
            OleDbDataReader dr1 = sorgu1.ExecuteReader();
            while (dr1.Read())
            {
                TreeNode node = new TreeNode(dr1["calistigiDepartman"].ToString());
                treeView2.Nodes.Add(node);

            }

            OleDbCommand sorgu3 = new OleDbCommand("select distinct * from teslimEdilen where urunAdi='Bilgisayar'", baglanti);
            OleDbDataReader rd = sorgu3.ExecuteReader();

            while (rd.Read())
            {
                for (int i = 0; i < treeView2.Nodes.Count; i++)
                {
                    if (treeView2.Nodes[i].Text == rd["calistigiDepartman"].ToString())
                    {
                        treeView2.Nodes[i].Nodes.Add(rd["personelAdi"].ToString() + " " + rd["personelSoyAdi"].ToString());
                        OleDbCommand sorgu4 = new OleDbCommand("select detay from urunler where kayitNo='" + rd["kayitNo"].ToString() + "'", baglanti);
                        OleDbDataReader rd2 = sorgu4.ExecuteReader();
                        if (rd2.Read())
                        {
                            for (int j = 0; j < treeView2.Nodes[i].Nodes.Count; j++)
                            {
                                if (treeView2.Nodes[i].Nodes[j].Text == rd["personelAdi"].ToString() + " " + rd["personelSoyAdi"].ToString())
                                {
                                    treeView2.Nodes[i].Nodes[j].Nodes.Add(rd2["detay"].ToString());

                                }
                            }


                        }
                        rd2.Close();
                    }


                }
            }
            dr1.Close(); rd.Close(); baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            urunEkle ac = new urunEkle();
            ac.ShowDialog();
        }

        public string seciliKullanici;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int toplam = 0;
            int toplam2 = 0;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
            baglanti.Open();
            OleDbCommand say = new OleDbCommand("select adet,teslimEdilenAdet from urunler", baglanti);
            OleDbDataReader oku2 = say.ExecuteReader();
            while (oku2.Read())
            {

                toplam = toplam + Convert.ToInt32(oku2["adet"]);
                toplam2 = toplam2 + Convert.ToInt32(oku2["teslimEdilenAdet"]);
            }
            oku2.Close(); baglanti.Close();

            label2.Text = toplam.ToString();
            label3.Text = toplam2.ToString();
            if (girisForm.yetki == "kullanici")
            {

                baglanti.Open();
                OleDbCommand sorgu2 = new OleDbCommand("select * from kullanicilar where kuladi='" + girisForm.kulAdi + "'", baglanti);
                OleDbDataReader oku = sorgu2.ExecuteReader();
                if (oku.Read())
                {
                    if (oku["urunEkle"].ToString() == "yok")
                    {
                        button1.Enabled = false;
                    }
                    else if (oku["urunEkle"].ToString() == "var")
                    {
                        button1.Enabled = true;
                    }

                    if (oku["urunDuzenle"].ToString() == "yok")
                    {
                        button4.Enabled = false;
                        contextMenuStrip2.Items[0].Enabled = false;
                        contextMenuStrip2.Items[2].Enabled = false;
                    }
                    else if (oku["urunDuzenle"].ToString() == "var")
                    {
                        button4.Enabled = true;
                    }

                    if (oku["teslimEt"].ToString() == "yok")
                    {
                        button2.Enabled = false;
                        contextMenuStrip2.Items[1].Enabled = false;
                    }
                    else if (oku["teslimEt"].ToString() == "var")
                    {
                        button2.Enabled = true;

                    }

                    if (oku["teslimAl"].ToString() == "yok")
                    {
                        button3.Enabled = false;
                    }
                    else if (oku["teslimAl"].ToString() == "var")
                    {
                        button3.Enabled = true;
                    }

                    if (oku["teslimEdilenler"].ToString() == "yok")
                    {
                        button8.Enabled = false;
                    }
                    else if (oku["teslimEdilenler"].ToString() == "var")
                    {
                        button8.Enabled = true;
                    }

                    if (oku["islemler"].ToString() == "yok")
                    {
                        button7.Enabled = false;
                    }
                    else if (oku["islemler"].ToString() == "var")
                    {
                        button7.Enabled = true;
                    }

                    groupBox3.Visible = false;
                }


            }
            else
            {
                groupBox2.Text = "Admin";
                baglanti.Open();

                OleDbCommand kul = new OleDbCommand("select * from kullanicilar", baglanti);
                OleDbDataReader oku = kul.ExecuteReader();
                while (oku.Read())
                {

                    if (oku["yetki"].ToString() == "kullanici")
                    {

                        comboBox1.Items.Add(oku["ad"].ToString());
                    }

                }
                oku.Close();
            }

           /* OleDbCommand sorgu = new OleDbCommand("select distinct calistigiDepartman from teslimEdilen", baglanti);
            OleDbDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                TreeNode node = new TreeNode(dr["calistigiDepartman"].ToString());
                treeView2.Nodes.Add(node);

            }*/

           /* OleDbCommand sorgu5 = new OleDbCommand("select kayitNo,detay  from urunler", baglanti);
            OleDbDataReader dr5 = sorgu.ExecuteReader();

            OleDbCommand sorguTW = new OleDbCommand("select distinct * from teslimEdilen", baglanti);
            OleDbDataReader dr2 = sorguTW.ExecuteReader();

            while (dr2.Read())
            {
                for (int i = 0; i < treeView2.Nodes.Count; i++)
                {
                    if (treeView2.Nodes[i].Text == dr2["calistigiDepartman"].ToString())
                    {

                        treeView2.Nodes[i].Nodes.Add(dr2["personelAdi"].ToString() + " " + dr2["personelSoyAdi"].ToString());
                        OleDbCommand sorgu4 = new OleDbCommand("select detay from urunler where kayitNo='" + dr["kayitNo"].ToString() + "'", baglanti);
                        OleDbDataReader rd2 = sorgu4.ExecuteReader();
                    }


                }
            }*/





             OleDbCommand sorgu = new OleDbCommand("select distinct calistigiDepartman from teslimEdilen", baglanti);
              OleDbDataReader dr = sorgu.ExecuteReader();
              while (dr.Read())
              {
                  TreeNode node = new TreeNode(dr["calistigiDepartman"].ToString());
                  treeView2.Nodes.Add(node);

              }

              OleDbCommand sorgu3 = new OleDbCommand("select * from teslimEdilen where urunAdi='Bilgisayar'", baglanti);
              OleDbDataReader rd = sorgu3.ExecuteReader();

              while (rd.Read())
              {
                  for (int i = 0; i < treeView2.Nodes.Count; i++)
                  {
                      if (treeView2.Nodes[i].Text == rd["calistigiDepartman"].ToString())
                      {
                          treeView2.Nodes[i].Nodes.Add(rd["personelAdi"].ToString() + " " + rd["personelSoyAdi"].ToString());
                          OleDbCommand sorgu4 = new OleDbCommand("select detay from urunler where kayitNo='" + rd["kayitNo"].ToString() + "'", baglanti);
                          OleDbDataReader rd2 = sorgu4.ExecuteReader();
                          if (rd2.Read())
                          {
                            for (int j = 0; j < treeView2.Nodes[i].Nodes.Count; j++)
                            {
                                if (treeView2.Nodes[i].Nodes[j].Text == rd["personelAdi"].ToString() + " " + rd["personelSoyAdi"].ToString())
                                {
                                    treeView2.Nodes[i].Nodes[j].Nodes.Add(rd2["detay"].ToString());

                                }

                            }

                        }
                          rd2.Close();
                      }


                  }
              }
            dr.Close(); baglanti.Close();
            label1.Text = girisForm.kullanici;
        }
    }
}
