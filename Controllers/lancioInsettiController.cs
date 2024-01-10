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
    public class lancioInsettiController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private lancioInsettiModel lancioInsetti;
        private List<lancioInsettiModel> _lstLancioInsetti;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<lancioInsettiModel> getAllLancioInsetti()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from lancioInsetti ";
            _dr = _cmd.ExecuteReader();
            _lstLancioInsetti = new List<lancioInsettiModel>();
            while (_dr.Read())
            {
                lancioInsetti = new lancioInsettiModel();
                lancioInsetti.IdLancioInsetti = Convert.ToInt32(_dr["idLancioInsetti"]);
                lancioInsetti.Data = Convert.ToDateTime(_dr["data"]);
            
                lancioInsetti.IdZona = Convert.ToInt32(_dr["idZona"]);


                _lstLancioInsetti.Add(lancioInsetti);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstLancioInsetti;
        }

        [HttpPost]
        public IEnumerable<lancioInsettiModel> getLancioInsetti([FromBody] dynamic data)
        {
            int idLancioInsetti = data.id;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idLancioInsetti", idLancioInsetti);
            _cmd.CommandText = "select * from lancioInsetti where idLancioInsetti=@idLancioInsetti";
            _dr = _cmd.ExecuteReader();
            _lstLancioInsetti = new List<lancioInsettiModel>();
            while (_dr.Read())
            {
                lancioInsetti = new lancioInsettiModel();
                lancioInsetti.IdLancioInsetti = Convert.ToInt32(_dr["idLancioInsetti"]);
                lancioInsetti.Data = Convert.ToDateTime(_dr["data"]);
            
                lancioInsetti.IdZona = Convert.ToInt32(_dr["idZona"]);
                _lstLancioInsetti.Add(lancioInsetti);
            }

            return _lstLancioInsetti;
        }
        [HttpPost]
        public IEnumerable<lancioInsettiModel> getAlllancioInsettiZone([FromBody] dynamic data)
        {
            int idZona = data.idZona;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idZona", idZona);
            _cmd.CommandText = "select * from lancioInsetti where idZona=@idZona";
            _dr = _cmd.ExecuteReader();
            _lstLancioInsetti = new List<lancioInsettiModel>();
            while (_dr.Read())
            {
                lancioInsetti = new lancioInsettiModel();
                lancioInsetti.IdLancioInsetti = Convert.ToInt32(_dr["idLancioInsetti"]);
                lancioInsetti.Data = Convert.ToDateTime(_dr["data"]);
         
                lancioInsetti.IdZona = Convert.ToInt32(_dr["idZona"]);

                _lstLancioInsetti.Add(lancioInsetti);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstLancioInsetti;
        }

        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idLancioInsetti = data.id;
                string dataLancioInsetti = data.dataLancioInsetti;
               
                int idZona = data.idZona;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idLancioInsetti", idLancioInsetti);
                _cmd.Parameters.AddWithValue("@dataLancioInsetti", dataLancioInsetti);
             
                _cmd.Parameters.AddWithValue("@idZona", idZona);



                _cmd.CommandText = "INSERT into lancioInsetti (idLancioInsetti, data, idZona) values (@idLancioInsetti, @dataLancioInsetti, @idZona)";
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
                int idLancioInsetti = data.id;
                DateTime dataLancioInsetti = data.dataLancioInsetti;
                int quantita = data.quantita;
                int idZona = data.idZona;




                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idLancioInsetti", idLancioInsetti);
                _cmd.Parameters.AddWithValue("@dataLancioInsetti", dataLancioInsetti);
               
                _cmd.Parameters.AddWithValue("@idZona", idZona);

                _cmd.CommandText = "UPDATE lancioInsetti set data=@dataLancioInsetti,idZona=@idZona where idLancioInsetti=@idLancioInsetti  ";
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
                int idLancioInsetti = data.id;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idLancioInsetti", idLancioInsetti);
                _cmd.CommandText = "DELETE from lancioInsetti where idLancioInsetti=@idLancioInsetti ";
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
            _cmd.CommandText = "select top 1 idLancioInsetti from lancioInsetti order by idLancioInsetti desc";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {

                id = Convert.ToInt32(_dr["idLancioInsetti"]);
            }

            return id;
        }
    }
}