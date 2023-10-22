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
    public partial class FormAccoutManager : Form
    {
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
            dataGridViewUser.Columns[5].Visible = false;
            dataGridViewUser.Columns[6].Visible = false;
        }
        private void FormAccoutManager_Load(object sender, EventArgs e)
        {
            loadAccountType();
            loadDGV();
        }
    }
}
