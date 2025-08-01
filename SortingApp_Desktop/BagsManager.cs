using Microsoft.Extensions.Configuration;
using SortingApp_Desktop.DataContext;
using SortingApp_Desktop.Repository;
using SortingApp_Net.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using IronBarCode;
using SortingApp_Desktop.Report;
using FastReport;
using FastReport.Export.PdfSimple;
using System.Diagnostics;
using System.Drawing.Printing;


namespace SortingApp_Desktop
{
    public partial class BagsManager : Form
    {
        public BagsManager()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        public async Task LoadDataToListViewAsync()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            ConnectToSql connectToSql = new ConnectToSql(configuration);
            BagRepos bagRepos = new BagRepos(configuration);

            var result = await bagRepos.SearchAsync(Global.ProcessId);

            listConfigureDirection.Items.Clear();
            listConfigureDirection.Columns.Clear();
            listConfigureDirection.View = View.Details;

            listConfigureDirection.Columns.Add("STT", 100);
            listConfigureDirection.Columns.Add("Số túi", 200);
            listConfigureDirection.Columns.Add("Số chuyến thư", 400);
            listConfigureDirection.Columns.Add("Số lượng bưu gửi", 400);
            listConfigureDirection.Columns.Add("Trọng lượng túi", 400);
            listConfigureDirection.Columns.Add("Dịch vụ", 200);
            listConfigureDirection.Columns.Add("Bưu cục nhận", 400);
            //listConfigureDirection.Columns.Add("Trọng lượng vỏ", 400);
            listConfigureDirection.Columns.Add("Ngày đóng túi", 400);
            int i = 1;
            foreach (var product in result.Data)
            {
                ListViewItem item = new ListViewItem(i.ToString());
                item.SubItems.Add(product.BagNumber);
                item.SubItems.Add(product.MailNumber);
                item.SubItems.Add(product.CountItem.ToString());
                item.SubItems.Add(product.BagWeight.ToString());
                item.SubItems.Add(product.ServiceCode ?? "");
                item.SubItems.Add(product.DestinationPosCode ?? "");
                //item.SubItems.Add(product.ShellWeight.ToString());
                item.SubItems.Add(product.Modified?.ToString("dd/MM/yyyy HH:mm") ?? "");
                item.Tag = Tuple.Create(product.MailNumber, product.BagNumber, product.DestinationPosCode);
                listConfigureDirection.Items.Add(item);
                i++;
            }

        }

        private async void BagsManager_Load(object sender, EventArgs e)
        {
            await LoadDataToListViewAsync();
        }

        private void BagsManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Form1 mainForm = new Form1();
                mainForm.Show();
                this.Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();
            mainForm.Show();
            this.Close();
        }

        private async void btnPrintBD8_Click(object sender, EventArgs e)
        {

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            ConnectToSql connectToSql = new ConnectToSql(configuration);
            BagRepos bagRepos = new BagRepos(configuration);

            if (listConfigureDirection.SelectedItems.Count > 0)
            {
                var selectedItem = listConfigureDirection.SelectedItems[0];
                var Tag = (Tuple<string, string, string>)selectedItem.Tag;

                if (Tag.Item1 == "" || Tag.Item1 == null)
                {
                    MessageBox.Show("Túi chưa được đóng!");
                    return;
                }

                var result = await bagRepos.PrintBD8(Tag.Item1.ToString(), Tag.Item2.ToString(), Tag.Item3);

                if (result.Data == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu túi bưu gửi!");
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
            }
        }

        //private void listConfigureDirection_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        //{
        //    if (listConfigureDirection.SelectedItems.Count > 0)
        //    {
        //        var selectedItem = listConfigureDirection.SelectedItems[0];
        //        var Tag = (Tuple<string, string>)selectedItem.Tag;
        //        var MailNumber = Tag.Item1.ToString();
        //        var BagNumber = Tag.Item2.ToString();
        //        var ServiceCode = "";

        //        string[] rowData = new string[selectedItem.SubItems.Count];
        //        for (int i = 0; i < selectedItem.SubItems.Count; i++)
        //        {
        //            if (i == 5)
        //            {
        //                ServiceCode = selectedItem.SubItems[i].Text;
        //            }
        //        }
        //        Global.MailNumberReport = MailNumber;
        //        Global.BagNumberReport = BagNumber;
        //        Global.ServiceCodeReport = ServiceCode;
        //        ReportItem reportItem = new ReportItem();
        //        reportItem.ShowDialog();

        //        //ReportGerenalDetail reportGerenalDetail = new ReportGerenalDetail();
        //        //reportGerenalDetail.ShowDialog();
        //    }
        //}

        private void bntViewDetail_Click(object sender, EventArgs e)
        {
            if (listConfigureDirection.SelectedItems.Count > 0)
            {

                var selectedItem = listConfigureDirection.SelectedItems[0];
                var Tag = (Tuple<string, string, string>)selectedItem.Tag;

                var MailNumber = (Tag.Item1 == null ? "" : Tag?.Item1.ToString());
                var BagNumber = Tag?.Item2.ToString();
                var ServiceCode = "";

                string[] rowData = new string[selectedItem.SubItems.Count];
                for (int i = 0; i < selectedItem.SubItems.Count; i++)
                {
                    if (i == 5)
                    {
                        ServiceCode = selectedItem.SubItems[i].Text;
                    }
                }
                Global.MailNumberReport = MailNumber;
                Global.BagNumberReport = BagNumber;
                Global.ServiceCodeReport = ServiceCode;
                ReportItem reportItem = new ReportItem();
                reportItem.ShowDialog();

                //ReportGerenalDetail reportGerenalDetail = new ReportGerenalDetail();
                //reportGerenalDetail.ShowDialog();
            }
        }
    }
}
