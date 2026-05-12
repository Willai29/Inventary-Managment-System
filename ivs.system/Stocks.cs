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
            Stocks_dataGridView.AutoGenerateColumns = false;
        }

        public override void viewBtn_Click(object sender, EventArgs e)
        {
            // Load the data into the grid first
            re.showStocks(Stocks_dataGridView, stkdGv, ProIdGv, BarcodeGv, CatgoeryGv, ProductGv, PrizePrUntGv, QtyGv, TotalAmtGv, ExDateGv, StatusGv);

            double grandTotal = 0;

            foreach (DataGridViewRow row in Stocks_dataGridView.Rows)
            {
                // Skip the empty "new row" at the bottom of the grid
                if (row.IsNewRow) continue;

                // Ensure the Total Amount cell is not empty
                if (row.Cells["TotalAmtGv"].Value != null && row.Cells["TotalAmtGv"].Value != DBNull.Value)
                {
                    double rowAmount;
                    if (double.TryParse(row.Cells["TotalAmtGv"].Value.ToString(), out rowAmount))
                    {
                        grandTotal += rowAmount;
                    }
                }
            }

            // Display with comma separators and two decimal places
            GrossAmtPrzTxt.Text = grandTotal.ToString("N2");
        }
    }
}