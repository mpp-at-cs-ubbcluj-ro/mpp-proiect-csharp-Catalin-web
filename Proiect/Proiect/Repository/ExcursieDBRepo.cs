using Microsoft.Data.Sqlite;
using Proiect.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class ExcursieDBRepo : Repository<int, Excursie>
    {

        public void adauga(Excursie entity)
        {
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = "insert into excursie(@id_obiectiv, @id_firma_transport, @ora, @nr_locuri_totale, @pret) values (?,?,?,?,?)";
            command.Parameters.Add("@id_obiectiv", SqliteType.Integer);
            command.Parameters.Add("@id_firma_transport", SqliteType.Integer);
            command.Parameters.Add("@ora", SqliteType.Text);
            command.Parameters.Add("@nr_locuri_totale", SqliteType.Integer);
            command.Parameters.Add("@pret", SqliteType.Real);
            command.Parameters["@id_obiectiv"].Value = entity.idObiectiv;
            command.Parameters["@id_firma_transport"].Value = entity.idFirmaTransport;
            command.Parameters["@ora"].Value = entity.ora.ToString();
            command.Parameters["@nr_locuri_totale"].Value = entity.nrLocuriTotale;
            command.Parameters["@pret"].Value = entity.pret;
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
                var ora = reader.GetTimeSpan(3);
                var nrLocuriTotale = reader.GetInt16(4);
                var pret = reader.GetFloat(5);
                var excursie = new Excursie()
                {
                    id = id,
                    idObiectiv = idObiectiv,
                    idFirmaTransport = idFirmaTransport,
                    ora = ora,
                    nrLocuriTotale = nrLocuriTotale,
                    pret = pret
                };
                list.Add(excursie);
            }

            return list;
        }

        public void sterge(Excursie entity)
        {
            var list = new List<Excursie>();
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();

            command.CommandText = "delete from excursie where id=@id";
            command.Parameters.Add("@id", SqliteType.Integer);
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
            var list = new List<Excursie>();
            var connection = ConnectionUtils.CreateConnection();
            var command = connection.CreateCommand();

            command.CommandText = "update excursie set id_obiectiv=@id_obiectiv, id_firma_transport=@id_firma_transport, ora=@ora, nr_locuri_totale=@nr_locuri_totale, pret=@pret where id=@id";
            
            command.Parameters.Add("@id_obiectiv", SqliteType.Integer);
            command.Parameters.Add("@id_firma_transport", SqliteType.Integer);
            command.Parameters.Add("@ora", SqliteType.Text);
            command.Parameters.Add("@nr_locuri_totale", SqliteType.Integer);
            command.Parameters.Add("@pret", SqliteType.Real);
            command.Parameters["@id_obiectiv"].Value = nouaEntitate.idObiectiv;
            command.Parameters["@id_firma_transport"].Value = nouaEntitate.idFirmaTransport;
            command.Parameters["@ora"].Value = nouaEntitate.ora.ToString();
            command.Parameters["@nr_locuri_totale"].Value = nouaEntitate.nrLocuriTotale;
            command.Parameters["@pret"].Value = nouaEntitate.pret;
            command.Parameters.Add("@id", SqliteType.Integer);
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
