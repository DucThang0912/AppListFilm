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

        private async void FormTrangChu_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            await DoWorkAsync();
            progressBar1.Visible = false;
        }

        private async Task DoWorkAsync()
        {
            for (int i = progressBar1.Minimum; i <= progressBar1.Maximum; i++)
            {
                progressBar1.Value = i;
                await Task.Delay(10); // Giả lập công việc mất thời gian
            }
        }
    }
}
