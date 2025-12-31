using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS; // Gọi lớp BUS
using DAL; // Gọi lớp DAL (để lấy kiểu NhanVien nếu cần)

namespace GUI
{
    public partial class frmQuanLyTaiKhoan : Form
    {
        // Khởi tạo BUS để xử lý nghiệp vụ
        TaiKhoanBUS bus = new TaiKhoanBUS();

        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
        }

        // --- 1. SỰ KIỆN LOAD FORM ---
        private void frmQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadComboboxNhanVien();
            LoadComboboxQuyen();
        }

        // Hàm phụ: Load danh sách nhân viên chưa có tài khoản
        void LoadComboboxNhanVien()
        {
            // Lấy danh sách từ BUS
            var listNV = bus.LayDSNhanVienNoAccount();

            // Đổ vào ComboBox
            cboNhanVien.DataSource = listNV;
            cboNhanVien.DisplayMember = "HoTen"; // Hiển thị tên
            cboNhanVien.ValueMember = "MaNV";    // Giá trị ngầm là Mã NV

            // Nếu không còn ai thì thông báo nhỏ
            if (listNV.Count == 0)
            {
                cboNhanVien.Text = "Đã cấp đủ tài khoản!";
                cboNhanVien.Enabled = false;
                btnLuu.Enabled = false;
            }
            else
            {
                cboNhanVien.Enabled = true;
                btnLuu.Enabled = true;
            }
        }

        // Hàm phụ: Tạo danh sách quyền (Admin / Nhân viên)
        void LoadComboboxQuyen()
        {
            cboQuyen.Items.Clear();
            // Lưu ý: Thứ tự này phải khớp với SQL (0: Nhân viên, 1: Admin)
            cboQuyen.Items.Add("Nhân Viên"); // Index 0
            cboQuyen.Items.Add("Admin (Quản trị)"); // Index 1

            // Mặc định chọn dòng đầu tiên (Nhân viên) cho an toàn
            cboQuyen.SelectedIndex = 0;
        }

        // --- 2. SỰ KIỆN BẤM NÚT LƯU ---
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // A. Kiểm tra dữ liệu đầu vào
            if (cboNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần cấp tài khoản!");
                return;
            }
            if (string.IsNullOrEmpty(txtTenDangNhap.Text) || string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!");
                return;
            }

            // B. Lấy thông tin từ giao diện
            int maNV = (int)cboNhanVien.SelectedValue;
            string user = txtTenDangNhap.Text.Trim();
            string pass = txtMatKhau.Text.Trim();
            int quyen = cboQuyen.SelectedIndex; // 0 hoặc 1

            // C. Gọi BUS xử lý lưu
            string ketQua = bus.CapTaiKhoan(user, pass, quyen, maNV);

            // D. Thông báo kết quả
            if (ketQua == "Thành công")
            {
                MessageBox.Show("Cấp tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset giao diện để nhập người tiếp theo
                LoadComboboxNhanVien(); // Load lại để mất người vừa cấp
                txtTenDangNhap.Clear();
                txtMatKhau.Clear();
                cboQuyen.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show(ketQua, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- CÁC SỰ KIỆN KHÁC (Để trống nếu không dùng) ---
        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtTenDangNhap_TextChanged(object sender, EventArgs e) { }
        private void txtMatKhau_TextChanged(object sender, EventArgs e) { }
        private void cboQuyen_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}