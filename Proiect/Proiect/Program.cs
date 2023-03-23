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

            _logger.Info("Hello logging world!");
            Console.WriteLine("Hit enter");

            ExcursieDBRepo repo = new ExcursieDBRepo();

            Excursie excursie = new Excursie()
            {
                idFirmaTransport = 4,
                idObiectiv = 2,
                nrLocuriTotale = 1314,
                ora = "dasdsa",
                pret = 5.9f
            };
            repo.adauga(excursie);
            var all = repo.getAll();
            excursie = repo.getAll().ElementAt(0);
            Excursie newExcursie = new Excursie()
            {
                idFirmaTransport = 2,
                idObiectiv = 2,
                nrLocuriTotale = 3,
                ora = "newOra",
                pret = 5.6f
            };
            repo.update(excursie, newExcursie);
            newExcursie = repo.getAll().ElementAt(0);
            newExcursie = repo.cautaId(newExcursie.id);
            repo.sterge(newExcursie);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
