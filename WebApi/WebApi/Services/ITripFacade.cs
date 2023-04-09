using Model.Entities;

namespace WebApi.Services
{
    public interface ITripFacade
    {
        void authentificare(string email, string parola);
        List<Excursie> getAllExcursii();
        List<Excursie> getAllExcursiiByNumeAndInterval(string numeObiectiv, int oraMinim, int oraMaxim);
        FirmaTransport getFirmaTransportById(int idFirma);
        FirmaTransport getFirmaTransportByNume(string numeFirma);
        int getNrLocuriDisponibile(int idExcursie);
        ObiectiveTuristice getObiectivById(int idObiectiv);
        ObiectiveTuristice getObiectivByNume(string numeObiectiv);
        Persoana getPersoanaByNumeAndTelefon(string numeClient, string numarTelefon);
        void login(string email, string parola);
        void logout();
        void rezervaLocuri(string numeClient, string numarTelefon, int numarBileteDorite, int idExcursie);
    }
}