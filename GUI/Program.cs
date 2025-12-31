using System;
using System.Windows.Forms;
using DAL; // Để dùng biến TaiKhoan

namespace GUI
{
    static class Program
    {
        // Biến toàn cục duy nhất
        public static TaiKhoan CurrentUser = null;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. Chạy Form Login trước (Dưới dạng hộp thoại)
            frmLogin frm = new frmLogin();

            // Nếu Đăng nhập thành công (Trả về OK) thì mới chạy Form Main
            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Lúc này Program.CurrentUser đã có dữ liệu rồi
                Application.Run(new frmMain());
            }
            else
            {
                // Nếu tắt form Login hoặc Cancel thì thoát luôn
                Application.Exit();
            }
        }
    }
}