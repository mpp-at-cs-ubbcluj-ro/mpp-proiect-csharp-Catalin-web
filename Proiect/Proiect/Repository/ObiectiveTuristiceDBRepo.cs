using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class ObiectiveTuristiceDBRepo : Repository<int, ObiectiveTuristice>
    {
        private readonly ILog _logger = LogManager.GetLogger("Monitor");
        public void adauga(ObiectiveTuristice entity)
        {
            _logger.Info("adauga obiectiv turistic");
            var connection = ConnectionUtils.CreateConnection();
                var command = connection.CreateCommand();
                command.CommandText = "insert into obiectiv_turistic(nume) values (@nume)";
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

        public ObiectiveTuristice cautaId(int id)
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

        public List<ObiectiveTuristice> getAll()
        {
            _logger.Info("get all obiectiv turistic");
            var list = new List<ObiectiveTuristice>();
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "select * from obiectiv_turistic";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt16(0);
                var nume = reader.GetString(1);
                var el = new ObiectiveTuristice()
                {
                    id = id,
                    nume = nume
                };
                list.Add(el);
            }
            return list;
        }

        public void sterge(ObiectiveTuristice entity)
        {
            _logger.Info("sterge obiectiv turistic");
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "delete from obiectiv_turistic where id=@id";
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

        public void update(ObiectiveTuristice entitate, ObiectiveTuristice nouaEntitate)
        {
            _logger.Info("update obiectiv turistic");
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "update obiectiv_turistic set nume=@nume where id=@id";
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
