using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            // Cài đặt DateTimePicker chỉ hiện Tháng/Năm
            dtpChonThang.Format = DateTimePickerFormat.Custom;
            dtpChonThang.CustomFormat = "MM/yyyy";
            dtpChonThang.ShowUpDown = true;

            // --- ĐOẠN SỬA ---
            // Thay vì dùng DateTime.Now (lấy cả ngày 31), ta lấy ngày 1 của tháng hiện tại
            // New DateTime(Năm, Tháng, Ngày)
            dtpChonThang.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            // 1. Lấy tháng và năm người dùng chọn
            int thang = dtpChonThang.Value.Month;
            int nam = dtpChonThang.Value.Year;

            // 2. Mở form ReportViewer và truyền tham số sang
            frmReportViewer f = new frmReportViewer(thang, nam);

            // 3. Ẩn form này đi cho đỡ rối (hoặc để nguyên tùy bạn)
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        // Các sự kiện thừa
        private void dtpChonThang_ValueChanged(object sender, EventArgs e) { }
    }
}