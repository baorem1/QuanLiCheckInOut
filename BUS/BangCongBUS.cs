using DAL;
using System;
using System.Collections.Generic;

namespace BUS
{
    public class BangCongBUS
    {
        BangCongDAL dal = new BangCongDAL();

        public List<BangChamCong> GetBangCong(DateTime from, DateTime to)
        {
            return dal.GetBangCong(from, to);
        }
    }
}