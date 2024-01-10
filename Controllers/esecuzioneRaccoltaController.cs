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
    public class esecuzioneRaccoltaController : ApiController
    {

        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private esecuzioneRaccoltaModel esecuzioneRaccolta;
        private List<esecuzioneRaccoltaModel> _lstEsecuzioneRaccolta;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        [HttpPost]
        public IEnumerable<esecuzioneRaccoltaModel> getEsecuzioneRaccolta([FromBody] dynamic data)
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
            _lstEsecuzioneRaccolta = new List<esecuzioneRaccoltaModel>();
            while (_dr.Read())
            {
                esecuzioneRaccolta = new esecuzioneRaccoltaModel();
                esecuzioneRaccolta.IdRaccolta = Convert.ToInt32(_dr["idRaccolta"]);
                esecuzioneRaccolta.IdOperaio = Convert.ToInt32(_dr["idOperaio"]);
                esecuzioneRaccolta.Data = Convert.ToDateTime(_dr["data"]);

                _lstEsecuzioneRaccolta.Add(esecuzioneRaccolta);
            }

            return _lstEsecuzioneRaccolta;
        }
        [HttpPost]
        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idRaccolta = data.idRaccolta;
                DateTime dataRaccolta = data.dataRaccolta;
                int idOperaio = data.idOperaio;
             



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idRaccolta", idRaccolta);
                _cmd.Parameters.AddWithValue("@dataRaccolta", dataRaccolta);
                _cmd.Parameters.AddWithValue("@idOperaio", idOperaio);
              


                _cmd.CommandText = "INSERT into esecuzioneRaccolta (idRaccolta, data, idOperaio) values (@idRaccolta, @dataRaccolta, @idOperaio)";
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
                int idRaccolta = data.idRaccolta;
                DateTime dataRaccolta = data.dataRaccolta;
                int idOperaio = data.idOperaio;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idRaccolta", idRaccolta);
                _cmd.Parameters.AddWithValue("@dataRaccolta", dataRaccolta);
                _cmd.Parameters.AddWithValue("@idOperaio", idOperaio);


                _cmd.CommandText = "UPDATE esecuzioneRaccolta set data=@dataRaccolta where idRaccolta=@idRaccolta and idOperaio=@idOperaio  ";
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
                int idRaccolta = data.id;
                int idOperaio = data.idOperaio;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idRaccolta", idRaccolta);
                _cmd.Parameters.AddWithValue("@idOperaio", idOperaio);
                _cmd.CommandText = "DELETE from raccolta where idRaccolta=@idRaccolta and idOperaio=@idOperaio";
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