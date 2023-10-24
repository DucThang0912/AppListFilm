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

namespace AppDanhSachPhim
{
    public partial class FormPhimMoi : Form
    {
        public FormPhimMoi()
        {
            InitializeComponent();
        }

        private void FormPhimMoi_Load(object sender, EventArgs e)
        {
            LoadImages();
        }

        private void LoadImages()
        {
            try
            {
                List<Movies> allMovies = MoviesService.GetAllMoviesByNewYear();
                List<string> allImagePaths = ImagesService.GetAllImagesByNewYear();

                int numColumns = 2;
                int numRows = (allMovies.Count + numColumns - 1) / numColumns;

                tableLayoutPanel1 = new TableLayoutPanel();
                tableLayoutPanel1.ColumnCount = 2;
                tableLayoutPanel1.Dock = DockStyle.Fill;
                tableLayoutPanel1.AutoSize = false;
                tableLayoutPanel1.AutoScroll = true;

                int pictureBoxWidth = 150;
                int pictureBoxHeight = 200;

                Controls.Add(tableLayoutPanel1);

                for (int row = 0; row < numRows; row++)
                {
                    for (int column = 0; column < numColumns; column++)
                    {
                        int index = row * numColumns + column;

                        if (index < allMovies.Count)
                        {
                            var movie = allMovies[index];

                            // Kiểm tra xem có hình ảnh tồn tại cho phim hay không
                            if (index < allImagePaths.Count)
                            {
                                string imagePath = allImagePaths[index];

                                if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                                {
                                    PictureBox pictureBox = CreateAndConfigurePictureBox(imagePath, pictureBoxWidth, pictureBoxHeight);

                                    GroupBox infoGroupBox = CreateAndConfigureInfoGroupBox(movie);

                                    Panel imagePanel = new Panel();
                                    imagePanel.Controls.Add(pictureBox);
                                    imagePanel.Controls.Add(infoGroupBox);
                                    pictureBox.Dock = DockStyle.Right;
                                    infoGroupBox.Dock = DockStyle.Right;
                                    imagePanel.Size = new Size(pictureBoxWidth + infoGroupBox.Width, pictureBoxHeight);
                                    imagePanel.BackColor = Color.Transparent;
                                    imagePanel.Anchor = AnchorStyles.Top;
                                    tableLayoutPanel1.Controls.Add(imagePanel, column, row);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load phim: " + ex.Message);
            }
        }

        // Tạo và cấu hình PictureBox
        private PictureBox CreateAndConfigurePictureBox(string imagePath, int width, int height)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = Image.FromFile(imagePath);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Size = new Size(width, height);
            return pictureBox;
        }

        // Tạo và cấu hình GroupBox cho thông tin phim
        private GroupBox CreateAndConfigureInfoGroupBox(Movies movie)
        {
            GroupBox infoGroupBox = new GroupBox();
            Font regularFont = new Font(infoGroupBox.Font, FontStyle.Regular);
            Font boldFont = new Font(infoGroupBox.Font, FontStyle.Bold);
            infoGroupBox.Font = boldFont;
            infoGroupBox.Text = "Thông tin phim";
            infoGroupBox.Dock = DockStyle.Top;

            Label movieTypeLabel = new Label();
            movieTypeLabel.Font = regularFont;
            if (movie.MovieType == true)
            {
                movieTypeLabel.Text = "Loại phim: Phim lẻ";
            }
            else
            {
                movieTypeLabel.Text = "Loại phim: Phim bộ";
            }
            //movieTypeLabel.Text = movie.MovieType ? "Loại phim: Phim lẻ" : "Loại phim: Phim bộ";
            movieTypeLabel.Dock = DockStyle.Top;
            infoGroupBox.Controls.Add(movieTypeLabel);

            Label yearLabel = new Label();
            yearLabel.Font = regularFont;
            yearLabel.Text = "Năm sản xuất: " + movie.Year;
            yearLabel.Dock = DockStyle.Top;
            infoGroupBox.Controls.Add(yearLabel);

            Label directorLabel = new Label();
            directorLabel.Font = regularFont;
            directorLabel.Text = "Đạo diễn: " + movie.Director;
            directorLabel.Dock = DockStyle.Top;
            infoGroupBox.Controls.Add(directorLabel);

            Label productionLabel = new Label();
            productionLabel.Font = regularFont;
            productionLabel.Text = "Sản xuất: " + movie.Production;
            productionLabel.Dock = DockStyle.Top;
            infoGroupBox.Controls.Add(productionLabel);

            Label endDateLabel = new Label();
            endDateLabel.Font = regularFont;
            endDateLabel.Text = "Ngày kết thúc: " + movie.ReleaseDate;
            endDateLabel.Dock = DockStyle.Top;
            infoGroupBox.Controls.Add(endDateLabel);

            Label releaseDateLabel = new Label();
            releaseDateLabel.Font = regularFont;
            releaseDateLabel.Text = "Ngày công chiếu: " + movie.EndDate;
            releaseDateLabel.Dock = DockStyle.Top;
            infoGroupBox.Controls.Add(releaseDateLabel);


            Label durationLabel = new Label();
            durationLabel.Font = regularFont;
            durationLabel.Text = "Thời lượng: " + movie.Duration;
            durationLabel.Dock = DockStyle.Top;
            infoGroupBox.Controls.Add(durationLabel);

            Label movieNameLabel = new Label();
            movieNameLabel.Font = regularFont;
            movieNameLabel.Text = "Tên phim: " + movie.MovieName;
            movieNameLabel.Dock = DockStyle.Top;
            infoGroupBox.Controls.Add(movieNameLabel);

            return infoGroupBox;
        }
    }
}
