using Proiect.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class FirmaTransportDBRepo : Repository<int, FirmaTransport>
    {
        private readonly Logger _logger = new FileLogger();
        public void adauga(FirmaTransport entity)
        {
            _logger.Log("Adauga firma transport");
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "insert into firma_transport(nume) values (@nume)";
            command.Parameters.Add("@nume", DbType.String);
            command.Parameters["@nume"].Value = entity.nume;
            
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public FirmaTransport cautaId(int id)
        {
            var list = getAll();
            foreach (var el in list)
            {
                if (el.id == id)
                {
                    return el;
                }
            }

            return null;
        }

        public List<FirmaTransport> getAll()
        {
            _logger.Log("get all firma transport");
            var list = new List<FirmaTransport>();
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "select * from firma_transport";
            var reader = command.ExecuteReader();
            while(reader.Read())
            {
                var id = reader.GetInt16(0);
                var nume = reader.GetString(1);
                var firmaTransport = new FirmaTransport()
                { id = id,
                nume = nume
                };
                list.Add(firmaTransport);
            }
            return list;
        }

        public void sterge(FirmaTransport entity)
        {
            _logger.Log("sterge" +
                " firma transport");
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "delete from firma_transport where id=@id";
            command.Parameters.Add("@id", DbType.Int16);
            command.Parameters["@id"].Value = entity.id;

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public void update(FirmaTransport entitate, FirmaTransport nouaEntitate)
        {
            _logger.Log("updatefirma transport");
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "update firma_transport set nume=@nume where id=@id";
            command.Parameters.Add("@nume", DbType.String);
            command.Parameters["@nume"].Value = nouaEntitate.nume;
            command.Parameters.Add("@id", DbType.Int16);
            command.Parameters["@id"].Value = entitate.id;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
