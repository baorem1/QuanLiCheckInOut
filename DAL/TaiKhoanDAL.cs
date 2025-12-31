using System.Collections.Generic; // Thêm thư viện này để dùng List
using System.Linq;

namespace DAL
{
    public class TaiKhoanDAL
    {
        private QuanLyChamCong_SimpleEntities db = new QuanLyChamCong_SimpleEntities();

        // --- CODE CŨ CỦA BẠN (GIỮ NGUYÊN) ---
        public TaiKhoan GetTaiKhoan(string user, string pass)
        {
            return db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == user && x.MatKhau == pass);
        }
        // ------------------------------------

        // === PHẦN BỔ SUNG MỚI (COPY VÀO ĐÂY) ===

        // 1. Lấy danh sách nhân viên CHƯA CÓ tài khoản
        public List<NhanVien> GetNhanVienChuaCoTK()
        {
            // Logic LINQ: Chọn nhân viên mà Mã NV của họ KHÔNG nằm trong bảng Tài Khoản
            return db.NhanViens
                     .Where(nv => !db.TaiKhoans.Any(tk => tk.MaNV == nv.MaNV))
                     .ToList();
        }

        // 2. Kiểm tra tên đăng nhập đã tồn tại chưa
        public bool KiemTraUserTonTai(string user)
        {
            return db.TaiKhoans.Any(x => x.TenDangNhap == user);
        }

        // 3. Thêm tài khoản mới
        public bool ThemTaiKhoan(string user, string pass, int quyen, int maNV)
        {
            try
            {
                TaiKhoan tk = new TaiKhoan();
                tk.TenDangNhap = user;
                tk.MatKhau = pass;
                tk.Quyen = quyen; // 1: Admin, 0: Nhân viên
                tk.MaNV = maNV;

                db.TaiKhoans.Add(tk);
                db.SaveChanges(); // Lưu xuống SQL
                return true;
            }
            catch
            {
                return false;
            }
        }
        // =======================================
    }
}