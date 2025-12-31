using Microsoft.Reporting.WinForms;
using BUS; // Gọi lớp xử lý nghiệp vụ
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmReportViewer : Form
    {
        // 1. Tạo biến để lưu tháng năm được truyền sang
        int _thang, _nam;

        // 2. Sửa Constructor để nhận tham số (Quan trọng)
        public frmReportViewer(int thang, int nam)
        {
            InitializeComponent();
            _thang = thang;
            _nam = nam;
        }

        // Khai báo BUS
        BaoCaoBUS bus = new BaoCaoBUS();

        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy dữ liệu
                var listBaoCao = bus.TinhLuongThang(_thang, _nam);

                // 2. Nạp dữ liệu vào Report
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.ReportPath = "rptBangLuong.rdlc";

                ReportDataSource rds = new ReportDataSource("DataSet1", listBaoCao);
                this.reportViewer1.LocalReport.DataSources.Add(rds);

                // --- ĐÂY LÀ ĐOẠN MỚI THÊM NÈ ---
                // Tạo tham số (Tên "paThangNam" phải trùng khít với cái bạn tạo ở BƯỚC 1)
                string chuoiThangNam = _thang.ToString() + "/" + _nam.ToString();
                ReportParameter p = new ReportParameter("paThangNam", chuoiThangNam);

                // Đẩy tham số vào báo cáo
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p });
                // --------------------------------

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            // Không cần làm gì ở đây
        }
    }
}