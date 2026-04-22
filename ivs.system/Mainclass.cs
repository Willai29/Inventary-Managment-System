using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace ivs.system
{
    class Mainclass
    {
        // Path to Documents folder
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Path to connect file
        private static string connectFile = Path.Combine(path, "connect");

        // Read connection string safely
        private static string s = File.Exists(connectFile)
            ? File.ReadAllText(connectFile).Trim()
            : "";

        // Create SQL connection
        public static SqlConnection con = new SqlConnection(s);

        // Message box function
        public static DialogResult showMsg(string msg, string heading, string type)
        {
            if (type == "success")
            {
                return MessageBox.Show(msg, heading, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (type == "Error")
            {
                return MessageBox.Show(msg, heading, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                return MessageBox.Show(msg, heading, MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        // Open window (with closing current)
        public static void showWindow(Form openWin, Form closeWin, Form MdIWin)
        {
            closeWin.Close();
            openWin.MdiParent = MdIWin;
            openWin.WindowState = FormWindowState.Maximized;
            openWin.Show();
        }

        // Open window (without closing)
        public static void showWindow(Form openWin, Form MdIWin)
        {
            openWin.MdiParent = MdIWin;
            openWin.WindowState = FormWindowState.Maximized;
            openWin.Show();
        }

        // Disable + reset controls
        public static void disable_reset(Panel p)
        {
            foreach (Control c in p.Controls)
            {
                if (c is TextBox t)
                {
                    t.Text = "";
                    t.Enabled = false;
                    t.BackColor = System.Drawing.Color.DarkGray;
                }
                else if (c is ComboBox cb)
                {
                    cb.Text = "";
                    cb.Enabled = false;
                    cb.SelectedIndex = -1;
                    cb.BackColor = System.Drawing.Color.DarkGray;
                }
                else if (c is RadioButton rb)
                {
                    rb.Enabled = false;
                    rb.Checked = false;
                }
                else if (c is CheckBox ch)
                {
                    ch.Enabled = false;
                    ch.Checked = false;
                }
            }
        }

        // Disable controls
        public static void disable(Panel p)
        {
            foreach (Control c in p.Controls)
            {
                if (c is TextBox t)
                {
                    t.Enabled = false;
                    t.BackColor = System.Drawing.Color.DarkGray;
                }
                else if (c is ComboBox cb)
                {
                    cb.Enabled = false;
                    cb.BackColor = System.Drawing.Color.DarkGray;
                }
                else if (c is RadioButton rb)
                {
                    rb.Enabled = false;
                }
                else if (c is CheckBox ch)
                {
                    ch.Enabled = false;
                }
            }
        }

        // Enable + reset controls
        public static void enable_reset(Panel p)
        {
            foreach (Control c in p.Controls)
            {
                if (c is TextBox t)
                {
                    t.Text = "";
                    t.Enabled = true;
                    t.BackColor = System.Drawing.Color.White;
                }
                else if (c is ComboBox cb)
                {
                    cb.Text = "";
                    cb.Enabled = true;
                    cb.SelectedIndex = -1;
                    cb.BackColor = System.Drawing.Color.White;
                }
                else if (c is RadioButton rb)
                {
                    rb.Enabled = true;
                    rb.Checked = false;
                }
                else if (c is CheckBox ch)
                {
                    ch.Enabled = true;
                    ch.Checked = false;
                }
            }
        }

        // Enable controls
        public static void enable(Panel p)
        {
            foreach (Control c in p.Controls)
            {
                if (c is TextBox t)
                {
                    t.Enabled = true;
                    t.BackColor = System.Drawing.Color.White;
                }
                else if (c is ComboBox cb)
                {
                    cb.Enabled = true;
                    cb.BackColor = System.Drawing.Color.White;
                }
                else if (c is RadioButton rb)
                {
                    rb.Enabled = true;
                }
                else if (c is CheckBox ch)
                {
                    ch.Enabled = true;
                }
            }
        }
    }
}