using DAL;
using System.Collections.Generic;

namespace BUS
{
    public class NhanVienBUS
    {
        NhanVienDAL dal = new NhanVienDAL();

        // --- CÁC HÀM CŨ CỦA BẠN (GetAll, Add, Update...) GIỮ NGUYÊN ---
        public List<NhanVien> GetAll()
        {
            return dal.GetAlNhanVien();
        }

        public bool Add(NhanVien nv)
        {
            return dal.AddNhanVien(nv);
        }

        public bool Update(NhanVien nv)
        {
            return dal.UpdateNhanVien(nv);
        }

        public bool Delete(int ma)
        {
            return dal.DeleteNhanVien(ma);
        }

        // --- BẠN THÊM ĐOẠN NÀY VÀO ĐỂ SỬA LỖI ---
        public NhanVien GetNhanVienByID(int maNV)
        {
            return dal.GetNhanVienByID(maNV);
        }
        // ----------------------------------------
    }
}