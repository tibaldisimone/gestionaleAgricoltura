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
    public class operaiController : ApiController
    {
        // GET api/<controller>
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private operaiModel operaio;
        private List<operaiModel> _lstOperai;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<operaiModel> getAllOperai()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from operai ";
            _dr = _cmd.ExecuteReader();
            _lstOperai = new List<operaiModel>();
            while (_dr.Read())
            {
                operaio = new operaiModel();
                operaio.IdOperaio = Convert.ToInt32(_dr["idOperaio"]);
                operaio.Cognome = Convert.ToString(_dr["cognome"]);
                operaio.Nome = Convert.ToString(_dr["nome"]);
                operaio.IdUtente = Convert.ToInt32(_dr["idUtente"]);


                _lstOperai.Add(operaio);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstOperai;
        }

        public operaiModel getOperaio([FromBody] dynamic data)
        {
            int idOperaio = data.idOperaio;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idOperaio", idOperaio);
            _cmd.CommandText = "select * from operai where idOperaio=@idOperaio";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {
                operaio = new operaiModel();
                operaio.IdOperaio = Convert.ToInt32(_dr["idOperaio"]);
                operaio.Cognome = Convert.ToString(_dr["cognome"]);
                operaio.Nome = Convert.ToString(_dr["nome"]);
                operaio.IdUtente = Convert.ToInt32(_dr["idUtente"]);
            }

            return operaio;
        }

        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idOperaio = data.idOperaio;
                string cognome = data.cognome;
                string nome = data.nome;
                int idUtente = data.idUtente;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idOperaio", idOperaio);
                _cmd.Parameters.AddWithValue("@cognome", cognome);
                _cmd.Parameters.AddWithValue("@nome", nome);
                _cmd.Parameters.AddWithValue("@idUtente", idUtente);



                _cmd.CommandText = "INSERT into operai (idOperaio, cognome, nome, idUtente) values (@idOperaio, @cognome, @nome, @idUtente)";
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
                int idOperaio = data.idOperaio;
                string cognome = data.cognome;
                string nome = data.nome;
                int idUtente = data.idUtente;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idOperaio", idOperaio);
                _cmd.Parameters.AddWithValue("@cognome", cognome);
                _cmd.Parameters.AddWithValue("@nome", nome);
                _cmd.Parameters.AddWithValue("@idUtente", idUtente);

                _cmd.CommandText = "UPDATE operai set cognome=@cognome,nome=@nome,idUtente=@idUtente where idOperaio=@idOperaio  ";
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
                int idOperaio = data.idOperaio;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idOperaio", idOperaio);
                _cmd.CommandText = "DELETE from operai where idOperaio=@idOperaio ";
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