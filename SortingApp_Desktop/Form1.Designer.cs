namespace SortingApp_Desktop
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txt_ItemCode = new TextBox();
            label3 = new Label();
            label6 = new Label();
            lblTotalItem = new Label();
            groupBox1 = new GroupBox();
            lbl_ItemCode = new Label();
            lblNumberBagCumulative = new Label();
            lblNumberBagConfirm = new Label();
            lblNumberBagBD10 = new Label();
            label7 = new Label();
            label5 = new Label();
            label4 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            rbScanInBag = new RadioButton();
            rbScanOutBag = new RadioButton();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // txt_ItemCode
            // 
            txt_ItemCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txt_ItemCode.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txt_ItemCode.Location = new Point(922, 21);
            txt_ItemCode.Margin = new Padding(3, 4, 3, 4);
            txt_ItemCode.Multiline = true;
            txt_ItemCode.Name = "txt_ItemCode";
            txt_ItemCode.Size = new Size(934, 41);
            txt_ItemCode.TabIndex = 0;
            txt_ItemCode.TextAlign = HorizontalAlignment.Center;
            txt_ItemCode.TextChanged += txt_ItemCode_TextChanged;
            txt_ItemCode.KeyDown += txt_ItemCode_KeyDown;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(714, 30);
            label3.Name = "label3";
            label3.Size = new Size(202, 29);
            label3.TabIndex = 5;
            label3.Text = "Mã bưu gửi/Mã túi";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Red;
            label6.Location = new Point(304, 24);
            label6.Name = "label6";
            label6.Size = new Size(263, 20);
            label6.TabIndex = 14;
            label6.Text = "Ấn F2 để mở cấu hình hướng đóng";
            // 
            // lblTotalItem
            // 
            lblTotalItem.Dock = DockStyle.Left;
            lblTotalItem.Font = new Font("Microsoft Sans Serif", 100F, FontStyle.Bold);
            lblTotalItem.ForeColor = Color.Red;
            lblTotalItem.Location = new Point(3, 42);
            lblTotalItem.Name = "lblTotalItem";
            lblTotalItem.Size = new Size(237, 150);
            lblTotalItem.TabIndex = 10;
            lblTotalItem.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(lbl_ItemCode);
            groupBox1.Controls.Add(lblNumberBagCumulative);
            groupBox1.Controls.Add(lblNumberBagConfirm);
            groupBox1.Controls.Add(lblNumberBagBD10);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(lblTotalItem);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(3, 478);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(1862, 196);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Hướng vừa quét";
            // 
            // lbl_ItemCode
            // 
            lbl_ItemCode.Font = new Font("Microsoft Sans Serif", 100F, FontStyle.Bold);
            lbl_ItemCode.ForeColor = Color.Black;
            lbl_ItemCode.Location = new Point(246, 42);
            lbl_ItemCode.Name = "lbl_ItemCode";
            lbl_ItemCode.Size = new Size(1616, 156);
            lbl_ItemCode.TabIndex = 17;
            lbl_ItemCode.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNumberBagCumulative
            // 
            lblNumberBagCumulative.AutoSize = true;
            lblNumberBagCumulative.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNumberBagCumulative.ForeColor = Color.Orange;
            lblNumberBagCumulative.Location = new Point(1735, 3);
            lblNumberBagCumulative.Name = "lblNumberBagCumulative";
            lblNumberBagCumulative.Size = new Size(0, 39);
            lblNumberBagCumulative.TabIndex = 16;
            // 
            // lblNumberBagConfirm
            // 
            lblNumberBagConfirm.AutoSize = true;
            lblNumberBagConfirm.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNumberBagConfirm.ForeColor = Color.Crimson;
            lblNumberBagConfirm.Location = new Point(1170, 3);
            lblNumberBagConfirm.Name = "lblNumberBagConfirm";
            lblNumberBagConfirm.Size = new Size(0, 39);
            lblNumberBagConfirm.TabIndex = 15;
            // 
            // lblNumberBagBD10
            // 
            lblNumberBagBD10.AutoSize = true;
            lblNumberBagBD10.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNumberBagBD10.ForeColor = Color.DodgerBlue;
            lblNumberBagBD10.Location = new Point(610, 3);
            lblNumberBagBD10.Name = "lblNumberBagBD10";
            lblNumberBagBD10.Size = new Size(0, 39);
            lblNumberBagBD10.TabIndex = 14;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(1321, 3);
            label7.Name = "label7";
            label7.Size = new Size(383, 39);
            label7.TabIndex = 13;
            label7.Text = "Số túi trong BD10 lũy kế";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(763, 3);
            label5.Name = "label5";
            label5.Size = new Size(364, 39);
            label5.TabIndex = 12;
            label5.Text = "Số túi đã xác nhận đến";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(279, 3);
            label4.Name = "label4";
            label4.Size = new Size(285, 39);
            label4.TabIndex = 11;
            label4.Text = "Số túi trong BD10";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 1);
            tableLayoutPanel1.Location = new Point(0, 88);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1868, 678);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1862, 468);
            flowLayoutPanel1.TabIndex = 17;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Times New Roman", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(1868, 71);
            label1.TabIndex = 1;
            label1.Text = "Quét thông tin bưu gửi";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Red;
            label2.Location = new Point(304, 46);
            label2.Name = "label2";
            label2.Size = new Size(185, 20);
            label2.TabIndex = 15;
            label2.Text = "Ấn F3 để mở quản lý túi";
            // 
            // rbScanInBag
            // 
            rbScanInBag.AutoSize = true;
            rbScanInBag.Checked = true;
            rbScanInBag.Location = new Point(586, 23);
            rbScanInBag.Name = "rbScanInBag";
            rbScanInBag.Size = new Size(111, 24);
            rbScanInBag.TabIndex = 16;
            rbScanInBag.TabStop = true;
            rbScanInBag.Text = "Quét vào túi";
            rbScanInBag.UseVisualStyleBackColor = true;
            // 
            // rbScanOutBag
            // 
            rbScanOutBag.AutoSize = true;
            rbScanOutBag.Location = new Point(586, 50);
            rbScanOutBag.Name = "rbScanOutBag";
            rbScanOutBag.Size = new Size(100, 24);
            rbScanOutBag.TabIndex = 17;
            rbScanOutBag.Text = "Quét ra túi";
            rbScanOutBag.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.Red;
            label8.Location = new Point(304, 2);
            label8.Name = "label8";
            label8.Size = new Size(215, 20);
            label8.TabIndex = 18;
            label8.Text = "Ấn F1 để mở Kế hoạch chia";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.Red;
            label9.Location = new Point(304, 66);
            label9.Name = "label9";
            label9.Size = new Size(168, 20);
            label9.TabIndex = 19;
            label9.Text = "Ấn F4 để mở báo cáo";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(-3, 175);
            label10.Name = "label10";
            label10.Size = new Size(131, 20);
            label10.TabIndex = 0;
            label10.Text = "Phiên bản v1.1.0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1868, 778);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(rbScanOutBag);
            Controls.Add(rbScanInBag);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(txt_ItemCode);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(label6);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "abc";
            WindowState = FormWindowState.Maximized;
            FormClosing += Form1_FormClosing;
            FormClosed += Form1_FormClosed;
            KeyDown += Form1_KeyDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_ItemCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private Label lblTotalItem;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private Label label2;
        private RadioButton rbScanInBag;
        private RadioButton rbScanOutBag;
        private Label label5;
        private Label label4;
        private Label label7;
        private Label lblNumberBagConfirm;
        private Label lblNumberBagBD10;
        private Label lblNumberBagCumulative;
        private Label label8;
        private Label lbl_ItemCode;
        private Label label9;
        private Label label10;
    }
}
