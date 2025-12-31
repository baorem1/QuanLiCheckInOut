using BUS;
using DAL; // Để dùng được kiểu TaiKhoan và NhanVien
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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        // --- 1. KHAI BÁO BUS ---
        TaiKhoanBUS busTK = new TaiKhoanBUS();
        NhanVienBUS busNV = new NhanVienBUS(); // Phải có dòng này mới dùng được biến busNV bên dưới

        // --- 2. NÚT ĐĂNG NHẬP ---
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!");
                return;
            }

            TaiKhoan tk = busTK.DangNhap(user, pass);

            if (tk != null)
            {
                // === CHỈ LƯU VÀO ĐÂY (DUY NHẤT) ===
                Program.CurrentUser = tk;
                // ==================================

                MessageBox.Show("Đăng nhập thành công!");

                frmMain f = new frmMain();
                this.Hide();
                f.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
            }
        }

        // --- 3. NÚT THOÁT ---
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // --- 4. CÁC HÀM SỰ KIỆN RỖNG (GIỮ NGUYÊN ĐỂ KHÔNG BỊ LỖI DESIGN) ---
        private void frmLogin_Load(object sender, EventArgs e)
        {
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
        }
    }
}