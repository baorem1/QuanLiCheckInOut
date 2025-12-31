using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class BaoCaoDAL
    {
        // Khởi tạo kết nối CSDL
        QuanLyChamCong_SimpleEntities db = new QuanLyChamCong_SimpleEntities();

        // Hàm lấy dữ liệu chấm công theo tháng/năm
        public List<BangChamCong> GetDuLieuThang(int thang, int nam)
        {
            // Lọc dữ liệu theo tháng và năm, kèm thông tin nhân viên
            return db.BangChamCongs
                     .Include("NhanVien")
                     .Where(x => x.NgayLam.Value.Month == thang && x.NgayLam.Value.Year == nam)
                     .ToList();
        }
    }
}