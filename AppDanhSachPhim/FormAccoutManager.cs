using BUS;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AppDanhSachPhim
{
    public partial class FormAccoutManager : Form
    {
        int index = -1;
        public FormAccoutManager()
        {
            InitializeComponent();
        }
        void loadAccountType()
        {
            cbbAccountType.DataSource = UserRolesService.getRoles();
            cbbAccountType.DisplayMember = "RoleName";
            cbbAccountType.ValueMember = "RoleID";
        }
        void loadDGV()
        {
            dataGridViewUser.DataSource = UserService.getAll();
            dataGridViewUser.Columns[0].Visible = false;
            dataGridViewUser.Columns[4].Visible = false;
            dataGridViewUser.Columns[5].Visible = false;
            dataGridViewUser.Columns[6].Visible = false;
        }
        void clearInput()
        {
            txtUserName.Clear();txtPassword.Clear();txtEmail.Clear();cbbAccountType.SelectedIndex = 0;
        }
        private void FormAccoutManager_Load(object sender, EventArgs e)
        {
            loadAccountType();
            loadDGV();
        }

        private void dataGridViewUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            if(index == -1)
            {
                return;
            }
            txtID.Text = dataGridViewUser.Rows[index].Cells[0].Value.ToString();
            txtUserName.Text = dataGridViewUser.Rows[index].Cells[1].Value.ToString();
            txtPassword.Text = dataGridViewUser.Rows[index].Cells[2].Value.ToString();
            txtEmail.Text = dataGridViewUser.Rows[index].Cells[3].Value.ToString();
            cbbAccountType.Text = dataGridViewUser.Rows[index].Cells[7].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(index < 0)
            {
                MessageBox.Show("Vui lòng chọn bản ghi!");
                return;
            }
            int id = Convert.ToInt32(txtID.Text);
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            string email = txtEmail.Text;
            int role = Convert.ToInt32(cbbAccountType.SelectedValue);

            Users users = new Users()
            {
                UserID = id,UserName = userName,Password = password,Email = email, Role = role
            };
            if (UserService.userExist(userName))
            {
                if (UserService.updateUsers(users))
                {
                    loadDGV();
                    MessageBox.Show("Update thành công");
                    clearInput();
                }
            }
            else
            {
                if (UserService.updateUsersHasUserName(users))
                {
                    loadDGV();
                    MessageBox.Show("Update thành công");
                    clearInput();
                }
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(index < 0)
            {
                MessageBox.Show("Vui lòng chọn bản ghi!");
                return;
            }
            if(cbbAccountType.SelectedIndex == 0)
            {
                if(MessageBox.Show("Bạn có chắc chắn xóa tài khoản có quyền admin?","Xác nhận",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (UserService.deleteUsers(Convert.ToInt32(txtID.Text)))
                    {
                        loadDGV();
                        MessageBox.Show("Xoá thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu để xóa");
                    }
                }
            }
            
        }

        private void FormAccoutManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Const.isExit)
            {
                Application.Exit();
            }
        }
    }
}
