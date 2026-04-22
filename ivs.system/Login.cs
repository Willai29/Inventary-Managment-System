using ivs.system.DbFiles;
using System;
using System.Windows.Forms;

namespace ivs.system
{
    public partial class Login : Sample
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (unameTxt.Text == "")
            {
                unameErrorLbl.Visible = true;
            }
            else
            {
                unameErrorLbl.Visible = false;
            }

            if (PassTxt.Text == "")
            {
                PassErrorLbl.Visible = true;
            }
            else
            {
                PassErrorLbl.Visible = false;
            }

            if (unameErrorLbl.Visible || PassErrorLbl.Visible)
            {
                Mainclass.showMsg("All fields are required", "Caption", "Error");
            }
            else
            {
                if (retrieval.getUserDetail(unameTxt.Text, PassTxt.Text))
                {
                    HomeScreen homeScreen = new HomeScreen();
                    Mainclass.showWindow(homeScreen, this, MDI.ActiveForm);
                }
            }
        }

        private void unameTxt_TextChanged(object sender, EventArgs e)
        {
            unameErrorLbl.Visible = unameTxt.Text == "";
        }

        private void PassTxt_TextChanged(object sender, EventArgs e)
        {
            PassErrorLbl.Visible = PassTxt.Text == "";
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (this.MdiParent is MDI mdi)
            {
                mdi.logoutToolStripMenuItem.Enabled = false;
            }

            retrieval.checkLogin = false;
        }
    }
}