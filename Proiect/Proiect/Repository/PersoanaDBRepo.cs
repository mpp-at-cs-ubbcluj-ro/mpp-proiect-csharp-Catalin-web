using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class PersoanaDBRepo : Repository<int, Persoana>
    {
        public void adauga(Persoana entity)
        {
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "insert into persoana(nume, numar_telefon) values (?,?)";
            command.Parameters.Add(entity.nume);
            command.Parameters.Add(entity.numarTelefon);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public Persoana cautaId(int id)
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

        public List<Persoana> getAll()
        {
            var list = new List<Persoana>();
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "select * from persoana";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt16(0);
                var nume = reader.GetString(1);
                var numarTelefon = reader.GetString(2);
                var el = new Persoana()
                {
                    id = id,
                    nume = nume,
                    numarTelefon = numarTelefon
                };
                list.Add(el);
            }
            return list;
        }

        public void sterge(Persoana entity)
        {
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "delete from persoana where id=?";
            command.Parameters.Add(entity.id);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public void update(Persoana entitate, Persoana nouaEntitate)
        {
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "update persoana set nume=?, numar_telefon=? where id=?";
            command.Parameters.Add(nouaEntitate.nume);
            command.Parameters.Add(nouaEntitate.numarTelefon);
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
