using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ivs.system
{
    class Insertion
    {
        public void insertUser(string Fname, string username, string email, string pass, Int16 sts)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_insertUser", Mainclass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", Fname);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@pwd", pass);
                cmd.Parameters.AddWithValue("@sts", sts);

                Mainclass.con.Open();
                cmd.ExecuteNonQuery();
                Mainclass.con.Close();

                Mainclass.showMsg(Fname + " insert successfully", "Success", "success");
            }
            catch (Exception ex)
            {
                if (Mainclass.con.State == ConnectionState.Open)
                {
                    Mainclass.con.Close();
                }

                Mainclass.showMsg(ex.Message, "Exception Error", "Error");
            }
        }

        public void insertCat(string name, Int16 sts)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_insertCat", Mainclass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Status", sts);

                Mainclass.con.Open();
                cmd.ExecuteNonQuery();
                Mainclass.con.Close();

                Mainclass.showMsg("Insert successfully", "Success", "success");
            }
            catch (Exception ex)
            {
                if (Mainclass.con.State == ConnectionState.Open)
                {
                    Mainclass.con.Close();
                }

                Mainclass.showMsg(ex.Message, "Exception Error", "Error");
            }
        }

        // ✅ UPDATED: Added Description WITHOUT breaking your existing calls
        public void insertProduct(string barcode, string name, int catId, float price, Int16 sts, int quantity, string imagePath, DateTime? exDate, string description)
        {
            try
            {
                // Ensure the name here matches the Stored Procedure in your SQL
                SqlCommand cmd = new SqlCommand("st_insertProducts", Mainclass.con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Barcode", barcode);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@CatId", catId);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Status", sts);

                // This parameter updates the initial stock in the Stocks table
                cmd.Parameters.AddWithValue("@Quantity", quantity);

                // Handling Nullable Expiry Date
                if (exDate == null)
                {
                    cmd.Parameters.AddWithValue("@ExpiryDate", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ExpiryDate", exDate);
                }

                // Handling Description
                if (string.IsNullOrEmpty(description))
                {
                    cmd.Parameters.AddWithValue("@Description", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Description", description);
                }

                // Handling Image conversion to Byte Array
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    byte[] img = File.ReadAllBytes(imagePath);
                    cmd.Parameters.AddWithValue("@ProductImage", img);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ProductImage", DBNull.Value);
                }

                Mainclass.con.Open();
                cmd.ExecuteNonQuery();
                Mainclass.con.Close();

                Mainclass.showMsg(name + " added to the system successfully.", "Success", "Success");
            }
            catch (Exception ex)
            {
                if (Mainclass.con.State == ConnectionState.Open)
                {
                    Mainclass.con.Close();
                }
                Mainclass.showMsg(ex.Message, "Error", "Error");
            }
        }
        public void insertSuppliers(string Company, string Employee, string Address, int phone1, Int16 sts, int? phone2 = null, string NTN = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_insertSuppliers", Mainclass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompName", Company);
                cmd.Parameters.AddWithValue("@EmpName", Employee);
                cmd.Parameters.AddWithValue("@phone1", phone1);

                if (phone2 != null)
                {
                    cmd.Parameters.AddWithValue("@phone2", phone2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@phone2", DBNull.Value);
                }

                if (NTN != null)
                {
                    cmd.Parameters.AddWithValue("@NTN", NTN);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@NTN", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("@CompAddress", Address);
                cmd.Parameters.AddWithValue("@Status", sts);

                Mainclass.con.Open();
                cmd.ExecuteNonQuery();
                Mainclass.con.Close();

                Mainclass.showMsg("Insert successfully", "Success", "success");
            }
            catch (Exception ex)
            {
                if (Mainclass.con.State == ConnectionState.Open)
                {
                    Mainclass.con.Close();
                }

                Mainclass.showMsg(ex.Message, "Exception Error", "Error");
            }
        }

        private Int64 PurcInvId;

        public Int64 insertPurchaseInvoice(DateTime Date, int UserId, int SupplierId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_insertPurInv", Mainclass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@fk_Uid", UserId);
                cmd.Parameters.AddWithValue("@fk_SuppId", SupplierId);

                Mainclass.con.Open();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "st_getPurInvLastID";
                cmd.Parameters.Clear();
                PurcInvId = Convert.ToInt64(cmd.ExecuteScalar());

                Mainclass.con.Close();
            }
            catch (Exception ex)
            {
                if (Mainclass.con.State == ConnectionState.Open)
                {
                    Mainclass.con.Close();
                }

                Mainclass.showMsg(ex.Message, "Exception Error", "Error");
            }

            return PurcInvId;
        }

        private int CountInsert;

        public int insertPurchaseInvoiceDetails(Int64 PurchuaseInvID, int ProId, int Qty, float PricePerUnit)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_insertPureInvDetails", Mainclass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fk_PInv_Id", PurchuaseInvID);
                cmd.Parameters.AddWithValue("@fk_Pro_Id", ProId);
                cmd.Parameters.AddWithValue("@Qty", Qty);
                cmd.Parameters.AddWithValue("@PricePerUnit", PricePerUnit);

                Mainclass.con.Open();
                CountInsert = cmd.ExecuteNonQuery();
                Mainclass.con.Close();
            }
            catch (Exception)
            {
                if (Mainclass.con.State == ConnectionState.Open)
                {
                    Mainclass.con.Close();
                }
            }

            return CountInsert;
        }

        public void insertStock(int ProId, int qty)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_insertStock", Mainclass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@proId", ProId);
                cmd.Parameters.AddWithValue("@qty", qty);

                Mainclass.con.Open();
                cmd.ExecuteNonQuery();
                Mainclass.con.Close();
            }
            catch (Exception ex)
            {
                if (Mainclass.con.State == ConnectionState.Open)
                {
                    Mainclass.con.Close();
                }

                Mainclass.showMsg(ex.Message, "Exception Error", "Error");
            }
        }

        public void insertDelItemTranck(Int64 PurInvId, int ProId, int qty, DateTime date)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TrackDelItemWRTPurchaseID", Mainclass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Pur_Id", PurInvId);
                cmd.Parameters.AddWithValue("@Pro_Id", ProId);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Qty", qty);

                Mainclass.con.Open();
                cmd.ExecuteNonQuery();
                Mainclass.con.Close();
            }
            catch (Exception ex)
            {
                if (Mainclass.con.State == ConnectionState.Open)
                {
                    Mainclass.con.Close();
                }

                Mainclass.showMsg(ex.Message, "Exception Error", "Error");
            }
        }
    }
}