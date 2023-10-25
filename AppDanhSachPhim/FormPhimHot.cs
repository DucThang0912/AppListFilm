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
    public partial class FormPhimHot : Form
    {
        public FormPhimHot()
        {
            InitializeComponent();
        }

        private void FormPhimHot_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Const.isExit)
            {
                Application.Exit();
            }
        }
    }
}
