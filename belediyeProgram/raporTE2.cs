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
    public partial class raporTE2 : Form
    {
        public raporTE2()
        {
            InitializeComponent();
        }

        private void raporTE2_Load(object sender, EventArgs e)
        {
            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0; Data Source=veritabani.accdb;Jet OLEDB:Database Password=didim0909");
            baglan.Open();
            OleDbDataAdapter komut = new OleDbDataAdapter(teslimVerilenForm.sorgu3, baglan);
            DataSet ds = new DataSet();
            komut.Fill(ds, "teslimAlinanlar");
            teslimAlinanlarBindingSource.DataSource = ds;

            
            
            // TODO: This line of code loads data into the 'veritabaniDataSet.teslimAlinanlar' table. You can move, or remove it, as needed.
            //this.teslimAlinanlarTableAdapter.Fill(this.veritabaniDataSet.teslimAlinanlar);

            this.reportViewer1.RefreshReport();
        }
    }
}
