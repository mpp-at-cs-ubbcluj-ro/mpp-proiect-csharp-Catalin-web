using System.Data.SQLite;
using Proiect.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;

namespace Proiect
{
    public class ExcursieDBRepo : Repository<int, Excursie>
    {
        private readonly Logger _logger = new FileLogger();

        public void adauga(Excursie entity)
        {
            _logger.Log("Adauga excursie");
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "insert into excursie(id_obiectiv, id_firma_transport, ora, nr_locuri_totale, pret) values (@id_obiectiv,@id_firma_transport,@ora,@nr_locuri_totale,@pret)";
            command.Parameters.AddWithValue("@id_obiectiv", entity.idObiectiv);
            command.Parameters.AddWithValue("@id_firma_transport", entity.idFirmaTransport);
            command.Parameters.AddWithValue("@ora", entity.ora);
            command.Parameters.AddWithValue("@pret", entity.pret);
            command.Parameters.AddWithValue("@nr_locuri_totale", entity.nrLocuriTotale);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public Excursie cautaId(int id)
        {
            _logger.Log("Cauta id excursie");
            var list = getAll();
            foreach (var excursie in list)
            {
                if (excursie.id == id)
                {
                    return excursie;
                }
            }

            return null;
        }

        public List<Excursie> getAll()
        {
            _logger.Log("Get all excursie");
            var list = new List<Excursie>();
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();

            command.CommandText = "select * from excursie";
            var reader = command.ExecuteReader();
            while(reader.Read())
            {
                var id = reader.GetInt16(0);
                var idObiectiv = reader.GetInt16(1);
                var idFirmaTransport = reader.GetInt16(2);
                var ora = reader.GetString(3);
                var nrLocuriTotale = reader.GetInt16(4);
                var pret = reader.GetFloat(5);
                var excursie = new Excursie()
                {
                    id = id,
                    idObiectiv = idObiectiv,
                    idFirmaTransport = idFirmaTransport,
                    ora= ora,
                    nrLocuriTotale = nrLocuriTotale,
                    pret = pret
                };
                list.Add(excursie);
            }

            return list;
        }

        public void sterge(Excursie entity)
        {
            _logger.Log("Sterge excursie");
            var list = new List<Excursie>();
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();

            command.CommandText = "delete from excursie where id=@id";
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

        public void update(Excursie entitate, Excursie nouaEntitate)
        {
            _logger.Log("Update excursie");
            var list = new List<Excursie>();
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();

            command.CommandText = "update excursie set id_obiectiv=@id_obiectiv, id_firma_transport=@id_firma_transport, ora=@ora, nr_locuri_totale=@nr_locuri_totale, pret=@pret where id=@id";
            
            command.Parameters.Add("@id_obiectiv", DbType.Int16);
            command.Parameters["@id_obiectiv"].Value = nouaEntitate.idObiectiv;
            command.Parameters.Add("@id_firma_transport", DbType.Int16);
            command.Parameters["@id_firma_transport"].Value = nouaEntitate.idFirmaTransport;
            command.Parameters.Add("@ora", DbType.String);
            command.Parameters["@ora"].Value = nouaEntitate.ora.ToString();
            command.Parameters.Add("@nr_locuri_totale", DbType.Int16);
            command.Parameters["@nr_locuri_totale"].Value = nouaEntitate.nrLocuriTotale;
            command.Parameters.Add("@pret", DbType.Double);
            command.Parameters["@pret"].Value = nouaEntitate.pret;
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
