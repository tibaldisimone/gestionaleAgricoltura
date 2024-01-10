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
    public class produzioneFinaleController : ApiController
    {

        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private produzioneFinaleModel produzioneFinale;
        private List<produzioneFinaleModel> _lstProduzioneFinale;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<produzioneFinaleModel> getAllProduzioneFinale()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from produzioneFinale ";
            _dr = _cmd.ExecuteReader();
            _lstProduzioneFinale = new List<produzioneFinaleModel>();
            while (_dr.Read())
            {
                produzioneFinale = new produzioneFinaleModel();
                produzioneFinale.IdProduzioneFinale = Convert.ToInt32(_dr["idProduzioneFinale"]);
                produzioneFinale.KgFinaliVerdi = Convert.ToInt32(_dr["kgFinaliVerdi"]);
                produzioneFinale.ColliFinaliVerde = Convert.ToInt32(_dr["colliFinaliVerde"]);
                produzioneFinale.KgFinaliRossi = Convert.ToInt32(_dr["kgFinaliRossi"]);
                produzioneFinale.ColliFinaliRossi = Convert.ToInt32(_dr["colliFinaliRosse"]);
                produzioneFinale.KgSeconda = Convert.ToInt32(_dr["kgSeconda"]);
                produzioneFinale.ColliFinaliSeconda = Convert.ToInt32(_dr["colliFinaliSeconda"]);
                produzioneFinale.Data = Convert.ToDateTime(_dr["data"]);
                produzioneFinale.IdRaccoltaFinale = Convert.ToInt32(_dr["idRaccoltaFinale"]);
                _lstProduzioneFinale.Add(produzioneFinale);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstProduzioneFinale;
        }

        public produzioneFinaleModel getProduzioneFinale([FromBody] dynamic data)
        {
            int idProduzioneFinale = data.idProduzioneFinale;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idProduzioneFinale", idProduzioneFinale);
            _cmd.CommandText = "select * from produzioneFinale where idProduzioneFinale=@idProduzioneFinale";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {
                produzioneFinale = new produzioneFinaleModel();
                produzioneFinale.IdProduzioneFinale = Convert.ToInt32(_dr["idProduzioneFinale"]);
                produzioneFinale.KgFinaliVerdi = Convert.ToInt32(_dr["kgFinaliVerdi"]);
                produzioneFinale.ColliFinaliVerde = Convert.ToInt32(_dr["colliFinaliVerde"]);
                produzioneFinale.KgFinaliRossi = Convert.ToInt32(_dr["kgFinaliRossi"]);
                produzioneFinale.ColliFinaliRossi = Convert.ToInt32(_dr["colliFinaliRossi"]);
                produzioneFinale.KgSeconda = Convert.ToInt32(_dr["kgSeconda"]);
                produzioneFinale.ColliFinaliSeconda = Convert.ToInt32(_dr["colliFinaliSeconda"]);
                produzioneFinale.Data = Convert.ToDateTime(_dr["data"]);
                produzioneFinale.IdRaccoltaFinale = Convert.ToInt32(_dr["idRaccoltaFinale"]);
            }

            return produzioneFinale;
        }

        public IEnumerable<produzioneFinaleModel> getProduzioneFinaleIdRaccoltaFinale([FromBody] dynamic data)
        {
            int idRaccoltaFinale = data.idRaccoltaFinale;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idRaccoltaFinale", idRaccoltaFinale);
            _cmd.CommandText = "select * from produzioneFinale where idRaccoltaFinale=@idRaccoltaFinale";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {
                produzioneFinale = new produzioneFinaleModel();
                produzioneFinale.IdProduzioneFinale = Convert.ToInt32(_dr["idProduzioneFinale"]);
                produzioneFinale.KgFinaliVerdi = Convert.ToInt32(_dr["kgFinaliVerdi"]);
                produzioneFinale.ColliFinaliVerde = Convert.ToInt32(_dr["colliFinaliVerde"]);
                produzioneFinale.KgFinaliRossi = Convert.ToInt32(_dr["kgFinaliRossi"]);
                produzioneFinale.ColliFinaliRossi = Convert.ToInt32(_dr["colliFinaliRossi"]);
                produzioneFinale.KgSeconda = Convert.ToInt32(_dr["kgSeconda"]);
                produzioneFinale.ColliFinaliSeconda = Convert.ToInt32(_dr["colliFinaliSeconda"]);
                produzioneFinale.Data = Convert.ToDateTime(_dr["data"]);
                produzioneFinale.IdRaccoltaFinale = Convert.ToInt32(_dr["idRaccoltaFinale"]);
                _lstProduzioneFinale.Add(produzioneFinale);
            }

            return _lstProduzioneFinale;
        }

        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idProduzioneFinale = data.idProduzioneFinale;
                int kgFinaliVerdi = data.kgFinaliVerdi;
                int colliFinaliVerde = data.colliFinaliVerde;
                int kgFinaliRossi = data.kgFinaliRossi;
                int colliFinaliRossi = data.colliFinaliRossi;
                int kgSeconda = data.kgSeconda;
                int colliFinaliSeconda = data.colliFinaliSeconda;
                int dataProduzioneFinale = data.dataProduzioneFinale;
                int idRaccoltaFinale = data.idRaccoltaFinale;


                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idProduzioneFinale", idProduzioneFinale);
                _cmd.Parameters.AddWithValue("@kgFinaliVerdi", kgFinaliVerdi);
                _cmd.Parameters.AddWithValue("@colliFinaliVerde", colliFinaliVerde);
                _cmd.Parameters.AddWithValue("@kgFinaliRossi", kgFinaliRossi);
                _cmd.Parameters.AddWithValue("@colliFinaliRossi", colliFinaliRossi);
                _cmd.Parameters.AddWithValue("@kgSeconda", kgSeconda);
                _cmd.Parameters.AddWithValue("@colliFinaliSeconda", colliFinaliSeconda);
                _cmd.Parameters.AddWithValue("@dataProduzioneFinale", dataProduzioneFinale);
                _cmd.Parameters.AddWithValue("@idRaccoltaFinale", idRaccoltaFinale);

                _cmd.CommandText = "INSERT into produzioneFinale (idProduzioneFinale, kgFinaliVerdi, colliFinaliVerde, kgFinaliRossi, colliFinaliRossi,kgSeconda,colliFinaliSeconda,data,idRaccoltaFinale) values (@idProduzioneFinale, @kgFinaliVerdi, @colliFinaliVerde, @kgFinaliRossi, @colliFinaliRossi,@kgSeconda,@colliFinaliSeconda,@dataProduzioneFinale,@idRaccoltaFinale)";
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
                int idProduzioneFinale = data.idProduzioneFinale;
                int kgFinaliVerdi = data.kgFinaliVerdi;
                int colliFinaliVerde = data.colliFinaliVerde;
                int kgFinaliRossi = data.kgFinaliRossi;
                int colliFinaliRossi = data.colliFinaliRossi;
                int kgSeconda = data.kgSeconda;
                int colliFinaliSeconda = data.colliFinaliSeconda;
                int dataProduzioneFinale = data.dataProduzioneFinale;
                int idRaccoltaFinale = data.idRaccoltaFinale;


                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idProduzioneFinale", idProduzioneFinale);
                _cmd.Parameters.AddWithValue("@kgFinaliVerdi", kgFinaliVerdi);
                _cmd.Parameters.AddWithValue("@colliFinaliVerde", colliFinaliVerde);
                _cmd.Parameters.AddWithValue("@kgFinaliRossi", kgFinaliRossi);
                _cmd.Parameters.AddWithValue("@colliFinaliRossi", colliFinaliRossi);
                _cmd.Parameters.AddWithValue("@kgSeconda", kgSeconda);
                _cmd.Parameters.AddWithValue("@colliFinaliSeconda", colliFinaliSeconda);
                _cmd.Parameters.AddWithValue("@dataProduzioneFinale", dataProduzioneFinale);
                _cmd.Parameters.AddWithValue("@idRaccoltaFinale", idRaccoltaFinale);

                _cmd.CommandText = "UPDATE produzioneFinale set idRaccoltaFinale=@idRaccoltaFinale,kgFinaliVerdi=@kgFinaliVerdi,colliFinaliVerde=@colliFinaliVerde,kgFinaliRossi=@kgFinaliRossi,colliFinaliRossi=@colliFinaliRossi,kgSeconda=@kgSeconda,colliFinaliSeconda=@colliFinaliSeconda,data=@dataProduzioneFinale where idProduzioneFinale=@idProduzioneFinale  ";
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
                int idProduzioneFinale = data.idProduzioneFinale;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idProduzioneFinale", idProduzioneFinale);
                _cmd.CommandText = "DELETE from produzioneFinale where idProduzioneFinale=@idProduzioneFinale ";
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