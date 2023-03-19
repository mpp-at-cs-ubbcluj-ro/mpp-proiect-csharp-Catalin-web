using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class RezervareDBRepo : Repository<int, Rezervare>
    {
        public void adauga(Rezervare entity)
        {
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "insert into rezervare(id_excursie, id_persoana, nr_bilete) values (?,?,?)";
            command.Parameters.Add(entity.idExcursie);
            command.Parameters.Add(entity.idPersoana);
            command.Parameters.Add(entity.nrBilete);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public Rezervare cautaId(int id)
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

        public List<Rezervare> getAll()
        {
            var list = new List<Rezervare>();
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "select * from rezervare";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt16(0);
                var idExcursie = reader.GetInt32(1);
                var idPersoana = reader.GetInt32(2);
                var nrBilete = reader.GetInt32(3);
                var el = new Rezervare()
                {
                    id = id,
                    idExcursie = idExcursie,
                    idPersoana = idPersoana,
                    nrBilete = nrBilete
                };
                list.Add(el);
            }
            return list;
        }

        public void sterge(Rezervare entity)
        {
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "delete from rezervare where id=?";
            command.Parameters.Add(entity.id);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public void update(Rezervare entitate, Rezervare nouaEntitate)
        {
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "update rezervare set id_excursie=?, id_persoana=?, nr_bilete=? where id=?";
            command.Parameters.Add(nouaEntitate.idExcursie);
            command.Parameters.Add(nouaEntitate.idPersoana);
            command.Parameters.Add(nouaEntitate.nrBilete);
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
