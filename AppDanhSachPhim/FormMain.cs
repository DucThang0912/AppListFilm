using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDanhSachPhim
{
    public partial class FormMain : Form
    {
        private Form activeForm = null;
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }

        void NewForm(Form f)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            else
            {
                activeForm = f;
                f.TopLevel = false;
                f.FormBorderStyle = FormBorderStyle.None;
                f.Dock = DockStyle.Fill;
                panelMain.Controls.Add(f);
                panelMain.Tag = f;
                f.BringToFront();
                f.Show();
            }
            
        }
        private void quảnLýPhimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewForm(new FormHome());
            activeForm = null;

        }
        private void buttonFilmHot_Click(object sender, EventArgs e)
        {
            NewForm(new FormPhimHot());
            activeForm = null;
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Const.isExit)
            {
                Application.Exit();
            }
        }

        public event EventHandler logOut;
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logOut(this, new EventArgs());
        }
    }
}
