using BUS;
using DAL; // Để dùng kiểu dữ liệu TaiKhoan và gọi CSDL
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmMain : Form
    {
        // Khai báo các BUS và biến cần thiết
        NhanVienBUS busNV = new NhanVienBUS();
        QuanLyChamCong_SimpleEntities db = new QuanLyChamCong_SimpleEntities();

        public frmMain()
        {
            InitializeComponent();

            // --- GỌI HÀM KHỞI TẠO NGAY TẠI ĐÂY ---
            KhoiTaoDuLieu();
        }

        // Hàm xử lý logic khởi động
        private void KhoiTaoDuLieu()
        {
            // 1. CHẾ ĐỘ CỨU NGUY (AUTO LOGIN)
            // Nếu chạy thẳng Form Main mà chưa đăng nhập -> Tự lấy nick Admin
            if (Program.CurrentUser == null)
            {
                try
                {
                    // Tìm tài khoản có quyền Admin (Quyen = 1)
                    var adminAccount = db.TaiKhoans.FirstOrDefault(x => x.Quyen == 1);

                    // Nếu không có, lấy đại người đầu tiên để test đỡ
                    if (adminAccount == null) adminAccount = db.TaiKhoans.FirstOrDefault();

                    if (adminAccount != null)
                    {
                        Program.CurrentUser = adminAccount;
                    }
                    else
                    {
                        MessageBox.Show("Cảnh báo: Database trống trơn!");
                        return;
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message);
                    return;
                }
            }

            // 2. HIỂN THỊ THÔNG TIN LÊN TIÊU ĐỀ
            TaiKhoan tk = Program.CurrentUser;
            string tenHienThi = tk.TenDangNhap;

            // Lấy tên thật của nhân viên
            if (tk.MaNV != null)
            {
                NhanVien nv = busNV.GetNhanVienByID(tk.MaNV.Value);
                if (nv != null) tenHienThi = nv.HoTen;
            }

            this.Text = "Hệ thống Quản lý Chấm công - Xin chào: " + tenHienThi;

            // 3. PHÂN QUYỀN MENU (QUAN TRỌNG: SỬA Ở ĐÂY)
            if (tk.Quyen == 1) // Là Admin
            {
                quảnLýToolStripMenuItem.Visible = true; // Hiện menu cha

                // --- BẮT BUỘC BẬT MENU CON NÀY LÊN ---
                // Vì có thể bên Design nó đang bị False
                cấpTàiKhoảnToolStripMenuItem.Visible = true;
            }
            else // Là Nhân viên
            {
                quảnLýToolStripMenuItem.Visible = false; // Ẩn luôn menu cha
            }

            // Bắt đầu đồng hồ
            timer1.Start();
        }

        // --- CÁC SỰ KIỆN MENU ---

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Đã gọi KhoiTaoDuLieu ở Constructor rồi nên ở đây để trống
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        // 1. Check In/Out
        private void checkInOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCheckIn f = new frmCheckIn();
            f.ShowDialog();
        }

        // 2. Đăng xuất
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Program.CurrentUser = null;
                this.Hide();
                frmLogin login = new frmLogin();
                login.ShowDialog();
                this.Close();
            }
        }

        // 3. Thoát
        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // 4. Đổi mật khẩu
        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau f = new frmDoiMatKhau();
            f.ShowDialog();
        }

        // --- MENU QUẢN LÝ ---

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployeeManager f = new frmEmployeeManager();
            f.ShowDialog();
        }

        private void bảngCôngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBangCong f = new frmBangCong();
            f.ShowDialog();
        }

        private void báoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.CurrentUser.Quyen != 1)
            {
                MessageBox.Show("Chức năng này chỉ dành cho Admin!");
                return;
            }
            frmReport f = new frmReport();
            f.ShowDialog();
        }

        // --- SỰ KIỆN CẤP TÀI KHOẢN ---
        private void cấpTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Kiểm tra bảo mật lần nữa
            if (Program.CurrentUser == null)
            {
                MessageBox.Show("Chưa đăng nhập!");
                return;
            }

            if (Program.CurrentUser.Quyen == 1)
            {
                frmQuanLyTaiKhoan f = new frmQuanLyTaiKhoan();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn không phải Admin!");
            }
        }

        // --- CÁC FORM PHỤ KHÁC ---
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThongTinCaNhan f = new frmThongTinCaNhan();
            f.ShowDialog();
        }

        private void xemLịchSửToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmXemLichSu f = new frmXemLichSu();
            f.ShowDialog();
        }

        // --- CÁC HÀM RỖNG (GIỮ LẠI ĐỂ TRÁNH LỖI DESIGN) ---
        private void OpenChildForm(Form childForm) { }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
        private void hệThốngToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void chấmCôngToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void lblTime_Click(object sender, EventArgs e) { }
        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
    }
}