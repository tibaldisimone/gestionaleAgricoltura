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
    public class trattamentiController : ApiController
    {
        // GET api/<controller>
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private trattamentiModel trattamento;
        private List<trattamentiModel> _lstTrattamento;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<trattamentiModel> getAllTrattamenti()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from trattamenti ";
            _dr = _cmd.ExecuteReader();
            _lstTrattamento = new List<trattamentiModel>();
            while (_dr.Read())
            {
                trattamento = new trattamentiModel();
                trattamento.IdTrattamento = Convert.ToInt32(_dr["idTrattamento"]);
                trattamento.Data = Convert.ToDateTime(_dr["data"]);
              
               
                trattamento.IdZona = Convert.ToInt32(_dr["idZona"]);

                _lstTrattamento.Add(trattamento);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstTrattamento;
        }
        [HttpPost]
        public IEnumerable<trattamentiModel> gettrattamenti([FromBody] dynamic data)
        {
            int idTrattamento = data.id;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idTrattamento", idTrattamento);
            _cmd.CommandText = "select * from trattamenti  where idTrattamento=@idTrattamento";
            _dr = _cmd.ExecuteReader();
            _lstTrattamento = new List<trattamentiModel>();
            while (_dr.Read())
            {
                trattamento = new trattamentiModel();
                trattamento.IdTrattamento = Convert.ToInt32(_dr["idTrattamento"]);
                trattamento.Data = Convert.ToDateTime(_dr["data"]);
         
            
                trattamento.IdZona = Convert.ToInt32(_dr["idZona"]);
                _lstTrattamento.Add(trattamento);
            }

            return _lstTrattamento;
        }

        [HttpPost]
        public trattamentiModel getTrattamentiIdProdotto([FromBody] dynamic data)
        {
            int idProdotto = data.idProdotto;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idProdotto", idProdotto);
            _cmd.CommandText = "select * from trattamenti where idProdotto=@idProdotto";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {
                trattamento = new trattamentiModel();
                trattamento.IdTrattamento = Convert.ToInt32(_dr["idTrattamento"]);
                trattamento.Data = Convert.ToDateTime(_dr["data"]);
            
                
                trattamento.IdZona = Convert.ToInt32(_dr["idZona"]);
            }

            return trattamento;
        }
        [HttpPost]
        public IEnumerable<trattamentiModel> getAlltrattamentiZone([FromBody] dynamic data)
        {
            int idZona = data.idZona;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idZona", idZona);
            _cmd.CommandText = "select * from trattamenti inner join fitofarmacoUtilizzato on trattamenti.idTrattamento=fitofarmacoUtilizzato.idTrattamento where idZona=@idZona";
            _dr = _cmd.ExecuteReader();
            _lstTrattamento = new List<trattamentiModel>();
            while (_dr.Read())
            {
                trattamento = new trattamentiModel();
                trattamento.IdTrattamento = Convert.ToInt32(_dr["idTrattamento"]);
                trattamento.Data = Convert.ToDateTime(_dr["data"]);
                trattamento.Nome = Convert.ToString(_dr["nome"]);
             
                trattamento.IdZona = Convert.ToInt32(_dr["idZona"]);

                _lstTrattamento.Add(trattamento);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstTrattamento;
        }
        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idTrattamento = data.idTrattamento;
                DateTime dataTrattamento = data.dataTrattamento;
               
                int idZona = data.idZona;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idTrattamento", idTrattamento);
                _cmd.Parameters.AddWithValue("@dataTrattamento", dataTrattamento);
     
           
                _cmd.Parameters.AddWithValue("@idZona", idZona);


                _cmd.CommandText = "INSERT into trattamenti (idTrattamento, data, idZona) values (@idTrattamento, @dataTrattamento, @idZona)";
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
                int idTrattamento = data.id;
                DateTime dataTrattamento = data.dataTrattamento;
                string quantità = data.quantità;
             
                int idZona = data.idZona;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idTrattamento", idTrattamento);
                _cmd.Parameters.AddWithValue("@dataTrattamento", dataTrattamento);
          
             
                _cmd.Parameters.AddWithValue("@idZona", idZona);

                _cmd.CommandText = "UPDATE trattamenti set data=@dataTrattamento,idZona=@idZona where idTrattamento=@idTrattamento  ";
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
                int idTrattamento = data.idTrattamento;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idTrattamento", idTrattamento);
                _cmd.CommandText = "DELETE from trattamenti where idTrattamento=@idTrattamento ";
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
            _cmd.CommandText = "select top 1 idTrattamento from trattamenti order by idTrattamento desc";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {

                id = Convert.ToInt32(_dr["idTrattamento"]);
            }

            return id;
        }
    }
}