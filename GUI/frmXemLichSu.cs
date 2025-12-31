using BUS;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmXemLichSu : Form
    {
        public frmXemLichSu()
        {
            InitializeComponent();
        }

        ChamCongBUS bus = new ChamCongBUS();

        private void frmXemLichSu_Load(object sender, EventArgs e)
        {
            dgvLichSu.AutoGenerateColumns = false; // Tắt tự sinh cột

            // 1. TẠO CỘT THỦ CÔNG (Khớp với hình thiết kế của bạn)
            dgvLichSu.Columns.Clear();

            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Mã NV", DataPropertyName = "MaNV", Width = 60 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Họ Tên", DataPropertyName = "HoTen", Width = 150 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Ngày Làm", DataPropertyName = "NgayLam" });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Giờ Vào", DataPropertyName = "GioVao" });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Giờ Ra", DataPropertyName = "GioRa" });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Phòng Ban", DataPropertyName = "TenPhong", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            // 2. Cài đặt ngày tháng
            DateTime now = DateTime.Now;
            dtpTuNgay.Value = now.Date; // Mặc định chỉ xem hôm nay cho gọn
            dtpDenNgay.Value = now.Date;

            LoadData();
        }

        void LoadData()
        {
            // Không cần kiểm tra MaNV nữa vì Admin cũng xem được
            if (Program.CurrentUser == null) return;

            DateTime tu = dtpTuNgay.Value.Date;
            DateTime den = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

            // 3. GỌI HÀM LẤY TẤT CẢ (Vừa tạo bên BUS)
            var list = bus.GetLichSuTatCa(tu, den);

            // 4. ĐỔ DỮ LIỆU
            dgvLichSu.DataSource = list.Select(x => new {
                MaNV = x.MaNV,
                // Lấy tên nhân viên (kiểm tra null để không lỗi)
                HoTen = (x.NhanVien != null) ? x.NhanVien.HoTen : "Không xác định",

                NgayLam = x.NgayLam.Value.ToString("dd/MM/yyyy"),
                GioVao = x.GioVao.HasValue ? x.GioVao.Value.ToString("HH:mm:ss") : "",
                GioRa = x.GioRa.HasValue ? x.GioRa.Value.ToString("HH:mm:ss") : "--",

                // Lấy tên phòng ban
                TenPhong = (x.NhanVien != null && x.NhanVien.PhongBan != null) ? x.NhanVien.PhongBan.TenPhong : ""
            }).ToList();
        }

        // --- CÁC SỰ KIỆN ---
        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        // Hàm thừa
        private void dgvLichSu_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvLichSu_CellContentClick_1(object sender, DataGridViewCellEventArgs e) { }
    }
}