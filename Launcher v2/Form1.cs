using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;
using Ionic.Zip;

namespace Launcher_v2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            //Download progress
            backgroundWorker1.RunWorkerAsync();
            button5.Enabled = false;
            label3.Visible = false;
            timer1.Enabled = false;

            this.FormBorderStyle = FormBorderStyle.None;
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
        }

        //Makes the form dragable
        private void Form1_MouseDown(object sender,
        System.Windows.Forms.MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        //Close Button
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Minimize Button
        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Delete File
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


        //background Worker: Handles downloading the updates
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
                            //zip.ExtractAll(Root + "\\Mindustry.ru\\", true);
                            zipFiles.Extract(Root + "\\Mindustry.ru\\", true);
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

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            button8.Enabled = false;
            progressBar1.Value = e.ProgressPercentage;
            downloadLbl.ForeColor = System.Drawing.Color.Red;
            downloadLbl.Text = "Скачивается обновление, ожидайте пожалуйста......";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 5;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            button5.Enabled = true;
            this.downloadLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            downloadLbl.Text = "Используется актуальная версия игры!";
            label1.Text = System.IO.File.ReadAllText(Application.StartupPath + "/version");
            label5.Text = System.IO.File.ReadAllText(Application.StartupPath + "/updater");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Maximum == progressBar1.Value)
            {
                timer1.Enabled = false;
                progressBar1.Visible = false;
                downloadLbl.Visible = true;
                label3.Visible = false;
                button8.Enabled = true;


            }
            else
            {
                downloadLbl.Visible = false;
                label3.Visible = true;
                progressBar1.Value++;
                button8.Enabled = false;

            }
        }

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

        private void patchNotes_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button6, "Откроет папку с модификациями игры");
            t.SetToolTip(button7, "Откроет папку с картами игры");
            t.SetToolTip(button4, "Функция ремонта и восстановления клиента в исходное состояние");
            t.SetToolTip(button1, "Свернуть лаунчер");
            t.SetToolTip(button5, "Закрыть лаунчер");
            t.SetToolTip(label2, "Версия игры установленная у вас.");
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы хотите перейти в Discord Mindustry.ru?", "Подтвердите действие!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("https://ds.mindustry.ru/");
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы хотите перейти перейти на сайт?", "Подтвердите действие!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("https://mindustry.ru/");
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

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
                //do something else
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void progressBar1_Click(object sender, EventArgs e)
        {
           
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (IsProcessOpen("Mindustry"))
            {
                MessageBox.Show("Клиент уже запущен!");
            }
            else
            {
                string dirName1 = (Application.StartupPath + "/Mindustry.ru//Mindustry.exe");
                string dirName = (Application.StartupPath + "/Mindustry.ru//");
                if (Directory.Exists(dirName) && File.Exists(dirName1) == true)
                {
                    Process.Start(Application.StartupPath + "\\Mindustry.ru\\Mindustry.exe");
                    Application.Exit();
                }
                else
                {
                    File.Delete(Application.StartupPath + "/version");
                    MessageBox.Show("Лаунчер будет перезагружен!\n\nОбнаружена ошибка расположения клиента", "Ошибка!");
                    Process.Start(Application.StartupPath + "/Updater.exe");
                    Application.Exit();
                }
            }
        }
    }
}