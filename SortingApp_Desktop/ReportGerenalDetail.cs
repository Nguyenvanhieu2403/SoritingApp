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
    public partial class ReportGerenalDetail : Form
    {
        public ReportGerenalDetail()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ReportGeneral reportGeneral = new ReportGeneral();
            reportGeneral.Show();
            this.Hide();
        }

        public async Task GetAllReportGeneralDetail()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            ConnectToSql connectToSql = new ConnectToSql(configuration);
            PackagingDirectionRepos packagingDirectionRepos = new PackagingDirectionRepos(configuration);


            var result = await packagingDirectionRepos.SearchReportGeneralDetailAsync(Global.ProcessIdReportGeneral, Global.FromDateReportGeneral, Global.ToDateReportGeneral);

            listReportGerenalDetail.Items.Clear();
            listReportGerenalDetail.Columns.Clear();
            listReportGerenalDetail.View = View.Details;

            listReportGerenalDetail.Columns.Add("STT", 100);
            listReportGerenalDetail.Columns.Add("Thời gian quét", 400);
            listReportGerenalDetail.Columns.Add("Mã bưu gửi", 400);
            listReportGerenalDetail.Columns.Add("Hướng đóng", 400);
            listReportGerenalDetail.Columns.Add("Bưu cục phát", 200);
            int i = 1;
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                return;
            }
            foreach (var product in result.Data)
            {
                ListViewItem item = new ListViewItem(i.ToString());
                item.SubItems.Add(product.CreateDate.HasValue
                                    ? product.CreateDate.Value.ToString("dd/MM/yyyy HH:mm")
                                    : "");
                item.SubItems.Add(product.ItemCode.ToString());
                item.SubItems.Add(product.Direction.ToString());
                item.SubItems.Add(product.PosCode.ToString());
                item.Tag = product.ItemCode;
                listReportGerenalDetail.Items.Add(item);
                i++;
            }
        }

        private async void ReportGerenalDetail_Load_1(object sender, EventArgs e)
        {
            await GetAllReportGeneralDetail();
        }

        private async void btnExportExcel_Click_1(object sender, EventArgs e)
        {
            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                PackagingDirectionRepos packagingDirectionRepos = new PackagingDirectionRepos(configuration);

                var memoryStream = await packagingDirectionRepos.ExportExcelReportGeneralDetailAsync(
                    Global.ProcessId,
                    Global.FromDateReportGeneral,
                    Global.ToDateReportGeneral);

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files|*.csv";
                    saveFileDialog.Title = "Save CSV File";
                    saveFileDialog.FileName = "BaoCaoChung.csv";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
                        {
                            memoryStream.WriteTo(fileStream);
                        }

                        MessageBox.Show("Xuất CSV thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xuất CSV: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                PackagingDirectionRepos packagingDirectionRepos = new PackagingDirectionRepos(configuration);

                var memoryStream = await packagingDirectionRepos.ExportExcel1ReportGeneralDetailAsync(
                    Global.ProcessId,
                    Global.FromDateReportGeneral,
                    Global.ToDateReportGeneral);

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.Title = "Save Excel File";
                    saveFileDialog.FileName = "BaoCaoChung.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
                        {
                            memoryStream.WriteTo(fileStream);
                        }

                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
