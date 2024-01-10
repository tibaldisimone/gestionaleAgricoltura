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
    public class sfemminellaturaController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private sfemminellaturaModel sfemminellatura;
        private esecuzioneSfemminellaturaModel esecuzioneSfemminellatura;
        private List<sfemminellaturaModel> _lstSfemminellatura;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<sfemminellaturaModel> getAllSfemminellature()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from sfemminellatura ";
            _dr = _cmd.ExecuteReader();
            _lstSfemminellatura = new List<sfemminellaturaModel>();
            while (_dr.Read())
            {
                sfemminellatura = new sfemminellaturaModel();
                sfemminellatura.IdSfemminellatura = Convert.ToInt32(_dr["IdSfemminellatura"]);
                sfemminellatura.Data = Convert.ToDateTime(_dr["data"]);
                sfemminellatura.IdZona = Convert.ToInt32(_dr["IdZona"]);


                _lstSfemminellatura.Add(sfemminellatura);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstSfemminellatura;
        }
        [HttpPost]
        public IEnumerable<sfemminellaturaModel> getsfemminellatura([FromBody] dynamic data)
        {
            int idSfemminellatura = data.id;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idSfemminellatura", idSfemminellatura);
            _cmd.CommandText = "select * from sfemminellatura where idSfemminellatura=@idSfemminellatura";
            _lstSfemminellatura = new List<sfemminellaturaModel>();
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {
                sfemminellatura = new sfemminellaturaModel();
                sfemminellatura.IdSfemminellatura = Convert.ToInt32(_dr["idSfemminellatura"]);
                sfemminellatura.Data = Convert.ToDateTime(_dr["data"]);
                sfemminellatura.IdZona = Convert.ToInt32(_dr["idZona"]);
                _lstSfemminellatura.Add(sfemminellatura);

            }

            return _lstSfemminellatura;
        }
        [HttpPost]
        public IEnumerable<sfemminellaturaModel> getAllSfemminellaturaZone([FromBody] dynamic data)
        {
            int idZona = data.idZona;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idZona", idZona);
            _cmd.CommandText = "select * from sfemminellatura inner join esecuzioneSfemminellatura on sfemminellatura.idSfemminellatura=esecuzioneSfemminellatura.idSfemminellatura inner join operai on esecuzioneSfemminellatura.idOperaio=operai.idOperaio where idZona=@idZona";
            _dr = _cmd.ExecuteReader();
            _lstSfemminellatura = new List<sfemminellaturaModel>();
            while (_dr.Read())
            {
                sfemminellatura = new sfemminellaturaModel();
                sfemminellatura.IdSfemminellatura = Convert.ToInt32(_dr["idSfemminellatura"]);
                sfemminellatura.Data = Convert.ToDateTime(_dr["data"]);
                sfemminellatura.IdZona = Convert.ToInt32(_dr["idZona"]);
                sfemminellatura.Nome = Convert.ToString(_dr["nome"]);
                
                _lstSfemminellatura.Add(sfemminellatura);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstSfemminellatura;
        }
        [HttpPost]
        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idSfemminellatura = data.idSfemminellatura;
                DateTime dataSfemminellatura = data.dataSfemminellatura;
                int idZona = data.idZona;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idSfemminellatura", idSfemminellatura);
                _cmd.Parameters.AddWithValue("@dataSfemminellatura", dataSfemminellatura);
                _cmd.Parameters.AddWithValue("@idZona", idZona);
                _cmd.CommandText = "INSERT into sfemminellatura (idSfemminellatura, data, idZona) values (@idSfemminellatura, @dataSfemminellatura, @idZona)";
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
                int idSfemminellatura = data.id;
                DateTime dataSfemminellatura = data.dataSfemminellatura;
                int idZona = data.idZona;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idSfemminellatura", idSfemminellatura);
                _cmd.Parameters.AddWithValue("@dataSfemminellatura", dataSfemminellatura);
                _cmd.Parameters.AddWithValue("@idZona", idZona);

                _cmd.CommandText = "UPDATE sfemminellatura set data=@dataSfemminellatura,idZona=@idZona where idSfemminellatura=@idSfemminellatura  ";
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
                int idSfemminellatura = data.idSfemminellatura;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idSfemminellatura", idSfemminellatura);
                _cmd.CommandText = "DELETE from sfemminellatura where idSfemminellatura=@idSfemminellatura ";
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
            _cmd.CommandText = "select top 1 idSfemminellatura from sfemminellatura order by idSfemminellatura desc";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {

                id = Convert.ToInt32(_dr["idSfemminellatura"]);
            }

            return id;
        }
    }
}