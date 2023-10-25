using BUS;
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
using System.Reflection;
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
                dataGridViewMain.Columns[0].Visible = false;
                dataGridViewMain.Rows.Clear();
                foreach (var item in MoviesService.GetAllMovies())
                {
                    int index = dataGridViewMain.Rows.Add();
                    dataGridViewMain.Rows[index].Cells[0].Value = item.MovieID;
                    dataGridViewMain.Rows[index].Cells[1].Value = item.MovieName;
                    dataGridViewMain.Rows[index].Cells[2].Value = item.Duration;
                    dataGridViewMain.Rows[index].Cells[3].Value = item.ReleaseDate.Value.ToShortDateString();
                    dataGridViewMain.Rows[index].Cells[4].Value = item.EndDate.Value.ToShortDateString();
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

        private bool AddImages(int movieID)
        {
            try
            {
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
       void clearInput()
       {
            textBoxMovieID.Clear();textBoxMovieName.Clear(); textBoxDescription.Clear();
            for (int i = 0; i < checkedListBoxGenres.Items.Count; i++)
            {
                checkedListBoxGenres.SetItemChecked(i, false);
            }
            textBoxDuration.Clear();
            radioButtonPhimLe.Checked = radioButtonPhimBo.Checked = false;
            textBoxProduction.Clear(); textBoxDirector.Clear(); textBoxYear.Clear();
            dateTimePickerReleaseDate.Value =dateTimePickerEndDate.Value= DateTime.Now;
            pictureBoxIMG.Image = null;
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
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

                int movieID;
                if (textBoxMovieID.Text == "")
                {
                    movieID = 0;
                }
                else
                {
                    movieID = int.Parse(textBoxMovieID.Text);
                }
                string movieName = textBoxMovieName.Text;
                string description = textBoxDescription.Text;
                string duration = textBoxDuration.Text;
                DateTime releaseDate = DateTime.Parse(dateTimePickerReleaseDate.Text);
                DateTime endDate = DateTime.Parse(dateTimePickerEndDate.Text);
                string production = textBoxProduction.Text;
                string director = textBoxDirector.Text;
                int year = int.Parse(textBoxYear.Text);
                bool movieType = KTMovieType();

                if (MoviesService.KTMovieName(movieName)) //sửa lại phim
                {
                    if (MoviesService.UpdateMovie(movieID, movieName, description, duration, 
                        releaseDate, endDate, production, director, year, movieType))
                    {
                        List<int> selectedGenres = checkedListBoxGenres.CheckedItems.Cast<Genres>().Select(p=> p.GenreID).ToList();

                        UpdateMovieGenres(movieID, selectedGenres);

                        if (AddImages(movieID))
                        {
                            MessageBox.Show("Cập nhật thành công!");
                            clearInput();
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
                        SaveMovieGenres(movie.MovieID);
                        if (imagePath == null)
                        {
                            LoadData();
                            MessageBox.Show("Thêm thành công!");
                            clearInput();
                        }
                        else
                        {
                            if (AddImages(movie.MovieID))
                            {
                                MessageBox.Show("Thêm thành công!");
                                clearInput();
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
                            if (MoviesService.DeleteMovieGenres(id) && MoviesService.deleteMovie(id))
                            {
                                LoadData();
                                MessageBox.Show("Xoá thành công!");
                                clearInput();

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
                    if (selectedRow.Cells[4].Value == null)
                    {
                        dateTimePickerReleaseDate.Text = null;
                    }
                    else
                    {
                        dateTimePickerEndDate.Text = selectedRow.Cells[4].Value.ToString();
                    }
                    textBoxProduction.Text = selectedRow.Cells[5].Value.ToString();
                    textBoxDirector.Text = selectedRow.Cells[6].Value.ToString();
                    textBoxYear.Text = selectedRow.Cells[7].Value.ToString();
                    radioButtonPhimLe.Checked = selectedRow.Cells[8].Value.ToString() == "Phim lẻ";
                    radioButtonPhimBo.Checked = selectedRow.Cells[8].Value.ToString() == "Phim bộ";
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
        private void SaveMovieGenres(int movieID)
        {
            try
            {
                // Xác định thể loại đã chọn từ checkedListBoxGenres
                List<Genres> selectedGenres = checkedListBoxGenres.CheckedItems.Cast<Genres>().ToList();


                // Lưu thể loại cho phim
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

        private void UpdateMovieGenres(int movieID, List<int> updatedGenres)
        {
            //sửa thể loại phim
            if (!MoviesService.UpdateMovieGenres(movieID, updatedGenres))
            {
                MessageBox.Show("Lỗi khi cập nhật thể loại cho phim!");
                return;
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
                System.Windows.Forms.Application.Exit();
            }
        }

    }
}