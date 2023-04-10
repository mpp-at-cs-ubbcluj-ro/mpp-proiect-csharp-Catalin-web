using Proiect.Client;
using System;
using System.Windows.Forms;

namespace Proiect
{
    internal static class Program
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "App.config"));

            ITripClient client = new TripClient("http://localhost:12500");
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var Login = new Login(client);
            Login.Show();
            Application.Run();

            Application.Exit();
        }
    }
}
