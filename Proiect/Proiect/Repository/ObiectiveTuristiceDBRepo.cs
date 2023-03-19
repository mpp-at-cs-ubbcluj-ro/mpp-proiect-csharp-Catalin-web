using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class ObiectiveTuristiceDBRepo : Repository<int, ObiectiveTuristice>
    {
        public void adauga(ObiectiveTuristice entity)
        {
                var connection = ConnectionUtils.CreateConnection();
                var command = connection.CreateCommand();
                command.CommandText = "insert into obiectiv_turistic(nume) values (?)";
                command.Parameters.Add(entity.nume);
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
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "delete from obiectiv_turistic where id=?";
            command.Parameters.Add(entity.id);

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
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "update obiectiv_turistic set nume=? where id=?";
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
