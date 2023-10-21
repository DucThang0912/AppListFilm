﻿using BUS;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        private void FormHome_Load(object sender, EventArgs e)
        {
            checkedListBoxGenres.DataSource = GenresService.GetAllGenres();
            checkedListBoxGenres.DisplayMember = "GenreName";
            checkedListBoxGenres.ValueMember = "GenreID";

            LoadData();
        }

        private void LoadData()
        {
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

                //int imageID = item.MovieID ; 
                //dataGridViewMain.Rows[index].Cells[10].Value = ImagesService.GetImageData(imageID);
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
                if(KTYear() == false)
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

                if (MoviesService.KTMovieID(int.Parse(textBoxMovieID.Text)) == true) //sửa lại phim
                {
                    if (MoviesService.UpdateMovie(movieID, movieName, description, duration, releaseDate, endDate, production, director, year, movieType))
                    {
                        LoadData();
                        MessageBox.Show("Cập nhật thành công!");
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
                        View = 0
                    };
                    if (MoviesService.addMovie(movie))
                    {
                        LoadData();
                        MessageBox.Show("Thêm thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Lỗi!");
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
                if (int.TryParse(textBoxMovieID.Text.Trim(), out int id)) 
                {     
                    if (MoviesService.KTMovieID(id))
                    {
                        if (MoviesService.deleteMovie(id))
                        {
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
                    string imagePath = openFileDialog.FileName;
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

                    //string imagePath = selectedRow.Cells[10].Value.ToString();

                    //if (!string.IsNullOrEmpty(imagePath))
                    //{
                    //    pictureBoxIMG.Image = System.Drawing.Image.FromFile(imagePath);
                    //}
                    //else
                    //{
                    //    pictureBoxIMG.Image = null;
                    //}
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi!");
            }
            
        }
    }
}