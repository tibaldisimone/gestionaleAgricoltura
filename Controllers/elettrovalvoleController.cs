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
    public class elettrovalvoleController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private elettrovalvoleModel elettrovalvola;
        private List<elettrovalvoleModel> _lstElettrovalvole;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<elettrovalvoleModel> getAllElettrovalvole()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from elettrovalvole ";
            _dr = _cmd.ExecuteReader();
            _lstElettrovalvole = new List<elettrovalvoleModel>();
            while (_dr.Read())
            {
                elettrovalvola = new elettrovalvoleModel();
                elettrovalvola.IdElettrovalvola = Convert.ToInt32(_dr["idElettrovalvola"]);
                elettrovalvola.OraInizioMattina = Convert.ToSingle(_dr["oraInizioMattina"]);
                elettrovalvola.OraFineMattina = Convert.ToSingle(_dr["oraFineMattina"]);
                elettrovalvola.OraInizioSera = Convert.ToSingle(_dr["oraInizioSera"]);
                elettrovalvola.OraFineSera = Convert.ToSingle(_dr["oraFineSera"]);
                elettrovalvola.Durata = Convert.ToInt32(_dr["durata"]);
                elettrovalvola.Giorni = Convert.ToString(_dr["giorni"]);
                elettrovalvola.IdZona = Convert.ToInt32(_dr["idZona"]);

                _lstElettrovalvole.Add(elettrovalvola);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstElettrovalvole;
        }
        public elettrovalvoleModel getElettrovalvola([FromBody] dynamic data)
        {
            int idElettrovalvola = data.idElettrovalvola;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idElettrovalvola", idElettrovalvola);
            _cmd.CommandText = "select * from elettrovalvole where idElettrovalvola=@idElettrovalvola";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {
                elettrovalvola = new elettrovalvoleModel();
                elettrovalvola.IdElettrovalvola = Convert.ToInt32(_dr["idElettrovalvola"]);
                elettrovalvola.OraInizioMattina = Convert.ToSingle(_dr["oraInizioMattina"]);
                elettrovalvola.OraFineMattina = Convert.ToSingle(_dr["oraFineMattina"]);
                elettrovalvola.OraInizioSera = Convert.ToSingle(_dr["oraInizioSera"]);
                elettrovalvola.OraFineSera = Convert.ToSingle(_dr["oraFineSera"]);
                elettrovalvola.Durata = Convert.ToInt32(_dr["durata"]);
                elettrovalvola.Giorni = Convert.ToString(_dr["giorni"]);
                elettrovalvola.IdZona = Convert.ToInt32(_dr["idZona"]);
            }

            return elettrovalvola;
        }
        public IEnumerable<elettrovalvoleModel> getAllElettrovalvole([FromBody] dynamic data)
        {
            int idElettrovalvola = data.idElettrovalvola;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idElettrovalvola", idElettrovalvola);
            _cmd.CommandText = "select * from elettrovalvole where idElettrovalvola=@idElettrovalvola";
            _dr = _cmd.ExecuteReader();
            _lstElettrovalvole = new List<elettrovalvoleModel>();
            while (_dr.Read())
            {
                elettrovalvola = new elettrovalvoleModel();
                elettrovalvola.IdElettrovalvola = Convert.ToInt32(_dr["idElettrovalvola"]);
                elettrovalvola.OraInizioMattina = Convert.ToSingle(_dr["oraInizioMattina"]);
                elettrovalvola.OraFineMattina = Convert.ToSingle(_dr["oraFineMattina"]);
                elettrovalvola.OraInizioSera = Convert.ToSingle(_dr["oraInizioSera"]);
                elettrovalvola.OraFineSera = Convert.ToSingle(_dr["oraFineSera"]);
                elettrovalvola.Durata = Convert.ToInt32(_dr["durata"]);
                elettrovalvola.Giorni = Convert.ToString(_dr["giorni"]);
                elettrovalvola.IdZona = Convert.ToInt32(_dr["idZona"]);

                _lstElettrovalvole.Add(elettrovalvola);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstElettrovalvole;
        }
        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idElettrovalvola = data.idElettrovalvola;
                float oraInizioMattina = data.oraInizioMattina;
                float oraFineMattina = data.oraFineMattina;
                float oraInizioSera = data.oraInizioSera;
                float oraFineSera = data.oraFineSera;
                int durata = data.durata;
                string giorni = data.giorni;
                int idZona = data.idZona;
                

                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idElettrovalvola", idElettrovalvola);
                _cmd.Parameters.AddWithValue("@oraInizioMattina", oraInizioMattina);
                _cmd.Parameters.AddWithValue("@oraFineMattina", oraFineMattina);
                _cmd.Parameters.AddWithValue("@oraInizioSera", oraInizioSera);
                _cmd.Parameters.AddWithValue("@oraFineSera", oraFineSera);
                _cmd.Parameters.AddWithValue("@durata", durata);
                _cmd.Parameters.AddWithValue("@giorni", giorni);
                _cmd.Parameters.AddWithValue("@idZona", idZona);
            

                _cmd.CommandText = "INSERT into elettrovalvole (idElettrovalvola, oraInizioMattina, oraFineMattina, oraInizioSera, oraFineSera,durata,giorni,idZona) values (@idElettrovalvola, @oraInizioMattina, @oraFineMattina, @oraInizioSera, @oraFineSera,@durata,@giorni,@idZona)";
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
                int idElettrovalvola = data.idElettrovalvola;
                float oraInizioMattina = data.oraInizioMattina;
                float oraFineMattina = data.oraFineMattina;
                float oraInizioSera = data.oraInizioSera;
                float oraFineSera = data.oraFineSera;
                int durata = data.durata;
                string giorni = data.giorni;
                int idZona = data.idZona;


                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idElettrovalvola", idElettrovalvola);
                _cmd.Parameters.AddWithValue("@oraInizioMattina", oraInizioMattina);
                _cmd.Parameters.AddWithValue("@oraFineMattina", oraFineMattina);
                _cmd.Parameters.AddWithValue("@oraInizioSera", oraInizioSera);
                _cmd.Parameters.AddWithValue("@oraFineSera", oraFineSera);
                _cmd.Parameters.AddWithValue("@durata", durata);
                _cmd.Parameters.AddWithValue("@giorni", giorni);
                _cmd.Parameters.AddWithValue("@idZona", idZona);

                _cmd.CommandText = "UPDATE elettrovalvole set oraInizioMattina=@oraInizioMattina,oraFineMattina=@oraFineMattina,oraInizioSera=@oraInizioSera,oraFineSera=@oraFineSera,durata=@durata,giorni=@giorni,idZona=@idZona where idElettrovalvola=@idElettrovalvola  ";
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
                int idElettrovalvola = data.idElettrovalvola;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idElettrovalvola", idElettrovalvola);
                _cmd.CommandText = "DELETE from elettrovalvole where idElettrovalvola=@idElettrovalvola ";
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