using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmDoiMatKhau : Form
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        // Gọi BUS Tài Khoản
        TaiKhoanBUS busTK = new TaiKhoanBUS();

        // --- LOAD FORM ---
        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            // Hiển thị tên người dùng đang đăng nhập lên tiêu đề
            if (Program.CurrentUser != null)
            {
                this.Text = "Đổi mật khẩu cho: " + Program.CurrentUser.TenDangNhap;
            }
        }

        // --- NÚT LƯU (ĐỔI MẬT KHẨU) ---
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra đăng nhập
            if (Program.CurrentUser == null)
            {
                MessageBox.Show("Phiên đăng nhập hết hạn! Vui lòng đăng nhập lại.");
                this.Close();
                return;
            }

            string passCu = txtMatKhauCu.Text.Trim();
            string passMoi = txtMatKhauMoi.Text.Trim();
            string xacNhan = txtXacNhan.Text.Trim();

            // 2. Kiểm tra rỗng
            if (string.IsNullOrEmpty(passCu) || string.IsNullOrEmpty(passMoi) || string.IsNullOrEmpty(xacNhan))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            // 3. Kiểm tra mật khẩu cũ có đúng không
            // So sánh pass nhập vào với pass đang lưu trong Program.CurrentUser
            if (passCu != Program.CurrentUser.MatKhau)
            {
                MessageBox.Show("Mật khẩu cũ không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhauCu.Focus();
                return;
            }

            // 4. Kiểm tra xác nhận mật khẩu
            if (passMoi != xacNhan)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 5. Gọi BUS để cập nhật xuống Database
            if (busTK.DoiMatKhau(Program.CurrentUser.TenDangNhap, passMoi))
            {
                MessageBox.Show("Đổi mật khẩu thành công!");

                // Cập nhật luôn mật khẩu mới vào phiên làm việc hiện tại (để không phải đăng nhập lại)
                Program.CurrentUser.MatKhau = passMoi;

                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi hệ thống! Không thể đổi mật khẩu.");
            }
        }

        // --- NÚT HỦY ---
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // --- CÁC HÀM RỖNG GIỮ NGUYÊN ---
        private void txtMatKhauCu_TextChanged(object sender, EventArgs e) { }
        private void txtMatKhauMoi_TextChanged(object sender, EventArgs e) { }
        private void txtXacNhan_TextChanged(object sender, EventArgs e) { }
    }
}