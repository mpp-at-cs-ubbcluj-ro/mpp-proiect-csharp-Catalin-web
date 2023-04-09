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
    public class PersoanaRepository : IPersoanaRepository
    {
        public IConnection _connection;
       public PersoanaRepository(IConnection connection)
        {
            _connection = connection;
        }

        private readonly ILog _logger = LogManager.GetLogger("Monitor");
        public void adauga(Persoana entity)
        {
            _logger.Info("adauga persoana");
            var connection = _connection.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "insert into persoana(nume, numar_telefon) values (@nume,@numar_telefon)";
            command.Parameters.Add("@nume", DbType.String);
            command.Parameters["@nume"].Value = entity.nume;
            command.Parameters.Add("@numar_telefon", DbType.String);
            command.Parameters["@numar_telefon"].Value = entity.numarTelefon;

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
            _logger.Info("cauta persoana");
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
            _logger.Info("get all persoana");
            var list = new List<Persoana>();
            var connection = _connection.CreateConnection();
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
            _logger.Info("sterge persoana");
            var connection = _connection.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "delete from persoana where id=@id";
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

        public void update(Persoana entitate, Persoana nouaEntitate)
        {
            _logger.Info("update persoana");
            var connection = _connection.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "update persoana set nume=@nume, numar_telefon=@numar_telefon where id=@id";
            command.Parameters.Add("@nume", DbType.String);
            command.Parameters["@nume"].Value = nouaEntitate.nume;
            command.Parameters.Add("@numar_telefon", DbType.String);
            command.Parameters["@numar_telefon"].Value = nouaEntitate.numarTelefon;
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
