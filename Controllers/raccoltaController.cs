using esercizioProva.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace esercizioProva.Controllers
{
    public class raccoltaController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private raccoltaModel raccolto;
        private List<raccoltaModel> _lstRaccolte;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<raccoltaModel> getAllRaccolta()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from raccolta ";
            _dr = _cmd.ExecuteReader();
            _lstRaccolte = new List<raccoltaModel>();
            while (_dr.Read())
            {
                raccolto = new raccoltaModel();
                raccolto.IdRaccolta = Convert.ToInt32(_dr["idRaccolta"]);
                raccolto.Data = Convert.ToDateTime(_dr["data"]);
                raccolto.Quantita = Convert.ToInt32(_dr["quantita"]);
                raccolto.IdZona = Convert.ToInt32(_dr["idZona"]);
                raccolto.IdRaccoltaFinale = Convert.ToInt32(_dr["idRaccoltaFinale"]);

                _lstRaccolte.Add(raccolto);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstRaccolte;
        }

        [HttpPost]
        public IEnumerable<raccoltaModel> getRaccolta([FromBody] dynamic data)
        {
            int idRaccolta = data.id;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idRaccolta", idRaccolta);
            _cmd.CommandText = "select * from raccolta where idRaccolta=@idRaccolta";
            _dr = _cmd.ExecuteReader();
            _lstRaccolte = new List<raccoltaModel>();
            while (_dr.Read())
            {
                raccolto = new raccoltaModel();
                raccolto.IdRaccolta = Convert.ToInt32(_dr["idRaccolta"]);
                raccolto.Data = Convert.ToDateTime(_dr["data"]);
                raccolto.Quantita = Convert.ToInt32(_dr["quantita"]);
                raccolto.IdZona = Convert.ToInt32(_dr["idZona"]);
                raccolto.IdRaccoltaFinale = Convert.ToInt32(_dr["idRaccoltaFinale"]);
                _lstRaccolte.Add(raccolto);
            }

            return _lstRaccolte;
        }

        [HttpPost]
        public IEnumerable<raccoltaModel> getAllraccoltaZone([FromBody] dynamic data)
        {
            int idZona = data.idZona;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idZona", idZona);
            _cmd.CommandText = "select * from raccolta inner join esecuzioneRaccolta on raccolta.idRaccolta=esecuzioneRaccolta.idRaccolta inner join operai on esecuzioneRaccolta.idOperaio=operai.idOperaio where idZona=@idZona";
            _dr = _cmd.ExecuteReader();
            _lstRaccolte = new List<raccoltaModel>();
            while (_dr.Read())
            {
                raccolto = new raccoltaModel();
                raccolto.IdRaccolta = Convert.ToInt32(_dr["idRaccolta"]);
                raccolto.Data = Convert.ToDateTime(_dr["data"]);
                raccolto.Quantita = Convert.ToInt32(_dr["quantita"]);
                raccolto.IdZona = Convert.ToInt32(_dr["idZona"]);
                raccolto.IdRaccoltaFinale = Convert.ToInt32(_dr["idRaccoltaFinale"]);
                raccolto.Nome = Convert.ToString(_dr["nome"]);
                _lstRaccolte.Add(raccolto);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstRaccolte;
        }
        [HttpPost]
        public IEnumerable<raccoltaModel> getAllRaccoltaIdRaccolteFinale([FromBody] dynamic data)
        {
            int idRaccoltaFinale = data.idRaccoltaFinale;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idRaccoltaFinale", idRaccoltaFinale);
            _cmd.CommandText = "select * from raccolta where idRaccoltaFinale=@idRaccoltaFinale";
            _dr = _cmd.ExecuteReader();
            _lstRaccolte = new List<raccoltaModel>();
            while (_dr.Read())
            {
                raccolto = new raccoltaModel();
                raccolto.IdRaccolta = Convert.ToInt32(_dr["idRaccolta"]);
                raccolto.Data = Convert.ToDateTime(_dr["data"]);
                raccolto.Quantita = Convert.ToInt32(_dr["quantita"]);
                raccolto.IdZona = Convert.ToInt32(_dr["idZona"]);
                raccolto.IdRaccoltaFinale = Convert.ToInt32(_dr["idRaccoltaFinale"]);

                _lstRaccolte.Add(raccolto);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstRaccolte;
        }
        [HttpPost]
        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idRaccolta = data.idRaccolta;
                DateTime dataRaccolta = data.dataRaccolta;
                int quantita = data.quantita;
                int idRaccoltaFinale = data.idRaccoltaFinale;
                int idZona = data.idZona;
             


                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idRaccolta", idRaccolta);
                _cmd.Parameters.AddWithValue("@dataRaccolta", dataRaccolta);
                _cmd.Parameters.AddWithValue("@quantita", quantita);
                _cmd.Parameters.AddWithValue("@idRaccoltaFinale", idRaccoltaFinale);
                _cmd.Parameters.AddWithValue("@idZona", idZona);


                _cmd.CommandText = "INSERT into raccolta (idRaccolta, data, quantita, idRaccoltaFinale, idZona) values (@idRaccolta, @dataRaccolta, @quantita, @idRaccoltaFinale, @idZona)";
                _cmd.ExecuteNonQuery();
                return ("Inserimento avvenuto con successo");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }
        [HttpPost]
        public string update([FromBody] dynamic data)
        {
            try
            {
                int idRaccolta = data.id;
                DateTime dataRaccolta = data.dataRaccolta;
                int quantita = data.quantita;
                int idRaccoltaFinale = data.idRaccoltaFinale;
                int idZona = data.idZona;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idRaccolta", idRaccolta);
                _cmd.Parameters.AddWithValue("@dataRaccolta", dataRaccolta);
                _cmd.Parameters.AddWithValue("@quantita", quantita);
                _cmd.Parameters.AddWithValue("@idRaccoltaFinale", idRaccoltaFinale);
                _cmd.Parameters.AddWithValue("@idZona", idZona);


                _cmd.CommandText = "UPDATE raccolta set data=@dataRaccolta,quantita=@quantita,idRaccoltaFinale=@idRaccoltaFinale,idZona=@idZona where idRaccolta=@idRaccolta  ";
                _cmd.ExecuteNonQuery();
                return ("Inserimento avvenuto con successo");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }
        [HttpPost]
        public string delete([FromBody] dynamic data)
        {
            try
            {
                int idRaccolta = data.id;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idRaccolta", idRaccolta);
                _cmd.CommandText = "DELETE from raccolta where idRaccolta=@idRaccolta ";
                _cmd.ExecuteNonQuery();
                return ("eliminazione avvenuto con successo");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }
        [HttpPost]
        public int getLastId([FromBody] dynamic data)
        {

            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            int id = 0;
            _cmd.CommandText = "select top 1 idRaccolta from raccolta order by idRaccolta desc";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {

                id = Convert.ToInt32(_dr["idRaccolta"]);
            }

            return id;
        }
    }
}