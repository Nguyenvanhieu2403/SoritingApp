namespace SortingApp_Desktop
{
    partial class ReportGeneral
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
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            groupBox1 = new GroupBox();
            label3 = new Label();
            dtpToDate = new DateTimePicker();
            label2 = new Label();
            btnBack = new Button();
            btnSearch = new Button();
            dtpFromDate = new DateTimePicker();
            listReportGerenal = new ListView();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 1);
            tableLayoutPanel1.Controls.Add(listReportGerenal, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.Size = new Size(1125, 635);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Times New Roman", 30F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(1119, 63);
            label1.TabIndex = 3;
            label1.Text = "Báo cáo chung";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(dtpToDate);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnBack);
            groupBox1.Controls.Add(btnSearch);
            groupBox1.Controls.Add(dtpFromDate);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 66);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1119, 57);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(277, 23);
            label3.Name = "label3";
            label3.Size = new Size(72, 20);
            label3.TabIndex = 5;
            label3.Text = "Đến ngày";
            // 
            // dtpToDate
            // 
            dtpToDate.Location = new Point(356, 20);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new Size(181, 27);
            dtpToDate.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 23);
            label2.Name = "label2";
            label2.Size = new Size(62, 20);
            label2.TabIndex = 3;
            label2.Text = "Từ ngày";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(702, 19);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(132, 29);
            btnBack.TabIndex = 2;
            btnBack.Text = "Trở về (ESC)";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(584, 19);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // dtpFromDate
            // 
            dtpFromDate.Location = new Point(74, 20);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new Size(181, 27);
            dtpFromDate.TabIndex = 0;
            // 
            // listReportGerenal
            // 
            listReportGerenal.Dock = DockStyle.Fill;
            listReportGerenal.FullRowSelect = true;
            listReportGerenal.Location = new Point(3, 129);
            listReportGerenal.Name = "listReportGerenal";
            listReportGerenal.Size = new Size(1119, 503);
            listReportGerenal.TabIndex = 5;
            listReportGerenal.UseCompatibleStateImageBehavior = false;
            listReportGerenal.ItemSelectionChanged += listReportGerenal_ItemSelectionChanged_1;
            // 
            // ReportGeneral
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1125, 635);
            Controls.Add(tableLayoutPanel1);
            Name = "ReportGeneral";
            Text = "ReportGeneral";
            WindowState = FormWindowState.Maximized;
            Load += ReportGeneral_Load_1;
            KeyDown += ReportGeneral_KeyDown;
            tableLayoutPanel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private GroupBox groupBox1;
        private Button btnSearch;
        private DateTimePicker dtpFromDate;
        private Button btnBack;
        private ListView listReportGerenal;
        private Label label3;
        private DateTimePicker dtpToDate;
        private Label label2;
    }

}