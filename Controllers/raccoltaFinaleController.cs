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
    public class raccoltaFinaleController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private raccoltaFinaleModel raccoltaFinale;
        private List<raccoltaFinaleModel> _lstRaccoltaFinale;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<raccoltaFinaleModel> getAllRaccolteFinali()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from raccoltaFinale ";
            _dr = _cmd.ExecuteReader();
            _lstRaccoltaFinale = new List<raccoltaFinaleModel>();
            while (_dr.Read())
            {
                raccoltaFinale = new raccoltaFinaleModel();
                raccoltaFinale.IdRaccoltaFinale = Convert.ToInt32(_dr["idRaccoltaFinale"]);
                raccoltaFinale.Data = Convert.ToDateTime(_dr["data"]);
                raccoltaFinale.Quantita = Convert.ToInt32(_dr["quantita"]);
                raccoltaFinale.NumZone = Convert.ToInt32(_dr["numZone"]);
                raccoltaFinale.Conclusa = Convert.ToBoolean(_dr["conclusa"]);


                _lstRaccoltaFinale.Add(raccoltaFinale);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstRaccoltaFinale;
        }

        public raccoltaFinaleModel getRaccoltaFinali([FromBody] dynamic data)
        {
            int idRaccoltaFinale = data.idRaccoltaFinale;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idRaccoltaFinale", idRaccoltaFinale);
            _cmd.CommandText = "select * from raccoltaFinale where idRaccoltaFinale=@idRaccoltaFinale";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {
                raccoltaFinale = new raccoltaFinaleModel();
                raccoltaFinale.IdRaccoltaFinale = Convert.ToInt32(_dr["idRaccoltaFinale"]);
                raccoltaFinale.Data = Convert.ToDateTime(_dr["data"]);
                raccoltaFinale.Quantita = Convert.ToInt32(_dr["quantita"]);
                raccoltaFinale.NumZone = Convert.ToInt32(_dr["numZone"]);
                raccoltaFinale.Conclusa = Convert.ToBoolean(_dr["conclusa"]);
            }

            return raccoltaFinale;
        }

        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idRaccoltaFinale = data.idRaccoltaFinale;
                DateTime dataRaccoltaFinale = data.dataRaccoltaFinale;
                int quantita = data.quantita;
                int numZone = data.numZone;
                bool conclusa = data.conclusa;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idRaccoltaFinale", idRaccoltaFinale);
                _cmd.Parameters.AddWithValue("@dataRaccoltaFinale", dataRaccoltaFinale);
                _cmd.Parameters.AddWithValue("@quantita", quantita);
                _cmd.Parameters.AddWithValue("@numZone", numZone);
                _cmd.Parameters.AddWithValue("@conclusa", conclusa);
               



                _cmd.CommandText = "INSERT into raccoltaFinale (idRaccoltaFinale, dataRaccoltaFinale, quantita, numZone,conclusa) values (@idRaccoltaFinale, @dataRaccoltaFinale, @quantita, @numZone, @conclusa)";
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
                int idRaccoltaFinale = data.idRaccoltaFinale;
                DateTime dataRaccoltaFinale = data.dataRaccoltaFinale;
                int quantita = data.quantita;
                int numZone = data.numZone;
                bool conclusa = data.conclusa;


                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idRaccoltaFinale", idRaccoltaFinale);
                _cmd.Parameters.AddWithValue("@dataRaccoltaFinale", dataRaccoltaFinale);
                _cmd.Parameters.AddWithValue("@quantita", quantita);
                _cmd.Parameters.AddWithValue("@numZone", numZone);
                _cmd.Parameters.AddWithValue("@conclusa", conclusa);

                _cmd.CommandText = "UPDATE raccoltaFinale set dataRaccoltaFinale=@dataRaccoltaFinale,quantita=@quantita,numZone=@numZone, conclusa=@conclusa where idRaccoltaFinale=@idRaccoltaFinale  ";
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
                int idRaccoltaFinale = data.idRaccoltaFinale;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idRaccoltaFinale", idRaccoltaFinale);
                _cmd.CommandText = "DELETE from raccoltaFinale where idRaccoltaFinale=@idRaccoltaFinale ";
                _cmd.ExecuteNonQuery();
                return ("eliminazione avvenuto con successo");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }
        [HttpPost]
        public IEnumerable<raccoltaFinaleModel> getRaccoltaFinaliNonConcluse([FromBody] dynamic data)
        {
           
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _lstRaccoltaFinale = new List<raccoltaFinaleModel>();
            _cmd.CommandText = "select * from raccoltaFinale where conclusa='false'";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {
                raccoltaFinale = new raccoltaFinaleModel();
                raccoltaFinale.IdRaccoltaFinale = Convert.ToInt32(_dr["idRaccoltaFinale"]);
                raccoltaFinale.Data = Convert.ToDateTime(_dr["data"]);
                raccoltaFinale.Quantita = Convert.ToInt32(_dr["quantita"]);
                raccoltaFinale.NumZone = Convert.ToInt32(_dr["numZone"]);
                raccoltaFinale.Conclusa = Convert.ToBoolean(_dr["conclusa"]);
                _lstRaccoltaFinale.Add(raccoltaFinale);
            }

            return _lstRaccoltaFinale;
        }
    }
}