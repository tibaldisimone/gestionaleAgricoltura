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
    public class concimazioneController : ApiController
    {
        // GET api/<controller>
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private concimazioneModel concimazione;
        private List<concimazioneModel> _lstConcimazione;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<concimazioneModel> getAllConcimazioni()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from concimazione ";
            _dr = _cmd.ExecuteReader();
            _lstConcimazione = new List<concimazioneModel>();
            while (_dr.Read())
            {
                concimazione = new concimazioneModel();
                concimazione.IdConcimazione = Convert.ToInt32(_dr["idConcimazione"]);
                concimazione.Data = Convert.ToDateTime(_dr["data"]);
               
           
                concimazione.IdZona = Convert.ToInt32(_dr["idZona"]);

                _lstConcimazione.Add(concimazione);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstConcimazione;
        }
        [HttpPost]
        public IEnumerable<concimazioneModel> getConcimazione([FromBody] dynamic data)
        {
            int idConcimazione = data.id;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idConcimazione", idConcimazione);
            _cmd.CommandText = "select * from concimazione where idConcimazione=@idConcimazione";
            _dr = _cmd.ExecuteReader();
            _lstConcimazione = new List<concimazioneModel>();
            while (_dr.Read())
            {
                concimazione = new concimazioneModel();
                concimazione.IdConcimazione = Convert.ToInt32(_dr["idConcimazione"]);
                concimazione.Data = Convert.ToDateTime(_dr["data"]);
               
               
                concimazione.IdZona = Convert.ToInt32(_dr["idZona"]);
                _lstConcimazione.Add(concimazione);
            }

            return _lstConcimazione;
        }
        [HttpPost]
        public concimazioneModel getConcimazioneIdProdotto([FromBody] dynamic data)
        {
            int idProdotto = data.idProdotto;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idProdotto", idProdotto);
            _cmd.CommandText = "select * from concimazione where idProdotto=@idProdotto";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {
                concimazione = new concimazioneModel();
                concimazione.IdConcimazione = Convert.ToInt32(_dr["idConcimazione"]);
                concimazione.Data = Convert.ToDateTime(_dr["data"]);
               
               
                concimazione.IdZona = Convert.ToInt32(_dr["idZona"]);
            }

            return concimazione;
        }
        [HttpPost]
        public IEnumerable<concimazioneModel> getAllConcimazioneZone([FromBody] dynamic data)
        {
            int idZona = data.idZona;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idZona", idZona);
            _cmd.CommandText = "select * from concimazione inner join concimeUtilizzato on concimazione.idConcimazione=concimeUtilizzato.idConcimazione  where idZona=@idZona";
            _dr = _cmd.ExecuteReader();
            _lstConcimazione = new List<concimazioneModel>();
            while (_dr.Read())
            {
                concimazione = new concimazioneModel();
                concimazione.IdConcimazione = Convert.ToInt32(_dr["idConcimazione"]);
                concimazione.Data = Convert.ToDateTime(_dr["data"]);
                concimazione.Nome = Convert.ToString(_dr["nome"]);

                concimazione.IdZona = Convert.ToInt32(_dr["idZona"]);

                _lstConcimazione.Add(concimazione);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstConcimazione;
        }
        [HttpPost]
        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idConcimazione = data.idConcimazione;
                DateTime dataConcimazione = data.dataConcimazione;
       
         
                int idZona = data.idZona;
                


                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idConcimazione", idConcimazione);
                _cmd.Parameters.AddWithValue("@dataConcimazione", dataConcimazione);
            
   
                _cmd.Parameters.AddWithValue("@idZona", idZona);


                _cmd.CommandText = "INSERT into concimazione (idConcimazione, data, idZona) values (@idConcimazione, @dataConcimazione, @idZona)";
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
                int idConcimazione = data.id;
                DateTime dataConcimazione = data.dataConcimazione;
                int quantità = data.quantita;
            
                int idZona = data.idZona;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idConcimazione", idConcimazione);
                _cmd.Parameters.AddWithValue("@dataConcimazione", dataConcimazione);
               
          
                _cmd.Parameters.AddWithValue("@idZona", idZona);

                _cmd.CommandText = "UPDATE concimazione set data=@dataConcimazione,idZona=@idZona where idConcimazione=@idConcimazione  ";
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
                int idConcimazione = data.idConcimazione;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idConcimazione", idConcimazione);
                _cmd.CommandText = "DELETE from concimazione where idConcimazione=@idConcimazione ";
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
            _cmd.CommandText = "select top 1 idConcimazione from concimazione order by idConcimazione desc";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {

                id = Convert.ToInt32(_dr["idConcimazione"]);
            }

            return id;
        }
    }
}