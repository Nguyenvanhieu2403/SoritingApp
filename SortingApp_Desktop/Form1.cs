using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using NAudio.Wave;
using Newtonsoft.Json;
using SortingApp_Desktop.DataContext;
using SortingApp_Desktop.DataContext.models;
using SortingApp_Desktop.Repository;
using SortingApp_Net.DataContext;
using System.Configuration;
using System.Text;
using System.Windows.Forms;

namespace SortingApp_Desktop
{
    public partial class Form1 : Form
    {
        private Guid? idClient = null;
        private int ItemScanCumulative = 0;
        public string ConfigIdScan = "";
        public List<PackagingDirectionConfig> packagingDirectionConfigs = new List<PackagingDirectionConfig>();
        public string OldScan = "";
        public bool OldScanCheck;

        public Form1()
        {
            this.Controls.Clear();
            InitializeComponent();
            GetListConfigureDirection();
            if (Global.ProcessId == 1)
                Text = "Quét thông tin bưu gửi - chiều đến";
            else
                Text = "Quét thông tin bưu gửi - chiều đi";
            this.KeyPreview = true;
            string connectionString = Program.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            idClient = Guid.NewGuid();
        }

        private async Task GetListConfigureDirection()
        {
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.AutoScroll = true;


            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            ConnectToSql connectToSql = new ConnectToSql(configuration);
            PackagingDirectionRepos packagingDirectionRepos = new PackagingDirectionRepos(configuration);

            var result = await packagingDirectionRepos.SearchAsync(Global.ProcessId, null);

            if (result?.Data.Count > 0)
            {
                packagingDirectionConfigs = result?.Data;
                flowLayoutPanel1.Controls.Clear();
                foreach (var item in result.Data)
                {
                    Panel panel = new Panel();
                    panel.Width = 100;
                    panel.Height = 150;
                    panel.BorderStyle = BorderStyle.FixedSingle;

                    panel.Tag = item.ConfigId;

                    // Gán sự kiện Click
                    panel.Click += Panel_Click;
                    if (item.CountItem >= 10 || item.TotalItemWeight >= 50)
                    {
                        panel.BackColor = Color.Orange;
                    }

                    if (ConfigIdScan == item.ConfigId.ToString())
                    {
                        panel.BackColor = Color.DodgerBlue;
                    }


                    Label lblId = new Label();
                    lblId.Text = item.Name.ToString();
                    lblId.AutoSize = false;
                    lblId.TextAlign = ContentAlignment.MiddleCenter;
                    lblId.Dock = DockStyle.Top;
                    lblId.Height = 30;
                    lblId.Click += Panel_Click;

                    Label lblName = new Label();
                    lblName.Text = item.DisplayName;
                    lblName.AutoSize = false;
                    lblName.TextAlign = ContentAlignment.MiddleCenter;
                    lblName.Dock = DockStyle.Top;
                    lblName.Height = 40;
                    lblName.Click += Panel_Click;

                    Label lblValue = new Label();
                    lblValue.Text = item.CountItem.ToString();
                    lblValue.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                    lblValue.AutoSize = false;
                    lblValue.ForeColor = Color.Red;
                    lblValue.TextAlign = ContentAlignment.MiddleCenter;
                    lblValue.Dock = DockStyle.Fill;
                    lblValue.Click += Panel_Click;

                    Label lblWeight = new Label();
                    lblWeight.Text = item.TotalItemWeight.ToString() + "(kg)";
                    lblWeight.AutoSize = false;
                    lblWeight.TextAlign = ContentAlignment.MiddleCenter;
                    lblWeight.Dock = DockStyle.Top;
                    lblWeight.Height = 40;
                    lblWeight.Click += Panel_Click;

                    panel.Controls.Add(lblValue);
                    panel.Controls.Add(lblWeight);
                    panel.Controls.Add(lblName);
                    panel.Controls.Add(lblId);

                    flowLayoutPanel1.Controls.Add(panel);
                }
            }
        }

        private async void Panel_Click(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            Panel panel = ctrl as Panel ?? ctrl.Parent as Panel;

            if (panel != null && panel.Tag != null)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
                PackagingDirectionRepos packagingDirectionRepos = new PackagingDirectionRepos(configuration);
                Global.ConfigId = int.Parse(panel.Tag.ToString());
                var result = await packagingDirectionRepos.GetByIdCloseBag(int.Parse(panel.Tag.ToString()));
                if (result.Data != null)
                {
                    Global.TotalWeight = result.Data.TotalWeight ?? 0f;
                    Global.CountItem = result.Data.CountItem ?? 0;
                    Global.BagNumber = result.Data.BagNumber ?? "";
                    Global.MailNumber = result.Data.MailNumber ?? "";
                    Global.OriginalPost = result.Data.OriginalPost ?? "";
                    Global.DestinationPosCode = result.Data.DestinationPosCode ?? "";
                }
                else
                {
                    Global.TotalWeight = 0;
                    Global.CountItem = 0;
                    Global.BagNumber = "";
                    Global.MailNumber = "";
                    Global.OriginalPost = "";
                    Global.DestinationPosCode = "";
                }
                CloseBag closeBag = new CloseBag();
                closeBag.RefreshParent = RefreshForm;
                closeBag.ShowDialog();
            }
        }

        private async void RefreshForm()
        {
            // Load lại dữ liệu, gán lại label, list, v.v.
            await GetListConfigureDirection(); // hoặc bất kỳ hàm nào bạn dùng để refresh UI
        }


        private async Task CallApi(string ItemCode)
        {
            if (string.IsNullOrEmpty(ItemCode))
            {
                return;
            }
            
            
            //Console.WriteLine("Bắt đầu sử lý: ", timer.ToString());
            string appPath = @"C:\SortingApp";
            UserInput userInput = new UserInput();
            userInput.Uuid = idClient.ToString();
            userInput.Username = "root";
            userInput.ShiftConfigId = 13;
            userInput.LineId = 1;
            userInput.TransferMachine = Environment.MachineName;

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            ConnectToSql connectToSql = new ConnectToSql(configuration);
            EmployeeShiftRepos employeeShiftRepos = new EmployeeShiftRepos(configuration, connectToSql);

           var isInMail = await employeeShiftRepos.CheckInMail(ItemCode);

            if(isInMail)
            {
                MessageBox.Show("Bưu gửi đã được đóng trong chuyến thư khác", "Cảnh báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_ItemCode.Clear();
                return;
            }

            bool CheckScanIn = true;
            if (rbScanInBag.Checked)
            {
                CheckScanIn = true;
            }
            else
            {
                CheckScanIn = false;
            }
            var t1 = DateTime.Now;
            var t2 = t1;
            var result = await employeeShiftRepos.ScanItem(ItemCode, userInput, Global.ProcessId, CheckScanIn, Global.DividingStage);
            t2 = DateTime.Now;

            ItemScanCumulative++;
            lblNumberBagCumulative.Text = ItemScanCumulative.ToString();
            var soundFiles = new List<string>();
            if (result.Name == "-1")
            {
                LogScanErrorInput logScanErrorInput = new LogScanErrorInput();
                logScanErrorInput.ItemCode = ItemCode;
                logScanErrorInput.Direction = result.Message;
                logScanErrorInput.ProcessId = Global.ProcessId;

                await employeeShiftRepos.CreateLogScanError(logScanErrorInput);
                //await AddToResponseList("Không có hướng đóng");
                lbl_ItemCode.Text = "Không có hướng đóng";
                string mp3Path = Path.Combine(appPath, "MP3");
                string[] mp3Files = Directory.GetFiles(mp3Path, "*.mp3");
                string mp3FilePath = FindMatchingMp3File(mp3Files, result.Name);

                if (!string.IsNullOrEmpty(mp3FilePath))
                {
                    await PlayMp3Async(mp3FilePath);
                    //soundFiles.Add(mp3FilePath);
                }
                else
                {
                    Console.WriteLine("Không tìm thấy file MP3 phù hợp.");
                    WriteToFile("Không tìm thấy file MP3 phù hợp.");
                }
            }
            else
            {
                PackagingDirectionRepos packagingDirectionRepos = new PackagingDirectionRepos(configuration);
                var resultTotalItem = await packagingDirectionRepos.SearchAsync(Global.ProcessId, null);
                if (resultTotalItem.Data != null && resultTotalItem.Data.Count > 0)
                {
                    var TotalItem = resultTotalItem.Data.Find(x => x.ConfigId.ToString() == result?.config_id);
                    lblTotalItem.Text = TotalItem?.CountItem.ToString() ?? "";
                }
                ConfigIdScan = result?.config_id;
                var textRespons = result.Name + " - " + result.display_name;
                //await AddToResponseList(textRespons);
                lbl_ItemCode.Text = textRespons;
                lblNumberBagBD10.Text = result.TotalPostBag.ToString();
                lblNumberBagConfirm.Text = result.TotalScanItem.ToString();
                GetListConfigureDirection();
                string mp3Path = Path.Combine(appPath, "MP3");
                string[] mp3Files = Directory.GetFiles(mp3Path, "*.mp3");
                if (int.Parse(result.Name) != 0)
                {
                    string mp3FilePath = FindMatchingMp3File(mp3Files, result.Name);

                    if (!string.IsNullOrEmpty(mp3FilePath))
                    {
                        PlayMp3Async(mp3FilePath);
                        //soundFiles.Add(mp3FilePath);
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy file MP3 phù hợp.");
                        WriteToFile("Không tìm thấy file MP3 phù hợp.");
                    }
                }

                //if (result.ItemBatchIndex != null)
                //{
                //    // Đọc "Lô"
                //    if (true)
                //    {
                //        mp3Path = Path.Combine(appPath, "MP4");
                //        mp3Files = Directory.GetFiles(mp3Path, "*.wav");
                //        var namefile = result.ItemBatchIndex + "Tren" + result.TotalItemOfBatch;
                //        string mp3FilePath = FindMatchingMp3File(mp3Files, namefile);

                //        if (!string.IsNullOrEmpty(mp3FilePath))
                //        {
                //            //PlayMp3Async(mp3FilePath);
                //            soundFiles.Add(mp3FilePath);
                //        }
                //        else
                //        {
                //            Console.WriteLine("Không tìm thấy file MP3 phù hợp.");
                //            WriteToFile("Không tìm thấy file MP3 Lô phù hợp.");
                //        }
                //    }
                //}

                //if (result.BatchCode != null)
                //{
                //    // Đọc "Lô"
                //    if (true)
                //    {
                //        string mp3FilePath = FindMatchingMp3File(mp3Files, "Loso");

                //        if (!string.IsNullOrEmpty(mp3FilePath))
                //        {
                //            await PlayMp3Async(mp3FilePath);
                //        }
                //        else
                //        {
                //            Console.WriteLine("Không tìm thấy file MP3 phù hợp.");
                //            WriteToFile("Không tìm thấy file MP3 Lô phù hợp.");
                //        }
                //    }
                //    //Số lô
                //    if (result.BatchCode != null)
                //    {
                //        string mp3FilePath = FindMatchingMp3File(mp3Files, result.BatchCode);

                //        if (!string.IsNullOrEmpty(mp3FilePath))
                //        {
                //            await PlayMp3Async(mp3FilePath);
                //        }
                //        else
                //        {
                //            Console.WriteLine("Không tìm thấy file MP3 phù hợp.");
                //            WriteToFile("Không tìm thấy file MP3 Số lô phù hợp.");
                //        }
                //    }
                //    // STT kiện
                //    if (result.ItemBatchIndex != null && result.ItemBatchIndex != 0)
                //    {
                //        string mp3FilePath = FindMatchingMp3File(mp3Files, result.ItemBatchIndex.ToString());

                //        if (!string.IsNullOrEmpty(mp3FilePath))
                //        {
                //            await PlayMp3Async(mp3FilePath);
                //        }
                //        else
                //        {
                //            Console.WriteLine("Không tìm thấy file MP3 phù hợp.");
                //            WriteToFile("Không tìm thấy file MP3 STT kiện phù hợp.");
                //        }
                //    }
                //    // Đọc "Trên"
                //    if (true)
                //    {
                //        string mp3FilePath = FindMatchingMp3File(mp3Files, "tren");

                //        if (!string.IsNullOrEmpty(mp3FilePath))
                //        {
                //            await PlayMp3Async(mp3FilePath);
                //        }
                //        else
                //        {
                //            Console.WriteLine("Không tìm thấy file MP3 phù hợp.");
                //            WriteToFile("Không tìm thấy file MP3 Trên phù hợp.");
                //        }
                //    }
                //    // Tổng số kiện
                //    if (result.TotalItemOfBatch != null && result.TotalItemOfBatch != 0)
                //    {
                //        string mp3FilePath = FindMatchingMp3File(mp3Files, result.TotalItemOfBatch.ToString());

                //        if (!string.IsNullOrEmpty(mp3FilePath))
                //        {
                //            await PlayMp3Async(mp3FilePath);
                //        }
                //        else
                //        {
                //            Console.WriteLine("Không tìm thấy file MP3 phù hợp.");
                //            WriteToFile("Không tìm thấy file MP3 Tổng số kiện phù hợp.");
                //        }
                //    }

                //}
            }
            var t3 = DateTime.Now;

            //MessageBox.Show("Thời gian xử lý xong: " + (t2 -t1).TotalSeconds.ToString()+ " "  + (t3 - t2).TotalSeconds.ToString());
            //PlaySoundSequence(soundFiles);
        }

        private void PlaySoundSequence(List<string> soundFilePaths)
        {
            Task.Run(async () =>
            {
                foreach (var path in soundFilePaths)
                {
                    if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    {
                        await PlayMp3Async(path); // hàm phát vẫn await để đảm bảo thứ tự
                    }
                }
            });
        }


        private async Task CloseForm()
        {

            //Application.Exit();


            //using (HttpClient client = new HttpClient())
            //{
            //    try
            //    {
            //        string appPath = @"C:\SortingApp"; //Path.GetDirectoryName(Application.ExecutablePath);
            //        string configFilePath = Path.Combine(appPath, "config.txt");

            //        if (!File.Exists(configFilePath))
            //        {
            //            MessageBox.Show("Không tìm thấy file config.txt");
            //            return;
            //        }

            //        string[] configLines = File.ReadAllLines(configFilePath);
            //        string ipServer = configLines.Length > 0 ? configLines[0].Trim() : "";
            //        string apiUrl = "http://" + ipServer + "/v1/line/cleanLineActive?uuid=" + (idClient ?? null);

            //        HttpResponseMessage response = await client.PutAsync(apiUrl, null);

            //        if (response.IsSuccessStatusCode)
            //        {
            //            string respon = await response.Content.ReadAsStringAsync();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Không thể cập nhật dây chuyền khi đóng .");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Lỗi " + ex.Message);
            //        WriteToFile("Lỗi " + ex.Message);
            //    }
            //}
        }

        static string FindMatchingMp3File(string[] mp3Files, string inputValue)
        {
            var t = DateTime.Now;
            var t1 = t;
            string targetFileName = $"{inputValue}.mp3";

            // Tạo dictionary để tra cứu nhanh
            var fileMap = mp3Files.ToDictionary(f => Path.GetFileName(f), f => f);

            if (fileMap.TryGetValue(targetFileName, out string fullPath) && File.Exists(fullPath))
            {
                t1 = DateTime.Now;
                //MessageBox.Show((t1 - t).TotalSeconds.ToString());
                return fullPath;
            }

            //MessageBox.Show((t1 - t).TotalSeconds.ToString());
            return null;

        }


        //static async Task PlayMp3Async(string mp3FilePath)
        //{
        //    try
        //    {
        //        var t = DateTime.Now;
        //        var t1 = t;
        //        using (var reader = new MediaFoundationReader(mp3FilePath))
        //        {

        //            var newFormat = new WaveFormat(reader.WaveFormat.SampleRate * 2, reader.WaveFormat.Channels);

        //            using (var resampler = new MediaFoundationResampler(reader, newFormat))
        //            using (var outputDevice = new WaveOutEvent())
        //            {
        //                outputDevice.Init(resampler);
        //                outputDevice.Play();

        //                Console.WriteLine($"Đang phát file: {Path.GetFileName(mp3FilePath)} với tốc độ 2x");

        //                while (outputDevice.PlaybackState == PlaybackState.Playing)
        //                {
        //                    await Task.Delay(100);
        //                }
        //                t1 = DateTime.Now;
        //            }
        //        }
        //        MessageBox.Show((t1 - t).TotalSeconds.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Lỗi: {ex.Message}");
        //    }

        //    Console.WriteLine("Phát âm thanh hoàn tất.");
        //}

        static async Task PlayMp3Async(string wavFilePath)
        {
            try
            {
                var t = DateTime.Now;
                var t1 = t;

                using (var reader = new MediaFoundationReader(wavFilePath))
                {
                    // Tăng tốc độ phát lên gấp 2 lần bằng cách tăng sample rate
                    var newFormat = new WaveFormat(reader.WaveFormat.SampleRate * 2, reader.WaveFormat.Channels);

                    using (var resampler = new MediaFoundationResampler(reader, newFormat))
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(resampler);
                        outputDevice.Play();

                        Console.WriteLine($"Đang phát file: {Path.GetFileName(wavFilePath)} với tốc độ 2x");

                        while (outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            await Task.Delay(100);
                        }

                        t1 = DateTime.Now;
                    }
                }

                //MessageBox.Show($"Phát xong sau {(t1 - t).TotalSeconds:F2} giây");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }

            Console.WriteLine("Phát âm thanh hoàn tất.");
        }

        //static async Task PlayMp3Async(string mp3FilePath)
        //{
        //    try
        //    {
        //        var start = DateTime.Now;

        //        // Đọc toàn bộ file vào RAM
        //        byte[] mp3Bytes = await File.ReadAllBytesAsync(mp3FilePath);
        //        using (var ms = new MemoryStream(mp3Bytes))
        //        using (var reader = new Mp3FileReader(ms)) // Giải mã từ RAM
        //        {
        //            // Tăng tốc phát bằng cách gấp đôi sample rate
        //            var newFormat = new WaveFormat(reader.Mp3WaveFormat.SampleRate * 2, reader.Mp3WaveFormat.Channels);
        //            using (var resampler = new MediaFoundationResampler(reader, newFormat)
        //            {
        //                ResamplerQuality = 1 // thấp để xử lý nhanh hơn
        //            })
        //            using (var outputDevice = new WaveOutEvent())
        //            {
        //                outputDevice.Init(resampler);
        //                outputDevice.Play();

        //                Console.WriteLine($"Đang phát file: {Path.GetFileName(mp3FilePath)} từ RAM với tốc độ 2x");

        //                while (outputDevice.PlaybackState == PlaybackState.Playing)
        //                {
        //                    await Task.Delay(100);
        //                }
        //            }
        //        }

        //        var end = DateTime.Now;
        //        //MessageBox.Show($"Phát xong sau {(end - start).TotalSeconds:0.00} giây");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Lỗi khi phát file: {ex.Message}");
        //    }
        //}


        //private async Task AddToResponseList(string text)
        //{
        //    if (responseList.Count > 7)
        //    {
        //        responseList.RemoveAt(responseList.Count - 1);
        //    }
        //    responseList.Insert(0, text);
        //    CreateNewListBox(text);
        //    responseHistoryShowList.Insert(0, text);


        //}

        //private void CreateNewListBox(string data)
        //{
        //    int padding = 10;
        //    int spacingX = 10;
        //    int spacingY = 5;

        //    int normalBoxWidth = 300, normalBoxHeight = 50;

        //    foreach (var listBox in listBoxes)
        //    {
        //        if (listBox.Parent != null)
        //        {
        //            listBox.Parent.Controls.Remove(listBox);
        //        }
        //    }
        //    listBoxes.Clear();

        //    if (responseHistoryShowList.Count > 8)
        //    {
        //        responseHistoryShowList.RemoveAt(responseHistoryShowList.Count - 1);
        //    }

        //    int startX2 = padding;
        //    int startY2 = padding + 80;

        //    int formWidth = groupBox2.ClientSize.Width;

        //    int maxColumns = Math.Max(1, (formWidth - 2 * padding) / (normalBoxWidth + spacingX));

        //    for (int i = 0; i < responseHistoryShowList.Count; i++)
        //    {
        //        int width = normalBoxWidth;
        //        int height = normalBoxHeight;

        //        int posX, posY;
        //        Control parent;
        //        int row = (i - 1) / maxColumns;
        //        int col = (i - 1) % maxColumns;

        //        posX = startX2 + col * (normalBoxWidth + spacingX);
        //        posY = startY2 + row * (normalBoxHeight + spacingY);
        //        parent = groupBox2;

        //        ListBox newListBox = new ListBox();
        //        newListBox.Items.Add(responseHistoryShowList[i]);
        //        newListBox.Width = width;
        //        newListBox.Height = height;
        //        newListBox.Location = new Point(posX, posY);
        //        newListBox.BackColor = Color.WhiteSmoke;
        //        newListBox.Font = new Font("Times New Roman", 20, FontStyle.Bold);

        //        parent.Controls.Add(newListBox);
        //        listBoxes.Add(newListBox);
        //    }
        //}

        private async void txt_ItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string ItemCode = txt_ItemCode.Text.Trim();

                    //if (OldScan == ItemCode)
                    //{
                    //    OldScan = "";
                    //    txt_ItemCode.Clear();
                    //    return;
                    //}
                    //else
                    //{
                    //    OldScan = ItemCode;
                    //}

                    if (!string.IsNullOrEmpty(ItemCode))
                    {
                        await CallApi(ItemCode);
                        txt_ItemCode.Clear();
                    }

                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message);
                WriteToFile("Lỗi " + ex.Message);
            }
        }

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Ứng dụng sẽ thoát. Bạn có chắc chắn muốn thoát không?",
                                          "Xác nhận thoát",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Error);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }

            await CloseForm();
        }

        private async void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            await CloseForm();
        }
        public void WriteToFile(string Message)
        {
            string path = @"C:\SortingApp\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = "C:\\SortingApp\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                ConfigureDirection configureDirection = new ConfigureDirection();
                configureDirection.Show();
                this.Hide();
            }

            if (e.KeyCode == Keys.F3)
            {
                BagsManager bagsManager = new BagsManager();
                bagsManager.Show();
                this.Hide();
            }

            if (e.KeyCode == Keys.F1)
            {
                ConfigProcessId configProcessId = new ConfigProcessId();
                configProcessId.ShowDialog();
                this.Hide();
            }

            if (e.KeyCode == Keys.F4)
            {
                ReportGeneral reportGeneral = new ReportGeneral();
                reportGeneral.Show();
                this.Hide();
            }
        }

        private async void txt_ItemCode_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    string ItemCode = txt_ItemCode.Text.Trim();

            //    if (OldScan == ItemCode && !string.IsNullOrEmpty(ItemCode) && ItemCode.Length >= 13)
            //    {
            //        OldScan = "";
            //        txt_ItemCode.Clear();
            //        return;
            //    }
            //    else if (!string.IsNullOrEmpty(ItemCode) && ItemCode.Length >= 13)
            //    {
            //        OldScan = ItemCode;
            //    }

            //    if (!string.IsNullOrEmpty(ItemCode) && ItemCode.Length >= 13)
            //    {
            //        await CallApi(ItemCode);
            //        txt_ItemCode.Clear();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi " + ex.Message);
            //    WriteToFile("Lỗi " + ex.Message);
            //}
        }
    }
}
