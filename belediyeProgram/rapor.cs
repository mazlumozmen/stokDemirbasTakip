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
    public partial class rapor : Form
    {
        public rapor()
        {
            InitializeComponent();
        }

        private void rapor_Load(object sender, EventArgs e)
        {
            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0; Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
            baglan.Open();
            OleDbDataAdapter komut = new OleDbDataAdapter(Form1.sorgu, baglan);
            DataSet ds = new DataSet();
            komut.Fill(ds, "urunler");

            urunlerBindingSource.DataSource = ds;
            // TODO: This line of code loads data into the 'veritabaniDataSet.urunler' table. You can move, or remove it, as needed.
            //this.urunlerTableAdapter.Fill(this.veritabaniDataSet.urunler);
            
            this.reportViewer1.RefreshReport();
        }
    }
}
