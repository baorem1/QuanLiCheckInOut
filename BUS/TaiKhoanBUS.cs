using DAL;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    public class TaiKhoanBUS
    {
        // Gọi lớp DAL để xử lý dữ liệu
        TaiKhoanDAL dal = new TaiKhoanDAL();
        QuanLyChamCong_SimpleEntities db = new QuanLyChamCong_SimpleEntities(); // Dùng tạm cho hàm đổi MK cũ

        // 1. Đăng nhập
        public TaiKhoan DangNhap(string user, string pass)
        {
            return dal.GetTaiKhoan(user, pass);
        }

        // 2. Đổi mật khẩu (HÀM BỊ THIẾU ĐÂY NÈ)
        public bool DoiMatKhau(string user, string passMoi)
        {
            try
            {
                var tk = db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == user);
                if (tk != null)
                {
                    tk.MatKhau = passMoi;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        // 3. Lấy danh sách nhân viên chưa có tài khoản
        public List<NhanVien> LayDSNhanVienNoAccount()
        {
            return dal.GetNhanVienChuaCoTK();
        }

        // 4. Cấp tài khoản mới
        public string CapTaiKhoan(string user, string pass, int quyen, int maNV)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                return "Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!";

            if (dal.KiemTraUserTonTai(user))
                return "Tên đăng nhập này đã có người dùng! Hãy chọn tên khác.";

            bool ketQua = dal.ThemTaiKhoan(user, pass, quyen, maNV);

            if (ketQua) return "Thành công";
            else return "Lỗi hệ thống! Không thể thêm tài khoản.";
        }
    }
}