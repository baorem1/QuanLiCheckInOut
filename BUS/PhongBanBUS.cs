using DAL;
using System.Collections.Generic;

namespace BUS
{
    public class PhongBanBUS
    {
        PhongBanDAL dal = new PhongBanDAL();

        public List<PhongBan> GetAll()
        {
            return dal.GetAllPhongBan();
        }
    }
}