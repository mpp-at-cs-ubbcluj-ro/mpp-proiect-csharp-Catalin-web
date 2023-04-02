using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            Repository<int, Excursie> excursii = new ExcursieDBRepo();
            Repository<int, FirmaTransport> firmeTransport = new FirmaTransportDBRepo();
            Repository<int, ObiectiveTuristice> obiectiveTuristice = new ObiectiveTuristiceDBRepo();
            Repository<int, Persoana> persoane = new PersoanaDBRepo();
            Repository<int, Rezervare> rezervari = new RezervareDBRepo();
            Repository<int, User> useri = new UserDBRepo();
            Service srv = new Service(excursii, firmeTransport, obiectiveTuristice, persoane, rezervari, useri);

            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var Main = new Main(srv);
            Main.Show();
            Application.Run();

            Application.Exit();
        }
    }
}
