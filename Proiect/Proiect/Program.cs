using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ExcursieDBRepo repo = new ExcursieDBRepo();
            Excursie excursie = new Excursie()
            {
                idFirmaTransport = 1,
                idObiectiv = 2,
                ora = new TimeSpan(1234),
                pret = 5,
                nrLocuriTotale = 6,
            };
            repo.adauga(excursie);
            var lst = repo.getAll();
            Console.WriteLine(repo.getAll().FirstOrDefault().idFirmaTransport);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
