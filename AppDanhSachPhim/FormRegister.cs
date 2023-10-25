using BUS;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace AppDanhSachPhim
{
    public partial class FormRegister : Form
    {
        Random random = new Random();
        int otp;
        public FormRegister()
        {
            InitializeComponent();
        }
        public event EventHandler Back;
        bool checkInput()
        {
            if (txtUser.Text == "" || txtPassword.Text == "" || txtAgainPass.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return false;
            }
            if (txtAgainPass.Text != txtPassword.Text)
            {
                MessageBox.Show("Không trùng nhau, kiểm tra lại mật khẩu!");
                return false;
            }
            return true;
        }
        bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]*[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{1,}$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
        }
        
        private void btnBack_Click(object sender, EventArgs e)
        {
            Back(this, new EventArgs());
        }
        bool sendOTP()
        {
            try
            {
                otp = random.Next(100000, 1000000);
                var fromAddress = new MailAddress("nvhhoang286@gmail.com");
                var toAddress = new MailAddress(txtEmail.ToString());
                const string frompass = "uxcp kmht wsnh uvxc";
                const string subject = "OTP code";
                string body = otp.ToString();
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, frompass),
                    Timeout = 200000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                })
                {
                    smtp.Send(  message);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string userName =txtUser.Text;
            if (!checkInput())
            {
                return;
            }
            if (UserService.userExist(userName))
            {
                MessageBox.Show("Tài khoản đã có người dùng, vui lòng nhập lại!");
                txtUser.Focus();
                return;
            }
            if (UserService.emailExist(email))
            {
                MessageBox.Show("Email đã được sử dụng, vui lòng sử dụng email khác!");
                txtEmail.Focus();
                return;
            }
            if (IsValidEmail(email) == false)
            {
                MessageBox.Show("Sai định dạng email!");
                txtEmail.Focus();
                return;
            }

            if (sendOTP())
            {
                MessageBox.Show("OTP đã được gửi qua mail. Vui lòng kiểm tra email và nhập mã OTP.");
                label6.Visible = true;
                txtOTP.Visible = true;
                btnRegister.Visible = false;
                btnRegisterConfirm.Visible = true;
            }
            else
            {
                MessageBox.Show("Không thể gửi OTP. Vui lòng kiểm tra lại thông tin email và mật khẩu.");
            }
        }
        private void btnRegisterConfirm_Click(object sender, EventArgs e) // ấn f5 // xong ấn f10 để chạy từng dòng
        {
            if (int.TryParse(txtOTP.Text, out int enteredOTP))
            {
                if (otp == enteredOTP) // So sánh mã OTP
                {
                    string username = txtUser.Text;
                    string password = txtPassword.Text;
                    string email = txtEmail.Text;
                    int role = 3;

                    Users user = new Users()
                    {
                        UserName = username,
                        Password = password,
                        Email = email,
                        Role = role
                    };

                    if (UserService.addUsers(user))
                    {
                        MessageBox.Show("Đăng ký tài khoản thành công!");
                        Back(this, new EventArgs());
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi đăng ký tài khoản.");
                    }
                }
                else
                {
                    MessageBox.Show("Mã OTP không chính xác. Vui lòng kiểm tra lại.");
                }
            }
            else
            {
                MessageBox.Show("Mã OTP không hợp lệ. Vui lòng kiểm tra lại.");
            }
        }   

        private void FormRegister_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(Const.isExit)
            {
                Application.Exit();
            }
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            label6.Visible = false;
            txtOTP.Visible = false;
            btnRegisterConfirm.Visible = false;
        }

        private void checkboxHidePass_CheckedChanged(object sender, EventArgs e)
        {
            if(checkboxHidePass.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
                txtAgainPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                txtAgainPass.UseSystemPasswordChar = true;
            }
        }
    }
}
