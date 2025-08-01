using FastReport.Export.PdfSimple;
using Microsoft.Extensions.Configuration;
using SortingApp_Desktop.DataContext;
using SortingApp_Desktop.DataContext.models;
using SortingApp_Desktop.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SortingApp_Desktop
{
    public partial class CloseBag : Form
    {
        public Action RefreshParent { get; set; }
        public CloseBag()
        {
            InitializeComponent();
            this.KeyPreview = true;
            lblCountItem.Text = Global.CountItem.ToString();
            lblTotalWeight.Text = Global.TotalWeight.ToString();
            txtbagNumber.Text = Global.BagNumber.ToString();
            lblMailNumber.Text = Global.MailNumber.ToString();
            lblOriginalPost.Text = Global.OriginalPost.ToString();
            lblDestinationPosCode.Text = Global.DestinationPosCode.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            RefreshParent?.Invoke();
        }


        private async void btnCLoseBag_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Bạn có chắc muốn đóng túi không?",
                                          "Xác nhận đóng túi",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Error);
            if (confirm == DialogResult.No)
            {
                return;
            }

            if (string.IsNullOrEmpty(txtbagNumber.Text))
            {
                MessageBox.Show("Lỗi chưa có số túi");
                return;
            }

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            BagRepos bagRepos = new BagRepos(configuration);

            BagInput bagInput = new BagInput();
            bagInput.ConfigId = Global.ConfigId;
            bagInput.BagNumber = txtbagNumber.Text;
            //bagInput.MailNumber = txtMailNumber.Text;

            var result = await bagRepos.Create(bagInput);
            if ((result.Data?.BagWeight ?? 0m) > 0m)
            {
                MessageBox.Show("Thêm mới thành công");
                this.Close();
                RefreshParent?.Invoke();
                txtbagNumber.Text = "";
                //txtMailNumber.Text = "";
                //lblCountItem.Text = Global.CountItem.ToString();
                //lblTotalWeight.Text = Global.TotalWeight.ToString();
            }
            else
            {
                MessageBox.Show("Thêm mới thất bại");
            }
        }

        private async void btnPrintBD8_Click(object sender, EventArgs e)
        {

            if(lblCountItem.Text == "0")
            {
                MessageBox.Show("Chưa có bưu gửi nào ở trong túi");
                return;
            }

            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();
            ConnectToSql connectToSql = new ConnectToSql(configuration);

            BagRepos bagRepos = new BagRepos(configuration);
            CloseBagInput closeBagInput = new CloseBagInput();
            closeBagInput.MailNumber = lblMailNumber.Text;
            closeBagInput.OriginalPost = lblOriginalPost.Text;
            closeBagInput.DestinationPosCode = lblDestinationPosCode.Text;
            closeBagInput.BagNumber = txtbagNumber.Text;
            closeBagInput.MailType = "A";
            closeBagInput.ConfigId = Global.ConfigId;


            var result = await bagRepos.CloseBag(closeBagInput);

            if (result.Data == null)
            {
                MessageBox.Show(result.Error);
                return;
            }

            FastReport.Report report = new FastReport.Report();
            report.Load("ReportBD8.frx");

            // Truyền dữ liệu từ stored procedure (DataTable)
            report.SetParameterValue("MailNumber", result.Data.MailNumber ?? "");
            report.SetParameterValue("CreateDate_BD8", result.Data.CreateDate_BD8 ?? "");
            report.SetParameterValue("BagNumber", "1");
            report.SetParameterValue("BagWeight", result.Data.BagWeight ?? "");
            report.SetParameterValue("ServiceCode", result.Data.ServiceCode ?? "");
            report.SetParameterValue("OriginalPost", result.Data.OriginalPost ?? "");
            report.SetParameterValue("DestinationPosCode", result.Data.DestinationPosCode ?? "");
            report.SetParameterValue("DistrictCode", result.Data.DistrictCode ?? "");
            report.SetParameterValue("ProvinceCode", result.Data.ProvinceCode ?? "");
            report.SetParameterValue("ProvinceName", result.Data.ProvinceName ?? "");
            report.SetParameterValue("BD8Code", result.Data.BD8Code ?? "");
            report.SetParameterValue("Note", result.Data.Note ?? "");
            report.Prepare();

            string timePart = DateTime.Now.ToString("ddMMyyyyHHmmss");
            string fileName = $"BD8_{timePart}.pdf";
            string filePath = Path.Combine(Application.StartupPath, fileName);

            PDFSimpleExport pdfExport = new PDFSimpleExport();
            report.Export(pdfExport, fileName);
            report.Dispose();

            MessageBox.Show("Đóng túi thành công, đang mở file BD8");
            //using (var document = PdfiumViewer.PdfDocument.Load(filePath))
            //{
            //    using (var printDoc = document.CreatePrintDocument())
            //    {
            //        printDoc.PrinterSettings = new PrinterSettings
            //        {
            //            PrinterName = new PrinterSettings().PrinterName
            //        };
            //        PrintDialog dialog = new PrintDialog();
            //        dialog.Document = printDoc;
            //        if (dialog.ShowDialog() == DialogResult.OK)
            //        {
            //            printDoc.Print();
            //        }
            //    }
            //}

            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });

            this.Close();
        }

        private void CloseBag_Load(object sender, EventArgs e)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            ConnectToSql connectToSql = new ConnectToSql(configuration);
        }
    }
}
