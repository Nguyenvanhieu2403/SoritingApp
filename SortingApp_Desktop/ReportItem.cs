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
    public partial class ReportItem : Form
    {
        public ReportItem()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void ReportItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BagsManager bagsManager = new BagsManager();
                bagsManager.Show();
                this.Close();
            }
        }

        private async void ReportItem_Load(object sender, EventArgs e)
        {
            await GetAllReportItems();
        }

        public async Task GetAllReportItems()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            ConnectToSql connectToSql = new ConnectToSql(configuration);
            PackagingDirectionRepos packagingDirectionRepos = new PackagingDirectionRepos(configuration);


            var result = await packagingDirectionRepos.SearchReportItemAsync(Global.MailNumberReport, Global.BagNumberReport, Global.ServiceCodeReport);

            listReportGerenalDetail.Items.Clear();
            listReportGerenalDetail.Columns.Clear();
            listReportGerenalDetail.View = View.Details;

            listReportGerenalDetail.Columns.Add("STT", 100);
            listReportGerenalDetail.Columns.Add("Số túi", 100);
            listReportGerenalDetail.Columns.Add("Mã bưu gửi", 200);
            listReportGerenalDetail.Columns.Add("Dịch vụ", 200);
            listReportGerenalDetail.Columns.Add("Khối lượng", 200);
            int i = 1;
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                return;
            }
            foreach (var product in result.Data)
            {
                ListViewItem item = new ListViewItem(i.ToString());
                item.SubItems.Add(product.BagNumber);
                item.SubItems.Add(product.ItemCode.ToString());
                item.SubItems.Add(product.ServiceCode.ToString());
                item.SubItems.Add(product.ItemWeight.ToString());
                item.Tag = product.ItemCode;
                listReportGerenalDetail.Items.Add(item);
                i++;
            }
        }

        private async void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                PackagingDirectionRepos packagingDirectionRepos = new PackagingDirectionRepos(configuration);

                var memoryStream = await packagingDirectionRepos.ExportExcelReportItemAsync(
                    Global.MailNumberReport,
                    Global.BagNumberReport,
                    Global.ServiceCodeReport);

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files|*.csv";
                    saveFileDialog.Title = "Save CSV File";
                    saveFileDialog.FileName = "BaoCaoBuuGuiTrongTui.csv";

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

                var memoryStream = await packagingDirectionRepos.ExportExcel1ReportItemAsync(
                    Global.MailNumberReport,
                    Global.BagNumberReport,
                    Global.ServiceCodeReport);

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.Title = "Save Excel File";
                    saveFileDialog.FileName = "BaoCaoBuuGuiTrongTui.xlsx";

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
