﻿using BUS;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace AppDanhSachPhim
{
    public partial class FormHome : Form
    {
        public FormHome()
        {
            InitializeComponent();
        }


        string imagePath = null;

        private void FormHome_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        
        private void LoadData()
        {
            try
            {
                checkedListBoxGenres.DataSource = GenresService.GetAllGenres();
                checkedListBoxGenres.DisplayMember = "GenreName";
                checkedListBoxGenres.ValueMember = "GenreID";

                dataGridViewMain.Rows.Clear();
                foreach (var item in MoviesService.GetAllMovies())
                {
                    int index = dataGridViewMain.Rows.Add();
                    dataGridViewMain.Rows[index].Cells[0].Value = item.MovieID;
                    dataGridViewMain.Rows[index].Cells[1].Value = item.MovieName;
                    dataGridViewMain.Rows[index].Cells[2].Value = item.Duration;
                    dataGridViewMain.Rows[index].Cells[3].Value = item.ReleaseDate;
                    dataGridViewMain.Rows[index].Cells[4].Value = item.EndDate;
                    dataGridViewMain.Rows[index].Cells[5].Value = item.Production;
                    dataGridViewMain.Rows[index].Cells[6].Value = item.Director;
                    dataGridViewMain.Rows[index].Cells[7].Value = item.Year;
                    if (item.MovieType == true)
                    {
                        dataGridViewMain.Rows[index].Cells[8].Value = radioButtonPhimLe.Text;
                    }
                    else
                    {
                        dataGridViewMain.Rows[index].Cells[8].Value = radioButtonPhimBo.Text;
                    }
                    dataGridViewMain.Rows[index].Cells[9].Value = item.Description;

                    int id = item.MovieID;
                    dataGridViewMain.Rows[index].Cells[10].Value = ImagesService.GetImageData(id).ToString();
                }
            }
            catch (Exception)
            {

            }
        }

        private bool KTMovieType()
        {
            if (radioButtonPhimLe.Checked == true) { return true; }
            else { return false; }
        }
        private bool KTDate()
        {
            if (dateTimePickerEndDate.Value < dateTimePickerReleaseDate.Value) { return false; }
            else { return true; }
        }
        private bool KTYear()
        {
            DateTime releaseDate = DateTime.Parse(dateTimePickerReleaseDate.Text);
            int year = releaseDate.Year;
            if (int.Parse(textBoxYear.Text.Trim()) < year) { return true; }
            else { return false; }
        }

        private bool AddImages()
        {
            try
            {
                int movieID = int.Parse(textBoxMovieID.Text);
                if (ImagesService.KTMovieID(movieID)) // nếu có sẽ sửa lại đường dẫn ảnh
                {
                    if (ImagesService.UpdateImages(movieID, imagePath))
                    {
                        LoadData();
                        return true;
                    }
                }
                else // chưa có sẽ thêm mới đường dẫn ảnh     
                {
                    Images image = new Images()
                    {
                        ImageID = movieID,
                        MovieID = movieID,
                        ImageData = imagePath
                    };

                    if (ImagesService.addImage(image))
                    {
                        LoadData();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxMovieID.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập mã phim!");
                    return;
                }
                if (KTDate() == false)
                {
                    MessageBox.Show("Ngày kết thúc phải sau ngày công chiếu!");
                    return;
                }
                if (KTYear() == false)
                {
                    MessageBox.Show("Năm sản xuất không hợp lệ! (NamSX < releaseDate)");
                    return;
                }

                int movieID = int.Parse(textBoxMovieID.Text);
                string movieName = textBoxMovieName.Text;
                string description = textBoxDescription.Text;
                string duration = textBoxDuration.Text;
                DateTime releaseDate = DateTime.Parse(dateTimePickerReleaseDate.Text);
                DateTime endDate = DateTime.Parse(dateTimePickerEndDate.Text);
                string production = textBoxProduction.Text;
                string director = textBoxDirector.Text;
                int year = int.Parse(textBoxYear.Text);
                bool movieType = KTMovieType();

                if (MoviesService.KTMovieID(movieID)) //sửa lại phim
                {
                    if (MoviesService.UpdateMovie(movieID, movieName, description, duration, releaseDate, endDate, production, director, year, movieType))
                    {
                        SaveOrUpdateMobieGenres(movieID); // cái hàm này ở dưới nó chỉ save chứ ko update đc // sửa chỗ này
                                                          // còn lại chạy ổn hết // chỉ sủa update lại thể loại
                                                          //hoặc làm cái hàm sửa thể loại luôn bên moviesService t đang làm
                        if (AddImages())
                        {
                            MessageBox.Show("Cập nhật thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Lỗi ảnh!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lỗi!");
                    }

                }
                else //thêm mới phim
                {
                    Movies movie = new Movies()
                    {
                        MovieID = movieID,
                        MovieName = movieName,
                        Description = description,
                        Duration = duration,
                        ReleaseDate = releaseDate,
                        EndDate = endDate,
                        Production = production,
                        Director = director,
                        Year = year,
                        MovieType = movieType,
                        Views = 0
                    };

                    if (MoviesService.addMovie(movie))
                    {
                        SaveOrUpdateMobieGenres(movieID);
                        if (imagePath == null)
                        {
                            LoadData();
                            MessageBox.Show("Thêm thành công!");
                        }
                        else
                        {
                            if (AddImages())
                            {
                                MessageBox.Show("Thêm thành công!");
                            }
                            else
                            {
                                LoadData();
                                MessageBox.Show("Lỗi ảnh!");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại!");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Vui Lòng nhập đúng thông tin!");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Bạn có muốn xoá?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (int.TryParse(textBoxMovieID.Text.Trim(), out int id))
                    {
                        if (MoviesService.KTMovieID(id))
                        {
                            if (MoviesService.deleteMovie(id))
                            {
                                MoviesService.RemoveMovieGenres(id);
                                LoadData();
                                MessageBox.Show("Xoá thành công!");
                            }
                            else
                            {
                                MessageBox.Show("Xoá thất bại!");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy phim với MovieID: " + id);
                        }
                    }
                    else
                    {
                        MessageBox.Show("MovieID không hợp lệ. Vui lòng nhập một số nguyên.");
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi!");
            }
        }

        private void buttonSelectIMG_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = openFileDialog.FileName;
                    pictureBoxIMG.Image = System.Drawing.Image.FromFile(imagePath);
                }
            }
        }

        private void dataGridViewMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dataGridViewMain.Rows.Count)
                {
                    DataGridViewRow selectedRow = dataGridViewMain.Rows[e.RowIndex];
                    textBoxMovieID.Text = selectedRow.Cells[0].Value.ToString();
                    textBoxMovieName.Text = selectedRow.Cells[1].Value.ToString();
                    textBoxDuration.Text = selectedRow.Cells[2].Value.ToString();
                    dateTimePickerReleaseDate.Text = selectedRow.Cells[3].Value.ToString();
                    dateTimePickerEndDate.Text = selectedRow.Cells[4].Value.ToString();
                    textBoxProduction.Text = selectedRow.Cells[5].Value.ToString();
                    textBoxDirector.Text = selectedRow.Cells[6].Value.ToString();
                    textBoxYear.Text = selectedRow.Cells[7].Value.ToString();
                    radioButtonPhimLe.Checked = selectedRow.Cells[8].Value.ToString() == radioButtonPhimLe.Text;
                    radioButtonPhimBo.Checked = selectedRow.Cells[8].Value.ToString() == radioButtonPhimBo.Text;
                    textBoxDescription.Text = selectedRow.Cells[9].Value.ToString();

                    imagePath = selectedRow.Cells[10].Value.ToString();

                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        if (Uri.IsWellFormedUriString(imagePath, UriKind.Absolute)) // Kiểm tra xem đường dẫn là URL
                        {
                            using (WebClient webClient = new WebClient())
                            {
                                byte[] data = webClient.DownloadData(imagePath);
                                using (MemoryStream ms = new MemoryStream(data))
                                {
                                    pictureBoxIMG.Image = System.Drawing.Image.FromStream(ms);
                                }
                            }
                        }
                        else // Đây là đường dẫn file cục bộ
                        {
                            pictureBoxIMG.Image = System.Drawing.Image.FromFile(imagePath);
                        }
                    }
                    else
                    {
                        pictureBoxIMG.Image = null;
                    }
                  
                    UpdateCheckedGenresForMovie(int.Parse(textBoxMovieID.Text));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi!");
            }

        }

        private void UpdateCheckedGenresForMovie(int movieID)
        {
            // Lấy thể loại của phim được chọn
            List<Genres> genresList = GenresService.GetGenresByMovieID(movieID);

            // Xác định và đánh dấu các mục trong checkedListBoxGenres
            for (int i = 0; i < checkedListBoxGenres.Items.Count; i++)
            {
                Genres genre = (Genres)checkedListBoxGenres.Items[i];
                bool isGenreOfMovie = genresList.Any(g => g.GenreID == genre.GenreID);
                checkedListBoxGenres.SetItemChecked(i, isGenreOfMovie);
            }
        }
        private void SaveOrUpdateMobieGenres(int movieID)
        {
            try
            {
                // Xác định thể loại đã chọn từ checkedListBoxGenres
                List<Genres> selectedGenres = checkedListBoxGenres.CheckedItems.OfType<Genres>().ToList();

                // Lưu hoặc sửa thể loại cho phim
                foreach (var genre in selectedGenres)
                {
                    MoviesService.AddGenreToMovie(movieID, genre.GenreID);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi khi cập nhật thể loại cho phim!");
            }
        }

        // Xử lý sự kiện khi danh sách thể loại đã được lấy
        private void HandleMovieGenresLoaded(List<Genres> genresList)
        {
            foreach (Genres genre in genresList)
            {
                int index = checkedListBoxGenres.FindString(genre.GenreName);
                if (index >= 0)
                {
                    checkedListBoxGenres.SetItemChecked(index, true);
                }
            }
        }
        private void GetMovieGenres(int movieID)
        {
            List<Genres> genresList = GenresService.GetGenresByMovieID(movieID);

            HandleMovieGenresLoaded(genresList);
        }

        private void FormHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Const.isExit)
            {
                Application.Exit();
            }
        }
    }
}