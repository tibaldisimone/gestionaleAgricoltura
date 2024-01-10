using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using esercizioProva.Models;

namespace esercizioProva.Controllers
{
    public class serraController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private serraModel serra;
        private List<serraModel> _lstSerra;

        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>

        [HttpPost]

        public IEnumerable<serraModel> getSerra([FromBody] dynamic data)
        {
            int idSerra = data.idSerra;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idSerra", idSerra);

            _cmd.CommandText = "select * from serra where idSerra=@idSerra ";
            _dr = _cmd.ExecuteReader();
            _lstSerra = new List<serraModel>();
            while (_dr.Read())
            {
                serra = new serraModel();
                serra.IdSerra = Convert.ToInt32(_dr["idSerra"].ToString());
                serra.NumPiante = Convert.ToInt32(_dr["numPiante"].ToString());
                serra.Disinfettata = Convert.ToBoolean(_dr["disinfettata"].ToString()) ;
                serra.VerduraPresentePrima = Convert.ToBoolean(_dr["verduraPresentePrima"].ToString());
                serra.KgTotaliRaccolti = Convert.ToInt32(_dr["kgTotaliRaccolti"].ToString());
                serra.IdZona = Convert.ToInt32(_dr["idZona"].ToString());
                serra.AnnoNylon = Convert.ToInt32(_dr["annoNylon"].ToString());
                serra.AnnoGomme = Convert.ToInt32(_dr["annoGomme"].ToString());
                _lstSerra.Add(serra);
            }

            return _lstSerra;
        }

        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idSerra = data.idSerra;
                int numPiante = data.numPiante;
                bool disinfettata = data.disinfettata;
                bool verduraPresentePrima = data.verduraPresentePrima;
                int kgTotaliRaccolti = data.kgTotaliRaccolti;
                int idZona = data.idZona;
                


                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idSerra", idSerra);
                _cmd.Parameters.AddWithValue("@numPiante", numPiante);
                _cmd.Parameters.AddWithValue("@disinfettata", disinfettata);
                _cmd.Parameters.AddWithValue("@verduraPresentePrima", verduraPresentePrima);
                _cmd.Parameters.AddWithValue("@kgTotaliRaccolti", kgTotaliRaccolti);
                _cmd.Parameters.AddWithValue("@idZona", idZona);


                _cmd.CommandText = "INSERT into serra (idSerra, numPiante, disinfettata, verduraPresentePrima,kgTotaliRaccolti, idZona) values (@idSerra, @numPiante, @disinfettata, @verduraPresentePrima,@kgTotaliRaccolti, @idZona)";
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
                int idSerra = data.idSerra;
                int numPiante = data.numPiante;
                bool disinfettata = data.disinfettata;
                bool verduraPresentePrima = data.verduraPresentePrima;
                int kgTotaliRaccolti = data.kgTotaliRaccolti;
                int idZona = data.idZona;
                
                int annoNylon = data.annoNylon;
                int annoGomme = data.annoGomme;




                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idSerra", idSerra);
                _cmd.Parameters.AddWithValue("@numPiante", numPiante);
                _cmd.Parameters.AddWithValue("@disinfettata", disinfettata);
                _cmd.Parameters.AddWithValue("@verduraPresentePrima", verduraPresentePrima);
                _cmd.Parameters.AddWithValue("@kgTotaliRaccolti", kgTotaliRaccolti);
                _cmd.Parameters.AddWithValue("@idZona", idZona);
                
                _cmd.Parameters.AddWithValue("@annoNylon", annoNylon);
                _cmd.Parameters.AddWithValue("@annoGomme", annoGomme);

                _cmd.CommandText = "UPDATE serra set numPiante=@numPiante,disinfettata=@disinfettata,verduraPresentePrima=@verduraPresentePrima,kgTotaliRaccolti=@kgTotaliRaccolti,idZona=@idZona,  annoNylon=@annoNylon, annoGomme=@annoGomme where idSerra=@idSerra  ";
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
                int idSerra = data.idSerra;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idSerra", idSerra);
                _cmd.CommandText = "DELETE from serra where idSerra=@idSerra ";
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