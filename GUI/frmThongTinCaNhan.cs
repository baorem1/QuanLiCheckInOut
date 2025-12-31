using BUS; // Gọi BUS
using DAL; // Gọi DAL để hiểu kiểu NhanVien
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; // QUAN TRỌNG: Để xử lý ảnh
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmThongTinCaNhan : Form
    {
        public frmThongTinCaNhan()
        {
            InitializeComponent();
        }

        // Khai báo BUS để lấy thông tin chi tiết nhân viên
        NhanVienBUS busNV = new NhanVienBUS();

        // --- 1. SỰ KIỆN LOAD FORM ---
        private void frmThongTinCaNhan_Load(object sender, EventArgs e)
        {
            // 1. Kiểm tra đăng nhập
            if (Program.CurrentUser == null)
            {
                MessageBox.Show("Bạn chưa đăng nhập!", "Lỗi");
                this.Close();
                return;
            }

            // 2. Kiểm tra tài khoản này có phải nhân viên không
            if (Program.CurrentUser.MaNV == null)
            {
                MessageBox.Show("Tài khoản Admin này chưa liên kết với hồ sơ nhân viên nào!", "Thông báo");
                return;
            }

            // 3. Lấy thông tin nhân viên từ Database dựa vào ID đang đăng nhập
            int maNV = Program.CurrentUser.MaNV.Value;
            NhanVien nv = busNV.GetNhanVienByID(maNV);

            if (nv == null)
            {
                MessageBox.Show("Không tìm thấy dữ liệu nhân viên!");
                return;
            }

            // --- ĐỔ DỮ LIỆU VÀO CÁC Ô TEXTBOX (Giữ nguyên tên biến của bạn) ---
            txtMaNV.Text = nv.MaNV.ToString();
            txtHoTen.Text = nv.HoTen;

            if (nv.NgaySinh != null)
                txtNgaySinh.Text = nv.NgaySinh.ToString("dd/MM/yyyy");

            txtGioiTinh.Text = nv.GioiTinh;
            txtSDT.Text = nv.SDT;

            // Giữ nguyên tên biến "texCCCD" như design của bạn
            texCCCD.Text = nv.CCCD;

            txtChucVu.Text = nv.ChucVu;

            // Xử lý Phòng ban
            if (nv.PhongBan != null)
                txtPhongBan.Text = nv.PhongBan.TenPhong;
            else
                txtPhongBan.Text = "Chưa phân phòng";

            // --- XỬ LÝ ẢNH ĐẠI DIỆN ---
            try
            {
                if (!string.IsNullOrEmpty(nv.AnhDaiDien))
                {
                    string fullPath = Path.Combine(Application.StartupPath, nv.AnhDaiDien);

                    if (File.Exists(fullPath))
                    {
                        picAvatar.Image = Image.FromFile(fullPath);
                        picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        picAvatar.Image = null;
                    }
                }
            }
            catch
            {
                picAvatar.Image = null;
            }
        }

        // --- 2. NÚT ĐÓNG ---
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // --- CÁC HÀM RỖNG GIỮ NGUYÊN ĐỂ KHÔNG LỖI DESIGN ---
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void txtMaNV_TextChanged(object sender, EventArgs e) { }
        private void txtHoTen_TextChanged(object sender, EventArgs e) { }
        private void txtNgaySinh_TextChanged(object sender, EventArgs e) { }
        private void txtGioiTinh_TextChanged(object sender, EventArgs e) { }
        private void txtSDT_TextChanged(object sender, EventArgs e) { }
        private void texCCCD_TextChanged(object sender, EventArgs e) { }
        private void txtChucVu_TextChanged(object sender, EventArgs e) { }
        private void txtPhongBan_TextChanged(object sender, EventArgs e) { }
        private void picAvatar_Click(object sender, EventArgs e) { }
    }
}