namespace GUI
{
    partial class frmBangCong
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpTimKiem = new System.Windows.Forms.GroupBox();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dgvBangCong = new System.Windows.Forms.DataGridView();
            this.colMaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGioVao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGioRa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExcel = new System.Windows.Forms.Button();
            this.txtTimNhanVien = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureAnhDaChamCong = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grpTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBangCong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAnhDaChamCong)).BeginInit();
            this.SuspendLayout();
            // 
            // grpTimKiem
            // 
            this.grpTimKiem.Controls.Add(this.dtpTuNgay);
            this.grpTimKiem.Controls.Add(this.label2);
            this.grpTimKiem.Controls.Add(this.dtpDenNgay);
            this.grpTimKiem.Controls.Add(this.label1);
            this.grpTimKiem.Location = new System.Drawing.Point(26, 12);
            this.grpTimKiem.Name = "grpTimKiem";
            this.grpTimKiem.Size = new System.Drawing.Size(568, 106);
            this.grpTimKiem.TabIndex = 0;
            this.grpTimKiem.TabStop = false;
            this.grpTimKiem.Text = "Bảng Công";
            this.grpTimKiem.Enter += new System.EventHandler(this.grpTimKiem_Enter);
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(138, 37);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(151, 26);
            this.dtpTuNgay.TabIndex = 4;
            this.dtpTuNgay.ValueChanged += new System.EventHandler(this.dtpTuNgay_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(310, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Đến Ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(411, 37);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(151, 26);
            this.dtpDenNgay.TabIndex = 1;
            this.dtpDenNgay.ValueChanged += new System.EventHandler(this.dtpDenNgay_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ Ngày:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dgvBangCong
            // 
            this.dgvBangCong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvBangCong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBangCong.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaNV,
            this.colHoTen,
            this.colNgay,
            this.colGioVao,
            this.colGioRa,
            this.colPhong});
            this.dgvBangCong.Location = new System.Drawing.Point(77, 124);
            this.dgvBangCong.Name = "dgvBangCong";
            this.dgvBangCong.RowHeadersWidth = 62;
            this.dgvBangCong.RowTemplate.Height = 28;
            this.dgvBangCong.Size = new System.Drawing.Size(1020, 518);
            this.dgvBangCong.TabIndex = 1;
            this.dgvBangCong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBangCong_CellClick_1);
            this.dgvBangCong.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBangCong_CellContentClick);
            // 
            // colMaNV
            // 
            this.colMaNV.DataPropertyName = "MaNV";
            this.colMaNV.HeaderText = "Mã NV";
            this.colMaNV.MinimumWidth = 8;
            this.colMaNV.Name = "colMaNV";
            this.colMaNV.Width = 93;
            // 
            // colHoTen
            // 
            this.colHoTen.DataPropertyName = "HoTen";
            this.colHoTen.HeaderText = "Họ Tên";
            this.colHoTen.MinimumWidth = 8;
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.Width = 97;
            // 
            // colNgay
            // 
            this.colNgay.DataPropertyName = "NgayLam";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            this.colNgay.DefaultCellStyle = dataGridViewCellStyle1;
            this.colNgay.HeaderText = "Ngày Làm";
            this.colNgay.MinimumWidth = 8;
            this.colNgay.Name = "colNgay";
            this.colNgay.Width = 116;
            // 
            // colGioVao
            // 
            this.colGioVao.DataPropertyName = "GioVao";
            dataGridViewCellStyle2.Format = "HH:mm:ss";
            this.colGioVao.DefaultCellStyle = dataGridViewCellStyle2;
            this.colGioVao.HeaderText = "Giờ Vào";
            this.colGioVao.MinimumWidth = 8;
            this.colGioVao.Name = "colGioVao";
            this.colGioVao.Width = 103;
            // 
            // colGioRa
            // 
            this.colGioRa.DataPropertyName = "GioRa";
            dataGridViewCellStyle3.Format = "HH:mm:ss";
            this.colGioRa.DefaultCellStyle = dataGridViewCellStyle3;
            this.colGioRa.HeaderText = "Giờ Ra";
            this.colGioRa.MinimumWidth = 8;
            this.colGioRa.Name = "colGioRa";
            this.colGioRa.Width = 95;
            // 
            // colPhong
            // 
            this.colPhong.DataPropertyName = "TenPhong";
            this.colPhong.HeaderText = "Phòng Ban";
            this.colPhong.MinimumWidth = 8;
            this.colPhong.Name = "colPhong";
            this.colPhong.Width = 124;
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(1137, 553);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(100, 51);
            this.btnExcel.TabIndex = 6;
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // txtTimNhanVien
            // 
            this.txtTimNhanVien.Location = new System.Drawing.Point(854, 75);
            this.txtTimNhanVien.Name = "txtTimNhanVien";
            this.txtTimNhanVien.Size = new System.Drawing.Size(165, 26);
            this.txtTimNhanVien.TabIndex = 7;
            this.txtTimNhanVien.TextChanged += new System.EventHandler(this.txtTimNhanVien_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(736, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tìm Nhân Viên";
            // 
            // pictureAnhDaChamCong
            // 
            this.pictureAnhDaChamCong.Location = new System.Drawing.Point(1154, 124);
            this.pictureAnhDaChamCong.Name = "pictureAnhDaChamCong";
            this.pictureAnhDaChamCong.Size = new System.Drawing.Size(219, 214);
            this.pictureAnhDaChamCong.TabIndex = 9;
            this.pictureAnhDaChamCong.TabStop = false;
            this.pictureAnhDaChamCong.Click += new System.EventHandler(this.pictureAnhDaChamCong_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(1132, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(257, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Ảnh Nhân VIên Chấm Công";
            // 
            // frmBangCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GUI.Properties.Resources.abstract_geometric_white_background_free_vector;
            this.ClientSize = new System.Drawing.Size(1591, 697);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureAnhDaChamCong);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTimNhanVien);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.dgvBangCong);
            this.Controls.Add(this.grpTimKiem);
            this.Name = "frmBangCong";
            this.Text = "Bảng Công";
            this.Load += new System.EventHandler(this.frmBangCong_Load);
            this.grpTimKiem.ResumeLayout(false);
            this.grpTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBangCong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAnhDaChamCong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTimKiem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DataGridView dgvBangCong;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGioVao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGioRa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhong;
        private System.Windows.Forms.TextBox txtTimNhanVien;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureAnhDaChamCong;
        private System.Windows.Forms.Label label4;
    }
}