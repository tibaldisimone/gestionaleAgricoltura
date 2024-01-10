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
    public class piantatoController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private piantatoModel piantato;
        private List<piantatoModel> _lstPiantato;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<piantatoModel> getAllPiantate()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from piantato ";
            _dr = _cmd.ExecuteReader();
            _lstPiantato = new List<piantatoModel>();
            while (_dr.Read())
            {
                piantato = new piantatoModel();
                piantato.IdPiantato = Convert.ToInt32(_dr["idPiantato"]);
                piantato.Data = Convert.ToDateTime(_dr["data"]);
                piantato.Varieta = Convert.ToString(_dr["varieta"]);
                piantato.NumPiante = Convert.ToInt32(_dr["numPiante"]);
                piantato.IdZona = Convert.ToInt32(_dr["idZona"]);

                _lstPiantato.Add(piantato);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstPiantato;
        }
        [HttpPost]
        public IEnumerable<piantatoModel> getpiantato([FromBody] dynamic data)
        {
            int idPiantato = data.id;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idPiantato", idPiantato);
            _cmd.CommandText = "select * from piantato where idPiantato=@idPiantato";
            _dr = _cmd.ExecuteReader();
            _lstPiantato = new List<piantatoModel>();
            while (_dr.Read())
            {
                piantato = new piantatoModel();
                piantato.IdPiantato = Convert.ToInt32(_dr["idPiantato"]);
                piantato.Data = Convert.ToDateTime(_dr["data"]);
                piantato.Varieta = Convert.ToString(_dr["varieta"]);
                piantato.NumPiante = Convert.ToInt32(_dr["numPiante"]);
                piantato.IdZona = Convert.ToInt32(_dr["idZona"]);
                _lstPiantato.Add(piantato);
            }

            return _lstPiantato;
        }

        [HttpPost]
        public IEnumerable<piantatoModel> getAllpiantatoZone([FromBody] dynamic data)
        {
            int idZona = data.idZona;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idZona", idZona);
            _cmd.CommandText = "select * from piantato where idZona=@idZona";
            _dr = _cmd.ExecuteReader();
            _lstPiantato = new List<piantatoModel>();
            while (_dr.Read())
            {
                piantato = new piantatoModel();
                piantato.IdPiantato = Convert.ToInt32(_dr["idPiantato"]);
                piantato.Data = Convert.ToDateTime(_dr["data"]);
                piantato.Varieta = Convert.ToString(_dr["varieta"]);
                piantato.NumPiante = Convert.ToInt32(_dr["numPiante"]);
                piantato.IdZona = Convert.ToInt32(_dr["idZona"]);

                _lstPiantato.Add(piantato);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstPiantato;
        }
        public string insertPiantato([FromBody] dynamic data)
        {
            try
            {
                int idPiantato = data.idPiantato;
                DateTime dataPiantata = data.dataPiantata;
                string varieta = data.varieta;
                int numPiante = data.numPiante;
                int idZona = data.idZona;
         


                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idPiantato", idPiantato);
                _cmd.Parameters.AddWithValue("@dataPiantata", dataPiantata);
                _cmd.Parameters.AddWithValue("@varieta", varieta);
                _cmd.Parameters.AddWithValue("@numPiante", numPiante);
                _cmd.Parameters.AddWithValue("@idZona", idZona);
           

                _cmd.CommandText = "INSERT into piantato (idPiantato, data, varieta, numPiante, idZona) values (@idPiantato, @dataPiantata, @varieta, @numPiante, @idZona)";
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
                int idPiantato = data.id;
                DateTime dataPiantata = data.dataPiantata;
                string varieta = data.varieta;
                int numPiante = data.numPiante;
                int idZona = data.idZona;




                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idPiantato", idPiantato);
                _cmd.Parameters.AddWithValue("@dataPiantata", dataPiantata);
                _cmd.Parameters.AddWithValue("@varieta", varieta);
                _cmd.Parameters.AddWithValue("@numPiante", numPiante);
                _cmd.Parameters.AddWithValue("@idZona", idZona);

                _cmd.CommandText = "UPDATE piantato set data=@dataPiantata,varietà=@varieta,numPiante=@numPiante,idZona=@idZona where idPiantato=@idPiantato  ";
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
                int idPiantato = data.idPiantato;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idPiantato", idPiantato);
                _cmd.CommandText = "DELETE from piantato where idPiantato=@idPiantato ";
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
            _cmd.CommandText = "select top 1 idPiantato from piantato order by idPiantato desc";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {

                id = Convert.ToInt32(_dr["idPiantato"]);
            }

            return id;
        }

    }
}