using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class PhongBanDAL
    {
        QuanLyChamCong_SimpleEntities db = new QuanLyChamCong_SimpleEntities();

        // Lấy tất cả phòng ban để đổ vào ComboBox
        public List<PhongBan> GetAllPhongBan()
        {
            return db.PhongBans.ToList();
        }
    }
}
