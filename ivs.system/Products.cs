using ivs.system.DbFiles;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

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
        }

        public override void SaveBtn_Click(object sender, EventArgs e)
        {
            if (BrTxt.Text == "") { BrErrorLbl.Visible = true; } else { BrErrorLbl.Visible = false; }
            if (PNameTxt.Text == "") { PNameErrorLbl.Visible = true; } else { PNameErrorLbl.Visible = false; }
            if (CatIdDD.Text == "") { CatIdDDErrorLbl.Visible = true; } else { CatIdDDErrorLbl.Visible = false; }
            if (PPrizeTxt.Text == "") { PPrizeErrorLbl.Visible = true; } else { PPrizeErrorLbl.Visible = false; }
            if (ExDatePickerTxt.Value < DateTime.Now) { ExDatePickerErrorLbl.Visible = true; ExDatePickerErrorLbl.Text = "Invalid Date"; } else { ExDatePickerErrorLbl.Visible = false; }
            if (ExDatePickerTxt.Value.Date == DateTime.Now.Date) { ExDatePickerErrorLbl.Visible = false; }
            if (StatusDD.SelectedIndex == -1) { StatusErrorLbl.Visible = true; } else { StatusErrorLbl.Visible = false; }

            if (StatusDD.SelectedIndex == 0)
            {
                Stat = 1;
            }
            else if (StatusDD.SelectedIndex == 1)
            {
                Stat = 0;
            }

            if (BrErrorLbl.Visible | PNameErrorLbl.Visible | StatusErrorLbl.Visible | CatIdDDErrorLbl.Visible | PPrizeErrorLbl.Visible | ExDatePickerErrorLbl.Visible)
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
                        i.insertProduct(BrTxt.Text, PNameTxt.Text, Convert.ToInt32(CatIdDD.SelectedValue), Convert.ToSingle(PPrizeTxt.Text), Stat, Convert.ToInt32(QtyTxt.Value), imagePath, ExDatePickerTxt.Value);
                    }
                    else
                    {
                        i.insertProduct(BrTxt.Text, PNameTxt.Text, Convert.ToInt32(CatIdDD.SelectedValue), Convert.ToSingle(PPrizeTxt.Text), Stat, Convert.ToInt32(QtyTxt.Value), imagePath);
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
                            u.updateProduct(ProID, BrTxt.Text, PNameTxt.Text, Convert.ToInt32(CatIdDD.SelectedValue), Convert.ToSingle(PPrizeTxt.Text), Stat, Convert.ToInt32(QtyTxt.Value), imagePath, currentImage, ExDatePickerTxt.Value);
                        }
                        else
                        {
                            u.updateProduct(ProID, BrTxt.Text, PNameTxt.Text, Convert.ToInt32(CatIdDD.SelectedValue), Convert.ToSingle(PPrizeTxt.Text), Stat, Convert.ToInt32(QtyTxt.Value), imagePath, currentImage);
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
                ProID = Convert.ToInt32(row.Cells["IdGv"].Value.ToString());
                PNameTxt.Text = row.Cells["NameGv"].Value.ToString();
                BrTxt.Text = row.Cells["BarchorGv"].Value.ToString();
                if (row.Cells["CatIDGV"].Value != DBNull.Value)
                {
                    CatIdDD.SelectedValue = Convert.ToInt32(row.Cells["CatIDGV"].Value);
                }
                PPrizeTxt.Text = row.Cells["PrizeGv"].Value.ToString();

                if (row.Cells["ExDateGv"].FormattedValue.ToString() == "")
                {
                    ExDatePickerTxt.Value = DateTime.Now;
                }
                else
                {
                    ExDatePickerTxt.Value = Convert.ToDateTime(row.Cells["ExDateGv"].Value.ToString());
                }

                if (row.Cells["StsGv"].Value.ToString() == "1")
                {
                    StatusDD.SelectedIndex = 0; // Active
                }
                else
                {
                    StatusDD.SelectedIndex = 1; // Inactive
                }
            }

            Mainclass.disable(LeftPanel);
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