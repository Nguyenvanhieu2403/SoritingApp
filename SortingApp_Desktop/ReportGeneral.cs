using Microsoft.Extensions.Configuration;
using SortingApp_Desktop.DataContext;
using SortingApp_Desktop.Repository;
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
    public partial class ReportGeneral : Form
    {
        public ReportGeneral()
        {
            InitializeComponent();
            this.KeyPreview = true;
            dtpFromDate.Value = DateTime.Now.AddDays(-1);
            dtpToDate.Value = DateTime.Now;
        }

        public async Task GetAllReportGeneral()
        {

            var FromDate = dtpFromDate.Value;
            var ToDate = dtpToDate.Value;

            if (FromDate == null || ToDate == null)
            {
                MessageBox.Show("Vui lòng chọn thời gian!");
                return;
            }

            if (FromDate > ToDate)
            {
                MessageBox.Show("Thời gian không hợp lệ!");
                return;
            }


            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            ConnectToSql connectToSql = new ConnectToSql(configuration);
            PackagingDirectionRepos packagingDirectionRepos = new PackagingDirectionRepos(configuration);


            var result = await packagingDirectionRepos.SearchReportGeneralAsync(FromDate, ToDate);

            listReportGerenal.Items.Clear();
            listReportGerenal.Columns.Clear();
            listReportGerenal.View = View.Details;

            listReportGerenal.Columns.Add("Công đoạn", 400);
            listReportGerenal.Columns.Add("Số lượng bưu gửi", 400);
            listReportGerenal.Columns.Add("Số lượng bưu gửi quét thành công", 400);
            listReportGerenal.Columns.Add("Số lượng bưu gửi quét không thành công", 400);
            listReportGerenal.Columns.Add("Tỷ lệ lỗi", 200);
            int i = 1;
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                return;
            }
            foreach (var product in result.Data)
            {
                ListViewItem item = new ListViewItem(product.ProcessName);
                item.SubItems.Add(product.CountTotalItem.ToString());
                item.SubItems.Add(product.CountItemSuccess.ToString());
                item.SubItems.Add(product.CountItemError.ToString());
                var totalItem = 1;
                if (product.CountTotalItem <= 0)
                {
                    totalItem = 1;
                }
                else
                {
                    totalItem = product.CountTotalItem;
                }
                double percent = ((double)product.CountItemError / totalItem) * 100;
                item.SubItems.Add(percent.ToString("0.##") + "%");
                item.Tag = product.ProcessId;
                listReportGerenal.Items.Add(item);
                i++;
            }
        }


        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await GetAllReportGeneral();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();
            mainForm.Show();
            this.Close();
        }

        private async void ReportGeneral_Load_1(object sender, EventArgs e)
        {
            await GetAllReportGeneral();
        }

        private void listReportGerenal_ItemSelectionChanged_1(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listReportGerenal.SelectedItems.Count > 0)
            {
                var selectedItem = listReportGerenal.SelectedItems[0];
                Global.ProcessIdReportGeneral = (int)selectedItem.Tag;
                Global.FromDateReportGeneral = dtpFromDate.Value;
                Global.ToDateReportGeneral = dtpToDate.Value;
                ReportGerenalDetail reportGerenalDetail = new ReportGerenalDetail();
                reportGerenalDetail.ShowDialog();
            }
        }

        private void ReportGeneral_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Form1 mainForm = new Form1();
                mainForm.Show();
                this.Close();
            }
        }
    }
}
