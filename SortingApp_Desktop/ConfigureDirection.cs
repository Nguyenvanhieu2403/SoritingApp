using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SortingApp_Desktop.DataContext.models;
using SortingApp_Desktop.DataContext;
using SortingApp_Desktop.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using System.Text.RegularExpressions;

namespace SortingApp_Desktop
{
    public partial class ConfigureDirection : Form
    {
        public static long ConfigIdSelected = 0;
        public ConfigureDirection()
        {
            InitializeComponent();
            this.KeyPreview = true;
            string appPath = @"C:\SortingApp";
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.FromArgb(0, 122, 204); // Màu nền xanh dương đậm
                    btn.ForeColor = Color.White;                 // Màu chữ trắng
                    btn.FlatStyle = FlatStyle.Flat;              // Kiểu phẳng
                    btn.FlatAppearance.BorderSize = 0;           // Không viền
                }
            }

        }

        public async Task LoadDataToListViewAsync()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            ConnectToSql connectToSql = new ConnectToSql(configuration);
            PackagingDirectionRepos packagingDirectionRepos = new PackagingDirectionRepos(configuration);

            var result = await packagingDirectionRepos.SearchAsync(Global.ProcessId, null);

            listConfigureDirection.Items.Clear();
            listConfigureDirection.Columns.Clear();
            listConfigureDirection.View = View.Details;
            listConfigureDirection.Scrollable = true;

            listConfigureDirection.Columns.Add("STT", 100);
            listConfigureDirection.Columns.Add("Hướng đóng", 300);
            listConfigureDirection.Columns.Add("Tên hướng đóng", 400);
            listConfigureDirection.Columns.Add("Đơn vị", 300);
            listConfigureDirection.Columns.Add("Dịch vụ", 300);
            listConfigureDirection.Columns.Add("BC nhận", 200);
            listConfigureDirection.Columns.Add("PTVC", 200);
            int i = 1;
            foreach (var product in result.Data)
            {
                ListViewItem item = new ListViewItem(i.ToString());
                item.SubItems.Add(product.Name);
                item.SubItems.Add(product.DisplayName);
                item.SubItems.Add(product.UnitCodeList);
                item.SubItems.Add(product.ServiceCode);
                item.SubItems.Add(product.DestinationPosCode);
                item.SubItems.Add(product.PTVC);
                item.Tag = product.ConfigId;
                listConfigureDirection.Items.Add(item);
                i++;
            }
        }

        public async Task SaveConfigureDirectionAsync()
        {

            if (string.IsNullOrEmpty(txtServiceCode.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtDisplayName.Text)
                || string.IsNullOrEmpty(txtUnit.Text) || string.IsNullOrEmpty(txtDestinationPosCode.Text) || string.IsNullOrEmpty(txtPTVC.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(txtDestinationPosCode.Text.Length < 6)
            {
                MessageBox.Show("Bưu cục nhận phải là mã 6 số.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> unitCodeList = txtUnit.Text
            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToList();

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            ConnectToSql connectToSql = new ConnectToSql(configuration);
            PackagingDirectionRepos packagingDirectionRepos = new PackagingDirectionRepos(configuration);

            var ValidateDestinationPosCode = await packagingDirectionRepos.ValidateDestinationPosCode(txtDestinationPosCode.Text.Trim());

            if(ValidateDestinationPosCode.Data == null)
            {
                MessageBox.Show("Bưu cục nhận không tồn tại", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var ValidateUnitAndPos = await packagingDirectionRepos.ValidateUnitCode(unitCodeList);
            if (ValidateUnitAndPos.Status == 400)
            {
                MessageBox.Show("Đơn vị không tồn tại", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtName.Text, out int number))
            {
                MessageBox.Show("Vui lòng nhập hướng đóng là số", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            PackagingDirectionInput packagingDirectionInput = new PackagingDirectionInput();
            packagingDirectionInput.Name = txtName.Text;
            packagingDirectionInput.DisplayName = txtDisplayName.Text;
            packagingDirectionInput.Status = 1;
            packagingDirectionInput.UnitCodeList = unitCodeList;
            packagingDirectionInput.ProcessId = Global.ProcessId;
            packagingDirectionInput.ServiceCode = txtServiceCode.Text.ToUpper();
            packagingDirectionInput.DestinationPosCode = txtDestinationPosCode.Text;
            packagingDirectionInput.PTVC = txtPTVC.Text;

            var result = await packagingDirectionRepos.CreateAsync(packagingDirectionInput);

            if (result.Success)
            {
                MessageBox.Show("Thêm thành công hướng đóng mới!");
                txtServiceCode.Text = "";
                txtName.Text = "";
                txtDisplayName.Text = "";
                txtUnit.Text = "";
                txtDestinationPosCode.Text = "";
                await LoadDataToListViewAsync();
            }
            else
            {
                MessageBox.Show("Thêm mới thất bại: " + result.Message.ToString());
            }
        }

        private async void ConfigureDirection_Load(object sender, EventArgs e)
        {
            await LoadDataToListViewAsync();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            await SaveConfigureDirectionAsync();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();
            mainForm.Show();
            this.Hide();
        }

        private async void listConfigureDirection_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listConfigureDirection.SelectedItems.Count > 0)
            {
                var selectedItem = listConfigureDirection.SelectedItems[0];
                long id = (long)selectedItem.Tag;

                if (id > 0)
                {
                    ConfigIdSelected = id;
                    IConfiguration configuration = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();
                    ConnectToSql connectToSql = new ConnectToSql(configuration);
                    PackagingDirectionRepos packagingDirectionRepos = new PackagingDirectionRepos(configuration);

                    var result = await packagingDirectionRepos.FindByIdAsync(id, Global.ProcessId);
                    txtName.Text = result.Data.Name;
                    txtDisplayName.Text = result.Data.DisplayName;
                    txtUnit.Text = string.Join(", ", result.Data.UnitCodeList);
                    txtServiceCode.Text = (result.Data.ServiceCode ?? "");
                    txtDestinationPosCode.Text = (result.Data.DestinationPosCode ?? "");
                    txtPTVC.Text = (result.Data.PTVC ?? "");
                    //await ConfigureDirectionGetById(id);
                }
            }
        }

        public async Task UpdateConfigureDirectionAsync()
        {

            if (string.IsNullOrEmpty(txtServiceCode.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtDisplayName.Text)
                || string.IsNullOrEmpty(txtUnit.Text) || string.IsNullOrEmpty(txtDestinationPosCode.Text) || string.IsNullOrEmpty(txtPTVC.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (txtDestinationPosCode.Text.Length < 6 || !Regex.IsMatch(txtDestinationPosCode.Text.Trim(), @"^\d+$"))
            {
                MessageBox.Show("Bưu cục nhận phải là mã 6 số.");
                return;
            }

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            ConnectToSql connectToSql = new ConnectToSql(configuration);
            PackagingDirectionRepos packagingDirectionRepos = new PackagingDirectionRepos(configuration);

            var ValidateDestinationPosCode = await packagingDirectionRepos.ValidateDestinationPosCode(txtDestinationPosCode.Text.Trim());

            if (ValidateDestinationPosCode.Data == null)
            {
                MessageBox.Show("Bưu cục nhận không tồn tại", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> unitCodeList = txtUnit.Text
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToList();

            var ValidateUnitAndPos = await packagingDirectionRepos.ValidateUnitCode(unitCodeList);
            if (ValidateUnitAndPos.Status == 400)
            {
                MessageBox.Show("Đơn vị không tồn tại", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtName.Text, out int number))
            {
                MessageBox.Show("Vui lòng nhập hướng đóng là số", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            PackagingDirectionInput packagingDirectionInput = new PackagingDirectionInput();
            packagingDirectionInput.Name = txtName.Text;
            packagingDirectionInput.DisplayName = txtDisplayName.Text;
            packagingDirectionInput.Status = 1;
            packagingDirectionInput.UnitCodeList = unitCodeList;
            packagingDirectionInput.ProcessId = Global.ProcessId;
            packagingDirectionInput.ServiceCode = txtServiceCode.Text.ToUpper();
            packagingDirectionInput.DestinationPosCode = txtDestinationPosCode.Text;
            packagingDirectionInput.PTVC  = txtPTVC.Text;

            var result = await packagingDirectionRepos.UpdateAsync(packagingDirectionInput, ConfigIdSelected);

            if (result.Success)
            {
                MessageBox.Show("Sửa thành công hướng đóng!");
                txtServiceCode.Text = "";
                txtName.Text = "";
                txtDisplayName.Text = "";
                txtUnit.Text = "";
                txtDestinationPosCode.Text = "";
                await LoadDataToListViewAsync();
            }
            else
            {
                MessageBox.Show("Lỗi: " + result.Message);
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (ConfigIdSelected > 0)
            {
                await UpdateConfigureDirectionAsync();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hướng đóng để sửa!");
            }
        }

        public async Task DeleteConfigureDirectionAsync()
        {

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            ConnectToSql connectToSql = new ConnectToSql(configuration);
            PackagingDirectionRepos packagingDirectionRepos = new PackagingDirectionRepos(configuration);

            var result = await packagingDirectionRepos.DeleteAsync(ConfigIdSelected);

            if (result.Success)
            {
                MessageBox.Show("Xóa thành công hướng đóng!");
                txtServiceCode.Text = "";
                txtName.Text = "";
                txtDisplayName.Text = "";
                txtUnit.Text = "";
                txtDestinationPosCode.Text = "";
                await LoadDataToListViewAsync();
            }
            else
            {
                MessageBox.Show("Lỗi: " + result.Message);
            }
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            await DeleteConfigureDirectionAsync();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void ConfigureDirection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Form1 mainForm = new Form1();
                mainForm.Show();
                this.Close();
            }
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                await ImportFromExcelAsync(openFileDialog.FileName);
            }
        }

        public async Task ImportFromExcelAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("Vui lòng chọn file Excel hợp lệ.");
                return;
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    IConfiguration configuration = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();
                    PackagingDirectionRepos packagingDirectionRepos = new PackagingDirectionRepos(configuration);

                    int successCount = 0;
                    int failCount = 0;
                    StringBuilder errorMessages = new StringBuilder();

                    for (int row = 3; row <= rowCount; row++)
                    {
                        string name = worksheet.Cells[row, 1]?.Text?.Trim();
                        string displayName = worksheet.Cells[row, 2]?.Text?.Trim();
                        string unit = worksheet.Cells[row, 3]?.Text?.Trim();
                        string serviceCode = worksheet.Cells[row, 4]?.Text?.Trim();
                        string destinationPosCode = worksheet.Cells[row, 5]?.Text?.Trim();
                        string PTVC = worksheet.Cells[row, 6]?.Text?.Trim();

                        if (string.IsNullOrEmpty(serviceCode) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(unit) || string.IsNullOrEmpty(destinationPosCode) || string.IsNullOrEmpty(PTVC))
                        {
                            errorMessages.AppendLine($"Dòng {row}: Thiếu dữ liệu bắt buộc.");
                            failCount++;
                            continue;
                        }

                        if(destinationPosCode.Length < 6)
                        {
                            errorMessages.AppendLine($"Dòng {row}: Bưu cục nhận phải là mã 6 số.");
                            failCount++;
                            continue;
                        }

                        if (!int.TryParse(name, out int number))
                        {
                            errorMessages.AppendLine($"Dòng {row}: Vui lòng nhập hướng đóng là số.");
                            failCount++;
                            continue;
                        }

                        var ValidateDestinationPosCode = await packagingDirectionRepos.ValidateDestinationPosCode(destinationPosCode.Trim());

                        if (ValidateDestinationPosCode.Data == null)
                        {
                            errorMessages.AppendLine($"Dòng {row}: Bưu cục nhận không tồn tại.");
                            failCount++;
                            continue;
                        }

                        List<string> unitCodeList = unit
                           .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(x => x.Trim())
                           .ToList();

                        var ValidateUnitAndPos = await packagingDirectionRepos.ValidateUnitCode(unitCodeList);
                        if (ValidateUnitAndPos.Status == 400)
                        {
                            errorMessages.AppendLine($"Dòng {row}: Đơn vị không tồn tại.");
                            failCount++;
                            continue;
                        }

                        PackagingDirectionInput input = new PackagingDirectionInput
                        {
                            Name = name,
                            DisplayName = displayName,
                            UnitCodeList = unitCodeList,
                            Status = 1,
                            ProcessId = Global.ProcessId,
                            ServiceCode = serviceCode,
                            DestinationPosCode = destinationPosCode,
                            PTVC = PTVC,
                        };

                        var result = await packagingDirectionRepos.CreateAsync(input);
                        if (result.Success)
                            successCount++;
                        else
                        {
                            failCount++;
                            errorMessages.AppendLine($"Dòng {row}: {result.Message}");
                        }
                    }

                    MessageBox.Show($"Import hoàn tất. Thành công: {successCount}, Thất bại: {failCount}\n{errorMessages.ToString()}");

                    await LoadDataToListViewAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi import: " + ex.Message);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtServiceCode.Text = "";
            txtName.Text = "";
            txtDisplayName.Text = "";
            txtUnit.Text = "";
            txtDestinationPosCode.Text = "";
            await LoadDataToListViewAsync();
        }

        private void btnTemplateImport_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "template_import.xlsx");

            if (File.Exists(path))
            {
                System.Diagnostics.Process.Start("explorer.exe", path);
            }
            else
            {
                MessageBox.Show("Không tìm thấy file mẫu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
