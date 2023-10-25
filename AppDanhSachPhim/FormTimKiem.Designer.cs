namespace AppDanhSachPhim
{
    partial class FormTimKiem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxMovieType = new System.Windows.Forms.ComboBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtReleaseYear = new System.Windows.Forms.TextBox();
            this.txtMovieName = new System.Windows.Forms.TextBox();
            this.buttonFindFilm = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBoxMovieType);
            this.panel1.Controls.Add(this.txtYear);
            this.panel1.Controls.Add(this.txtReleaseYear);
            this.panel1.Controls.Add(this.txtMovieName);
            this.panel1.Controls.Add(this.buttonFindFilm);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 41);
            this.panel1.TabIndex = 1;
            // 
            // comboBoxMovieType
            // 
            this.comboBoxMovieType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMovieType.FormattingEnabled = true;
            this.comboBoxMovieType.Items.AddRange(new object[] {
            "",
            "Phim lẻ",
            "Phim bộ"});
            this.comboBoxMovieType.Location = new System.Drawing.Point(646, 12);
            this.comboBoxMovieType.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxMovieType.Name = "comboBoxMovieType";
            this.comboBoxMovieType.Size = new System.Drawing.Size(119, 21);
            this.comboBoxMovieType.TabIndex = 8;
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(497, 14);
            this.txtYear.Margin = new System.Windows.Forms.Padding(2);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(76, 20);
            this.txtYear.TabIndex = 7;
            // 
            // txtReleaseYear
            // 
            this.txtReleaseYear.Location = new System.Drawing.Point(340, 14);
            this.txtReleaseYear.Margin = new System.Windows.Forms.Padding(2);
            this.txtReleaseYear.Name = "txtReleaseYear";
            this.txtReleaseYear.Size = new System.Drawing.Size(76, 20);
            this.txtReleaseYear.TabIndex = 6;
            // 
            // txtMovieName
            // 
            this.txtMovieName.Location = new System.Drawing.Point(61, 14);
            this.txtMovieName.Margin = new System.Windows.Forms.Padding(2);
            this.txtMovieName.Name = "txtMovieName";
            this.txtMovieName.Size = new System.Drawing.Size(176, 20);
            this.txtMovieName.TabIndex = 5;
            // 
            // buttonFindFilm
            // 
            this.buttonFindFilm.Location = new System.Drawing.Point(794, 11);
            this.buttonFindFilm.Margin = new System.Windows.Forms.Padding(2);
            this.buttonFindFilm.Name = "buttonFindFilm";
            this.buttonFindFilm.Size = new System.Drawing.Size(84, 28);
            this.buttonFindFilm.TabIndex = 4;
            this.buttonFindFilm.Text = "Tìm kiếm";
            this.buttonFindFilm.UseVisualStyleBackColor = true;
            this.buttonFindFilm.Click += new System.EventHandler(this.buttonFindFilm_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(585, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Loại phim";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(420, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Năm sản xuất";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Năm công chiếu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên phim";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(423, 221);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.69231F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.30769F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(33, 26);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // FormTimKiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 500);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormTimKiem";
            this.Text = "FormTimKiem";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormTimKiem_FormClosed);
            this.Load += new System.EventHandler(this.FormTimKiem_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBoxMovieType;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtReleaseYear;
        private System.Windows.Forms.TextBox txtMovieName;
        private System.Windows.Forms.Button buttonFindFilm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}