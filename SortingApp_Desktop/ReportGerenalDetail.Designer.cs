namespace SortingApp_Desktop
{
    partial class ReportGerenalDetail
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
            groupBox1 = new GroupBox();
            btnExportExcel = new Button();
            label1 = new Label();
            listReportGerenalDetail = new ListView();
            button1 = new Button();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(listReportGerenalDetail, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.Size = new Size(1249, 711);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(btnExportExcel);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1243, 136);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Location = new Point(20, 23);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(140, 47);
            btnExportExcel.TabIndex = 6;
            btnExportExcel.Text = "Xuất csv";
            btnExportExcel.UseVisualStyleBackColor = true;
            btnExportExcel.Click += btnExportExcel_Click_1;
            // 
            // label1
            // 
            label1.Font = new Font("Times New Roman", 30F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(-3, 23);
            label1.Name = "label1";
            label1.Size = new Size(1246, 77);
            label1.TabIndex = 4;
            label1.Text = "Báo cáo chi tiết theo công đoạn";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // listReportGerenalDetail
            // 
            listReportGerenalDetail.Dock = DockStyle.Fill;
            listReportGerenalDetail.Location = new Point(3, 145);
            listReportGerenalDetail.Name = "listReportGerenalDetail";
            listReportGerenalDetail.Size = new Size(1243, 563);
            listReportGerenalDetail.TabIndex = 1;
            listReportGerenalDetail.UseCompatibleStateImageBehavior = false;
            // 
            // button1
            // 
            button1.Location = new Point(20, 76);
            button1.Name = "button1";
            button1.Size = new Size(140, 47);
            button1.TabIndex = 7;
            button1.Text = "Xuất excel";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ReportGerenalDetail
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1249, 711);
            Controls.Add(tableLayoutPanel1);
            Name = "ReportGerenalDetail";
            Text = "ReportGerenalDetail";
            Load += ReportGerenalDetail_Load_1;
            tableLayoutPanel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBox1;
        private Label label1;
        private ListView listReportGerenalDetail;
        private Button btnExportExcel;
        private Button button1;
    }

}