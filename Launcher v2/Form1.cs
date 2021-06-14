using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;
using Ionic.Zip;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;

namespace Launcher_v2
{
    public partial class Form1 : Form
    {
        #region -- Инициализировать компоненты --
        public Form1()
        {
            InitializeComponent();
            button5.Enabled = false;
            button11.Enabled = false;
            label3.Visible = false;
            timer1.Enabled = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            backgroundWorker1.RunWorkerAsync();
        }
        #endregion

        #region -- Делает форму перетаскиваемой --
        private void Form1_MouseDown(object sender,
        System.Windows.Forms.MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
        #endregion

        #region -- Загрузчик обновлений клиента --
        static bool deleteFile(string f)
        {
            try
            {
                File.Delete(f);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //Defines the server's update directory
            string Server = "http://update.mindustry.ru/client/";

            //Defines application root
            string Root = AppDomain.CurrentDomain.BaseDirectory;

            //Make sure version file exists
            FileStream fs = null;
            if (!File.Exists("version"))
            {
                using (fs = File.Create("version"))
                {

                }

                using (StreamWriter sw = new StreamWriter("version"))
                {
                    sw.Write("1");
                }
            }
            //checks client version
            string lclVersion;
            using (StreamReader reader = new StreamReader("version"))
            {
                lclVersion = reader.ReadLine();
            }
            decimal localVersion = decimal.Parse(lclVersion);


            //server's list of updates
            XDocument serverXml = XDocument.Load(@Server + "Updates.xml");

            //The Update Process
            foreach (XElement update in serverXml.Descendants("update"))
            {
                string version = update.Element("version").Value;
                string file = update.Element("file").Value;

                decimal serverVersion = decimal.Parse(version);


                string sUrlToReadFileFrom = Server + file;

                string sFilePathToWriteFileTo = Root + file;

                if (serverVersion > localVersion)
                {
                    Uri url = new Uri(sUrlToReadFileFrom);
                    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                    System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                    response.Close();

                    Int64 iSize = response.ContentLength;

                    Int64 iRunningByteTotal = 0;

                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        using (System.IO.Stream streamRemote = client.OpenRead(new Uri(sUrlToReadFileFrom)))
                        {
                            using (Stream streamLocal = new FileStream(sFilePathToWriteFileTo, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                int iByteSize = 0;
                                byte[] byteBuffer = new byte[iSize];
                                while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                                {
                                    streamLocal.Write(byteBuffer, 0, iByteSize);
                                    iRunningByteTotal += iByteSize;

                                    double dIndex = (double)(iRunningByteTotal);
                                    double dTotal = (double)byteBuffer.Length;
                                    double dProgressPercentage = (dIndex / dTotal);
                                    int iProgressPercentage = (int)(dProgressPercentage * 100);

                                    backgroundWorker1.ReportProgress(iProgressPercentage);
                                }

                                streamLocal.Close();
                            }

                            streamRemote.Close();
                        }
                    }

                    //unzip
                    using (ZipFile zip = ZipFile.Read(file))
                    {
                        foreach (ZipEntry zipFiles in zip)
                        {
                            zipFiles.Extract(Root + "\\Mindustry\\", true);
                        }
                    }

                    //download new version file
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(Server + "version.txt", @Root + "version");

                    //Delete Zip File
                    deleteFile(file);
                }

            }
        }
        #endregion

        #region -- Загрузка обновлений --
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //this.Size = new Size(175, 125);

            downloadLbl.Visible = true;
            button8.Enabled = false;
            button4.Enabled = false;
            button10.Enabled = false;
            progressBar1.Value = e.ProgressPercentage;
            label19.Text = (e.ProgressPercentage.ToString() + "%");
            downloadLbl.ForeColor = System.Drawing.Color.Red;
            downloadLbl.Text = "Скачивается обновление, ожидайте пожалуйста......";
        }
        #endregion

        #region -- Загрузка Обновлений не требуется --
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Invalidate();
            timer1.Enabled = true;
            timer1.Interval = 5;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            button5.Enabled = true;
            button11.Enabled = true;
            button10.Enabled = true;
            label19.Visible = false;
            this.downloadLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            downloadLbl.Text = "Обновлений для игры нет.";
            label1.Text = System.IO.File.ReadAllText(Application.StartupPath + "/version");
            FileStream fs = null;
            if (!File.Exists("updater"))
            {
                using (fs = File.Create("updater"))
                {

                }
                using (StreamWriter sw = new StreamWriter("updater"))
                {
                    sw.Write("0");
                }
            }
            label5.Text = System.IO.File.ReadAllText(Application.StartupPath + "/updater");
        }
        #endregion

        #region -- Таймер при запуске формы --
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Maximum == progressBar1.Value)
            {
                timer1.Enabled = false;
                //progressBar1.Visible = false; //Видимость progressBar После загрузки и проверки файлов
                downloadLbl.Visible = true;
                label3.Visible = false;
                button8.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                downloadLbl.Visible = false;
                label3.Visible = true;
                progressBar1.Value++;
                button8.Enabled = false;
                button4.Enabled = false;
            }
        }
        #endregion

        #region -- Провеьрить пинг и статус -- 
        private void OnlStatus_Tick(object sender, EventArgs e)
        {
            TimerOnlStatus.Interval = 15000;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Check Online
            IPStatus status = IPStatus.TimedOut;
            try
            {
                Ping ping1 = new Ping();
                PingReply reply = ping1.Send(@"update.mindustry.ru");
                status = reply.Status;
            }
            catch { }
            if (status != IPStatus.Success)
            {
                pictureBox6.BackgroundImage = global::Launcher_v2.Properties.Resources.server_offline;
                return;
            }
            else
            {
                pictureBox6.BackgroundImage = global::Launcher_v2.Properties.Resources.server_online;
                var content = new WebClient { Encoding = Encoding.UTF8 }.DownloadString("http://update.mindustry.ru/OnlStatus/status/Hub.json");
                var result = content;
                label11.Text = result;

                var content1 = new WebClient { Encoding = Encoding.UTF8 }.DownloadString("http://update.mindustry.ru/OnlStatus/status/Survival.json");
                var result1 = content1;
                label12.Text = result1;

                var content2 = new WebClient { Encoding = Encoding.UTF8 }.DownloadString("http://update.mindustry.ru/OnlStatus/status/PvP.json");
                var result2 = content2;
                label13.Text = result2;

                var content3 = new WebClient { Encoding = Encoding.UTF8 }.DownloadString("http://update.mindustry.ru/OnlStatus/status/Hex.json");
                var result3 = content3;
                label14.Text = result3;

                var content4 = new WebClient { Encoding = Encoding.UTF8 }.DownloadString("http://update.mindustry.ru/OnlStatus/status/TowerDefence.json");
                var result4 = content4;
                label15.Text = result4;

                var content5 = new WebClient { Encoding = Encoding.UTF8 }.DownloadString("http://update.mindustry.ru/OnlStatus/status/SandBox.json");
                var result5 = content5;
                label16.Text = result5;

                var content6 = new WebClient { Encoding = Encoding.UTF8 }.DownloadString("http://update.mindustry.ru/OnlStatus/status/BBM-Server.json");
                var result6 = content6;
                label17.Text = result6;

                var content7 = new WebClient { Encoding = Encoding.UTF8 }.DownloadString("http://update.mindustry.ru/OnlStatus/status/Attack.json");
                var result7 = content7;
                label21.Text = result7;
            }
            //End Check Online

            //Ping
            List<string> serversList = new List<string>();
            serversList.Add("EasyPlay.su"); //address
            Ping ping = new System.Net.NetworkInformation.Ping();
            PingReply pingReply = null;

            foreach (string server in serversList)
            {
                pingReply = ping.Send(server);

                if (pingReply.Status != IPStatus.TimedOut)
                {
                    label18.Text = pingReply.RoundtripTime.ToString();
                }

            }
            //End ping 

            //Check online Status Hub
            var client = new TcpClient();
            if (client.ConnectAsync("EasyPlay.su", 6567).Wait(70))
            {
                pictureBox1.Image = global::Launcher_v2.Properties.Resources.Sonline;
                label2.Text = "Сервер включен.";
            }
            else
            {
                pictureBox1.Image = global::Launcher_v2.Properties.Resources.Soffline;
                label2.Text = "Сервер выключен.";
            }
            //End Check online Status

            
            //Check online Status Survival
            var client1 = new TcpClient();
            if (client1.ConnectAsync("S.EasyPlay.su", 6567).Wait(75))
            {
                pictureBox2.Image = global::Launcher_v2.Properties.Resources.Sonline;
                label4.Text = "Сервер включен.";
            }
            else
            {
                pictureBox2.Image = global::Launcher_v2.Properties.Resources.Soffline;
                label4.Text = "Сервер выключен.";
            }
            //End Check online Status

            //Check online Status PvP
            var client2 = new TcpClient();
            if (client2.ConnectAsync("S.EasyPlay.su", 6577).Wait(80))
            {
                pictureBox3.Image = global::Launcher_v2.Properties.Resources.Sonline;
                label6.Text = "Сервер включен.";
            }
            else
            {
                pictureBox3.Image = global::Launcher_v2.Properties.Resources.Soffline;
                label6.Text = "Сервер выключен.";
            }
            //End Check online Status

            //Check online Status hex
            var client3 = new TcpClient();
            if (client3.ConnectAsync("S.EasyPlay.su", 6676).Wait(85))
            {
                pictureBox4.Image = global::Launcher_v2.Properties.Resources.Sonline;
                label7.Text = "Сервер включен.";
            }
            else
            {
                pictureBox4.Image = global::Launcher_v2.Properties.Resources.Soffline;
                label7.Text = "Сервер выключен.";
            }
            //End Check online Status

            //Check online Status Td
            var client4 = new TcpClient();
            if (client4.ConnectAsync("S.EasyPlay.su", 6597).Wait(90))
            {
                pictureBox5.Image = global::Launcher_v2.Properties.Resources.Sonline;
                label8.Text = "Сервер включен.";
            }
            else
            {
                pictureBox5.Image = global::Launcher_v2.Properties.Resources.Soffline;
                label8.Text = "Сервер выключен.";
            }
            //End Check online Status

            //Check online Status SandBox
            var client5 = new TcpClient();
            if (client5.ConnectAsync("S.EasyPlay.su", 6667).Wait(95))
            {
                pictureBox7.Image = global::Launcher_v2.Properties.Resources.Sonline;
                label9.Text = "Сервер включен.";
            }
            else
            {
                pictureBox7.Image = global::Launcher_v2.Properties.Resources.Soffline;
                label9.Text = "Сервер выключен.";
            }
            //End Check online Status

            //Check online Status BBM-Server
            var client6 = new TcpClient();
            if (client6.ConnectAsync("S.EasyPlay.su", 7777).Wait(100))
            {
                pictureBox8.Image = global::Launcher_v2.Properties.Resources.Sonline;
                label10.Text = "Сервер включен.";
            }
            else
            {
                pictureBox8.Image = global::Launcher_v2.Properties.Resources.Soffline;
                label10.Text = "Сервер выключен.";
            }
            //End Check online Status

            //Check online Status Attack
            var client7 = new TcpClient();
            if (client7.ConnectAsync("S.EasyPlay.su", 6587).Wait(105))
            {
                pictureBox9.Image = global::Launcher_v2.Properties.Resources.Sonline;
                label20.Text = "Сервер включен.";
            }
            else
            {
                pictureBox9.Image = global::Launcher_v2.Properties.Resources.Soffline;
                label20.Text = "Сервер выключен.";
            }
            //End Check online Status

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
        #endregion

        #region -- Проверить, запущен ли Mindustry.exe --
        public bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }

            }
            return false;
        }
        #endregion

        #region -- Загрузить форму --
        private void Form1_Load(object sender, EventArgs e)
        {
            downloadLbl.Visible = false;
            TimerOnlStatus.Enabled = true;
            SetRoundedShape(progressBar1, 7);
            ToolTip t = new ToolTip();
            IPStatus status = IPStatus.TimedOut;
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(@"update.mindustry.ru");
                status = reply.Status;
            }
            catch { }
            if (status != IPStatus.Success)
            {
                DialogResult dialogResult = MessageBox.Show("У лаунчера нет доступа к севреру обновлений.\nСервер обновлений на Тех.Работах и будет доступен в ближайшее время.\n\nПродолжить работу автономно?", "Внимание!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    pictureBox6.BackgroundImage = global::Launcher_v2.Properties.Resources.server_offline;
                    webBrowser1.Visible = false;   
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                pictureBox6.BackgroundImage = global::Launcher_v2.Properties.Resources.server_online;
            }            
            t.SetToolTip(button6, "Откроет папку с модификациями игры");
            t.SetToolTip(button7, "Откроет папку с картами игры");
            t.SetToolTip(button4, "Функция ремонта и восстановления клиента в исходное состояние");
            t.SetToolTip(button1, "Свернуть лаунчер");
            t.SetToolTip(button5, "Закрыть лаунчер");
            t.SetToolTip(pictureBox6, "Статус подключения к серверу обновлений.");
            t.SetToolTip(label18, "Ping до серверов EasyPlay.su");         
        }
        #endregion

        #region -- Кнопки --

        #region -- Кнопка Закрыть\Свернуть лаунчер --
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region -- Кнопка Discord --
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы хотите перейти в Discord EasyPlay?", "Подтвердите действие!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("https://ds.easyplay.su/");
            }
            else if (dialogResult == DialogResult.No)
            {
                //
            }
        }
        #endregion

        #region -- Кнопка Сайт --
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы хотите перейти перейти на сайт?", "Подтвердите действие!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("https://easyplay.su/");
            }
            else if (dialogResult == DialogResult.No)
            {
                //
            }
        }
        #endregion

        #region -- Кнопка карты --
        private void button7_Click(object sender, EventArgs e)
        {
            {
                string dirName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Mindustry/maps/";
                if (Directory.Exists(dirName))
                {
                    Process.Start(dirName);
                }
                else
                {
                    MessageBox.Show("У вас не создана папка с картами, повторите попытку позже.");
                }
            }
        }
        #endregion

        #region -- Кнопка моды --
        private void button6_Click(object sender, EventArgs e)
        {
            {
                string dirName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Mindustry/mods/";
                if (Directory.Exists(dirName))
                {
                    Process.Start(dirName);
                }
                else
                {
                    MessageBox.Show("У вас не создана папка с модификациями, повторите попытку позже.");
                }
            }
        }
        #endregion

        #region -- Кнопка ремонта --
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы запустили средство восстановления клиента\n\nВы уверены что хотите заново переустановить клиент?", "Внимание!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                File.Delete(Application.StartupPath + "/version");
                MessageBox.Show("Лаунчер будет перезагружен!\n\nПроизойдет переустановка клиента.", "Внимание!");
                Application.Restart();
            }
            else if (dialogResult == DialogResult.No)
            {
                //
            }
        }
        #endregion

        #region -- Кнопка настроек --
        private void button10_Click(object sender, EventArgs e)
        {
            /*       ------Функция временно отключена------
            {
                string dirName1 = (Application.StartupPath + "/Settings//Mindustry.ru.crt");
                string dirName = (Application.StartupPath + "/Settings//");
                if (Directory.Exists(dirName) && File.Exists(dirName1) == true)
                {
                    DialogResult dialogResult = MessageBox.Show("Сейчас вам будет предложено установить сертификат безопасности Mindustry.ru \nУстановить?", "Внимание!", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Process.Start(Application.StartupPath + "//Settings//Mindustry.ru.crt");
                    }
                    else
                    {
                        //
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Лаунчер не может найти Сертификат безопасности \nХотите ли вы запустить средство устранения ошибок?", "Внимание!", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                        if (!File.Exists("Updater.exe"))
                        {
                            MessageBox.Show("Лаунчер не может найти средство устранения ошибок\nПереустановите программу.", "Внимание!");
                            Application.Exit();
                        }
                        else
                        {
                            File.Delete(Application.StartupPath + "/updater");
                            Process.Start(Application.StartupPath + "/Updater.exe");
                            Application.Exit();
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //
                    }
                }
            }
            */
        }
        #endregion

        #region -- Кнопка запуска игры --
        private void button8_Click(object sender, EventArgs e)
        {
            if (IsProcessOpen("Mindustry"))
            {
                MessageBox.Show("Клиент уже запущен!");
            }
            else
            {
                string dirName1 = (Application.StartupPath + "/Mindustry//Mindustry.exe");
                string dirName = (Application.StartupPath + "/Mindustry//");
                if (Directory.Exists(dirName) && File.Exists(dirName1) == true)
                {
                    Process.Start(Application.StartupPath + "//Mindustry//Mindustry.exe");
                    Application.Exit();
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Лаунчер не может найти каталог игры \nХотите ли вы запустить средство устранения ошибок?", "Внимание!", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                        if (!File.Exists("Updater.exe"))
                        {
                            MessageBox.Show("Лаунчер не может найти средство устранения ошибок\nПереустановите программу.", "Внимание!");
                            Application.Exit();
                        }
                        else
                        {
                            File.Delete(Application.StartupPath + "/version");
                            Process.Start(Application.StartupPath + "/Updater.exe");
                            Application.Exit();
                        }    
                        

                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //
                    }
                }
            }
        }
        #endregion

        #region -- Кнопка GitHub --
        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы хотите перейти перейти в GitHub?", "Подтвердите действие!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("https://github.com/EasyPlaySu/Game-Launcher");
            }
            else if (dialogResult == DialogResult.No)
            {
                //
            }
        }
        #endregion

        #region -- Кнопка Anti-Grief --
        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы хотите запустить Mindustry Anti-Grief клиент?", "Подтвердите действие!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (IsProcessOpen("Mindustry"))
                {
                    MessageBox.Show("Клиент уже запущен!");
                }
                else
                {
                    string dirName1 = (Application.StartupPath + "/Mindustry//A-G//Mindustry.exe");
                    string dirName = (Application.StartupPath + "/Mindustry//A-G//");
                    if (Directory.Exists(dirName) && File.Exists(dirName1) == true)
                    {
                        Process.Start(Application.StartupPath + "//Mindustry//A-G//Mindustry.exe");
                        Application.Exit();
                    }
                    else
                    {
                        DialogResult dialogResult1 = MessageBox.Show("Лаунчер не может найти каталог игры \nХотите ли вы запустить средство устранения ошибок?", "Внимание!", MessageBoxButtons.YesNo);
                        if (dialogResult1 == DialogResult.Yes)
                        {

                            if (!File.Exists("Updater.exe"))
                            {
                                MessageBox.Show("Лаунчер не может найти средство устранения ошибок\nПереустановите программу.", "Внимание!");
                                Application.Exit();
                            }
                            else
                            {
                                File.Delete(Application.StartupPath + "/version");
                                Process.Start(Application.StartupPath + "/Updater.exe");
                                Application.Exit();
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            //
                        }
                    }
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //
            }
        }
        #endregion

        #endregion

        #region -- Закругление progressBar --
        public static void SetRoundedShape(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(radius, 0, control.Width - radius, 0);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddLine(control.Width, radius, control.Width, control.Height - radius);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddLine(control.Width - radius, control.Height, radius, control.Height);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.AddLine(0, control.Height - radius, 0, radius);
            path.AddArc(0, 0, radius, radius, 180, 90);
            control.Region = new Region(path);
        }
        #endregion

    }
}