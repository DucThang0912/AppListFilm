using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDanhSachPhim
{
    public partial class FormTrangChu : Form
    {
        public FormTrangChu()
        {
            InitializeComponent();
        }

        private void FormTrangChu_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Const.isExit)
            {
                Application.Exit();
            }
        }

        private void FormTrangChu_Load(object sender, EventArgs e)
        {

        }
    }
}
