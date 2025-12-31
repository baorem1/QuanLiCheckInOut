using BUS;
using DAL; // Để dùng kiểu dữ liệu NhanVien, PhongBan
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; // QUAN TRỌNG: Để xử lý file ảnh
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmEmployeeManager : Form
    {
        public frmEmployeeManager()
        {
            InitializeComponent();
        }

        // --- KHAI BÁO CÁC BIẾN DÙNG CHUNG ---
        NhanVienBUS busNV = new NhanVienBUS();
        PhongBanBUS busPB = new PhongBanBUS();
        string currentAvatarPath = ""; // Biến lưu đường dẫn ảnh hiện tại của nhân viên

        // --- 1. LOAD FORM ---
        private void frmEmployeeManager_Load(object sender, EventArgs e)
        {
            // Tắt tự động sinh cột (để dùng cột bạn đã chỉnh tay trong Design)
            dgvNhanVien.AutoGenerateColumns = false;

            // Load dữ liệu
            LoadComboboxPhongBan();
            LoadData();

            // Mặc định chọn Nam
            radioNam.Checked = true;
        }

        // Hàm hỗ trợ Load danh sách nhân viên lên lưới
        void LoadData()
        {
            // 1. Làm mới kết nối BUS (Xóa bộ nhớ đệm)
            busNV = new NhanVienBUS();

            // 2. Ngắt kết nối dữ liệu cũ trên lưới (Để tránh xung đột)
            dgvNhanVien.DataSource = null;

            // 3. Lấy danh sách mới về
            List<NhanVien> listNV = busNV.GetAll();
            dgvNhanVien.DataSource = listNV;

            // 4. Cấu hình lại hiển thị (Vì gán null ở trên nên có thể mất định dạng)
            // Nếu bạn đã tắt AutoGenerateColumns = false ở Form_Load thì dòng này không cần, 
            // nhưng cứ để cho chắc.
            if (dgvNhanVien.Columns["colLuong"] != null)
                dgvNhanVien.Columns["colLuong"].DefaultCellStyle.Format = "N0";
            if (dgvNhanVien.Columns["colNgaySinh"] != null)
                dgvNhanVien.Columns["colNgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Cập nhật tổng số
            if (txtTongSoNhanVien != null)
                txtTongSoNhanVien.Text = listNV.Count.ToString();
        }

        // Hàm hỗ trợ Load ComboBox Phòng Ban
        void LoadComboboxPhongBan()
        {
            cboPhongBan.DataSource = busPB.GetAll();
            cboPhongBan.DisplayMember = "TenPhong"; // Hiện tên phòng
            cboPhongBan.ValueMember = "MaPhong";    // Lấy giá trị là ID
        }

        // --- 2. XỬ LÝ CLICK VÀO LƯỚI (HIỆN THÔNG TIN LÊN TRÊN) ---
        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Gọi hàm xử lý click (viết tách ra cho gọn)
            HandleCellClick(e.RowIndex);
        }

        // (Nếu bạn có sự kiện CellClick thì cũng gọi hàm này vào)
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleCellClick(e.RowIndex);
        }

        void HandleCellClick(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[rowIndex];

                // Gán dữ liệu vào các ô nhập
                // Lưu ý: Tên cột ("colMaNV", "colHoTen"...) phải trùng với tên bạn đặt trong Edit Columns
                // Nếu chưa đặt Name cho cột thì dùng DataPropertyName: row.Cells[0].Value...

                txtMaNV.Text = row.Cells["colMaNV"].Value.ToString();
                txtHoTen.Text = row.Cells["colHoTen"].Value.ToString();
                texCCCD.Text = row.Cells["colCCCD"].Value != null ? row.Cells["colCCCD"].Value.ToString() : "";
                txtSDT.Text = row.Cells["colSDT"].Value != null ? row.Cells["colSDT"].Value.ToString() : "";
                txtChucVu.Text = row.Cells["colChucVu"].Value != null ? row.Cells["colChucVu"].Value.ToString() : "";
                txtLuong.Text = row.Cells["colLuong"].Value != null ? row.Cells["colLuong"].Value.ToString() : "0";

                // Xử lý Ngày sinh
                if (row.Cells["colNgaySinh"].Value != null)
                    dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["colNgaySinh"].Value);

                // Xử lý Giới tính
                string gioiTinh = row.Cells["colGioiTinh"].Value != null ? row.Cells["colGioiTinh"].Value.ToString() : "Nam";
                if (gioiTinh == "Nam") radioNam.Checked = true;
                else radioNu.Checked = true;

                // Xử lý Phòng ban
                if (row.Cells["colMaPhong"].Value != null)
                {
                    cboPhongBan.SelectedValue = row.Cells["colMaPhong"].Value;
                }

                // Xử lý Ảnh đại diện
                string imgPath = row.Cells["AnhDaiDien"].Value != null ? row.Cells["AnhDaiDien"].Value.ToString() : "";

                if (!string.IsNullOrEmpty(imgPath) && File.Exists(Path.Combine(Application.StartupPath, imgPath)))
                {
                    picAvatar.Image = Image.FromFile(Path.Combine(Application.StartupPath, imgPath));
                    currentAvatarPath = imgPath;
                }
                else
                {
                    picAvatar.Image = null;
                    currentAvatarPath = "";
                }
            }
        }

        // --- 3. UPLOAD ẢNH ---
        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                picAvatar.Image = Image.FromFile(open.FileName);
                // Lưu tạm đường dẫn gốc vào Tag để lát copy
                picAvatar.Tag = open.FileName;
            }
        }

        // --- MỚI: XÓA ẢNH ---
        private void btnxoaanh_Click(object sender, EventArgs e)
        {
            // Xóa ảnh trên giao diện
            picAvatar.Image = null;
            // Xóa tag (để không upload file mới)
            picAvatar.Tag = null;
            // Xóa đường dẫn hiện tại (để khi lưu sẽ xóa trong DB)
            currentAvatarPath = "";
        }

        // Hàm hỗ trợ copy ảnh vào thư mục Project
        private string SaveAvatar(string sourcePath, string maNV)
        {
            if (string.IsNullOrEmpty(sourcePath)) return "";

            // Tạo tên file mới để tránh trùng: Avatar_MaNV_TimeTick.jpg
            string fileName = "Avatar_" + maNV + "_" + DateTime.Now.Ticks + Path.GetExtension(sourcePath);
            string destFolder = Path.Combine(Application.StartupPath, "Images", "Avatars");

            // Tạo thư mục nếu chưa có
            if (!Directory.Exists(destFolder)) Directory.CreateDirectory(destFolder);

            string destPath = Path.Combine(destFolder, fileName);
            File.Copy(sourcePath, destPath, true);

            return "Images/Avatars/" + fileName; // Trả về đường dẫn tương đối để lưu vào DB
        }

        // --- 4. NÚT THÊM ---
        private void btnadd_Click(object sender, EventArgs e)
        {
            // Validate dữ liệu
            if (!string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Bạn đang chọn một nhân viên có sẵn.\nVui lòng nhấn nút 'Reset ô' để làm mới trước khi Thêm!", "Nhầm lẫn thao tác", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng lại ngay, không chạy tiếp
            }

            // 2. Validate dữ liệu (Code cũ của bạn)
            if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(texCCCD.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ tên và CCCD!");
                return;
            }

            NhanVien nv = new NhanVien();
            nv.HoTen = txtHoTen.Text;
            nv.CCCD = texCCCD.Text;
            nv.SDT = txtSDT.Text;
            nv.NgaySinh = dtpNgaySinh.Value;
            nv.GioiTinh = radioNam.Checked ? "Nam" : "Nữ";
            nv.ChucVu = txtChucVu.Text;

            decimal luong = 0;
            decimal.TryParse(txtLuong.Text, out luong);
            nv.LuongTheoGio = luong;

            if (cboPhongBan.SelectedValue != null)
                nv.MaPhong = (int)cboPhongBan.SelectedValue;

            // Xử lý ảnh
            if (picAvatar.Image != null && picAvatar.Tag != null)
            {
                nv.AnhDaiDien = SaveAvatar(picAvatar.Tag.ToString(), "New");
            }

            // Gọi BUS thêm
            if (busNV.Add(nv))
            {
                MessageBox.Show("Thêm nhân viên thành công!");
                LoadData();

                // --- QUAN TRỌNG: Thêm xong phải Reset form ngay ---
                // Để người dùng không bị nhầm lẫn giữa nhân viên vừa thêm và nhân viên đang hiện
                btnLamMoi_Click(null, null);
            }
            else
            {
                MessageBox.Show("Thêm thất bại! (Có thể trùng số CCCD)");
            }
        }
        // --- 5. NÚT SỬA (ĐÃ NÂNG CẤP: TỰ XÓA ẢNH CŨ) ---
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có đang chọn nhân viên nào không
            if (string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!");
                return;
            }

            int id = int.Parse(txtMaNV.Text);

            // --- BƯỚC 1: GIẢI PHÓNG ẢNH CŨ TRƯỚC KHI LÀM BẤT CỨ GÌ ---
            // Điều này giúp tránh lỗi file bị khóa khi ta thao tác liên tục
            if (picAvatar.Image != null)
            {
                picAvatar.Image.Dispose();
                picAvatar.Image = null;
            }

            // --- BƯỚC 2: LẤY DỮ LIỆU TỪ FORM ---
            NhanVien nv = new NhanVien();
            nv.MaNV = id;
            nv.HoTen = txtHoTen.Text;
            nv.CCCD = texCCCD.Text;
            nv.SDT = txtSDT.Text;
            nv.NgaySinh = dtpNgaySinh.Value;
            nv.GioiTinh = radioNam.Checked ? "Nam" : "Nữ";
            nv.ChucVu = txtChucVu.Text;

            decimal luong = 0;
            decimal.TryParse(txtLuong.Text, out luong);
            nv.LuongTheoGio = luong;

            if (cboPhongBan.SelectedValue != null)
                nv.MaPhong = (int)cboPhongBan.SelectedValue;

            // --- BƯỚC 3: XỬ LÝ LOGIC ẢNH ---
            // picAvatar.Tag chứa đường dẫn file ảnh MỚI (nếu user vừa chọn Upload)
            if (picAvatar.Tag != null)
            {
                // Xóa ảnh cũ trên ổ cứng nếu có (để dọn rác)
                if (!string.IsNullOrEmpty(currentAvatarPath))
                {
                    try
                    {
                        string oldPath = Path.Combine(Application.StartupPath, currentAvatarPath);
                        if (File.Exists(oldPath)) File.Delete(oldPath);
                    }
                    catch { /* Nếu không xóa được thì thôi, bỏ qua lỗi này */ }
                }

                // Lưu ảnh mới
                nv.AnhDaiDien = SaveAvatar(picAvatar.Tag.ToString(), id.ToString());
            }
            else
            {
                // Không chọn ảnh mới -> Giữ nguyên đường dẫn cũ
                nv.AnhDaiDien = currentAvatarPath;
            }

            // --- BƯỚC 4: GỌI BUS UPDATE ---
            if (busNV.Update(nv))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
                btnLamMoi_Click(null, null); // Reset form để tránh lỗi tiếp theo
            }
            else
            {
                MessageBox.Show("Lỗi cập nhật! (Kiểm tra lại kết nối hoặc dữ liệu)");
                // Load lại dữ liệu để phục hồi ảnh hiển thị nếu bị lỗi
                LoadData();
            }
        }
        // --- 6. NÚT XÓA ---
        private void btndelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text)) return;

            if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int id = int.Parse(txtMaNV.Text);
                if (busNV.Delete(id))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Không thể xóa (Nhân viên này đang có dữ liệu chấm công)!");
                }
            }
        }

        // --- 7. NÚT LÀM MỚI ---
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            texCCCD.Clear();
            txtSDT.Clear();
            txtChucVu.Clear();
            txtLuong.Text = "20000";

            dtpNgaySinh.Value = DateTime.Now;
            radioNam.Checked = true;

            picAvatar.Image = null;
            picAvatar.Tag = null;
            currentAvatarPath = "";

            if (cboPhongBan.Items.Count > 0) cboPhongBan.SelectedIndex = 0;
        }

        // --- CÁC HÀM SỰ KIỆN KHÁC (ĐỂ TRỐNG CŨNG ĐƯỢC) ---
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void txtHoTen_TextChanged(object sender, EventArgs e) { }
        private void dtpNgaySinh_ValueChanged(object sender, EventArgs e) { }
        private void radioNam_CheckedChanged(object sender, EventArgs e) { }
        private void radioNu_CheckedChanged(object sender, EventArgs e) { }
        private void txtSDT_TextChanged(object sender, EventArgs e) { }
        private void txtChucVu_TextChanged(object sender, EventArgs e) { }
        private void cboPhongBan_SelectedIndexChanged(object sender, EventArgs e) { }
        private void picAvatar_Click(object sender, EventArgs e) { }
        private void txtTongSoNhanVien_TextChanged(object sender, EventArgs e) { }
        private void texCCCD_TextChanged(object sender, EventArgs e) { }
        private void txtLuong_TextChanged(object sender, EventArgs e) { }
        private void dgvNhanVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra xem cột đang vẽ có phải là cột "Mã Phòng" không
            // (Bạn nhớ kiểm tra kỹ tên cột bên Design có đúng là colMaPhong không nhé)
            if (dgvNhanVien.Columns[e.ColumnIndex].Name == "colMaPhong" && e.Value != null)
            {
                // Lấy đối tượng Nhân viên của dòng hiện tại
                NhanVien nv = dgvNhanVien.Rows[e.RowIndex].DataBoundItem as NhanVien;

                // Nếu tìm thấy Phòng ban tương ứng
                if (nv != null && nv.PhongBan != null)
                {
                    // Thay thế hiển thị từ số ID sang Tên Phòng
                    e.Value = nv.PhongBan.TenPhong;
                    e.FormattingApplied = true;
                }
            }
        }
    }
}