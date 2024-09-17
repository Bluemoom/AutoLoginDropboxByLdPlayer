using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoLoginDropboxByLdPlayer 
{
    public partial class Form1 : Form
    {
        #region data

        private Bitmap LOGIN_CENTER_BMP;
        private Bitmap LOGIN_BOTTON_BMP;
        private Bitmap NEXT_BOTTON_BMP;
        private Bitmap INCREASE_SUCCESS_BMP;
        private Bitmap GOBACK_BUTTON_BMP;

        #endregion data

        // string BASE_URL = "http://localhost:8000/api/";
        private string BASE_URL = "http://118.70.131.239:20903/api/";

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            LOGIN_CENTER_BMP = (Bitmap)Bitmap.FromFile("Data//LoginCenter.png");
            LOGIN_BOTTON_BMP = (Bitmap)Bitmap.FromFile("Data//LoginBotton.png");
            NEXT_BOTTON_BMP = (Bitmap)Bitmap.FromFile("Data//NextButton.png");
            INCREASE_SUCCESS_BMP = (Bitmap)Bitmap.FromFile("Data//25GB.png");
            GOBACK_BUTTON_BMP = (Bitmap)Bitmap.FromFile("Data//GoBackButton.png");
        }

        private bool isStop = false;

        private void btnStart_Click(object sender, EventArgs e)
        {
            Task runAutomationTask = new Task(() =>
            {
                isStop = false;
                RunAutomation();
            });
            runAutomationTask.Start();
        }

        private void RunAutomation()
        {
            // lấy ra danh sách ldplayer đang bật
            List<string> devices = new List<string>();
            devices = KAutoHelper.ADBHelper.GetDevices();

            // chạy từng device để điều khiển theo kịch bản
            foreach (var deviceID in devices)
            {
                Delay(2);
                // tạo ra một luồng xử lý riêng biệt để xử lý cho device này
                Task t = new Task(async () =>
                {
                    try
                    {
                        // Gọi API lấy tài khoản email đã đăng ký cũ nhất
                        EmailVm email = await GetOlderRegisterredEmail();
                        if (email == null)
                        {
                            lblNotification.Text = "Thông báo: Không có email khả dụng";
                            isStop = true;
                            return;
                        }

                        // Nhan nut ESC de tat quang cao

                        // click vào icon Dropbox
                        Delay(1);
                        if (isStop) return;
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 60.8, 16.3);
                        Delay(6);

                        // click vào nut login center nếu có
                        var loginScreen = KAutoHelper.ADBHelper.ScreenShoot(deviceID, false);
                        var loginCenterButton = KAutoHelper.ImageScanOpenCV.FindOutPoint(loginScreen, LOGIN_CENTER_BMP);
                        if (loginCenterButton != null)
                        {
                            if (isStop)
                                return;
                            KAutoHelper.ADBHelper.Tap(deviceID, loginCenterButton.Value.X, loginCenterButton.Value.Y);
                            Delay(30);
                        }
                        else
                        {
                            // nếu không có nút login phía trên giữa = > click vào nút login in phía dưới nếu có
                            var loginBottonButton = KAutoHelper.ImageScanOpenCV.FindOutPoint(loginScreen, LOGIN_BOTTON_BMP);
                            if (loginBottonButton != null)
                            {
                                if (isStop)
                                    return;
                                KAutoHelper.ADBHelper.Tap(deviceID, loginBottonButton.Value.X, loginBottonButton.Value.Y);
                                Delay(6);

                                // click vào nut dang nhap bang google
                                if (isStop)
                                    return;
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.5, 65.5);
                                Delay(30);
                            }
                        }

                        // click vào ô nhập email
                        if (isStop)
                            return;

                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 23.1, 30.2);

                        // nhập email tài khoản
                        if (isStop)
                            return;
                        KAutoHelper.ADBHelper.InputText(deviceID, email.Username);
                        Delay(3);

                        // nhấn Enter
                        if (isStop)
                            return;
                        KAutoHelper.ADBHelper.Key(deviceID, KAutoHelper.ADBKeyEvent.KEYCODE_ENTER);
                        Delay(7);

                        // click vào ô nhập password
                        if (isStop)
                            return;
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 20.0, 32.7);

                        // nhập password từ bàn phím
                        if (isStop)
                            return;
                        KAutoHelper.ADBHelper.InputText(deviceID, email.Password);
                        Delay(5);

                        // nhấn Enter
                        if (isStop)
                            return;
                        KAutoHelper.ADBHelper.Key(deviceID, KAutoHelper.ADBKeyEvent.KEYCODE_ENTER);
                        Delay(9);

                        // CLICK VÀO NÚT TÔI ĐỒNG Ý
                        if (isStop)
                            return;
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 79.8, 95.1);
                        Delay(10);

                        // CLICK VÀO NÚT CHẤP NHẬN
                        if (isStop)
                            return;
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.4, 95.4);
                        Delay(20);

                        // CLICK NÚT CẤP QUYỀN ỨNG DỤNG
                        if (isStop)
                            return;
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 68.7, 68.6);
                        Delay(1);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 73.3, 55.4);
                        Delay(38);

                        // CLICK NÚT QUAY LẠI Ở MÀN HÌNH TRY FREE
                        var TryFreeScreen = KAutoHelper.ADBHelper.ScreenShoot(deviceID, false);
                        var goBackButton = KAutoHelper.ImageScanOpenCV.FindOutPoint(TryFreeScreen, GOBACK_BUTTON_BMP);
                        if (goBackButton != null)
                        {
                            if (isStop)
                                return;
                            KAutoHelper.ADBHelper.Tap(deviceID, goBackButton.Value.X, goBackButton.Value.Y);
                            Delay(4);
                        }

                        // CLICK NÚT "CONTINUNE"
                        if (isStop)
                            return;
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.8, 94.3);
                        Delay(4);

                        // CLICK NÚT "ADD CONTENT LATER"
                        if (isStop)
                            return;
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.3, 94.2);
                        Delay(4);

                        // CLICK ACCOUNT
                        if (isStop)
                            return;
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 86.8, 96.2);
                        Delay(8);

                        // KIỂM TRA XEM ĐC 2,5GB KHÔNG?
                        var accountScreen = KAutoHelper.ADBHelper.ScreenShoot(deviceID, false);
                        var increaseSuccess = KAutoHelper.ImageScanOpenCV.FindOutPoint(accountScreen, INCREASE_SUCCESS_BMP);
                        if (increaseSuccess != null)
                        {
                            byte EMAIL_LOGINED_STATE = 4;
                            // TODO: CALL API UPDATE TRẠNG THÁI TK SANG ĐÃ LOGIN
                            bool resutl = await UpdateEmailState(email.EmailId, EMAIL_LOGINED_STATE);
                            Console.WriteLine("Thanh cong");
                            Delay(4);
                        }
                        else
                        {
                            byte NOT_SUCCESS_STATE = 7;
                            // TODO : KIỂM TRA XEM ĐC 2GB KHÔNG NẾU CÓ UPDATE TRẠNG THÁI TK SANG KHÔNG THÀNH CÔNG
                            bool resutl = await UpdateEmailState(email.EmailId, NOT_SUCCESS_STATE);
                            Console.WriteLine("That bai");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Co loi xay ra trong qua trinh chay: {ex}");
                    }
                });
                t.Start();
            }
        }

        private async Task<EmailVm> GetOlderRegisterredEmail()
        {
            var email = new EmailVm();
            HttpClientHelper http = new HttpClientHelper();
            try
            {
                string relativeUrl = "Emails/get-registered-email";

                HttpResponseMessage result = await http.GetAsync(BASE_URL + relativeUrl);
                var responseContent = await result.Content.ReadAsStringAsync();

                // Nhận dữ liệu trả về từ API và xử lý
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    email = JsonConvert.DeserializeObject<EmailVm>(responseContent);
                }
                else
                {
                    Console.WriteLine($"Error: {responseContent}");
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Xử lý các lỗi liên quan đến HTTP request và ghi log
                Console.WriteLine($"HTTP request error: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác và ghi log
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            return email;
        }

        private async Task<bool> UpdateEmailState(long emailId, byte state)
        {
            HttpClientHelper http = new HttpClientHelper();
            try
            {
                string relativeUrl = "Emails/" + emailId.ToString() + "/change-state?newState=" + state.ToString();

                HttpResponseMessage result = await http.PutAsync(BASE_URL + relativeUrl, null);
                var responseContent = await result.Content.ReadAsStringAsync();

                // Nhận dữ liệu trả về từ API và xử lý
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Xử lý các lỗi liên quan đến HTTP request và ghi log
                Console.WriteLine($"HTTP request error: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác và ghi log
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            return false;
        }

        private void Delay(int delay)
        {
            while (delay > 0)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                delay--;
                if (isStop)
                    break;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            isStop = true;
        }

        private void btnBatMayAo_Click(object sender, EventArgs e)
        {
            // Khoi dong LDPlayer dau tien
            //try
            //{
            //    string ldPlayerPath = @"D:\LDPlayer\LDPlayer9\dnplayer.exe";

            //    if(File.Exists(ldPlayerPath))
            //    {
            //        Process ldPlayerProcess = new Process();
            //        ldPlayerProcess.StartInfo.FileName = ldPlayerPath;

            //        ldPlayerProcess.Start();
            //    } else
            //    {
            //        MessageBox.Show("Khong tim thay ldplayer. Vui long kiem tra duong dan");
            //    }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show("Co loi xay ra: "+ex.ToString());
            //}

            // Khoi dong LDPlayer dau tien
            try
            {
                string ldPlayerPath = @"D:\ld9.lnk";

                if (File.Exists(ldPlayerPath))
                {
                    Process ldPlayerProcess = new Process();
                    ldPlayerProcess.StartInfo.FileName = ldPlayerPath;

                    ldPlayerProcess.Start();
                }
                else
                {
                    MessageBox.Show("Khong tim thay ldplayer. Vui long kiem tra duong dan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Co loi xay ra: " + ex.ToString());
            }
        }
    }
}