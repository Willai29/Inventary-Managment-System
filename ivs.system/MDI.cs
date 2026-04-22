using System;
using System.IO;
using System.Windows.Forms;
using ivs.system.DbFiles;

namespace ivs.system
{
    public partial class MDI : Form
    {
        public MDI()
        {
            InitializeComponent();
        }

        private void MDI_Load(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (File.Exists(path + "\\connect"))
            {
                Login log = new Login();
                Mainclass.showWindow(log, this);
            }
            else
            {
                setting sett = new setting();
                Mainclass.showWindow(sett, this);
            }
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setting sett = new setting();
            Mainclass.showWindow(sett, this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logoutToolStripMenuItem.Enabled = false;

            Login l = new Login();
            Mainclass.showWindow(l, this);
        }
    }
}