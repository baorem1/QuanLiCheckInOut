using DAL; // Gọi DAL
using System;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    // Class DTO để chứa dữ liệu báo cáo (Phải đặt public để Form dùng được)
    public class BaoCaoLuongDTO
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public decimal LuongTheoGio { get; set; }
        public double TongGioLam { get; set; }
        public decimal TongLuong { get; set; }
    }

    public class BaoCaoBUS
    {
        BaoCaoDAL dal = new BaoCaoDAL();

        // Hàm tính toán lương
        public List<BaoCaoLuongDTO> TinhLuongThang(int thang, int nam)
        {
            var listChamCong = dal.GetDuLieuThang(thang, nam);

            // Gom nhóm theo nhân viên và tính tổng
            var listBaoCao = listChamCong
                .GroupBy(x => x.MaNV)
                .Select(g => {
                    var nv = g.First().NhanVien;

                    // Tính tổng giờ làm
                    double tongGio = g.Sum(x =>
                        (x.GioRa != null && x.GioVao != null)
                        ? (x.GioRa.Value - x.GioVao.Value).TotalHours
                        : 0
                    );

                    decimal luong1Gio = nv.LuongTheoGio ?? 0;

                    return new BaoCaoLuongDTO
                    {
                        MaNV = nv.MaNV,
                        HoTen = nv.HoTen,
                        LuongTheoGio = luong1Gio,
                        TongGioLam = Math.Round(tongGio, 2),
                        TongLuong = (decimal)tongGio * luong1Gio
                    };
                })
                .OrderBy(x => x.MaNV)
                .ToList();

            return listBaoCao;
        }
    }
}