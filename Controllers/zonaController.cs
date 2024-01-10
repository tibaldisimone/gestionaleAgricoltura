using esercizioProva.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using esercizioProva.Models;
using System.Security.Policy;

namespace esercizioProva.Controllers
{
    public class zonaController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private zonaModel zona;
        private List<zonaModel> _lstZona;

        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>

        [HttpPost]

        public IEnumerable<zonaModel> getZona([FromBody] dynamic data)
        {
            int idZona = data.idZona;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idZona", idZona);

            _cmd.CommandText = "select * from zone where idZona=@idZona ";
            _dr = _cmd.ExecuteReader();
            _lstZona = new List<zonaModel>();
            while (_dr.Read())
            {
                zona = new zonaModel();
                zona.IdZona = Convert.ToInt32(_dr["idZona"].ToString());
                zona.NumSerre = Convert.ToInt32(_dr["numSerre"].ToString());
                zona.Nome = Convert.ToString(_dr["nome"].ToString());
                
                _lstZona.Add(zona);
            }

            return _lstZona;
        }

        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idZona = data.idZona;
                int numSerre = data.numSerre;
                string nome = data.nome;
                



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idZona", idZona);
                _cmd.Parameters.AddWithValue("@numSerre", numSerre);
                _cmd.Parameters.AddWithValue("@nome", nome);
   
            


                _cmd.CommandText = "INSERT into zone (idZona, numSerre, nome) values (@idZona, @numSerre, @nome)";
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
                int idZona = data.idZona;
                int numSerre = data.numSerre;
                string nome = data.nome;




                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idZona", idZona);
                _cmd.Parameters.AddWithValue("@numSerre", numSerre);
                _cmd.Parameters.AddWithValue("@nome", nome);

                _cmd.CommandText = "UPDATE zone set numSerre=@numSerre,nome=@nome where idZona=@idZona  ";
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
                int idZona = data.idZona;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idZona", idZona);
                _cmd.CommandText = "DELETE from zone where idZona=@idZona ";
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