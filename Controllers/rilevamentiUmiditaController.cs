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
    public class rilevamentiUmiditaController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private rilevamentiUmiditaModel rilevamentiUmidita;
        private List<rilevamentiUmiditaModel> _lstRilevamentiUmidita;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<rilevamentiUmiditaModel> getAllRilevamentiUmidita()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from rilevamentiUmidita ";
            _dr = _cmd.ExecuteReader();
            _lstRilevamentiUmidita = new List<rilevamentiUmiditaModel>();
            while (_dr.Read())
            {
                rilevamentiUmidita = new rilevamentiUmiditaModel();
                rilevamentiUmidita.IdRilevamentoUmidita = Convert.ToInt32(_dr["idRilevamentoUmidita"]);
                rilevamentiUmidita.ValoreUmidita = Convert.ToInt32(_dr["valoreUmidita"]);
                rilevamentiUmidita.DataOra = Convert.ToDateTime(_dr["dataOra"]);
                rilevamentiUmidita.IdZona = Convert.ToInt32(_dr["idZona"]);


                _lstRilevamentiUmidita.Add(rilevamentiUmidita);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstRilevamentiUmidita;
        }
        [HttpPost]
        public IEnumerable<rilevamentiUmiditaModel> getrilevamentiUmidita([FromBody] dynamic data)
        {
            int idRilevamentoUmidita = data.id;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idRilevamentoUmidita", idRilevamentoUmidita);
            _cmd.CommandText = "select * from rilevamentiUmidita where idRilevamentoUmidita=@idRilevamentoUmidita";
            _dr = _cmd.ExecuteReader();
            _lstRilevamentiUmidita = new List<rilevamentiUmiditaModel>();
            while (_dr.Read())
            {
                rilevamentiUmidita = new rilevamentiUmiditaModel();
                rilevamentiUmidita.IdRilevamentoUmidita = Convert.ToInt32(_dr["idRilevamentoUmidita"]);
                rilevamentiUmidita.ValoreUmidita = Convert.ToInt32(_dr["valoreUmidita"]);
                rilevamentiUmidita.DataOra = Convert.ToDateTime(_dr["dataOra"]);
                rilevamentiUmidita.IdZona = Convert.ToInt32(_dr["idZona"]);
                _lstRilevamentiUmidita.Add(rilevamentiUmidita);
            }

            return _lstRilevamentiUmidita;
        }
        [HttpPost]
        public IEnumerable<rilevamentiUmiditaModel> getAllrilevamentiUmiditaZone([FromBody] dynamic data)
        {
            int idZona = data.idZona;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idZona", idZona);
            _cmd.CommandText = "select * from rilevamentiUmidita where idZona=@idZona";
            _dr = _cmd.ExecuteReader();
            _lstRilevamentiUmidita = new List<rilevamentiUmiditaModel>();
            while (_dr.Read())
            {
                rilevamentiUmidita = new rilevamentiUmiditaModel();
                rilevamentiUmidita.IdRilevamentoUmidita = Convert.ToInt32(_dr["idRilevamentoUmidita"]);
                rilevamentiUmidita.ValoreUmidita = Convert.ToInt32(_dr["valoreUmidita"]);
                rilevamentiUmidita.DataOra = Convert.ToDateTime(_dr["dataOra"]);
                rilevamentiUmidita.IdZona = Convert.ToInt32(_dr["idZona"]);


                _lstRilevamentiUmidita.Add(rilevamentiUmidita);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstRilevamentiUmidita;
        }

        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idRilevamentoUmidita = data.idRilevamentoUmidita;
                int valoreUmidita = data.valoreUmidita;
                DateTime dataOra = data.dataOra;
                int idZona = data.idZona;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idRilevamentoUmidita", idRilevamentoUmidita);
                _cmd.Parameters.AddWithValue("@valoreUmidita", valoreUmidita);
                _cmd.Parameters.AddWithValue("@dataOra", dataOra);
                _cmd.Parameters.AddWithValue("@idZona", idZona);



                _cmd.CommandText = "INSERT into rilevamentiUmidita (idRilevamentoUmidita, valoreUmidita, dataOra, idZona) values (@idRilevamentoUmidita, @valoreUmidita, @dataOra, @idZona)";
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
                int idRilevamentoUmidita = data.id;
                int valoreUmidita = data.valoreUmidita;
                DateTime dataOra = data.dataOra;
                int idZona = data.idZona;




                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idRilevamentoUmidita", idRilevamentoUmidita);
                _cmd.Parameters.AddWithValue("@valoreUmidita", valoreUmidita);
                _cmd.Parameters.AddWithValue("@dataOra", dataOra);
                _cmd.Parameters.AddWithValue("@idZona", idZona);

                _cmd.CommandText = "UPDATE rilevamentiUmidita set valoreUmidita=@valoreUmidita,dataOra=@dataOra,idZona=@idZona where idRilevamentoUmidita=@idRilevamentoUmidita  ";
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
                int idRilevamentoUmidita = data.idRilevamentoUmidita;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idRilevamentoUmidita", idRilevamentoUmidita);
                _cmd.CommandText = "DELETE from rilevamentiUmidita where idRilevamentoUmidita=@idRilevamentoUmidita ";
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