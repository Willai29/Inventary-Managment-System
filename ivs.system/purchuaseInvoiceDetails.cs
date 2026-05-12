using ivs.system.DbFiles;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;

namespace ivs.system
{
    public partial class purchuaseInvoiceDetails : Sample2
    {
        public purchuaseInvoiceDetails()
        {
            InitializeComponent();
        }

        retrieval re = new retrieval();
        Insertion i = new Insertion();
        Updatation u = new Updatation();
        Deletion d = new Deletion();

        private void LoadEshopOrdersToGrid()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.st_getPurInvList", Mainclass.con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                da.Fill(tb);

                // ALIGNED: Mapping to match your dbo.PurchaseInvoiceDetails table structure
                PurInvDtlIdGv.DataPropertyName = "InvoiceID";
                ProIdGv.DataPropertyName = "ProductID";
                ProductGv.DataPropertyName = "ProductName";
                PrizePrUntGv.DataPropertyName = "Price";
                QtyGv.DataPropertyName = "Quantity";
                TotalAmtGv.DataPropertyName = "TotalAmount";

                PurInvDetail_dataGridView.AutoGenerateColumns = false;
                PurInvDetail_dataGridView.DataSource = null;
                PurInvDetail_dataGridView.DataSource = tb;

                // FIXED: Safe calculation for the Gross Amount
                float Gt = 0;
                foreach (DataGridViewRow row in PurInvDetail_dataGridView.Rows)
                {
                    // Added !row.IsNewRow to prevent "Input string was not in a correct format" error
                    if (!row.IsNewRow && row.Cells["TotalAmtGv"].Value != null)
                    {
                        float val = 0;
                        if (float.TryParse(row.Cells["TotalAmtGv"].Value.ToString(), out val))
                        {
                            Gt += val;
                        }
                    }
                }

                GrossAmtPrzTxt.Text = Gt.ToString();
            }
            catch (Exception ex)
            {
                Mainclass.showMsg(ex.Message, "Error", "Error");
            }
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            LoadEshopOrdersToGrid();
        }

        private void CompanyDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CompanyDD.SelectedIndex != -1 && CompanyDD.SelectedIndex != 0)
            {
                re.showPurInvDetailWRTPurID(Convert.ToInt64(CompanyDD.SelectedValue), PurInvDetail_dataGridView, PurInvDtlIdGv, ProIdGv, ProductGv, PrizePrUntGv, QtyGv, TotalAmtGv);

                float Gt = 0;
                foreach (DataGridViewRow row in PurInvDetail_dataGridView.Rows)
                {
                    // FIXED: Safe calculation for SelectedIndexChanged
                    if (!row.IsNewRow && row.Cells["TotalAmtGv"].Value != null)
                    {
                        float val = 0;
                        if (float.TryParse(row.Cells["TotalAmtGv"].Value.ToString(), out val))
                        {
                            Gt += val;
                        }
                    }
                }
                GrossAmtPrzTxt.Text = Gt.ToString();
            }
        }

        private void purchuaseInvoiceDetails_Load(object sender, EventArgs e)
        {
            LoadEshopOrdersToGrid();
        }

        private void PurInvDetail_dataGridView_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (e.ColumnIndex == 0) // Action/Delete Button
                {
                    int q;
                    DialogResult dr = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        using (TransactionScope Trsc = new TransactionScope())
                        {
                            try
                            {
                                DataGridViewRow rows = PurInvDetail_dataGridView.Rows[e.RowIndex];

                                // FIXED: Using standardized ProIdGv and checking for null
                                if (rows.Cells["ProIdGv"].Value == null) return;

                                object ob = re.GetProductStockQuatity(Convert.ToInt32(rows.Cells["ProIdGv"].Value.ToString()));
                                if (ob != null)
                                {
                                    q = Convert.ToInt32(ob);

                                    // FIXED: Standardized to QtyGv
                                    q -= Convert.ToInt32(rows.Cells["QtyGv"].Value.ToString());

                                    u.updateStock(Convert.ToInt32(rows.Cells["ProIdGv"].Value.ToString()), q);
                                    i.insertDelItemTranck(Convert.ToInt64(CompanyDD.SelectedValue), Convert.ToInt32(rows.Cells["ProIdGv"].Value.ToString()), q, DateTime.Today);
                                    d.deleting(Convert.ToInt64(rows.Cells["PurInvDtlIdGv"].Value.ToString()), "st_deletePurInvDtlsWTRPvdId", "@id");

                                    // FIXED: Safe parsing for amount subtraction
                                    float tt = 0;
                                    float.TryParse(rows.Cells["TotalAmtGv"].Value.ToString(), out tt);

                                    float currentGross = 0;
                                    float.TryParse(GrossAmtPrzTxt.Text, out currentGross);

                                    float tot = currentGross - tt;
                                    GrossAmtPrzTxt.Text = tot.ToString();

                                    PurInvDetail_dataGridView.Rows.Remove(rows);
                                }
                                Trsc.Complete();
                            }
                            catch (Exception ex)
                            {
                                Mainclass.showMsg(ex.Message, "Error", "Error");
                            }
                        }
                    }
                }
                else
                {
                    Mainclass.showMsg("Column: " + e.ColumnIndex.ToString(), "Info", "Info");
                }
            }
        }

        public override void BackBtn_Click(object sender, EventArgs e)
        {
            HomeScreen hs = new HomeScreen();
            Mainclass.showWindow(hs, this, MDI.ActiveForm);
        }
    }
}