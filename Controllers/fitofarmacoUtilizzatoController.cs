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
    public class fitofarmacoUtilizzatoController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private fitofarmacoUtilizzatoModel fitofarmacoUtilizzato;
        private List<fitofarmacoUtilizzatoModel> _lstFitofarmacoUtilizzato;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        [HttpPost]
        public IEnumerable<fitofarmacoUtilizzatoModel> getFitofarmacoUtilizzato([FromBody] dynamic data)
        {
            int idTrattamento = data.idTrattamento;
            int idFitofarmaco = data.idFitofarmaco;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idTrattamento", idTrattamento);
            _cmd.Parameters.AddWithValue("@idFitofarmaco", idFitofarmaco);
            _cmd.CommandText = "select * from fitofarmacoUtilizzato where idFitofarmaco=@idFitofarmaco and idTrattamento=@idTrattamento";
            _dr = _cmd.ExecuteReader();
            _lstFitofarmacoUtilizzato = new List<fitofarmacoUtilizzatoModel>();
            while (_dr.Read())
            {
                fitofarmacoUtilizzato = new fitofarmacoUtilizzatoModel();
                fitofarmacoUtilizzato.IdFitofarmaco = Convert.ToInt32(_dr["idFitofarmaco"]);
                fitofarmacoUtilizzato.IdTrattamento = Convert.ToInt32(_dr["idTrattamento"]);
                fitofarmacoUtilizzato.Nome = Convert.ToString(_dr["nome"]);

                _lstFitofarmacoUtilizzato.Add(fitofarmacoUtilizzato);
            }

            return _lstFitofarmacoUtilizzato;
        }
        public IEnumerable<fitofarmacoUtilizzatoModel> getFitofarmaciUtilizzati([FromBody] dynamic data)
        {
            int idTrattamento = data.idTrattamento;
            int idFitofarmaco = data.idFitofarmaco;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@idTrattamento", idTrattamento);
            _cmd.Parameters.AddWithValue("@idFitofarmaco", idFitofarmaco);
            _cmd.CommandText = "select * from fitofarmacoUtilizzato where  idTrattamento=@idTrattamento";
            _dr = _cmd.ExecuteReader();
            _lstFitofarmacoUtilizzato = new List<fitofarmacoUtilizzatoModel>();
            while (_dr.Read())
            {
                fitofarmacoUtilizzato = new fitofarmacoUtilizzatoModel();
                fitofarmacoUtilizzato.IdFitofarmaco = Convert.ToInt32(_dr["idFitofarmaco"]);
                fitofarmacoUtilizzato.IdTrattamento = Convert.ToInt32(_dr["idTrattamento"]);
                fitofarmacoUtilizzato.Nome = Convert.ToString(_dr["nome"]);

                _lstFitofarmacoUtilizzato.Add(fitofarmacoUtilizzato);
            }

            return _lstFitofarmacoUtilizzato;
        }

        [HttpPost]
        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idTrattamento = data.idTrattamento;
                int idFitofarmaco = data.idFitofarmaco;
                string nome = data.nome;




                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idTrattamento", idTrattamento);
                _cmd.Parameters.AddWithValue("@idFitofarmaco", idFitofarmaco);
                _cmd.Parameters.AddWithValue("@nome", nome);



                _cmd.CommandText = "INSERT into fitofarmacoUtilizzato (idTrattamento, idFitofarmaco, nome) values (@idTrattamento, @idFitofarmaco, @nome)";
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
                int idTrattamento = data.idTrattamento;
                int idFitofarmaco = data.idFitofarmaco;
                string nome = data.nome;



                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idTrattamento", idTrattamento);
                _cmd.Parameters.AddWithValue("@idFitofarmaco", idFitofarmaco);
                _cmd.Parameters.AddWithValue("@nome", nome);


                _cmd.CommandText = "UPDATE fitofarmacoUtilizzato set nome=@nome where idTrattamento=@idTrattamento and idFitofarmaco=@idFitofarmaco  ";
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
                int idTrattamento = data.idTrattamento;
                int idFitofarmaco = data.idFitofarmaco;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idTrattamento", idTrattamento);
                _cmd.Parameters.AddWithValue("@idFitofarmaco", idFitofarmaco);
                _cmd.CommandText = "DELETE from fitofarmacoUtilizzato where idTrattamento=@idTrattamento and idFitofarmaco=@idFitofarmaco";
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