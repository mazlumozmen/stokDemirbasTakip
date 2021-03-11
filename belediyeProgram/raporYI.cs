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
    public partial class raporYI : Form
    {
        public raporYI()
        {
            InitializeComponent();
        }

        private void raporYI_Load(object sender, EventArgs e)
        {
            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0; Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
            baglan.Open();
            OleDbDataAdapter komut = new OleDbDataAdapter(yapilanIslemler.sorgu, baglan);
            DataSet ds = new DataSet();
            komut.Fill(ds, "islemler");

            islemlerBindingSource.DataSource = ds;

            // TODO: This line of code loads data into the 'veritabaniDataSet.islemler' table. You can move, or remove it, as needed.
            //this.islemlerTableAdapter.Fill(this.veritabaniDataSet.islemler);

            this.reportViewer1.RefreshReport();
        }
    }
}
