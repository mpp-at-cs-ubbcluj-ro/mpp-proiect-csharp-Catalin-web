using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class UserDBRepo : Repository<int, User>
    {
        public void adauga(User entity)
        {
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "insert into user(email, parola) values (?,?)";
            command.Parameters.Add(entity.email);
            command.Parameters.Add(entity.parola    );
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
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "delete from user where id=?";
            command.Parameters.Add(entity.id);

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
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "update user set email=?, parola=? where id=?";
            command.Parameters.Add(nouaEntitate.email);
            command.Parameters.Add(nouaEntitate.parola);
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
