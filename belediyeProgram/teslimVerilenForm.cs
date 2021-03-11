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
    public partial class teslimVerilenForm : Form
    {
        public static string sorgu2 = "select * from teslimEdilen";
        public static string sorgu3 = "select * from teslimAlinanlar";
        public teslimVerilenForm()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Columns[0].Width = 120;
            listView1.Columns[1].Width = 100;
            listView1.Columns[2].Width = 120;
            listView1.Columns[3].Width = 120;
            listView1.Columns[4].Width = 100;
            listView1.Columns[5].Width = 100;
            listView1.Columns[6].Width = 100;
            listView1.Columns[7].Width = 120;

            int sayi = 0;
            
            if (checkBox1.Checked == true)
            {
                if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "")
                {
                    sorgu3 = "select * from teslimAlinanlar";
                }
                else if (textBox1.Text == "" && textBox2.Text == "")
                {
                    sorgu3= "select * from teslimAlinanlar where telefonNo like '%" + textBox3.Text + "%'";
                }
                else if (textBox1.Text == "" && textBox3.Text == "")
                {
                    sorgu3 = "select * from teslimAlinanlar where personelSoyAdi like '%" + textBox2.Text + "%'";
                }
                else if (textBox2.Text == "" && textBox3.Text == "")
                {
                    sorgu3 = "select * from teslimAlinanlar where personelAdi like '%" + textBox1.Text + "%'";
                }
                else if (textBox1.Text == "")
                {
                    sorgu3 = "select * from teslimAlinanlar where personelSoyAdi like '%" + textBox2.Text + "%' and telefonNo like '%" + textBox3.Text + "%'";
                }
                else if (textBox2.Text == "")
                {
                    sorgu3 = "select * from teslimAlinanlar where personelAdi like '%" + textBox1.Text + "%' and telefonNo like '%" + textBox3.Text + "%'";
                }
                else if (textBox3.Text == "")
                {
                    sorgu3 = "select * from teslimAlinanlar where personelAdi like '%" + textBox1.Text + "%' and personelSoyAdi like '%" + textBox2.Text + "%'";
                }
                else
                {
                    sorgu3 = "select * from teslimAlinanlar where personelAdi like '%" + textBox1.Text + "%' and personelSoyAdi like '%" + textBox2.Text + "%' and telefonNo like '%" + textBox3.Text + "%'";
                }

                listView1.Items.Clear();
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand(sorgu3, baglanti);
                OleDbDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["personelAdi"].ToString());
                    item.SubItems.Add(dr["personelSoyAdi"].ToString());
                    item.SubItems.Add(dr["calistigiDepartman"].ToString());
                    item.SubItems.Add(dr["teslimTarihi"].ToString());
                    item.SubItems.Add(dr["urunAdi"].ToString());
                    item.SubItems.Add(dr["seriNo"].ToString());
                    item.SubItems.Add(dr["telefonNo"].ToString());
                    item.SubItems.Add(dr["teslimAlmaTarihi"].ToString());
                    listView1.Items.Add(item);
                    sayi++;
                }
                dr.Close();
                baglanti.Close();
                label2.Text = sayi.ToString();
            }
            else
            {
                if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "")
                {
                    sorgu2 = "select * from teslimEdilen";
                }
                else if (textBox1.Text == "" && textBox2.Text == "")
                {
                    sorgu2 = "select * from teslimEdilen where telefonNo like '%" + textBox3.Text + "%'";
                }
                else if (textBox1.Text == "" && textBox3.Text == "")
                {
                    sorgu2 = "select * from teslimEdilen where personelSoyAdi like '%" + textBox2.Text + "%'";
                }
                else if (textBox2.Text == "" && textBox3.Text == "")
                {
                    sorgu2 = "select * from teslimEdilen where personelAdi like '%" + textBox1.Text + "%'";
                }
                else if (textBox1.Text == "")
                {
                    sorgu2 = "select * from teslimEdilen where personelSoyAdi like '%" + textBox2.Text + "%' and telefonNo like '%" + textBox3.Text + "%'";
                }
                else if (textBox2.Text == "")
                {
                    sorgu2 = "select * from teslimEdilen where personelAdi like '%" + textBox1.Text + "%' and telefonNo like '%" + textBox3.Text + "%'";
                }
                else if (textBox3.Text == "")
                {
                    sorgu2 = "select * from urunler where personelAdi like '%" + textBox1.Text + "%' and personelSoyAdi like '%" + textBox2.Text + "%'";
                }
                else
                {
                    sorgu2 = "select * from teslimEdilen where personelAdi like '%" + textBox1.Text + "%' and personelSoyAdi like '%" + textBox2.Text + "%' and telefonNo like '%" + textBox3.Text + "%'";
                }




                listView1.Items.Clear();
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand(sorgu2, baglanti);
                OleDbDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["personelAdi"].ToString());
                    item.SubItems.Add(dr["personelSoyAdi"].ToString());
                    item.SubItems.Add(dr["calistigiDepartman"].ToString());
                    item.SubItems.Add(dr["teslimTarihi"].ToString());
                    item.SubItems.Add(dr["urunAdi"].ToString());
                    item.SubItems.Add(dr["seriNo"].ToString());
                    item.SubItems.Add(dr["telefonNo"].ToString());
                    listView1.Items.Add(item);
                    sayi++;
                }
                dr.Close();
                baglanti.Close();
                label2.Text = sayi.ToString();
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

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.BackColor = Color.Black;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0 && checkBox1.Checked==false)
            {
                sorgu2 = "select * from teslimEdilen where personelAdi='" + listView1.SelectedItems[0].SubItems[0].Text + "' and personelSoyAdi='"+ listView1.SelectedItems[0].SubItems[1].Text + "' and teslimTarihi='"+ listView1.SelectedItems[0].SubItems[3].Text + "'";
              
            }
            else if(listView1.SelectedItems.Count > 0 && checkBox1.Checked == true)
            {
               
                sorgu3 = "select * from teslimAlinanlar where personelAdi='" + listView1.SelectedItems[0].SubItems[0].Text + "' and personelSoyAdi='" + listView1.SelectedItems[0].SubItems[1].Text + "' and teslimTarihi='" + listView1.SelectedItems[0].SubItems[3].Text + "' and teslimAlmaTarihi='"+ listView1.SelectedItems[0].SubItems[7].Text + "'";
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                raporTE2 ac2 = new raporTE2();
                ac2.ShowDialog();
            }
            else
            {
                raporTE ac = new raporTE();
                ac.ShowDialog();
            }
            
        }

        private void teslimVerilenForm_Load(object sender, EventArgs e)
        {

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
