using Nest;
using Proiect.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Proiect.Client
{
    public class TripClient : ITripClient
    {
        private string _baseUrl;
        public TripClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        private static readonly HttpClient _client = new HttpClient();

        public async void authentificare(string email, string parola)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            var response = await CallAsync(HttpMethod.Post, "/v1/authenticate?email=" + email + "&password=" + parola, jsonBody);
        }

        public async Task<List<Excursie>> getAllExcursii()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            try
            {
                var response = await CallAsync(HttpMethod.Post, "/v1/query/trips", jsonBody);
                var lst = await DeserializeResponseContentAsync<List<Excursie>>(response);
                return lst;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<List<Excursie>> getAllExcursiiByNumeAndInterval(string numeObiectiv, int oraMinim, int oraMaxim)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            try
            {
                var response = await CallAsync(HttpMethod.Post, "/v1/query/trips/obiectiv?numeObiectiv=" + numeObiectiv + "&oraMinim=" + oraMinim.ToString() + "&oraMaxim=" + oraMaxim.ToString(), jsonBody);
                var lst = await DeserializeResponseContentAsync<List<Excursie>>(response);
                return lst;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<Int32> getFirmaTransportById(int idFirma)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            try
            {
                var response = await CallAsync(HttpMethod.Post, "/v1/query/firma/id?idFirma=" + idFirma.ToString(), jsonBody);
                return await DeserializeResponseContentAsync<Int32>(response);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<Int32> getFirmaTransportByNume(string numeFirma)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            try
            {
                var response = await CallAsync(HttpMethod.Post, "/v1/query/firma/nume?numeFirma=" + numeFirma, jsonBody);
                return await DeserializeResponseContentAsync<Int32>(response);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<int> getNrLocuriDisponibile(int idExcursie)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            try
            {
                var response = await CallAsync(HttpMethod.Post, "/v1/query/trip/left?idExcursie=" + idExcursie.ToString(), jsonBody);
                var left= await DeserializeResponseContentAsync<Left>(response);
                return left.left;
            }
            catch (Exception ex)
            {

            }
            return -1;
        }

        public async Task<ObiectiveTuristice> getObiectivById(int idObiectiv)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            try
            {
                var response = await CallAsync(HttpMethod.Post, "/v1/query/obiectiv/id?idObiectiv=" + idObiectiv.ToString(), jsonBody);
                return await DeserializeResponseContentAsync<ObiectiveTuristice>(response);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<ObiectiveTuristice> getObiectivByNume(string numeObiectiv)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            try
            {
                var response = await CallAsync(HttpMethod.Post, "/v1/query/obiectiv/nume?numeObiectiv=" + numeObiectiv, jsonBody);
                return await DeserializeResponseContentAsync<ObiectiveTuristice>(response);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<Persoana> getPersoanaByNumeAndTelefon(string numeClient, string numarTelefon)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            try
            {
                var response = await CallAsync(HttpMethod.Post, "/v1/query/persoana/nume?numeClient=" + numeClient + "&numarTelefon=" + numarTelefon, jsonBody);
                return await DeserializeResponseContentAsync<Persoana>(response);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async void login(string email, string parola)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            var response = await CallAsync(HttpMethod.Post, "/v1/login?email=" + email + "&password=" + parola, jsonBody);
        }

        public async void logout()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            var response = await CallAsync(HttpMethod.Post, "/v1/logout", jsonBody);
        }

        public async void rezervaLocuri(string numeClient, string numarTelefon, int numarBileteDorite, int idExcursie)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            var response = await CallAsync(HttpMethod.Post, "/v1/add/rezervare?numeClient=" + numeClient + "&numarTelefon=" + numarTelefon + "&numarBileteDorite=" + numarBileteDorite.ToString() + "&idExcursie="+ idExcursie.ToString(), jsonBody);
        }

        private static string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        private async Task<HttpResponseMessage> CallAsync(HttpMethod method, string route, string jsonBody, string mediaType = "application/json")
        {
            using (var requestMessage = new HttpRequestMessage(method, _baseUrl + route))
            {
                using (var stringContent = new StringContent(jsonBody, Encoding.UTF8, mediaType))
                {
                    requestMessage.Content = stringContent;
                    return await SendAsync(requestMessage);
                }
            }
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return _client.SendAsync(request);
        }

        public static async Task<T> DeserializeResponseContentAsync<T>(HttpResponseMessage response) where T : class
        {
            var body = await response.Content.ReadAsStringAsync();
            return Deserialize<T>(body);
        }

        private static T Deserialize<T>(string json) where T : class
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            return JsonSerializer.Deserialize<T>(json);
        }

        /*public async Task handleWebSocket(Action callback)
        {
            var socket = new ClientWebSocket();
            await socket.ConnectAsync(new Uri("ws://localhost:12500/v1/webSocket"), CancellationToken.None);
            var mesaj = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Hello from client"));
            socket.SendAsync(mesaj, WebSocketMessageType.Text, true, CancellationToken.None).Wait();

            while (true)
            {
                var buffer = new byte[1024 * 4];
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None).ConfigureAwait(true);
                var serverMsg = Encoding.UTF8.GetString(buffer, 0, result.Count);
                callback();
            }
        }*/

        public async Task handleWebSocket(Action callback)
        {
            try { 
            while (true)
            {
                callback();
                await Task.Delay(5000);
            }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
