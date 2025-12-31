using DAL; // Để dùng được kiểu TaiKhoan

namespace GUI
{
    public static class LuuTru
    {
        // Biến này sẽ lưu tài khoản vừa đăng nhập thành công
        public static TaiKhoan taikhoan;

        // Biến này lưu thông tin nhân viên tương ứng (để hiển thị tên, ảnh...)
        public static NhanVien nv;
    }
}