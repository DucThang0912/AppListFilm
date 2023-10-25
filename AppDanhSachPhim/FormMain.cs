using BUS;
using DAL.Model;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
            Decentralization();

            txtShowUserName.Text = Const.UserName;
        }


        void Decentralization()
        {

            string username = Const.UserName;
            int userID = UserService.GetCurrentUserID(username); // Thay thế bằng cách lấy userID của người dùng hiện tại

            if (userID != -1)
            {
                int userRole = UserService.GetUserRole(userID);

                // Kiểm tra và dựa vào Role của người dùng, ẩn hoặc hiện MenuStrip
                if (userRole == 1 || userRole == 2) 
                {
                    menuStrip1.Visible = true;
                }
                if(userRole == 3)
                {
                    menuStrip1.Visible = false;
                }
            }
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
        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewForm(new FormAccoutManager());
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            logOut(this, new EventArgs());
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
