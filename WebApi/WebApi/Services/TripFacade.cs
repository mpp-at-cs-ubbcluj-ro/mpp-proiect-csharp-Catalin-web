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
                    var ora = excursie.ora;
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
                _tripContext.SaveChanges();
            }
        }

        public Excursie addExcursie(string numeObiectiv, string numeFirma, int ora, float pret, int nrLocuriTotale)
        {
            int id_obiectiv = getObiectivByNume(numeObiectiv)?.id ?? 1;
            int id_firma_transport = getFirmaTransportByNume(numeFirma)?.id ?? 1;
            Excursie excurise= new Excursie()
            {
                id_obiectiv = id_obiectiv,
                id_firma_transport = id_firma_transport,
                ora = ora,
                pret = pret,
                nr_locuri_totale = nrLocuriTotale
            };
            _tripContext.Excursii.Add(excurise);
            _tripContext.SaveChanges();
            return _tripContext.Excursii.Where(e => e.id_obiectiv == id_obiectiv).FirstOrDefault();
        }

        public void deleteExcursie(int idExcursie)
        {
            var excursie = _tripContext.Excursii.Where(e => e.id == idExcursie).FirstOrDefault();
            _tripContext.Excursii.Remove(excursie);
            _tripContext.SaveChanges();
        }

        public void updateExcursie(int idExcursie, string numeObiectiv, string numeFirma, int ora, float pret, int nrLocuriTotale)
        {
            int id_obiectiv = getObiectivByNume(numeObiectiv)?.id ?? 1;
            int id_firma_transport = getFirmaTransportByNume(numeFirma)?.id ?? 1;
            Excursie newExcursie = new Excursie()
            {
                id_obiectiv = id_obiectiv,
                id_firma_transport = id_firma_transport,
                ora = ora,
                pret = pret,
                nr_locuri_totale = nrLocuriTotale
            };

            var excursie = _tripContext.Excursii.SingleOrDefault(e => e.id == idExcursie);
            if (excursie == null)
            {
                return;
            }
            excursie.id_obiectiv = newExcursie.id_obiectiv;
            excursie.id_firma_transport = newExcursie.id_firma_transport;
            excursie.ora = newExcursie.ora;
            excursie.pret = newExcursie.pret;
            excursie.nr_locuri_totale = newExcursie.nr_locuri_totale;
            _tripContext.SaveChanges();
        }

        public Excursie getExcursie(int idExcursie)
        {
            return _tripContext.Excursii.Single(e => e.id == idExcursie);
        }
    }
}
