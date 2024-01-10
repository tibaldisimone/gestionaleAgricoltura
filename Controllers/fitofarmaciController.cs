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
    public class fitofarmaciController : ApiController
    {
        // GET api/<controller>
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private fitofarmaciModel fitofarmaco;
        private List<fitofarmaciModel> _lstFitofarmaci;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<fitofarmaciModel> getAllFitofarmaci()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from fitofarmaci ";
            _dr = _cmd.ExecuteReader();
            _lstFitofarmaci = new List<fitofarmaciModel>();
            while (_dr.Read())
            {
                fitofarmaco = new fitofarmaciModel();
                fitofarmaco.IdFitofarmaco = Convert.ToInt32(_dr["idFitofarmaco"]);
                fitofarmaco.Nome = Convert.ToString(_dr["nome"]);
                fitofarmaco.Descrizione = Convert.ToString(_dr["descrizione"]);
                fitofarmaco.Carenza = Convert.ToInt32(_dr["carenza"]);


                _lstFitofarmaci.Add(fitofarmaco);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstFitofarmaci;
        }

        public fitofarmaciModel getFitofarmaci([FromBody] dynamic data)
        {
            int idFitofarmaco = data.idFitofarmaco;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idFitofarmaco", idFitofarmaco);
            _cmd.CommandText = "select * from fitofarmaci where idFitofarmaco=@idFitofarmaco";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {
                fitofarmaco = new fitofarmaciModel();
                fitofarmaco.IdFitofarmaco = Convert.ToInt32(_dr["idFitofarmaco"]);
                fitofarmaco.Nome = Convert.ToString(_dr["nome"]);
                fitofarmaco.Descrizione = Convert.ToString(_dr["descrizione"]);
                fitofarmaco.Carenza = Convert.ToInt32(_dr["carenza"]);
            }

            return fitofarmaco;
        }
        
        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idFitofarmaco = data.idFitofarmaco;
                string nome = data.nome;
                string descrizione = data.descrizione;
                int carenza = data.carenza;
               


                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idFitofarmaco", idFitofarmaco);
                _cmd.Parameters.AddWithValue("@nome", nome);
                _cmd.Parameters.AddWithValue("@descrizione", descrizione);
                _cmd.Parameters.AddWithValue("@carenza", carenza);
           


                _cmd.CommandText = "INSERT into fitofarmaci (idFitofarmaco, nome, descrizione, carenza) values (@idFitofarmaco, @nome, @descrizione, @carenza)";
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
                int idFitofarmaco = data.idFitofarmaco;
                string nome = data.nome;
                string descrizione = data.descrizione;
                int carenza = data.carenza;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idFitofarmaco", idFitofarmaco);
                _cmd.Parameters.AddWithValue("@nome", nome);
                _cmd.Parameters.AddWithValue("@descrizione", descrizione);
                _cmd.Parameters.AddWithValue("@carenza", carenza);

                _cmd.CommandText = "UPDATE fitofarmaci set nome=@nome,descrizione=@descrizione,carenza=@carenza where idFitofarmaco=@idFitofarmaco  ";
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
                int idFitofarmaco = data.idFitofarmaco;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idFitofarmaco", idFitofarmaco);
                _cmd.CommandText = "DELETE from fitofarmaci where idFitofarmaco=@idFitofarmaco ";
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