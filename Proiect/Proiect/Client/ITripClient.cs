using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Proiect.Client
{
    public interface ITripClient
    {
        void authentificare(string email, string parola);
        Task<List<Excursie>> getAllExcursii();
        Task<List<Excursie>> getAllExcursiiByNumeAndInterval(string numeObiectiv, int oraMinim, int oraMaxim);
        Task<Int32> getFirmaTransportById(int idFirma);
        Task<Int32> getFirmaTransportByNume(string numeFirma);
        Task<int> getNrLocuriDisponibile(int idExcursie);
        Task<ObiectiveTuristice> getObiectivById(int idObiectiv);
        Task<ObiectiveTuristice> getObiectivByNume(string numeObiectiv);
        Task<Persoana> getPersoanaByNumeAndTelefon(string numeClient, string numarTelefon);
        void login(string email, string parola);
        void logout();
        void rezervaLocuri(string numeClient, string numarTelefon, int numarBileteDorite, int idExcursie);
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
        void handleWebSocket(Action callback);
    }
}