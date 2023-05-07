using DataStore.Provicers.SQLite;
using Model.Entities;
using System.Text;

namespace WebApi.Services
{
    public class TripFacade : ITripFacade
    {
        private int userId = -1;
        private TripContext _tripContext;

        public TripFacade(TripContext tripContext)
        {
            _tripContext = tripContext;
        }

        public void login(string email, string parola)
        {
            var user = _tripContext.Useri.Where(u => u.email == email && u.parola == parola).FirstOrDefault();
            if (user != null)
            {
                userId = user.id;
            }

        }

        public void logout()
        {
            userId = -1;
        }

        public void authentificare(string email, string parola)
        {
            _tripContext.Useri.Add(new User() { email = email, parola = parola });
        }

        public ObiectiveTuristice getObiectivByNume(string numeObiectiv)
        {
            return _tripContext.ObiectiveTuristice.Where(o => o.nume == numeObiectiv).FirstOrDefault();
        }

        public ObiectiveTuristice getObiectivById(int idObiectiv)
        {
            return _tripContext.ObiectiveTuristice.Where(o => o.id == idObiectiv).FirstOrDefault();
        }

        public FirmaTransport getFirmaTransportByNume(string numeFirma)
        {
            return _tripContext.FirmeTransport.Where(f => f.nume == numeFirma).FirstOrDefault();
        }

        public FirmaTransport getFirmaTransportById(int idFirma)
        {
            return _tripContext.FirmeTransport.Where(f => f.id == idFirma).FirstOrDefault();
        }

        public List<Excursie> getAllExcursii()
        {
            return _tripContext.Excursii.ToList();
        }

        public List<Excursie> getAllExcursiiByNumeAndInterval(string numeObiectiv, int oraMinim, int oraMaxim)
        {
            var obiectiv = getObiectivByNume(numeObiectiv);
            List<Excursie> lst = new List<Excursie>();
            var excursii = _tripContext.Excursii.ToList();
            foreach (var excursie in excursii)
            {
                if (excursie.id_obiectiv.Equals(obiectiv.id))
                {
                    var ora = int.Parse(excursie.ora);
                    if (ora >= oraMinim && ora <= oraMaxim)
                    {
                        lst.Add(excursie);
                    }
                }
            }
            return lst;
        }

        public int getNrLocuriDisponibile(int idExcursie)
        {
            var excursie = _tripContext.Excursii.Where(e => e.id == idExcursie).FirstOrDefault();
            var totalLocuri = excursie.nr_locuri_totale;
            var rezervari = _tripContext.Rezervari.ToList();
            foreach (var rezervare in rezervari)
            {
                if (rezervare.id_excursie == idExcursie)
                {
                    totalLocuri -= rezervare.nr_bilete;
                }
            }
            return totalLocuri;
        }

        public Persoana getPersoanaByNumeAndTelefon(string numeClient, string numarTelefon)
        {
            return _tripContext.Persoane.Where(p => p.nume == numeClient && p.numar_telefon == numarTelefon).FirstOrDefault();
        }

        public void rezervaLocuri(string numeClient, string numarTelefon, int numarBileteDorite, int idExcursie)
        {
            var persoana = getPersoanaByNumeAndTelefon(numeClient, numarTelefon);
            Rezervare rezervare = new Rezervare()
            {
                id_excursie = idExcursie,
                id_persoana = persoana.id,
                nr_bilete = numarBileteDorite
            };
            var locuriDisponibile = getNrLocuriDisponibile(idExcursie);
            if (locuriDisponibile >= numarBileteDorite)
            {
                _tripContext.Rezervari.Add(rezervare);
            }
        }
    }
}
