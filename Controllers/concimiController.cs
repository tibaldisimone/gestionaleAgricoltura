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
    public class concimiController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private concimiModel concime;
        private List<concimiModel> _lstConcimi;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<concimiModel> getAllConcimi()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from concimi ";
            _dr = _cmd.ExecuteReader();
            _lstConcimi = new List<concimiModel>();
            while (_dr.Read())
            {
                concime = new concimiModel();
                concime.IdConcime = Convert.ToInt32(_dr["idConcime"]);
                concime.Nome = Convert.ToString(_dr["nome"]);
                concime.Descrizione = Convert.ToString(_dr["descrizione"]);


                _lstConcimi.Add(concime);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstConcimi;
        }
        [HttpPost]
        public concimiModel getConcime([FromBody] dynamic data)
        {
            int idConcime = data.idConcime;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idConcime", idConcime);
            _cmd.CommandText = "select * from concimi where idConcime=@idConcime";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {
                concime = new concimiModel();
                concime.IdConcime = Convert.ToInt32(_dr["idConcime"]);
                concime.Nome = Convert.ToString(_dr["nome"]);
                concime.Descrizione = Convert.ToString(_dr["descrizione"]);

            }

            return concime;
        }
        [HttpPost]
        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idConcime = data.idConcime;
                string nome = data.nome;
                string descrizione = data.descrizione;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idConcime", idConcime);
                _cmd.Parameters.AddWithValue("@nome", nome);
                _cmd.Parameters.AddWithValue("@descrizione", descrizione);
                _cmd.CommandText = "INSERT into concimi (idConcime, nome, descrizione) values (@idConcime, @nome, @descrizione)";
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
                string nome = data.nome;
                string descrizione = data.descrizione;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idConcime", idConcime);
                _cmd.Parameters.AddWithValue("@nome", nome);
                _cmd.Parameters.AddWithValue("@descrizione", descrizione);

                _cmd.CommandText = "UPDATE concimi set nome=@nome,descrizione=@descrizione where idConcime=@idConcime  ";
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
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idConcime", idConcime);
                _cmd.CommandText = "DELETE from concimi where idConcime=@idConcime ";
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