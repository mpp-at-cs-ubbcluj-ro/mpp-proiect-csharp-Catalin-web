using DataStore.Provicers.SQLite;
using log4net;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Core.Implementations
{
    public class RezervareRepository : IRezervareRepository
    {
        private IConnection _connection;
        public RezervareRepository(IConnection connection)
        {
            _connection = connection;
        }

        private readonly ILog _logger = LogManager.GetLogger("Monitor");
        public void adauga(Rezervare entity)
        {
            _logger.Info("adauga rezervare");
            var connection = _connection.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "insert into rezervare(id_excursie, id_persoana, nr_bilete) values (@id_excursie,@id_persoana,@nr_bilete)";
            command.Parameters.Add("@id_excursie", DbType.Int16);
            command.Parameters["@id_excursie"].Value = entity.idExcursie;
            command.Parameters.Add("@id_persoana", DbType.Int16);
            command.Parameters["@id_persoana"].Value = entity.idPersoana;
            command.Parameters.Add("@nr_bilete", DbType.Int16);
            command.Parameters["@nr_bilete"].Value = entity.nrBilete;
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
            _logger.Info("cauta rezervare");
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
            _logger.Info("get all rezervare");
            var list = new List<Rezervare>();
            var connection = _connection.CreateConnection();
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
            _logger.Info("sterge rezervare");
            var connection = _connection.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "delete from rezervare where id=@id";
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

        public void update(Rezervare entitate, Rezervare nouaEntitate)
        {
            _logger.Info("update rezervare");
            var connection = _connection.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "update rezervare set id_excursie=@id_excursie, id_persoana=@id_persoana, nr_bilete=@nr_bilete where id=@id";
            command.Parameters.Add("@id_excursie", DbType.Int16);
            command.Parameters["@id_excursie"].Value = nouaEntitate.idExcursie;
            command.Parameters.Add("@id_persoana", DbType.Int16);
            command.Parameters["@id_persoana"].Value = nouaEntitate.idPersoana;
            command.Parameters.Add("@nr_bilete", DbType.Int16);
            command.Parameters["@nr_bilete"].Value = nouaEntitate.nrBilete;
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
