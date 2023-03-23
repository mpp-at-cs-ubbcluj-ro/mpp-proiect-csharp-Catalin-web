using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class UserDBRepo : Repository<int, User>
    {
        private readonly ILog _logger = LogManager.GetLogger("Monitor");
        public void adauga(User entity)
        {
            _logger.Info("adauga user");
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "insert into user(email, parola) values (@email,@parola)";
            command.Parameters.Add("@email", DbType.String);
            command.Parameters["@email"].Value = entity.email;
            command.Parameters.Add("@parola", DbType.String);
            command.Parameters["@parola"].Value = entity.parola;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public User cautaId(int id)
        {
            _logger.Info("cauta user");
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

        public List<User> getAll()
        {
            _logger.Info("cauta user");
            var list = new List<User>();
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "select * from rezervare";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt16(0);
                var email = reader.GetString(1);
                var parola = reader.GetString(2);
                var el = new User()
                {
                    id = id,
                    email = email,
                    parola = parola
                };
                list.Add(el);
            }
            return list;
        }

        public void sterge(User entity)
        {
            _logger.Info("cauta user");
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "delete from user where id=@id";
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

        public void update(User entitate, User nouaEntitate)
        {
            _logger.Info("cauta user");
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "update user set email=@email, parola=@parola where id=@id";
            command.Parameters.Add("@email", DbType.String);
            command.Parameters["@email"].Value = entitate.email;
            command.Parameters.Add("@parola", DbType.String);
            command.Parameters["@parola"].Value = entitate.parola;
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
