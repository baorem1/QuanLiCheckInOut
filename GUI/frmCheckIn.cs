using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // Để xử lý lưu file ảnh
using AForge.Video; // Thư viện Camera
using AForge.Video.DirectShow; // Thư viện Camera
using BUS; // Gọi lớp xử lý nghiệp vụ
using DAL; // Gọi lớp dữ liệu (nếu cần dùng kiểu)

namespace GUI
{
    public partial class frmCheckIn : Form
    {
        public frmCheckIn()
        {
            InitializeComponent();
        }

        // 1. Khai báo biến toàn cục cho Camera và BUS
        FilterInfoCollection filterInfoCollection; // Danh sách các camera
        VideoCaptureDevice videoCaptureDevice;     // Thiết bị camera đang chọn
        ChamCongBUS bus = new ChamCongBUS();       // Lớp xử lý nghiệp vụ

        // --- SỰ KIỆN LOAD FORM ---
        private void frmCheckIn_Load(object sender, EventArgs e)
        {
            // Tìm tất cả thiết bị video đầu vào (Webcam)
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            // Đưa tên các camera vào ComboBox
            foreach (FilterInfo device in filterInfoCollection)
            {
                cboCamera.Items.Add(device.Name);
            }

            // Nếu tìm thấy camera thì chọn cái đầu tiên
            if (cboCamera.Items.Count > 0)
            {
                cboCamera.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Không tìm thấy Camera nào trên máy tính!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
     
        // --- SỰ KIỆN CHỌN CAMERA TRONG COMBOBOX ---
        private void cboCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            // (Có thể xử lý đổi camera ngay tại đây nếu muốn, nhưng để nút Start cho an toàn)
        }

        // --- NÚT BẬT CAMERA ---
        private void btnStart_Click(object sender, EventArgs e)
        {
            // Nếu camera đang chạy thì tắt đi trước khi bật cái mới
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.Stop();
            }

            // Khởi tạo camera dựa trên thiết bị đã chọn (MonikerString là ID của camera)
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboCamera.SelectedIndex].MonikerString);

            // Đăng ký sự kiện: Khi camera thu được hình mới -> Gọi hàm VideoCaptureDevice_NewFrame
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;

            // Bắt đầu chạy
            videoCaptureDevice.Start();
        }

        // --- HÀM VẼ HÌNH TỪ CAMERA LÊN PICTUREBOX ---
        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // Clone (sao chép) hình ảnh từ camera và gán vào PictureBox
            picCamera.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        // --- NÚT CHECK IN (VÀO CA) ---
        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra camera có bật chưa
            if (picCamera.Image == null)
            {
                MessageBox.Show("Vui lòng bật Camera và đợi hình ảnh hiển thị!", "Thông báo");
                return;
            }

            // 2. Lấy thông tin người đang đăng nhập
            if (Program.CurrentUser == null)
            {
                MessageBox.Show("Chưa đăng nhập! Vui lòng đăng nhập lại.");
                return;
            }
            int maNV = (int)Program.CurrentUser.MaNV;

            // 3. Tạo tên file ảnh: CheckIn_MaNV_NămThángNgày_GiờPhútGiây.jpg
            // Ví dụ: CheckIn_2_20251230_143000.jpg
            string fileName = $"CheckIn_{maNV}_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";

            // Tạo đường dẫn thư mục: bin/Debug/Images/CheckIn
            string folderPath = Path.Combine(Application.StartupPath, "Images", "CheckIn");

            // Nếu chưa có thư mục thì tạo mới
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Đường dẫn tuyệt đối để lưu file ra ổ cứng
            string fullPath = Path.Combine(folderPath, fileName);

            // 4. Lưu ảnh
            try
            {
                picCamera.Image.Save(fullPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu ảnh: " + ex.Message);
                return;
            }

            // 5. Lưu vào Database (Chỉ lưu đường dẫn tương đối để nhẹ DB)
            string dbPath = $"Images/CheckIn/{fileName}";

            bool ketQua = bus.CheckIn(maNV, dbPath);

            if (ketQua)
            {
                MessageBox.Show("Check-in thành công!\nĐã lưu ảnh minh chứng.", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Hôm nay bạn đã Check-in rồi!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --- NÚT CHECK OUT (RA CA) ---
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (Program.CurrentUser == null) return;
            int maNV = (int)Program.CurrentUser.MaNV;

            bool ketQua = bus.CheckOut(maNV);

            if (ketQua)
            {
                MessageBox.Show("Check-out thành công!\nKết thúc ngày làm việc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Bạn chưa Check-in nên không thể Check-out!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- QUAN TRỌNG: TẮT CAMERA KHI ĐÓNG FORM ---
        // Bạn cần vào Design, chọn Form -> Properties -> Sự kiện FormClosing -> Chọn hàm này
        // Nếu không tắt camera, ứng dụng sẽ bị treo khi tắt
        private void frmCheckIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.Stop();
            }
        }

        // Click vào ảnh (Không cần xử lý)
        private void picCamera_Click(object sender, EventArgs e) { }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void lblTime_Click_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}