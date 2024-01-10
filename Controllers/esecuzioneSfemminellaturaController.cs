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
    public class esecuzioneSfemminellaturaController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private esecuzioneSfemminellaturaModel esecuzioneSfemminellatura;
        private List<esecuzioneSfemminellaturaModel> _lstEsecuzioneSfemminellatura;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        [HttpPost]
        public IEnumerable<esecuzioneSfemminellaturaModel> getEsecuzioneSfemminellatura([FromBody] dynamic data)
        {
            int idRaccolta = data.id;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idRaccolta", idRaccolta);
            _cmd.CommandText = "select * from esecuzioneRaccolta where idRaccolta=@idRaccolta";
            _dr = _cmd.ExecuteReader();
            _lstEsecuzioneSfemminellatura = new List<esecuzioneSfemminellaturaModel>();
            while (_dr.Read())
            {
                esecuzioneSfemminellatura = new esecuzioneSfemminellaturaModel();
                esecuzioneSfemminellatura.IdSfemminellatura = Convert.ToInt32(_dr["idSfemminellatura"]);
                esecuzioneSfemminellatura.IdOperaio = Convert.ToInt32(_dr["idOperaio"]);
                esecuzioneSfemminellatura.Data = Convert.ToDateTime(_dr["data"]);

                _lstEsecuzioneSfemminellatura.Add(esecuzioneSfemminellatura);
            }

            return _lstEsecuzioneSfemminellatura;
        }

        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idSfemminellatura = data.idSfemminellatura;
                DateTime dataSfemminellatura = data.dataSfemminellatura;
                int idOperaio = data.idOperaio;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idSfemminellatura", idSfemminellatura);
                _cmd.Parameters.AddWithValue("@dataSfemminellatura", dataSfemminellatura);
                _cmd.Parameters.AddWithValue("@idOperaio", idOperaio);



                _cmd.CommandText = "INSERT into esecuzioneSfemminellatura (idSfemminellatura, data, idOperaio) values (@idSfemminellatura, @dataSfemminellatura, @idOperaio)";
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
                int idSfemminellatura = data.idSfemminellatura;
                DateTime dataSfemminellatura = data.dataSfemminellatura;
                int idOperaio = data.idOperaio;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idSfemminellatura", idSfemminellatura);
                _cmd.Parameters.AddWithValue("@dataSfemminellatura", dataSfemminellatura);
                _cmd.Parameters.AddWithValue("@idOperaio", idOperaio);


                _cmd.CommandText = "UPDATE esecuzioneSfemminellatura set data=@dataSfemminellatura where idSfemminellatura=@idSfemminellatura and idOperaio=@idOperaio";
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
                int idOperaio = data.idOperaio;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idSfemminellatura", idSfemminellatura);
                _cmd.Parameters.AddWithValue("@idOperaio", idOperaio);
                _cmd.CommandText = "DELETE from esecuzioneSfemminellatura where idSfemminellatura=@idSfemminellatura and idOperaio=@idOperaio";
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