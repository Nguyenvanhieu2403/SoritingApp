namespace SortingApp_Desktop
{
    partial class ConfigureDirection
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
            label1 = new Label();
            lblName = new Label();
            lblDisplayName = new Label();
            lblUnit = new Label();
            txtName = new TextBox();
            txtDisplayName = new TextBox();
            txtUnit = new TextBox();
            btnAdd = new Button();
            btnRemove = new Button();
            btnEdit = new Button();
            btnBack = new Button();
            groupBox1 = new GroupBox();
            groupBox4 = new GroupBox();
            listConfigureDirection = new ListView();
            Id = new ColumnHeader();
            STT = new ColumnHeader();
            name = new ColumnHeader();
            displayName = new ColumnHeader();
            ServiceCode = new ColumnHeader();
            groupBox3 = new GroupBox();
            btnTemplateImport = new Button();
            btnRefresh = new Button();
            btnImport = new Button();
            groupBox2 = new GroupBox();
            txtPTVC = new TextBox();
            label3 = new Label();
            txtDestinationPosCode = new TextBox();
            label2 = new Label();
            lblServiceCode = new Label();
            txtServiceCode = new TextBox();
            groupBox1.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Times New Roman", 30F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(1924, 71);
            label1.TabIndex = 2;
            label1.Text = "Quản lý hướng đóng";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblName.Location = new Point(314, 67);
            lblName.Name = "lblName";
            lblName.Size = new Size(119, 25);
            lblName.TabIndex = 9;
            lblName.Text = "Hướng đóng";
            // 
            // lblDisplayName
            // 
            lblDisplayName.AutoSize = true;
            lblDisplayName.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDisplayName.Location = new Point(637, 67);
            lblDisplayName.Name = "lblDisplayName";
            lblDisplayName.Size = new Size(156, 25);
            lblDisplayName.TabIndex = 10;
            lblDisplayName.Text = "Tên hướng đóng";
            // 
            // lblUnit
            // 
            lblUnit.AutoSize = true;
            lblUnit.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUnit.Location = new Point(1046, 61);
            lblUnit.Name = "lblUnit";
            lblUnit.Size = new Size(67, 25);
            lblUnit.TabIndex = 11;
            lblUnit.Text = "Đơn vị";
            // 
            // txtName
            // 
            txtName.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtName.Location = new Point(438, 55);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(158, 41);
            txtName.TabIndex = 2;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // txtDisplayName
            // 
            txtDisplayName.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDisplayName.Location = new Point(799, 55);
            txtDisplayName.Margin = new Padding(3, 4, 3, 4);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(229, 41);
            txtDisplayName.TabIndex = 3;
            // 
            // txtUnit
            // 
            txtUnit.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUnit.Location = new Point(1118, 55);
            txtUnit.Margin = new Padding(3, 4, 3, 4);
            txtUnit.Name = "txtUnit";
            txtUnit.Size = new Size(177, 41);
            txtUnit.TabIndex = 4;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdd.Location = new Point(48, 25);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 75);
            btnAdd.TabIndex = 15;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.FromArgb(231, 76, 60);
            btnRemove.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRemove.Location = new Point(351, 25);
            btnRemove.Margin = new Padding(3, 4, 3, 4);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(127, 75);
            btnRemove.TabIndex = 16;
            btnRemove.Text = "Xóa";
            btnRemove.UseVisualStyleBackColor = false;
            btnRemove.Click += btnRemove_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(52, 152, 219);
            btnEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEdit.Location = new Point(199, 25);
            btnEdit.Margin = new Padding(3, 4, 3, 4);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(128, 75);
            btnEdit.TabIndex = 17;
            btnEdit.Text = "Cập nhật";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(127, 140, 141);
            btnBack.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBack.Location = new Point(974, 28);
            btnBack.Margin = new Padding(3, 4, 3, 4);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(127, 75);
            btnBack.TabIndex = 18;
            btnBack.Text = "Trở về (ESC)";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(groupBox4);
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(1924, 951);
            groupBox1.TabIndex = 19;
            groupBox1.TabStop = false;
            groupBox1.Text = "Control";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(listConfigureDirection);
            groupBox4.Location = new Point(3, 327);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(1915, 624);
            groupBox4.TabIndex = 21;
            groupBox4.TabStop = false;
            groupBox4.Text = "Danh sách";
            // 
            // listConfigureDirection
            // 
            listConfigureDirection.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listConfigureDirection.Columns.AddRange(new ColumnHeader[] { Id, STT, name, displayName, ServiceCode });
            listConfigureDirection.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listConfigureDirection.FullRowSelect = true;
            listConfigureDirection.Location = new Point(3, 23);
            listConfigureDirection.Margin = new Padding(3, 4, 3, 4);
            listConfigureDirection.MultiSelect = false;
            listConfigureDirection.Name = "listConfigureDirection";
            listConfigureDirection.Size = new Size(1909, 598);
            listConfigureDirection.TabIndex = 19;
            listConfigureDirection.UseCompatibleStateImageBehavior = false;
            listConfigureDirection.ItemSelectionChanged += listConfigureDirection_ItemSelectionChanged;
            // 
            // Id
            // 
            Id.DisplayIndex = 3;
            Id.Text = "Id";
            Id.Width = 100;
            // 
            // STT
            // 
            STT.DisplayIndex = 0;
            STT.Text = "STT";
            STT.Width = 200;
            // 
            // name
            // 
            name.DisplayIndex = 1;
            name.Text = "Tên hướng đóng";
            name.Width = 600;
            // 
            // displayName
            // 
            displayName.DisplayIndex = 2;
            displayName.Text = "Mô tả hướng đóng";
            displayName.Width = 600;
            // 
            // ServiceCode
            // 
            ServiceCode.Text = "Dịch vụ";
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(btnTemplateImport);
            groupBox3.Controls.Add(btnRefresh);
            groupBox3.Controls.Add(btnImport);
            groupBox3.Controls.Add(btnEdit);
            groupBox3.Controls.Add(btnRemove);
            groupBox3.Controls.Add(btnBack);
            groupBox3.Controls.Add(btnAdd);
            groupBox3.Location = new Point(3, 212);
            groupBox3.Margin = new Padding(3, 4, 3, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 4, 3, 4);
            groupBox3.Size = new Size(1915, 108);
            groupBox3.TabIndex = 15;
            groupBox3.TabStop = false;
            groupBox3.Text = "Control";
            // 
            // btnTemplateImport
            // 
            btnTemplateImport.BackColor = Color.White;
            btnTemplateImport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTemplateImport.Location = new Point(662, 28);
            btnTemplateImport.Margin = new Padding(3, 4, 3, 4);
            btnTemplateImport.Name = "btnTemplateImport";
            btnTemplateImport.Size = new Size(127, 75);
            btnTemplateImport.TabIndex = 21;
            btnTemplateImport.Text = "Tải file mẫu import";
            btnTemplateImport.UseVisualStyleBackColor = false;
            btnTemplateImport.Click += btnTemplateImport_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.White;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.Location = new Point(816, 28);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(126, 75);
            btnRefresh.TabIndex = 20;
            btnRefresh.Text = "Làm mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnImport
            // 
            btnImport.BackColor = Color.FromArgb(243, 156, 18);
            btnImport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnImport.Location = new Point(503, 25);
            btnImport.Margin = new Padding(3, 4, 3, 4);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(127, 75);
            btnImport.TabIndex = 19;
            btnImport.Text = "Import excel";
            btnImport.UseVisualStyleBackColor = false;
            btnImport.Click += btnImport_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(txtPTVC);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(txtDestinationPosCode);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(lblServiceCode);
            groupBox2.Controls.Add(txtServiceCode);
            groupBox2.Controls.Add(txtDisplayName);
            groupBox2.Controls.Add(txtUnit);
            groupBox2.Controls.Add(lblDisplayName);
            groupBox2.Controls.Add(lblName);
            groupBox2.Controls.Add(lblUnit);
            groupBox2.Controls.Add(txtName);
            groupBox2.Location = new Point(3, 75);
            groupBox2.Margin = new Padding(3, 4, 3, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 4, 3, 4);
            groupBox2.Size = new Size(1918, 125);
            groupBox2.TabIndex = 20;
            groupBox2.TabStop = false;
            groupBox2.Text = "Input";
            // 
            // txtPTVC
            // 
            txtPTVC.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPTVC.Location = new Point(1782, 55);
            txtPTVC.Margin = new Padding(3, 4, 3, 4);
            txtPTVC.Name = "txtPTVC";
            txtPTVC.Size = new Size(98, 41);
            txtPTVC.TabIndex = 6;
            txtPTVC.Text = "A";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(1692, 61);
            label3.Name = "label3";
            label3.Size = new Size(67, 25);
            label3.TabIndex = 19;
            label3.Text = "PTVC";
            // 
            // txtDestinationPosCode
            // 
            txtDestinationPosCode.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDestinationPosCode.Location = new Point(1475, 55);
            txtDestinationPosCode.Margin = new Padding(3, 4, 3, 4);
            txtDestinationPosCode.Name = "txtDestinationPosCode";
            txtDestinationPosCode.Size = new Size(177, 41);
            txtDestinationPosCode.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(1347, 61);
            label2.Name = "label2";
            label2.Size = new Size(92, 25);
            label2.TabIndex = 17;
            label2.Text = "BC Nhận";
            // 
            // lblServiceCode
            // 
            lblServiceCode.AutoSize = true;
            lblServiceCode.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblServiceCode.Location = new Point(9, 63);
            lblServiceCode.Name = "lblServiceCode";
            lblServiceCode.Size = new Size(77, 25);
            lblServiceCode.TabIndex = 15;
            lblServiceCode.Text = "Dịch vụ";
            // 
            // txtServiceCode
            // 
            txtServiceCode.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtServiceCode.Location = new Point(93, 55);
            txtServiceCode.Margin = new Padding(3, 4, 3, 4);
            txtServiceCode.Name = "txtServiceCode";
            txtServiceCode.Size = new Size(158, 41);
            txtServiceCode.TabIndex = 1;
            // 
            // ConfigureDirection
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 951);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "ConfigureDirection";
            Text = "ConfigureDirection";
            WindowState = FormWindowState.Maximized;
            Load += ConfigureDirection_Load;
            KeyDown += ConfigureDirection_KeyDown;
            groupBox1.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private Label lblServiceCode;
        private TextBox txtServiceCode;
        private Button btnImport;
        private TextBox txtDestinationPosCode;
        private Label label2;
        private Button btnRefresh;
        private Button btnTemplateImport;
        private TextBox txtPTVC;
        private Label label3;
        private ListView listConfigureDirection;
        private ColumnHeader Id;
        private ColumnHeader STT;
        private ColumnHeader name;
        private ColumnHeader displayName;
        private ColumnHeader ServiceCode;
        private GroupBox groupBox4;
    }
}