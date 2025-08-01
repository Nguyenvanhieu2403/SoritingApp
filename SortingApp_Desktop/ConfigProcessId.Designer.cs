namespace SortingApp_Desktop
{
    partial class ConfigProcessId
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
            cbProcessId = new ComboBox();
            label1 = new Label();
            btn_Choose = new Button();
            label2 = new Label();
            cbbDividingStage = new ComboBox();
            SuspendLayout();
            // 
            // cbProcessId
            // 
            cbProcessId.DropDownStyle = ComboBoxStyle.DropDownList;
            cbProcessId.FormattingEnabled = true;
            cbProcessId.Items.AddRange(new object[] { "Chiều đến", "Chiều đi" });
            cbProcessId.Location = new Point(225, 123);
            cbProcessId.Name = "cbProcessId";
            cbProcessId.Size = new Size(222, 28);
            cbProcessId.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 127);
            label1.Name = "label1";
            label1.Size = new Size(181, 20);
            label1.TabIndex = 1;
            label1.Text = "Chọn công đoạn khai thác";
            // 
            // btn_Choose
            // 
            btn_Choose.Location = new Point(225, 225);
            btn_Choose.Name = "btn_Choose";
            btn_Choose.Size = new Size(94, 29);
            btn_Choose.TabIndex = 2;
            btn_Choose.Text = "Chọn";
            btn_Choose.UseVisualStyleBackColor = true;
            btn_Choose.Click += btn_Choose_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 172);
            label2.Name = "label2";
            label2.Size = new Size(149, 20);
            label2.TabIndex = 4;
            label2.Text = "Chọn công đoạn chia";
            // 
            // cbbDividingStage
            // 
            cbbDividingStage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbDividingStage.FormattingEnabled = true;
            cbbDividingStage.Items.AddRange(new object[] { "Phân hướng và đóng túi", "Phân hướng" });
            cbbDividingStage.Location = new Point(225, 168);
            cbbDividingStage.Name = "cbbDividingStage";
            cbbDividingStage.Size = new Size(222, 28);
            cbbDividingStage.TabIndex = 3;
            // 
            // ConfigProcessId
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(565, 292);
            Controls.Add(label2);
            Controls.Add(cbbDividingStage);
            Controls.Add(btn_Choose);
            Controls.Add(label1);
            Controls.Add(cbProcessId);
            Name = "ConfigProcessId";
            Text = "Cấu hình công đoạn khai thác";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbProcessId;
        private Label label1;
        private Button btn_Choose;
        private Label label2;
        private ComboBox cbbDividingStage;
    }
}