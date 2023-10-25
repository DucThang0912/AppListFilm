using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDanhSachPhim
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        void clearInput()
        {
            //txtUserName.Clear();
            txtPassword.Clear();
            checkBoxHidePass.Checked = false;
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {

            string username = txtUserName.Text;
            string password = txtPassword.Text;
            if (UserService.AuthenticateUser(username, password))
            {
                Const.UserName = username; 
                FormMain formMain = new FormMain();
                formMain.Show();
                this.Hide();
                formMain.logOut += FormMain_logOut;
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc password không đúng!");
            }
        }

        private void FormMain_logOut(object sender, EventArgs e)
        {
            Const.isExit = false;
            (sender as FormMain).Close();
            this.Show();
            clearInput();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            FormRegister formRegister = new FormRegister();
            formRegister.Show();
            this.Hide();
            formRegister.Back += FormRegister_Back;
            clearInput();
        }

        private void FormRegister_Back(object sender, EventArgs e)
        {
            Const.isExit = false;
            (sender as FormRegister).Close();
            this.Show();
        }

        private void checkBoxHidePass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHidePass.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtUserName.Text = "admin";txtPassword.Text = "admin";
        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Const.isExit)
            {
                Application.Exit();
            }
        }
    }
}
