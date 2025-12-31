using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity; // QUAN TRỌNG

namespace DAL
{
    public class BangCongDAL
    {
        QuanLyChamCong_SimpleEntities db = new QuanLyChamCong_SimpleEntities();

        public List<BangChamCong> GetBangCong(DateTime tuNgay, DateTime denNgay)
        {
            // --- SỬA LẠI ĐOẠN NÀY ---
            return db.BangChamCongs
                     .Include("NhanVien")             // Lấy thông tin Nhân viên
                     .Include("NhanVien.PhongBan")    // [MỚI] Lấy thêm Phòng ban của nhân viên đó
                     .Where(x => x.NgayLam >= tuNgay && x.NgayLam <= denNgay)
                     .OrderByDescending(x => x.NgayLam)
                     .ToList();
        }
        // Mở file BangCongDAL.cs trong project DAL và thêm hàm này vào class
        public List<BangChamCong> GetLichSuCaNhan(int maNV, DateTime tuNgay, DateTime denNgay)
        {
            return db.BangChamCongs
                     .Where(x => x.MaNV == maNV && x.NgayLam >= tuNgay && x.NgayLam <= denNgay)
                     .OrderByDescending(x => x.GioVao) // Mới nhất lên đầu
                     .ToList();
        }
    }
}