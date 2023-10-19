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
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            NewForm(new FormHome());
        }

        private void HideSubMenu()
        {
            if (buttonHome.Visible == true)
            {
                buttonHome.Visible = false;
            }
            if(buttonFilmHot.Visible == true)
            {
                buttonFilmHot.Visible = false;
            }
        }

        private Form activeForm = null;
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

        private void buttonFilmHot_Click(object sender, EventArgs e)
        {
            NewForm(new FormPhimHot());
            activeForm = null;
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            NewForm(new FormHome());
            activeForm = null;
        }
    }
}
