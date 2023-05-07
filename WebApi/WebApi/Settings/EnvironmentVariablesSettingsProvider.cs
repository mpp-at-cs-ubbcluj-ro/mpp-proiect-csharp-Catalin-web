using System.Globalization;

namespace WebApi.Settings
{
    public class EnvironmentVariablesSettingsProvider : ISettingsProvider
    {
        public string BindingAddress
        {
            get
            {
                return Environment.GetEnvironmentVariable("TRIP_BINDING_ADRESS") ?? "http://localhost";
            }
        }

        public int Port
        {
            get
            {
                return int.Parse(Environment.GetEnvironmentVariable("TRIP_PORT") ?? "12500", CultureInfo.InvariantCulture);
            }
        }

        public string ConnectionString
        {
            get
            {
                return Environment.GetEnvironmentVariable("TRIP_CONNECTION_STRING") ?? "Data Source=E:\\Facultate\\Sem_4\\MPP\\Laboratoare\\Lab3\\mpp-proiect-csharp-Catalin-web\\Proiect\\Proiect\\Files\\exursii.db;";
            }
        }
    }
}
