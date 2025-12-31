namespace GUI
{
    partial class frmReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dtpChonThang = new System.Windows.Forms.DateTimePicker();
            this.btnInBaoCao = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chọn tháng báo cáo";
            // 
            // dtpChonThang
            // 
            this.dtpChonThang.CustomFormat = "MM/yyyy";
            this.dtpChonThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChonThang.Location = new System.Drawing.Point(473, 44);
            this.dtpChonThang.Name = "dtpChonThang";
            this.dtpChonThang.ShowUpDown = true;
            this.dtpChonThang.Size = new System.Drawing.Size(200, 26);
            this.dtpChonThang.TabIndex = 1;
            this.dtpChonThang.ValueChanged += new System.EventHandler(this.dtpChonThang_ValueChanged);
            // 
            // btnInBaoCao
            // 
            this.btnInBaoCao.Location = new System.Drawing.Point(347, 122);
            this.btnInBaoCao.Name = "btnInBaoCao";
            this.btnInBaoCao.Size = new System.Drawing.Size(164, 46);
            this.btnInBaoCao.TabIndex = 2;
            this.btnInBaoCao.Text = "In Báo Cáo";
            this.btnInBaoCao.UseVisualStyleBackColor = true;
            this.btnInBaoCao.Click += new System.EventHandler(this.btnInBaoCao_Click);
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GUI.Properties.Resources.abstract_geometric_white_background_free_vector;
            this.ClientSize = new System.Drawing.Size(928, 273);
            this.Controls.Add(this.btnInBaoCao);
            this.Controls.Add(this.dtpChonThang);
            this.Controls.Add(this.label1);
            this.Name = "frmReport";
            this.Text = "Báo Cáo";
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpChonThang;
        private System.Windows.Forms.Button btnInBaoCao;
    }
}