using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using esercizioProva.Models;
using System.Security.Cryptography;
using System.Text;

namespace esercizioProva.Controllers
{
    public class coordinateController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private coordinateModel coordinate;
        private List<coordinateModel> _lstCoordinate;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<coordinateModel> getAllCoordinate()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            
            _cmd.CommandText = "select * from coordinate ";
            _dr = _cmd.ExecuteReader();
            _lstCoordinate = new List<coordinateModel>();
            while (_dr.Read())
            {
                coordinate = new coordinateModel();
                coordinate.IdCoordinate = Convert.ToInt32(_dr["idCoordinate"]);
                coordinate.Lat1 =  Convert.ToSingle(_dr["lat1"]);
                coordinate.Long1 = Convert.ToSingle(_dr["long1"]);
                coordinate.Lat2 = Convert.ToSingle(_dr["lat2"]);
                coordinate.Long2 = Convert.ToSingle(_dr["long2"]);
                coordinate.Lat3 = Convert.ToSingle(_dr["lat3"]);
                coordinate.Long3 = Convert.ToSingle(_dr["long3"]);
                coordinate.Lat4 = Convert.ToSingle(_dr["lat4"]);
                coordinate.Long4 = Convert.ToSingle(_dr["long4"]);
                _lstCoordinate.Add(coordinate);
            }

            _cmd.Dispose();
            _cn.Close();
            _cn.Dispose();
            return _lstCoordinate;
        }

        public string insertCoordinate([FromBody] dynamic data)
        {
            try
            {
                int idCoordinate = data.IdCoordinate;
                float lat1       = data.Lat1;
                float long1      = data.Long1;
                float lat2 = data.Lat2;
                float long2= data.Long2;
                float lat3= data.Lat3;
                float long3= data.Long3;
                float lat4= data.Lat4;
                float long4= data.Long4;

                
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idCoordinate", idCoordinate);
                _cmd.Parameters.AddWithValue("@lat1", lat1);
                _cmd.Parameters.AddWithValue("@long1", long1);
                _cmd.Parameters.AddWithValue("@lat2", lat2);
                _cmd.Parameters.AddWithValue("@long2", long2);
                _cmd.Parameters.AddWithValue("@lat3", lat3);
                _cmd.Parameters.AddWithValue("@long3", long3);
                _cmd.Parameters.AddWithValue("@lat4", lat4);
                _cmd.Parameters.AddWithValue("@long4", long4);
          
                _cmd.CommandText = "INSERT into coordinate (idCoordinate, lat1, long1, lat2, long2,lat3,long3,lat4,long4) values (@idCoordinate, @lat1, @long1, @lat2, @long2,@lat3,@long3,@lat4,@long4)";
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
                int idCoordinate = data.IdCoordinate;
                float lat1 = data.Lat1;
                float long1 = data.Long1;
                float lat2 = data.Lat2;
                float long2 = data.Long2;
                float lat3 = data.Lat3;
                float long3 = data.Long3;
                float lat4 = data.Lat4;
                float long4 = data.Long4;


                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idCoordinate", idCoordinate);
                _cmd.Parameters.AddWithValue("@lat1", lat1);
                _cmd.Parameters.AddWithValue("@long1", long1);
                _cmd.Parameters.AddWithValue("@lat2", lat2);
                _cmd.Parameters.AddWithValue("@long2", long2);
                _cmd.Parameters.AddWithValue("@lat3", lat3);
                _cmd.Parameters.AddWithValue("@long3", long3);
                _cmd.Parameters.AddWithValue("@lat4", lat4);
                _cmd.Parameters.AddWithValue("@long4", long4);

                _cmd.CommandText = "UPDATE coordinate set lat1=@lat1,long1=@long1,lat2=@lat2,long2=@long2,lat3=@lat3,long3=@long3,lat4=@lat4,long4=@long4 where idCoordinate=@idCoordinate  ";
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
                int idCoordinate = data.IdCoordinate;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idCoordinate", idCoordinate);
                _cmd.CommandText = "DELETE from coordinate where idCoordinate=@idCoordinate ";
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