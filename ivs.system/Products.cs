using ivs.system.DbFiles;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ivs.system
{
    public partial class Products : Sample2
    {
        int edit = 0;
        short Stat;
        int ProID;
        retrieval re = new retrieval();
        string imagePath = "";
        byte[] currentImage = null;

        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            Mainclass.disable(LeftPanel);
            re.ShowDropDownList("st_GetCategoryList", CatIdDD, "Category", "CatID");
        }

        public override void AddBtn_Click(object sender, EventArgs e)
        {
            Mainclass.enable_reset(LeftPanel);
            edit = 0;
            ProID = 0;
            imagePath = "";
            currentImage = null;
            productImagePB.Image = null;
            QtyTxt.Value = 0;
            StatusDD.SelectedIndex = -1;
        }

        public override void SaveBtn_Click(object sender, EventArgs e)
        {
            if (BrTxt.Text == "") { BrErrorLbl.Visible = true; } else { BrErrorLbl.Visible = false; }
            if (PNameTxt.Text == "") { PNameErrorLbl.Visible = true; } else { PNameErrorLbl.Visible = false; }
            if (CatIdDD.Text == "") { CatIdDDErrorLbl.Visible = true; } else { CatIdDDErrorLbl.Visible = false; }
            if (PPrizeTxt.Text == "") { PPrizeErrorLbl.Visible = true; } else { PPrizeErrorLbl.Visible = false; }

            if (ExDatePickerTxt.Value < DateTime.Now)
            {
                ExDatePickerErrorLbl.Visible = true;
                ExDatePickerErrorLbl.Text = "Invalid Date";
            }
            else
            {
                ExDatePickerErrorLbl.Visible = false;
            }

            if (ExDatePickerTxt.Value.Date == DateTime.Now.Date)
            {
                ExDatePickerErrorLbl.Visible = false;
            }

            if (StatusDD.SelectedItem == null)
            {
                StatusErrorLbl.Visible = true;
            }
            else
            {
                StatusErrorLbl.Visible = false;

                string statusText = StatusDD.Text.Trim().ToLower();

                if (statusText == "active")
                {
                    Stat = 1;
                }
                else
                {
                    Stat = 0;
                }
            }

            if (BrErrorLbl.Visible || PNameErrorLbl.Visible || StatusErrorLbl.Visible || CatIdDDErrorLbl.Visible || PPrizeErrorLbl.Visible || ExDatePickerErrorLbl.Visible)
            {
                Mainclass.showMsg("All Filed is required", "Caption", "Error");
            }
            else
            {
                if (edit == 0)
                {
                    Insertion i = new Insertion();

                    if (ExDatePickerTxt.Value.Date != DateTime.Now.Date)
                    {
                        i.insertProduct(
                            BrTxt.Text,
                            PNameTxt.Text,
                            Convert.ToInt32(CatIdDD.SelectedValue),
                            Convert.ToSingle(PPrizeTxt.Text),
                            Stat,
                            Convert.ToInt32(QtyTxt.Value),
                            imagePath,
                            ExDatePickerTxt.Value
                        );
                    }
                    else
                    {
                        i.insertProduct(
                            BrTxt.Text,
                            PNameTxt.Text,
                            Convert.ToInt32(CatIdDD.SelectedValue),
                            Convert.ToSingle(PPrizeTxt.Text),
                            Stat,
                            Convert.ToInt32(QtyTxt.Value),
                            imagePath
                        );
                    }
                }
                else if (edit == 1)
                {
                    DialogResult dr = MessageBox.Show("Are you sure Update your record", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        Updatation u = new Updatation();

                        if (ExDatePickerTxt.Value.Date != DateTime.Now.Date)
                        {
                            u.updateProduct(
                                ProID,
                                BrTxt.Text,
                                PNameTxt.Text,
                                Convert.ToInt32(CatIdDD.SelectedValue),
                                Convert.ToSingle(PPrizeTxt.Text),
                                Stat,
                                Convert.ToInt32(QtyTxt.Value),
                                imagePath,
                                currentImage,
                                ExDatePickerTxt.Value
                            );
                        }
                        else
                        {
                            u.updateProduct(
                                ProID,
                                BrTxt.Text,
                                PNameTxt.Text,
                                Convert.ToInt32(CatIdDD.SelectedValue),
                                Convert.ToSingle(PPrizeTxt.Text),
                                Stat,
                                Convert.ToInt32(QtyTxt.Value),
                                imagePath,
                                currentImage
                            );
                        }
                    }
                }

                re.showProducts(Product_dataGridView, IdGv, BarchorGv, NameGv, CatIDGV, CatNameGv, PrizeGv, QtyGv, ExDateGv, StsGv);
            }

            Mainclass.disable_reset(LeftPanel);
        }

        public override void DelBtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure Delect your record", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Deletion delRecord = new Deletion();
                delRecord.deleting(ProID, "st_DeleteProduct", "@Id");
            }

            re.showProducts(Product_dataGridView, IdGv, BarchorGv, NameGv, CatIDGV, CatNameGv, PrizeGv, QtyGv, ExDateGv, StsGv);
            Mainclass.disable(LeftPanel);
        }

        public override void EditBtn_Click(object sender, EventArgs e)
        {
            Mainclass.enable(LeftPanel);
            edit = 1;
        }

        public override void viewBtn_Click(object sender, EventArgs e)
        {
            re.showProducts(Product_dataGridView, IdGv, BarchorGv, NameGv, CatIDGV, CatNameGv, PrizeGv, QtyGv, ExDateGv, StsGv);
            Mainclass.disable_reset(LeftPanel);
        }

        public override void searchtxt_TextChanged(object sender, EventArgs e)
        {
            if (searchtxt.Text != "")
            {
                re.showProducts(Product_dataGridView, IdGv, BarchorGv, NameGv, CatIDGV, CatNameGv, PrizeGv, QtyGv, ExDateGv, StsGv, searchtxt.Text);
            }
            else
            {
                re.showProducts(Product_dataGridView, IdGv, BarchorGv, NameGv, CatIDGV, CatNameGv, PrizeGv, QtyGv, ExDateGv, StsGv);
            }
        }

        private void Product_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = Product_dataGridView.Rows[e.RowIndex];

                if (row.Cells["IdGv"].Value != null && row.Cells["IdGv"].Value != DBNull.Value)
                {
                    ProID = Convert.ToInt32(row.Cells["IdGv"].Value);
                }

                PNameTxt.Text = row.Cells["NameGv"].Value == null || row.Cells["NameGv"].Value == DBNull.Value
                    ? ""
                    : row.Cells["NameGv"].Value.ToString();

                BrTxt.Text = row.Cells["BarchorGv"].Value == null || row.Cells["BarchorGv"].Value == DBNull.Value
                    ? ""
                    : row.Cells["BarchorGv"].Value.ToString();

                if (row.Cells["CatIDGV"].Value != null && row.Cells["CatIDGV"].Value != DBNull.Value)
                {
                    CatIdDD.SelectedValue = Convert.ToInt32(row.Cells["CatIDGV"].Value);
                }

                PPrizeTxt.Text = row.Cells["PrizeGv"].Value == null || row.Cells["PrizeGv"].Value == DBNull.Value
                    ? ""
                    : row.Cells["PrizeGv"].Value.ToString();

                if (row.Cells["QtyGv"].Value != null && row.Cells["QtyGv"].Value != DBNull.Value)
                {
                    QtyTxt.Value = Convert.ToDecimal(row.Cells["QtyGv"].Value);
                }
                else
                {
                    QtyTxt.Value = 0;
                }

                if (row.Cells["ExDateGv"].Value == null || row.Cells["ExDateGv"].Value == DBNull.Value || row.Cells["ExDateGv"].FormattedValue.ToString() == "")
                {
                    ExDatePickerTxt.Value = DateTime.Now;
                }
                else
                {
                    ExDatePickerTxt.Value = Convert.ToDateTime(row.Cells["ExDateGv"].Value);
                }

                if (row.Cells["StsGv"].Value != null && row.Cells["StsGv"].Value != DBNull.Value)
                {
                    string statusText = row.Cells["StsGv"].Value.ToString().Trim().ToLower();

                    if (statusText == "active")
                    {
                        StatusDD.SelectedIndex = 0;
                    }
                    else
                    {
                        StatusDD.SelectedIndex = 1;
                    }
                }
                else
                {
                    StatusDD.SelectedIndex = -1;
                }

                imagePath = "";
                LoadProductImageById(ProID);
            }

            Mainclass.disable(LeftPanel);
        }

        private void LoadProductImageById(int productId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT ProductImage FROM dbo.Products WHERE Id = @Id", Mainclass.con);
                cmd.Parameters.AddWithValue("@Id", productId);

                if (Mainclass.con.State != ConnectionState.Open)
                {
                    Mainclass.con.Open();
                }

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    currentImage = (byte[])result;

                    using (MemoryStream ms = new MemoryStream(currentImage))
                    {
                        productImagePB.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    currentImage = null;
                    productImagePB.Image = null;
                }
            }
            catch (Exception ex)
            {
                currentImage = null;
                productImagePB.Image = null;
                Mainclass.showMsg(ex.Message, "Error", "Error");
            }
            finally
            {
                if (Mainclass.con.State == ConnectionState.Open)
                {
                    Mainclass.con.Close();
                }
            }
        }

        private void Product_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void LeftPanel_Scroll(object sender, ScrollEventArgs e)
        {
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imagePath = ofd.FileName;
                productImagePB.Image = Image.FromFile(imagePath);
            }
        }
    }
}