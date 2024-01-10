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
    public class oreOperaiController : ApiController
    {
        // GET api/<controller>
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private oreOperaiModel oreOperaio;
        private List<oreOperaiModel> _lstOreOperai;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<oreOperaiModel> getAllOreOperai()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from oreOperai ";
            _dr = _cmd.ExecuteReader();
            _lstOreOperai = new List<oreOperaiModel>();
            while (_dr.Read())
            {
                oreOperaio = new oreOperaiModel();
                oreOperaio.IdOre = Convert.ToInt32(_dr["idOre"]);
                oreOperaio.NumOreLavorate = Convert.ToInt32(_dr["numOreLavorate"]);
                oreOperaio.Data = Convert.ToDateTime(_dr["data"]);
                oreOperaio.IdOperaio = Convert.ToInt32(_dr["idOperaio"]);


                _lstOreOperai.Add(oreOperaio);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstOreOperai;
        }

        public oreOperaiModel getOreOperaio([FromBody] dynamic data)
        {
            int idOre = data.idOre;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idOre", idOre);
            _cmd.CommandText = "select * from oreOperai where idOre=@idOre";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {
                oreOperaio = new oreOperaiModel();
                oreOperaio.IdOre = Convert.ToInt32(_dr["idOre"]);
                oreOperaio.NumOreLavorate = Convert.ToInt32(_dr["numOreLavorate"]);
                oreOperaio.Data = Convert.ToDateTime(_dr["data"]);
                oreOperaio.IdOperaio = Convert.ToInt32(_dr["idOperaio"]);
            }

            return oreOperaio;
        }
        [HttpPost]
        public IEnumerable<oreOperaiModel> getAllOreOperaiIdOperaio([FromBody] dynamic data)
        {
            int idOperaio = data.idOperaio;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idOperaio", idOperaio);
            _cmd.CommandText = "select * from oreOperai where idOperaio=@idOperaio";
            _dr = _cmd.ExecuteReader();
            _lstOreOperai = new List<oreOperaiModel>();
            while (_dr.Read())
            {
                oreOperaio = new oreOperaiModel();
                oreOperaio.IdOre = Convert.ToInt32(_dr["idOre"]);
                oreOperaio.NumOreLavorate = Convert.ToInt32(_dr["numOreLavorate"]);
                oreOperaio.Data = Convert.ToDateTime(_dr["data"]);
                oreOperaio.IdOperaio = Convert.ToInt32(_dr["idOperaio"]);

                _lstOreOperai.Add(oreOperaio);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstOreOperai;
        }
        [HttpPost]
        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idOre = data.idOre;
                string numOreLavorate = data.numOreLavorate;
                string dataOraOperaio = data.dataOraOperaio;
                int idOperaio = data.idOperaio;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idOre", idOre);
                _cmd.Parameters.AddWithValue("@numOreLavorate", numOreLavorate);
                _cmd.Parameters.AddWithValue("@dataOraOperaio", dataOraOperaio);
                _cmd.Parameters.AddWithValue("@idOperaio", idOperaio);



                _cmd.CommandText = "INSERT into oreOperai (idOre, numOreLavorate, data, idOperaio) values (@idOre, @numOreLavorate, @dataOraOperaio, @idOperaio)";
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
                int idOre = data.idOre;
                string numOre = data.numOre;
                string dataOraOperaio = data.dataOraOperaio;
                int idOperaio = data.idOperaio;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idOre", idOre);
                _cmd.Parameters.AddWithValue("@numOre", numOre);
                _cmd.Parameters.AddWithValue("@dataOraOperaio", dataOraOperaio);
                _cmd.Parameters.AddWithValue("@idOperaio", idOperaio);

                _cmd.CommandText = "UPDATE oreOperai set numOreLavorate=@numOre,data=@dataOraOperaio,idOperaio=@idOperaio where idOre=@idOre  ";
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
                int idOre = data.idOre;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idOre", idOre);
                _cmd.CommandText = "DELETE from oreOperai where idOre=@idOre ";
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
            _cmd.CommandText = "select top 1 idOre from oreOperai order by idOre desc";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {

                id = Convert.ToInt32(_dr["idOre"]);
            }

            return id;
        }
    }
}