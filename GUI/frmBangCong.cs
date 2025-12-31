using BUS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing; // Thêm thư viện xử lý ảnh
using System.IO;      // Thêm thư viện xử lý đường dẫn file
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace GUI
{
    public partial class frmBangCong : Form
    {
        // Khai báo BUS
        BangCongBUS bus = new BangCongBUS();

        public frmBangCong()
        {
            InitializeComponent();
        }

        // --- 1. LOAD FORM ---
        private void frmBangCong_Load(object sender, EventArgs e)
        {
            dgvBangCong.AutoGenerateColumns = false;

            // Mặc định load từ ngày 1 đến cuối ngày hôm nay
            DateTime today = DateTime.Now;
            dtpTuNgay.Value = new DateTime(today.Year, today.Month, 1);
            dtpDenNgay.Value = today;

            LoadData();
        }

        // --- 2. HÀM LOAD DỮ LIỆU ---
        void LoadData()
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);
            string keyword = txtTimNhanVien.Text.Trim().ToLower();

            // A. Lấy dữ liệu gốc
            var listGoc = bus.GetBangCong(tuNgay, denNgay);

            // B. Lọc theo từ khóa
            if (!string.IsNullOrEmpty(keyword))
            {
                listGoc = listGoc.Where(x =>
                    (x.NhanVien != null && x.NhanVien.HoTen.ToLower().Contains(keyword)) ||
                    x.MaNV.ToString().Contains(keyword)
                ).ToList();
            }

            // C. Chuyển đổi dữ liệu hiển thị (QUAN TRỌNG: LẤY THÊM CỘT ANHMINHCHUNG)
            var listHienThi = listGoc.Select(x => new {
                MaNV = x.MaNV,
                HoTen = (x.NhanVien != null) ? x.NhanVien.HoTen : "Không xác định",
                NgayLam = x.NgayLam,
                GioVao = x.GioVao,
                GioRa = x.GioRa,
                TenPhong = (x.NhanVien != null && x.NhanVien.PhongBan != null) ? x.NhanVien.PhongBan.TenPhong : "",

                // Lấy đường dẫn ảnh từ DB (Cột này sẽ bị ẩn trên GridView)
                AnhMinhChung = x.AnhMinhChung
            }).ToList();

            // D. Đổ vào GridView
            dgvBangCong.DataSource = listHienThi;

            // E. Ẩn cột đường dẫn ảnh đi (chỉ để code đọc, không cho người dùng thấy)
            if (dgvBangCong.Columns["AnhMinhChung"] != null)
            {
                dgvBangCong.Columns["AnhMinhChung"].Visible = false;
            }
        }

 
        // Hàm phụ: Xử lý hiển thị ảnh an toàn
        private void HienThiAnhMinhChung(string duongDanAnh)
        {
            try
            {
                if (!string.IsNullOrEmpty(duongDanAnh))
                {
                    // Ghép đường dẫn tương đối trong DB với thư mục chạy của phần mềm
                    string fullPath = Path.Combine(Application.StartupPath, duongDanAnh);

                    if (File.Exists(fullPath))
                    {
                        pictureAnhDaChamCong.Image = Image.FromFile(fullPath);
                        pictureAnhDaChamCong.SizeMode = PictureBoxSizeMode.Zoom; // Co giãn vừa khung
                    }
                    else
                    {
                        pictureAnhDaChamCong.Image = null; // File không tồn tại
                    }
                }
                else
                {
                    pictureAnhDaChamCong.Image = null;
                }
            }
            catch
            {
                pictureAnhDaChamCong.Image = null; // Lỗi khác
            }
        }

        // --- 4. CÁC SỰ KIỆN TÌM KIẾM & LỌC ---
        private void txtTimNhanVien_TextChanged(object sender, EventArgs e)
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

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        // --- 5. XUẤT EXCEL ---
        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvBangCong.Rows.Count > 0)
            {
                ExportToExcel(dgvBangCong);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo");
            }
        }

        private void ExportToExcel(DataGridView dgv)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Application.Workbooks.Add(Type.Missing);

                // Header (Bỏ qua cột ẩn AnhMinhChung nếu không muốn xuất ra Excel)
                int colIndex = 1;
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (dgv.Columns[i].Visible) // Chỉ xuất cột đang hiện
                    {
                        excelApp.Cells[1, colIndex] = dgv.Columns[i].HeaderText;
                        colIndex++;
                    }
                }

                // Data
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    colIndex = 1;
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        if (dgv.Columns[j].Visible) // Chỉ xuất cột đang hiện
                        {
                            if (dgv.Rows[i].Cells[j].Value != null)
                            {
                                excelApp.Cells[i + 2, colIndex] = dgv.Rows[i].Cells[j].Value.ToString();
                            }
                            colIndex++;
                        }
                    }
                }

                excelApp.Columns.AutoFit();
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
        }

        // --- CÁC HÀM SỰ KIỆN THỪA (GIỮ LẠI ĐỂ TRÁNH LỖI DESIGN) ---
        private void dgvBangCong_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) { }
        private void grpTimKiem_Enter(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void dgvBangCong_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void pictureAnhDaChamCong_Click(object sender, EventArgs e) { }

      private void dgvBangCong_CellClick_1(object sender, DataGridViewCellEventArgs e)
{
    // Kiểm tra click hợp lệ
    if (e.RowIndex >= 0)
    {
        // --- CÁCH SỬA LỖI ---
        // Thay vì tìm cột (Cells["..."]) dễ bị lỗi nếu cột không có trên giao diện
        // Chúng ta lấy trực tiếp cục dữ liệu gốc đang gán vào dòng đó
        
        var dataItem = dgvBangCong.Rows[e.RowIndex].DataBoundItem;
        
        if (dataItem != null)
        {
            // Dùng kỹ thuật Reflection để "móc" lấy giá trị AnhMinhChung từ dữ liệu gốc
            // Cách này chạy được ngay cả khi bạn không có cột AnhMinhChung trên bảng
            string dbPath = "";
            try
            {
                var property = dataItem.GetType().GetProperty("AnhMinhChung");
                if (property != null)
                {
                    var value = property.GetValue(dataItem);
                    if (value != null) dbPath = value.ToString();
                }
            }
            catch { }

            // Gọi hàm hiển thị ảnh với đường dẫn vừa lấy được
            HienThiAnhMinhChung(dbPath);
        }
    }
}
    }
}