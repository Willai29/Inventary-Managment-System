using ivs.system.DbFiles;
using System;
using System.Windows.Forms;

namespace ivs.system
{
    public partial class Stocks : Sample2
    {
        retrieval re = new retrieval();

        public Stocks()
        {
            InitializeComponent();
        }

        private void Stocks_Load(object sender, EventArgs e)
        {
        }

        public override void viewBtn_Click(object sender, EventArgs e)
        {
            re.showStocks(Stocks_dataGridView, stkdGv, ProIdGv, BarcodeGv, CatgoeryGv, ProductGv, PrizePrUntGv, QtyGv, TotalAmtGv, ExDateGv, StatusGv);

            float gt = 0;

            foreach (DataGridViewRow row in Stocks_dataGridView.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["TotalAmtGv"].Value != null && row.Cells["TotalAmtGv"].Value != DBNull.Value)
                {
                    float value;
                    if (float.TryParse(row.Cells["TotalAmtGv"].Value.ToString(), out value))
                    {
                        gt += value;
                    }
                }
            }

            GrossAmtPrzTxt.Text = gt.ToString("0.00");
        }
    }
}