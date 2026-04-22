
namespace ivs.system
{
    partial class HomeScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SupplierBtn = new System.Windows.Forms.Button();
            this.Inv_purc = new System.Windows.Forms.Button();
            this.Stocks = new System.Windows.Forms.Button();
            this.Products = new System.Windows.Forms.Button();
            this.Users = new System.Windows.Forms.Button();
            this.CatBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.rightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BackBtn)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftPanel
            // 
            this.LeftPanel.Size = new System.Drawing.Size(200, 739);
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.tableLayoutPanel1);
            this.rightPanel.Size = new System.Drawing.Size(870, 739);
            this.rightPanel.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            // 
            // WlcUserLbl
            // 
            this.WlcUserLbl.Text = "Admin";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.SupplierBtn, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.Inv_purc, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.Stocks, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.Products, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Users, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CatBtn, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(90, 206);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(705, 189);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // SupplierBtn
            // 
            this.SupplierBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SupplierBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SupplierBtn.FlatAppearance.BorderSize = 2;
            this.SupplierBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SupplierBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SupplierBtn.Location = new System.Drawing.Point(355, 97);
            this.SupplierBtn.Name = "SupplierBtn";
            this.SupplierBtn.Size = new System.Drawing.Size(170, 89);
            this.SupplierBtn.TabIndex = 7;
            this.SupplierBtn.Text = "Suppliers";
            this.SupplierBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SupplierBtn.UseVisualStyleBackColor = true;
            this.SupplierBtn.Click += new System.EventHandler(this.SupplierBtn_Click);
            // 
            // Inv_purc
            // 
            this.Inv_purc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Inv_purc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Inv_purc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Inv_purc.FlatAppearance.BorderSize = 2;
            this.Inv_purc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.Inv_purc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Inv_purc.Location = new System.Drawing.Point(531, 3);
            this.Inv_purc.Name = "Inv_purc";
            this.Inv_purc.Size = new System.Drawing.Size(171, 88);
            this.Inv_purc.TabIndex = 3;
            this.Inv_purc.Text = "Invoice Purchase";
            this.Inv_purc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Inv_purc.UseVisualStyleBackColor = true;
            this.Inv_purc.Click += new System.EventHandler(this.Inv_purc_Click);
            // 
            // Stocks
            // 
            this.Stocks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Stocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Stocks.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Stocks.FlatAppearance.BorderSize = 2;
            this.Stocks.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.Stocks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Stocks.Location = new System.Drawing.Point(355, 3);
            this.Stocks.Name = "Stocks";
            this.Stocks.Size = new System.Drawing.Size(170, 88);
            this.Stocks.TabIndex = 2;
            this.Stocks.Text = "Stocks";
            this.Stocks.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Stocks.UseVisualStyleBackColor = true;
            this.Stocks.Click += new System.EventHandler(this.Stocks_Click);
            // 
            // Products
            // 
            this.Products.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Products.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Products.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Products.FlatAppearance.BorderSize = 2;
            this.Products.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.Products.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Products.Location = new System.Drawing.Point(179, 3);
            this.Products.Name = "Products";
            this.Products.Size = new System.Drawing.Size(170, 88);
            this.Products.TabIndex = 1;
            this.Products.Text = "Products";
            this.Products.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Products.UseVisualStyleBackColor = true;
            this.Products.Click += new System.EventHandler(this.Products_Click);
            // 
            // Users
            // 
            this.Users.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Users.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Users.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Users.FlatAppearance.BorderSize = 2;
            this.Users.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.Users.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Users.Location = new System.Drawing.Point(3, 3);
            this.Users.Name = "Users";
            this.Users.Size = new System.Drawing.Size(170, 88);
            this.Users.TabIndex = 0;
            this.Users.Text = "Users";
            this.Users.UseVisualStyleBackColor = true;
            this.Users.Click += new System.EventHandler(this.Users_Click);
            // 
            // CatBtn
            // 
            this.CatBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CatBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CatBtn.FlatAppearance.BorderSize = 2;
            this.CatBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CatBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CatBtn.Location = new System.Drawing.Point(3, 97);
            this.CatBtn.Name = "CatBtn";
            this.CatBtn.Size = new System.Drawing.Size(170, 89);
            this.CatBtn.TabIndex = 5;
            this.CatBtn.Text = "Catagories";
            this.CatBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CatBtn.UseVisualStyleBackColor = true;
            this.CatBtn.Click += new System.EventHandler(this.CatBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // HomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 739);
            this.Controls.Add(this.button1);
            this.Name = "HomeScreen";
            this.Text = "HomeScreen";
            this.Load += new System.EventHandler(this.HomeScreen_Load);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.LeftPanel, 0);
            this.Controls.SetChildIndex(this.rightPanel, 0);
            this.rightPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BackBtn)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button Stocks;
        private System.Windows.Forms.Button Products;
        private System.Windows.Forms.Button Users;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button CatBtn;
        private System.Windows.Forms.Button SupplierBtn;
        private System.Windows.Forms.Button Inv_purc;
    }
}