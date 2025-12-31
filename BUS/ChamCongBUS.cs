using DAL;
using System;
using System.Collections.Generic;

namespace BUS
{
    public class ChamCongBUS
    {
        ChamCongDAL dal = new ChamCongDAL();

        public bool CheckIn(int maNV, string imgPath)
        {
            return dal.CheckIn(maNV, imgPath);
        }

        public bool CheckOut(int maNV)
        {
            return dal.CheckOut(maNV);
        }

        // --- GỌI HÀM TỪ DAL ---
        public List<BangChamCong> GetLichSuCaNhan(int maNV, DateTime tuNgay, DateTime denNgay)
        {
            // Chỉ gọi hàm, không viết logic query ở đây
            return dal.GetLichSuCaNhan(maNV, tuNgay, denNgay);
        }
        public List<BangChamCong> GetLichSuTatCa(DateTime tu, DateTime den)
        {
            return dal.GetLichSuTatCa(tu, den);
        }
    }
}