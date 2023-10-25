using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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

        private void FormPhimHot_Load(object sender, EventArgs e)
        {
           
        }
    }
}
