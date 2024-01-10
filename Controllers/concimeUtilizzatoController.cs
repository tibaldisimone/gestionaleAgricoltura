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
    public class concimeUtilizzatoController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private concimeUtilizzatoModel concimeUtilizzato;
        private List<concimeUtilizzatoModel> _lstConcimeUtilizzato;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        [HttpPost]
        public IEnumerable<concimeUtilizzatoModel> getConcimeUtilizzato([FromBody] dynamic data)
        {
            int idConcime = data.idConcime;
            int idConcimazione = data.idConcimazione;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idConcime", idConcime);
            _cmd.Parameters.AddWithValue("@idConcimazione", idConcimazione);
            _cmd.CommandText = "select * from concimeUtilizzato where idConcime=@idConcime and idConcimazione=@idConcimazione";
            _dr = _cmd.ExecuteReader();
            _lstConcimeUtilizzato = new List<concimeUtilizzatoModel>();
            while (_dr.Read())
            {
                concimeUtilizzato = new concimeUtilizzatoModel();
                concimeUtilizzato.IdConcimazione = Convert.ToInt32(_dr["idConcimazione"]);
                concimeUtilizzato.IdConcime = Convert.ToInt32(_dr["idConcime"]);
                concimeUtilizzato.Nome = Convert.ToString(_dr["nome"]);

                _lstConcimeUtilizzato.Add(concimeUtilizzato);
            }

            return _lstConcimeUtilizzato;
        }

        [HttpPost]
        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idConcime = data.idConcime;
                int idConcimazione = data.idConcimazione;
                string nome = data.nome;




                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idConcime", idConcime);
                _cmd.Parameters.AddWithValue("@idConcimazione", idConcimazione);
                _cmd.Parameters.AddWithValue("@nome", nome);



                _cmd.CommandText = "INSERT into concimeUtilizzato (idConcime, idConcimazione, nome) values (@idConcime, @idConcimazione, @nome)";
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
                int idConcime = data.idConcime;
                int idConcimazione = data.idConcimazione;
                string nome = data.nome;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idConcime", idConcime);
                _cmd.Parameters.AddWithValue("@idConcimazione", idConcimazione);
                _cmd.Parameters.AddWithValue("@nome", nome);


                _cmd.CommandText = "UPDATE concimeUtilizzato set nome=@nome where idConcime=@idConcime and idConcimazione=@idConcimazione  ";
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
                int idConcime = data.idConcime;
                int idConcimazione = data.idConcimazione;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idConcime", idConcime);
                _cmd.Parameters.AddWithValue("@idConcimazione", idConcimazione);
                _cmd.CommandText = "DELETE from concimeUtilizzato where idConcime=@idConcime and idConcimazione=@idConcimazione";
                _cmd.ExecuteNonQuery();
                return ("eliminazione avvenuto con successo");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }
    }
}