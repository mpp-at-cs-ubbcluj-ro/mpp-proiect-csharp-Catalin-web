using DataStore.Core;
using Model.Entities;

namespace WebApi.Services
{
    public class TripFacade : ITripFacade
    {
        private readonly IExcursieRepository _excursii;
        private readonly IFirmaTransportRepository _firmeTransport;
        private readonly IObiectiveTuristiceRepository _obiectiveTuristice;
        private readonly IPersoanaRepository _persoane;
        private readonly IRezervareRepository _rezervari;
        private readonly IUserRepository _useri;
        private int userId = -1;

        public TripFacade(IExcursieRepository excursii, IFirmaTransportRepository firmeTransport, IObiectiveTuristiceRepository obiectiveTuristice, IPersoanaRepository persoane, IRezervareRepository rezervari, IUserRepository useri)
        {
            _excursii = excursii;
            _firmeTransport = firmeTransport;
            _obiectiveTuristice = obiectiveTuristice;
            _persoane = persoane;
            _rezervari = rezervari;
            _useri = useri;
        }

        public void login(string email, string parola)
        {
            var useri = _useri.getAll();
            foreach (var user in useri)
            {
                if (user.email == email && user.parola == parola)
                {
                    userId = user.id;
                    return;
                }
            }
        }

        public void logout()
        {
            userId = -1;
        }

        public void authentificare(string email, string parola)
        {
            User user = new User()
            {
                email = email,
                parola = parola
            };
            _useri.adauga(user);
        }

        public ObiectiveTuristice getObiectivByNume(string numeObiectiv)
        {
            var obiective = _obiectiveTuristice.getAll();
            foreach (var obiectiv in obiective)
            {
                if (obiectiv.nume.Equals(numeObiectiv))
                {
                    return obiectiv;
                }
            }
            return null;
        }

        public ObiectiveTuristice getObiectivById(int idObiectiv)
        {
            var obiective = _obiectiveTuristice.getAll();
            foreach (var obiectiv in obiective)
            {
                if (obiectiv.id == idObiectiv)
                {
                    return obiectiv;
                }
            }
            return null;
        }

        public FirmaTransport getFirmaTransportByNume(string numeFirma)
        {
            var firme = _firmeTransport.getAll();
            foreach (var firma in firme)
            {
                if (firma.nume.Equals(numeFirma))
                {
                    return firma;
                }
            }
            return null;
        }

        public FirmaTransport getFirmaTransportById(int idFirma)
        {
            var firme = _firmeTransport.getAll();
            foreach (var firma in firme)
            {
                if (firma.id == idFirma)
                {
                    return firma;
                }
            }
            return null;
        }

        public List<Excursie> getAllExcursii()
        {
            return _excursii.getAll();
        }

        public List<Excursie> getAllExcursiiByNumeAndInterval(string numeObiectiv, int oraMinim, int oraMaxim)
        {
            var obiectiv = getObiectivByNume(numeObiectiv);
            List<Excursie> lst = new List<Excursie>();
            var excursii = _excursii.getAll();
            foreach (var excursie in excursii)
            {
                if (excursie.idObiectiv.Equals(obiectiv.id))
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

        public int getNrLocuriDisponibile(Excursie excursie)
        {
            var totalLocuri = excursie.nrLocuriTotale;
            var rezervari = _rezervari.getAll();
            foreach (var rezervare in rezervari)
            {
                if (rezervare.idExcursie == excursie.id)
                {
                    totalLocuri -= rezervare.nrBilete;
                }
            }
            return totalLocuri;
        }

        public Persoana getPersoanaByNumeAndTelefon(string numeClient, string numarTelefon)
        {
            var persoane = _persoane.getAll();
            foreach (var persoana in persoane)
            {
                if (persoana.nume.Equals(numeClient) && persoana.numarTelefon.Equals(numarTelefon))
                {
                    return persoana;
                }
            }
            return null;
        }

        public void rezervaLocuri(string numeClient, string numarTelefon, int numarBileteDorite, Excursie excursie)
        {
            var persoana = getPersoanaByNumeAndTelefon(numeClient, numarTelefon);
            Rezervare rezervare = new Rezervare()
            {
                idExcursie = excursie.id,
                idPersoana = persoana.id,
                nrBilete = numarBileteDorite
            };
            var locuriDisponibile = getNrLocuriDisponibile(excursie);
            if (locuriDisponibile >= numarBileteDorite)
            {
                _rezervari.adauga(rezervare);
            }
        }
    }
}
