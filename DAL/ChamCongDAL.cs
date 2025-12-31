using System;
using System.Collections.Generic;
using System.Linq;
// Mình đã bỏ dòng "using System.Data.Entity" để tránh lỗi nếu máy bạn thiếu thư viện này

namespace DAL
{
    public class ChamCongDAL
    {
        QuanLyChamCong_SimpleEntities db = new QuanLyChamCong_SimpleEntities();

        // --- HÀM 1: Check In ---
        public bool CheckIn(int maNV, string imagePath)
        {
            try
            {
                DateTime homNay = DateTime.Now.Date;
                var check = db.BangChamCongs.FirstOrDefault(x => x.MaNV == maNV && x.NgayLam == homNay);

                if (check != null) return false;

                BangChamCong cc = new BangChamCong();
                cc.MaNV = maNV;
                cc.NgayLam = homNay;

                // Gán DateTime.Now (Giờ hiện tại)
                cc.GioVao = DateTime.Now;

                cc.AnhMinhChung = imagePath;

                db.BangChamCongs.Add(cc);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        // --- HÀM 2: Check Out ---
        public bool CheckOut(int maNV)
        {
            try
            {
                DateTime homNay = DateTime.Now.Date;
                var check = db.BangChamCongs.FirstOrDefault(x => x.MaNV == maNV && x.NgayLam == homNay);

                if (check == null) return false;

                // Gán DateTime.Now
                check.GioRa = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        // --- HÀM 3: Lấy Lịch Sử Cá Nhân ---
        public List<BangChamCong> GetLichSuCaNhan(int maNV, DateTime tuNgay, DateTime denNgay)
        {
            return db.BangChamCongs
                     .Where(x => x.MaNV == maNV && x.NgayLam >= tuNgay && x.NgayLam <= denNgay)
                     .OrderByDescending(x => x.NgayLam)
                     .ToList();
        }

        // --- HÀM 4: Lấy Lịch Sử Tất Cả ---
        public List<BangChamCong> GetLichSuTatCa(DateTime tuNgay, DateTime denNgay)
        {
            // Phiên bản đơn giản: Bỏ .Include đi để tránh lỗi thư viện
            // Dữ liệu vẫn lên, chỉ là có thể thiếu tên phòng ban (mình sẽ chỉ cách thêm lại sau khi code chạy được)
            return db.BangChamCongs
                     .Where(x => x.NgayLam >= tuNgay && x.NgayLam <= denNgay)
                     .OrderByDescending(x => x.NgayLam)
                     .ToList();
        }
    }
}