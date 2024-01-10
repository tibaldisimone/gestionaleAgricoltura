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
    public class prezziController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private prezziModel prezzo;
        private List<prezziModel> _lstPrezzi;

        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>

        [HttpPost]
        public IEnumerable<prezziModel> getAllPrezzi()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from prezzi ";
            _dr = _cmd.ExecuteReader();
            _lstPrezzi = new List<prezziModel>();
            while (_dr.Read())
            {
                prezzo = new prezziModel();
                prezzo.IdPrezzo = Convert.ToInt32(_dr["idPrezzo"]);
                prezzo.PrezzoVerdi = Convert.ToSingle(_dr["prezzoVerdi"]);
                prezzo.PrezzoRosse = Convert.ToSingle(_dr["prezzoRosse"]);
                prezzo.PrezzoSeconda = Convert.ToSingle(_dr["prezzoSeconda"]);
                prezzo.IdProduzioneFinale = Convert.ToInt32(_dr["idProduzioneFinale"]);


                _lstPrezzi.Add(prezzo);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstPrezzi;
        }
        public IEnumerable<prezziModel> getPrezzo([FromBody] dynamic data)
        {
            int idPrezzo = data.idPrezzo;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idPrezzo", idPrezzo);

            _cmd.CommandText = "select * from prezzi where idPrezzo=@idPrezzo ";
            _dr = _cmd.ExecuteReader();
            _lstPrezzi = new List<prezziModel>();
            while (_dr.Read())
            {
                prezzo = new prezziModel();
                prezzo.IdPrezzo = Convert.ToInt32(_dr["idPrezzo"]);
                prezzo.PrezzoVerdi = Convert.ToSingle(_dr["prezzoVerdi"]);
                prezzo.PrezzoRosse = Convert.ToSingle(_dr["prezzoRosse"]);
                prezzo.PrezzoSeconda = Convert.ToSingle(_dr["prezzoSeconda"]);
                prezzo.IdProduzioneFinale = Convert.ToInt32(_dr["idProduzioneFinale"]);
                _lstPrezzi.Add(prezzo);
            }

            return _lstPrezzi;
        }

        public IEnumerable<prezziModel> getAllPrezziProduzioneFinale([FromBody] dynamic data)
        {
            int idPrezzo = data.idPrezzo;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idPrezzo", idPrezzo);
            _cmd.CommandText = "select * from prezzi where idProduzioneFinale=@idProduzioneFinale";
            _dr = _cmd.ExecuteReader();
            _lstPrezzi = new List<prezziModel>();
            while (_dr.Read())
            {
                prezzo = new prezziModel();
                prezzo.IdPrezzo = Convert.ToInt32(_dr["idPrezzo"]);
                prezzo.PrezzoVerdi = Convert.ToSingle(_dr["prezzoVerdi"]);
                prezzo.PrezzoRosse = Convert.ToSingle(_dr["prezzoRosse"]);
                prezzo.PrezzoSeconda = Convert.ToSingle(_dr["prezzoSeconda"]);
                prezzo.IdProduzioneFinale = Convert.ToInt32(_dr["idProduzioneFinale"]);

                _lstPrezzi.Add(prezzo);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstPrezzi;
        }

        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idPrezzo = data.idPrezzo;
                float prezzoVerdi = data.prezzoVerdi;
                float prezzoRosse = data.prezzoRosse;
                float prezzoSeconda = data.prezzoSeconda;
                int idProduzioneFinale = data.idProduzioneFinale;
              



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idPrezzo", idPrezzo);
                _cmd.Parameters.AddWithValue("@prezzoVerdi", prezzoVerdi);
                _cmd.Parameters.AddWithValue("@prezzoRosse", prezzoRosse);
                _cmd.Parameters.AddWithValue("@prezzoSeconda", prezzoSeconda);
                _cmd.Parameters.AddWithValue("@idProduzioneFinale", idProduzioneFinale);
                


                _cmd.CommandText = "INSERT into prezzi (idPrezzo, prezzoVerdi, prezzoRosse, prezzoSeconda,idProduzioneFinale values (@idPrezzo, @prezzoVerdi, @prezzoRosse, @prezzoSeconda,@idProduzioneFinale)";
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
                int idPrezzo = data.idPrezzo;
                float prezzoVerdi = data.prezzoVerdi;
                float prezzoRosse = data.prezzoRosse;
                float prezzoSeconda = data.prezzoSeconda;
                int idProduzioneFinale = data.idProduzioneFinale;




                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idPrezzo", idPrezzo);
                _cmd.Parameters.AddWithValue("@prezzoVerdi", prezzoVerdi);
                _cmd.Parameters.AddWithValue("@prezzoRosse", prezzoRosse);
                _cmd.Parameters.AddWithValue("@prezzoSeconda", prezzoSeconda);
                _cmd.Parameters.AddWithValue("@idProduzioneFinale", idProduzioneFinale);

                _cmd.CommandText = "UPDATE prezzi set prezzoVerdi=@prezzoVerdi,prezzoRosse=@prezzoRosse,prezzoSeconda=@prezzoSeconda,idProduzioneFinale=@idProduzioneFinale where idPrezzo=@idPrezzo  ";
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
                int idPrezzo = data.idPrezzo;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idPrezzo", idPrezzo);
                _cmd.CommandText = "DELETE from prezzi where idPrezzo=@idPrezzo ";
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