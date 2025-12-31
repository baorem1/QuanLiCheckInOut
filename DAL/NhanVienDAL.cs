using System;
using System.Collections.Generic;
using System.Linq; // Bắt buộc có dòng này để dùng .FirstOrDefault()
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NhanVienDAL
    {
        // Khởi tạo kết nối Database
        QuanLyChamCong_SimpleEntities db = new QuanLyChamCong_SimpleEntities();

        // --- CÁC HÀM CŨ (Giữ nguyên hoặc tham khảo) ---

        public List<NhanVien> GetAlNhanVien()
        {
            return db.NhanViens.ToList();
        }

        public bool AddNhanVien(NhanVien nv)
        {
            try
            {
                db.NhanViens.Add(nv);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool UpdateNhanVien(NhanVien nv)
        {
            try
            {
                var p = db.NhanViens.FirstOrDefault(x => x.MaNV == nv.MaNV);
                if (p != null)
                {
                    p.HoTen = nv.HoTen;
                    p.NgaySinh = nv.NgaySinh;
                    p.GioiTinh = nv.GioiTinh;
                    p.SDT = nv.SDT;
                    p.CCCD = nv.CCCD;
                    p.ChucVu = nv.ChucVu;
                    p.MaPhong = nv.MaPhong;
                    p.AnhDaiDien = nv.AnhDaiDien;
                    // p.LuongTheoGio = nv.LuongTheoGio; // Nếu có

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public bool DeleteNhanVien(int ma)
        {
            try
            {
                var p = db.NhanViens.FirstOrDefault(x => x.MaNV == ma);
                if (p != null)
                {
                    db.NhanViens.Remove(p);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        // ==========================================================
        // --- ĐÂY LÀ HÀM QUAN TRỌNG BẠN CẦN THÊM VÀO ---
        // ==========================================================
        public NhanVien GetNhanVienByID(int maNV)
        {
            // Tìm nhân viên có MaNV trùng khớp
            return db.NhanViens.FirstOrDefault(x => x.MaNV == maNV);
        }
        // ==========================================================
    }
}