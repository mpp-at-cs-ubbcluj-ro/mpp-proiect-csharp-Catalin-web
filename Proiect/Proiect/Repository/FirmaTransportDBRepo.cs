using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class FirmaTransportDBRepo : Repository<int, FirmaTransport>
    {
        public void adauga(FirmaTransport entity)
        {
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "insert into firma_transport(nume) values (?)";
            command.Parameters.Add(entity.nume);
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
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "delete from firma_transport where id=?";
            command.Parameters.Add(entity.id);

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
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "update firma_transport set nume=? where id=?";
            command.Parameters.Add(nouaEntitate.nume);
            command.Parameters.Add(entitate.id);

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
