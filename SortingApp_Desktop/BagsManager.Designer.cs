namespace SortingApp_Desktop
{
    partial class BagsManager
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
            btnBack = new Button();
            ServiceCode = new ColumnHeader();
            displayName = new ColumnHeader();
            name = new ColumnHeader();
            STT = new ColumnHeader();
            Id = new ColumnHeader();
            listConfigureDirection = new ListView();
            groupBox4 = new GroupBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            bntViewDetail = new Button();
            btnPrintBD8 = new Button();
            groupBox4.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Location = new Point(12, 89);
            btnBack.Margin = new Padding(3, 4, 3, 4);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(127, 40);
            btnBack.TabIndex = 18;
            btnBack.Text = "Trở về (ESC)";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // ServiceCode
            // 
            ServiceCode.Text = "Dịch vụ";
            // 
            // displayName
            // 
            displayName.DisplayIndex = 2;
            displayName.Text = "Mô tả hướng đóng";
            displayName.Width = 600;
            // 
            // name
            // 
            name.DisplayIndex = 1;
            name.Text = "Tên hướng đóng";
            name.Width = 600;
            // 
            // STT
            // 
            STT.DisplayIndex = 0;
            STT.Text = "STT";
            STT.Width = 200;
            // 
            // Id
            // 
            Id.DisplayIndex = 3;
            Id.Text = "Id";
            Id.Width = 100;
            // 
            // listConfigureDirection
            // 
            listConfigureDirection.Columns.AddRange(new ColumnHeader[] { Id, STT, name, displayName, ServiceCode });
            listConfigureDirection.Dock = DockStyle.Fill;
            listConfigureDirection.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listConfigureDirection.FullRowSelect = true;
            listConfigureDirection.Location = new Point(3, 24);
            listConfigureDirection.Margin = new Padding(3, 4, 3, 4);
            listConfigureDirection.MultiSelect = false;
            listConfigureDirection.Name = "listConfigureDirection";
            listConfigureDirection.Size = new Size(1305, 778);
            listConfigureDirection.TabIndex = 19;
            listConfigureDirection.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox4.Controls.Add(listConfigureDirection);
            groupBox4.Location = new Point(6, 137);
            groupBox4.Margin = new Padding(3, 4, 3, 4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(3, 4, 3, 4);
            groupBox4.Size = new Size(1311, 806);
            groupBox4.TabIndex = 21;
            groupBox4.TabStop = false;
            groupBox4.Text = "Danh sách";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Times New Roman", 30F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(1323, 71);
            label1.TabIndex = 20;
            label1.Text = "Quản lý Túi";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(bntViewDetail);
            groupBox1.Controls.Add(btnPrintBD8);
            groupBox1.Controls.Add(btnBack);
            groupBox1.Controls.Add(groupBox4);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(1323, 951);
            groupBox1.TabIndex = 21;
            groupBox1.TabStop = false;
            groupBox1.Text = "Control";
            // 
            // bntViewDetail
            // 
            bntViewDetail.Location = new Point(357, 89);
            bntViewDetail.Margin = new Padding(3, 4, 3, 4);
            bntViewDetail.Name = "bntViewDetail";
            bntViewDetail.Size = new Size(127, 40);
            bntViewDetail.TabIndex = 23;
            bntViewDetail.Text = "Xem chi tiết";
            bntViewDetail.UseVisualStyleBackColor = true;
            bntViewDetail.Click += bntViewDetail_Click;
            // 
            // btnPrintBD8
            // 
            btnPrintBD8.Location = new Point(186, 89);
            btnPrintBD8.Margin = new Padding(3, 4, 3, 4);
            btnPrintBD8.Name = "btnPrintBD8";
            btnPrintBD8.Size = new Size(127, 40);
            btnPrintBD8.TabIndex = 22;
            btnPrintBD8.Text = "In BD8";
            btnPrintBD8.UseVisualStyleBackColor = true;
            btnPrintBD8.Click += btnPrintBD8_Click;
            // 
            // BagsManager
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1323, 951);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Name = "BagsManager";
            Text = "Quản lý túi";
            WindowState = FormWindowState.Maximized;
            Load += BagsManager_Load;
            KeyDown += BagsManager_KeyDown;
            groupBox4.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnBack;
        private ColumnHeader ServiceCode;
        private ColumnHeader displayName;
        private ColumnHeader name;
        private ColumnHeader STT;
        private ColumnHeader Id;
        private ListView listConfigureDirection;
        private GroupBox groupBox4;
        private Label label1;
        private GroupBox groupBox1;
        private Button btnPrintBD8;
        private Button bntViewDetail;
    }
}