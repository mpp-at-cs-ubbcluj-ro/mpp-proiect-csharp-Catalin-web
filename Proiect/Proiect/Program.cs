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
                idObiectiv = 1,
                nrLocuriTotale = 5,
                ora = "newOra",
                pret = 5.5f
            };
            repo.adauga(excursie);
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
