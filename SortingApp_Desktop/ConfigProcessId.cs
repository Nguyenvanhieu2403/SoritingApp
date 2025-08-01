using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortingApp_Desktop
{
    public partial class ConfigProcessId : Form
    {
        public static int ProcessId { get; set; }
        public static int DividingStage { get; set; }
        public ConfigProcessId()
        {
            InitializeComponent();
            var listProcessId = new Dictionary<string, int>();
            listProcessId.Add("Chiều đến", 1);
            listProcessId.Add("Chiều đi", 2);
            listProcessId.Add("Đóng túi tổng hợp", 3);
            listProcessId.Add("Túi chuyển hoàn", 4);
            cbProcessId.DataSource = new BindingSource(listProcessId, null);
            cbProcessId.DisplayMember = "Key";
            cbProcessId.ValueMember = "Value";

            var listDividingStage = new Dictionary<string, int>();
            listDividingStage.Add("Phân hướng và đóng túi", 1);
            listDividingStage.Add("Chỉ phân hướng", 2);
            cbbDividingStage.DataSource = new BindingSource(listDividingStage, null);
            cbbDividingStage.DisplayMember = "Key";
            cbbDividingStage.ValueMember = "Value";
        }

        private void btn_Choose_Click(object sender, EventArgs e)
        {
            cbProcessId.SelectedItem = cbProcessId.SelectedValue;
            ProcessId = (int)cbProcessId.SelectedValue;

            cbbDividingStage.SelectedItem = cbbDividingStage.SelectedValue;
            DividingStage = (int)cbbDividingStage.SelectedValue;

            Global.ProcessId = ProcessId;
            Global.DividingStage = DividingStage;

            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
