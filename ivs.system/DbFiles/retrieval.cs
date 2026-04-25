using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ivs.system.DbFiles
{
    class retrieval
    {
        public static string _UName { get; private set; }
        public static string _Username { get; set; }
        public static string _Pass { get; set; }
        public static int _UId { get; private set; }
        public static bool checkLogin { get; set; }

        public string[] prodArr = new string[4];

        public static bool getUserDetail(string name, string password)
        {
            checkLogin = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand("st_GetUserDetails", Mainclass.con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", name);
                    cmd.Parameters.AddWithValue("@pass", password);

                    if (Mainclass.con.State != ConnectionState.Open)
                    {
                        Mainclass.con.Open();
                    }

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            checkLogin = true;

                            while (dr.Read())
                            {
                                _UId = Convert.ToInt32(dr["Id"].ToString());
                                _Pass = dr["Password"].ToString();
                                _UName = dr["Username"].ToString();
                                _Username = dr["Username"].ToString();
                            }
                        }
                        else
                        {
                            Mainclass.showMsg("Invalid username or password", "Error", "Error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mainclass.showMsg(ex.Message, "Error", "Error");
            }
            finally
            {
                if (Mainclass.con.State == ConnectionState.Open)
                {
                    Mainclass.con.Close();
                }
            }

            return checkLogin;
        }

        public void ShowDropDownList(string proc, ComboBox cb, string DisplayMember, string ValueMember)
        {
            try
            {
                cb.DataSource = null;
                SqlCommand cmd = new SqlCommand(proc, Mainclass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dt = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                dt.Fill(tb);

                DataRow dr = tb.NewRow();
                dr.ItemArray = new object[] { 0, "Select ...." };
                tb.Rows.InsertAt(dr, 0);

                cb.DisplayMember = DisplayMember;
                cb.ValueMember = ValueMember;
                cb.DataSource = tb;
            }
            catch (Exception ex)
            {
                Mainclass.showMsg(ex.Message, "Error", "Error");
            }
        }

        public void ShowDropDownListWTRTwoParam(string proc, ComboBox cb, string DisplayMember, string ValueMember, string param1, object val1, string param2, object val2)
        {
            try
            {
                cb.DataSource = null;
                SqlCommand cmd = new SqlCommand(proc, Mainclass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(param1, val1);
                cmd.Parameters.AddWithValue(param2, val2);

                SqlDataAdapter dt = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                dt.Fill(tb);

                DataRow dr = tb.NewRow();
                dr.ItemArray = new object[] { 0, "Select ...." };
                tb.Rows.InsertAt(dr, 0);

                cb.DisplayMember = DisplayMember;
                cb.ValueMember = ValueMember;
                cb.DataSource = tb;
            }
            catch (Exception ex)
            {
                Mainclass.showMsg(ex.Message, "Error", "Error");
            }
        }

        public void showUser(DataGridView gv, DataGridViewColumn U_IdGv, DataGridViewColumn U_NameGv, DataGridViewColumn U_UsernameGv, DataGridViewColumn U_EmailGv, DataGridViewColumn passGv, DataGridViewColumn U_StsGv, string data = null)
        {
            SqlCommand cmd;
            try
            {
                if (data == null)
                {
                    cmd = new SqlCommand("st_GetUsersData", Mainclass.con);
                }
                else
                {
                    cmd = new SqlCommand("GetUserDataLike", Mainclass.con);
                    cmd.Parameters.AddWithValue("@searchVal", data);
                }

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                da.Fill(tb);

                U_IdGv.DataPropertyName = "ID";
                U_NameGv.DataPropertyName = "Name";
                U_EmailGv.DataPropertyName = "Email";
                passGv.DataPropertyName = "Password";
                U_UsernameGv.DataPropertyName = "Username";
                U_StsGv.DataPropertyName = "Status";

                gv.DataSource = tb;
            }
            catch (Exception ex)
            {
                Mainclass.showMsg(ex.Message, "Error ...", "Error");
            }
        }

        public void showCat(DataGridView gv, DataGridViewColumn IdGv, DataGridViewColumn NameGv, DataGridViewColumn StsGv, string data = null)
        {
            SqlCommand cmd;
            try
            {
                if (data == null)
                {
                    cmd = new SqlCommand("st_GetCatData", Mainclass.con);
                }
                else
                {
                    cmd = new SqlCommand("st_GetCatDataLike", Mainclass.con);
                    cmd.Parameters.AddWithValue("@searchVal", data);
                }

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                da.Fill(tb);

                IdGv.DataPropertyName = "ID";
                NameGv.DataPropertyName = "Name";
                StsGv.DataPropertyName = "Status";

                gv.DataSource = tb;
            }
            catch (Exception ex)
            {
                Mainclass.showMsg(ex.Message, "Error ...", "Error");
            }
        }

        public void showProducts(DataGridView gv, DataGridViewColumn IdGv, DataGridViewColumn BarchorGv, DataGridViewColumn NameGv, DataGridViewColumn CatIDGV, DataGridViewColumn CatNameGv, DataGridViewColumn PrizeGv, DataGridViewColumn QtyGv, DataGridViewColumn ExDateGv, DataGridViewColumn StsGv, string data = null)
        {
            SqlCommand cmd;
            try
            {
                if (data == null)
                {
                    cmd = new SqlCommand("st_GetProductData", Mainclass.con);
                }
                else
                {
                    cmd = new SqlCommand("st_GetProductDataLike", Mainclass.con);
                    cmd.Parameters.AddWithValue("@searchVal", data);
                }

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                da.Fill(tb);

                IdGv.DataPropertyName = "Id";
                BarchorGv.DataPropertyName = "Barcode";
                NameGv.DataPropertyName = "Name";
                CatIDGV.DataPropertyName = "CatId";
                CatNameGv.DataPropertyName = "CatName";
                PrizeGv.DataPropertyName = "Price";
                ExDateGv.DataPropertyName = "ExpiryDate";
                QtyGv.DataPropertyName = "Quantity";
                StsGv.DataPropertyName = "Status";

                gv.AutoGenerateColumns = false;
                gv.DataSource = tb;

            }
            catch (Exception ex)
            {
                Mainclass.showMsg(ex.Message, "Error ...", "Error");
            }
        }

        public void showSuppliers(DataGridView gv, DataGridViewColumn IdGv, DataGridViewColumn CompGv, DataGridViewColumn EmpNameGv, DataGridViewColumn AddressGv, DataGridViewColumn NTNGv, DataGridViewColumn Phone1Gv, DataGridViewColumn Phone2Gv, DataGridViewColumn StsGv, string data = null)
        {
            SqlCommand cmd;
            try
            {
                if (data == null)
                {
                    cmd = new SqlCommand("st_getSuppliersData", Mainclass.con);
                }
                else
                {
                    cmd = new SqlCommand("st_getSuppliersDataLike", Mainclass.con);
                    cmd.Parameters.AddWithValue("@SearchVal", data);
                }

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                da.Fill(tb);

                IdGv.DataPropertyName = "ID";
                CompGv.DataPropertyName = "Company";
                EmpNameGv.DataPropertyName = "Employee";
                AddressGv.DataPropertyName = "Address";
                NTNGv.DataPropertyName = "NTN";
                Phone1Gv.DataPropertyName = "Phone1";
                Phone2Gv.DataPropertyName = "Phone2";
                StsGv.DataPropertyName = "Status";

                gv.DataSource = tb;
            }
            catch (Exception ex)
            {
                Mainclass.showMsg(ex.Message, "Error ...", "Error");
            }
        }

        public string[] showProductWRTBarchoe(string barcode)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getProductDataWSRBarchode", Mainclass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@barcode", barcode);

                Mainclass.con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        prodArr[0] = dr[0].ToString();
                        prodArr[1] = dr[1].ToString();
                        prodArr[2] = dr[2].ToString();
                        prodArr[3] = dr[3].ToString();
                    }
                }

                Mainclass.con.Close();
            }
            catch (Exception ex)
            {
                if (Mainclass.con.State == ConnectionState.Open)
                {
                    Mainclass.con.Close();
                }

                Mainclass.showMsg(ex.Message, "Error", "Error");
            }

            return prodArr;
        }

        public void showPurInvDetailWRTPurID(long PurInvId, DataGridView gv, DataGridViewColumn mPurInvDtlIdGv, DataGridViewColumn proIdGv, DataGridViewColumn ProductGv, DataGridViewColumn PrizeGv, DataGridViewColumn QtyGv, DataGridViewColumn TotalAmountGv)
        {
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("st_getPurInvDetailWRTPurInvID", Mainclass.con);
                cmd.Parameters.AddWithValue("@id", PurInvId);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                da.Fill(tb);

                mPurInvDtlIdGv.DataPropertyName = "PurInvDtl_Id";
                proIdGv.DataPropertyName = "ProId";
                ProductGv.DataPropertyName = "Product";
                PrizeGv.DataPropertyName = "Prize";
                QtyGv.DataPropertyName = "Quatity";
                TotalAmountGv.DataPropertyName = "";

                gv.DataSource = tb;
            }
            catch (Exception ex)
            {
                Mainclass.showMsg(ex.Message, "Error ...", "Error");
            }
        }

        private object ProQtyCount = 0;

        public object GetProductStockQuatity(int ProId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getProStockQty", Mainclass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pro_Id", ProId);

                Mainclass.con.Open();
                ProQtyCount = cmd.ExecuteScalar();
                Mainclass.con.Close();
            }
            catch (Exception ex)
            {
                if (Mainclass.con.State == ConnectionState.Open)
                {
                    Mainclass.con.Close();
                }

                Mainclass.showMsg(ex.Message, "Expection Error", "Error");
            }

            return ProQtyCount;
        }

        public void showStocks(DataGridView gv, DataGridViewColumn stkIdGv, DataGridViewColumn ProIdGv, DataGridViewColumn BarcodeGv, DataGridViewColumn Catgoery, DataGridViewColumn ProductGv, DataGridViewColumn PrizeGv, DataGridViewColumn QtyGv, DataGridViewColumn TotalAmountGv, DataGridViewColumn ExdateGv, DataGridViewColumn StsGv)
        {
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("st_GetStocksData", Mainclass.con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                da.Fill(tb);

                stkIdGv.DataPropertyName = "stk_Id";
                ProIdGv.DataPropertyName = "ProId";
                ProductGv.DataPropertyName = "Product";
                PrizeGv.DataPropertyName = "Prize";
                QtyGv.DataPropertyName = "Qty";
                TotalAmountGv.DataPropertyName = "Total Amount";
                BarcodeGv.DataPropertyName = "Barcode";
                Catgoery.DataPropertyName = "Catgory";
                ExdateGv.DataPropertyName = "ExDate";
                StsGv.DataPropertyName = "Status";

                gv.AutoGenerateColumns = false;
                gv.DataSource = null;
                gv.DataSource = tb;
            }
            catch (Exception ex)
            {
                Mainclass.showMsg(ex.Message, "Error ...", "Error");
            }
        }

        // ✅ ADDED: Shows orders from e-shop in Purchase Invoice list
        public void showPurInvList(
            DataGridView gv,
            DataGridViewColumn invoiceIdGv,
            DataGridViewColumn orderIdGv,
            DataGridViewColumn productsGv,
            DataGridViewColumn prizeGv,
            DataGridViewColumn qtyGv,
            DataGridViewColumn totalAmountGv,
            DataGridViewColumn actionGv)
        {
            try
            {
                if (Mainclass.con.State != ConnectionState.Open)
                {
                    Mainclass.con.Open();
                }

                MessageBox.Show(
                    "Connected Database: " + Mainclass.con.Database,
                    "Database Check",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Mainclass.con.Close();

                SqlCommand cmd = new SqlCommand("dbo.st_getPurInvList", Mainclass.con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                da.Fill(tb);

                invoiceIdGv.DataPropertyName = "InvoiceID";
                orderIdGv.DataPropertyName = "OrderID";
                productsGv.DataPropertyName = "Products";
                prizeGv.DataPropertyName = "Prize Per Unit";
                qtyGv.DataPropertyName = "Quantity";
                totalAmountGv.DataPropertyName = "Total Amount";
                actionGv.DataPropertyName = "Action";

                gv.AutoGenerateColumns = false;
                gv.DataSource = null;
                gv.DataSource = tb;
            }
            catch (Exception ex)
            {
                if (Mainclass.con.State == ConnectionState.Open)
                {
                    Mainclass.con.Close();
                }

                Mainclass.showMsg(ex.Message, "Error ...", "Error");
            }
        }
    }
}