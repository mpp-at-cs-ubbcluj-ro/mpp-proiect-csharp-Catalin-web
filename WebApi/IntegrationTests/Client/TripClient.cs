using Model.Entities;
using System.Text;
using System.Text.Json;

namespace IntegrationTests.Client
{
    public class TripClient
    {
        private string _baseUrl = "http://localhost:12500";
        public TripClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        private static readonly HttpClient _client = new HttpClient();

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
        public async Task<Excursie> addExcursie(string numeObiectiv, string numeFirma, int ora, float pret, int nrLocuriTotale)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            try
            {
                var response = await CallAsync(HttpMethod.Post, "/v1/exursie/add?numeObiectiv=" + numeObiectiv+"&numeFirma="+numeFirma+"&ora="+ora+"&pret="+pret+"&nrLocuriTotale="+nrLocuriTotale,jsonBody);
                return await DeserializeResponseContentAsync<Excursie>(response);
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public async Task deleteExcursie(int idExcursie)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            try
            {
                await CallAsync(HttpMethod.Post, "/v1/exursie/delete?idExcursie=" + idExcursie, jsonBody);
            }
            catch (Exception ex)
            {
            }
        }
        public async Task updateExcursie(int idExcursie, string numeObiectiv, string numeFirma, int ora, float pret, int nrLocuriTotale)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            try
            {
                await CallAsync(HttpMethod.Post, "/v1/exursie/update?idExcursie=" + idExcursie+"&numeObiectiv=" + numeObiectiv + "&numeFirma=" + numeFirma + "&ora=" + ora + "&pret=" + pret + "&nrLocuriTotale=" + nrLocuriTotale, jsonBody);
            }
            catch (Exception ex)
            {

            }
        }
        public async Task<Excursie> getExcursie(int idExcursie)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var jsonBody = Serialize(dict);
            try
            {
                var response =  await CallAsync(HttpMethod.Post, "/v1/exursie/get?idExcursie=" + idExcursie, jsonBody);
                return await DeserializeResponseContentAsync<Excursie>(response);
            }
            catch (Exception ex)
            {
            }
            return null;
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
        private Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return _client.SendAsync(request);
        }
        private async Task<T> DeserializeResponseContentAsync<T>(HttpResponseMessage response) where T : class
        {
            var body = await response.Content.ReadAsStringAsync();
            return Deserialize<T>(body);
        }

        private T Deserialize<T>(string json) where T : class
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
