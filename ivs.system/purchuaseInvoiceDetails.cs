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

                PurInvDtlIdGv.DataPropertyName = "InvoiceID";
                ProIdGv.DataPropertyName = "OrderID";
                ProductGv.DataPropertyName = "Products";
                PrizePrUntGv.DataPropertyName = "Prize Per Unit";
                QtyGv.DataPropertyName = "Quantity";
                TotalAmtGv.DataPropertyName = "Total Amount";

                PurInvDetail_dataGridView.AutoGenerateColumns = false;
                PurInvDetail_dataGridView.DataSource = null;
                PurInvDetail_dataGridView.DataSource = tb;

                float Gt = 0;
                foreach (DataGridViewRow row in PurInvDetail_dataGridView.Rows)
                {
                    if (row.Cells["TotalAmtGv"].Value != null)
                    {
                        Gt += Convert.ToSingle(row.Cells["TotalAmtGv"].Value.ToString());
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
                    Gt += Convert.ToSingle(row.Cells["TotalAmtGv"].Value.ToString());
                }
                GrossAmtPrzTxt.Text = Gt.ToString();
                Gt = 0;
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
                if (e.ColumnIndex == 0)
                {
                    int q;
                    DialogResult dr = MessageBox.Show("Are you sure?", "Caption", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (dr == DialogResult.Yes)
                    {
                        using (TransactionScope Trsc = new TransactionScope())
                        {
                            DataGridViewRow rows = PurInvDetail_dataGridView.Rows[e.RowIndex];
                            object ob = re.GetProductStockQuatity(Convert.ToInt32(rows.Cells["ProIdGv"].Value.ToString()));
                            if (ob != null)
                            {
                                q = Convert.ToInt32(ob);
                                q -= Convert.ToInt32(rows.Cells["QtyGV"].Value.ToString());
                                u.updateStock(Convert.ToInt32(rows.Cells["ProIdGv"].Value.ToString()), q);
                                i.insertDelItemTranck(Convert.ToInt64(CompanyDD.SelectedValue), Convert.ToInt32(rows.Cells["ProIdGv"].Value.ToString()), q, DateTime.Today);
                                d.deleting(Convert.ToInt64(rows.Cells["PurInvDtlIdGv"].Value.ToString()), "st_deletePurInvDtlsWTRPvdId", "@id");
                                float tt = Convert.ToSingle(rows.Cells["TotalAmtGv"].Value.ToString());
                                float tot = Convert.ToSingle(GrossAmtPrzTxt.Text) - tt;
                                GrossAmtPrzTxt.Text = tot.ToString();
                                PurInvDetail_dataGridView.Rows.Remove(rows);
                            }
                            Trsc.Complete();
                        }
                    }
                }
                else
                {
                    Mainclass.showMsg(e.ColumnIndex.ToString(), "eror ", "Error");
                }
            }
        }

        public override void BackBtn_Click(object sender, EventArgs e)
        {
            PurchuaseInvoice hs = new PurchuaseInvoice();
            Mainclass.showWindow(hs, this, MDI.ActiveForm);
        }
    }
}