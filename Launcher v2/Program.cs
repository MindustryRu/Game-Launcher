using System;
using System.Threading;
using System.Windows.Forms;

namespace TempWinFormProject
{
    internal sealed class StartPatams
    {
    }
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool oneonly;
            Mutex m = new Mutex(true, "Launcher.exe", out oneonly);
            if (oneonly)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Launcher_v2.Form1());
            }
            else
            {
                MessageBox.Show("Лаунчер уже запущен!", "Ошибка!");
            }
        }
    }
}