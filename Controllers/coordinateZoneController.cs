using esercizioProva.Models;
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
    public class coordinateZoneController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private coordinateZoneModel coordinate;
        private List<coordinateZoneModel> _lstCoordinate;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        // GET api/<controller>
        [HttpGet]

        public IEnumerable<coordinateZoneModel> getAllCoordinate()
        {
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;

            _cmd.CommandText = "select * from coordinateZone ";
            _dr = _cmd.ExecuteReader();
            _lstCoordinate = new List<coordinateZoneModel>();
            while (_dr.Read())
            {
                coordinate = new coordinateZoneModel();
                coordinate.IdCoordinate = Convert.ToInt32(_dr["IdCoordinate"]);
                coordinate.Lat1 = Convert.ToSingle(_dr["lat1"]);
                coordinate.Long1 = Convert.ToSingle(_dr["long1"]);
                coordinate.Lat2 = Convert.ToSingle(_dr["lat2"]);
                coordinate.Long2 = Convert.ToSingle(_dr["long2"]);
                coordinate.Lat3 = Convert.ToSingle(_dr["lat3"]);
                coordinate.Long3 = Convert.ToSingle(_dr["long3"]);
                coordinate.Lat4 = Convert.ToSingle(_dr["lat4"]);
                coordinate.Long4 = Convert.ToSingle(_dr["long4"]);
                coordinate.Lat5 = Convert.ToSingle(_dr["lat5"]);
                coordinate.Long5 = Convert.ToSingle(_dr["long5"]);
                coordinate.Lat6 = Convert.ToSingle(_dr["lat6"]);
                coordinate.Long6 = Convert.ToSingle(_dr["long6"]);
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
                float lat1 = data.Lat1;
                float long1 = data.Long1;
                float lat2 = data.Lat2;
                float long2 = data.Long2;
                float lat3 = data.Lat3;
                float long3 = data.Long3;
                float lat4 = data.Lat4;
                float long4 = data.Long4;
                float lat5 = data.Lat5;
                float long5 = data.Long5;
                float lat6 = data.Lat6;
                float long6 = data.Long6;


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
                _cmd.Parameters.AddWithValue("@lat5", lat5);
                _cmd.Parameters.AddWithValue("@long5", long5);
                _cmd.Parameters.AddWithValue("@lat6", lat6);
                _cmd.Parameters.AddWithValue("@long6", long6);

                _cmd.CommandText = "INSERT into coordinateZone (idCoordinate, lat1, long1, lat2, long2,lat3,long3,lat4,long4,lat5,long5,lat6,long6) values (@idCoordinate, @lat1, @long1, @lat2, @long2,@lat3,@long3,@lat4,@long4,@lat5,@long5,@lat6,@long6)";
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
                float lat5 = data.Lat5;
                float long5 = data.Long5;
                float lat6 = data.Lat6;
                float long6 = data.Long6;


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
                _cmd.Parameters.AddWithValue("@lat5", lat5);
                _cmd.Parameters.AddWithValue("@long5", long5);
                _cmd.Parameters.AddWithValue("@lat6", lat6);
                _cmd.Parameters.AddWithValue("@long6", long6);

                _cmd.CommandText = "UPDATE coordinateZone set lat1=@lat1,long1=@long1,lat2=@lat2,long2=@long2,lat3=@lat3,long3=@long3,lat4=@lat4,long4=@long4,lat5=@lat5,long5=@long5,lat6=@lat6,lat6=@lat6 where idCoordinate=@idCoordinate  ";
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
                _cmd.CommandText = "DELETE from coordinateZone where idCoordinate=@idCoordinate ";
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