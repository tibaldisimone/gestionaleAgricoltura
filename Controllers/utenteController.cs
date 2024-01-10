using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using esercizioProva.Models;
using System.Web.Http;
using System.Security.Policy;
using Microsoft.Ajax.Utilities;

namespace esercizioProva.Controllers
{
    public class utenteController : ApiController
    {
        private SqlConnection _cn;
        private SqlCommand _cmd;
        private SqlDataReader _dr;
        private utentiModel utenti;
        private List<utentiModel> _lstUtenti;
        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];


        [HttpPost]
        public string insert([FromBody] dynamic data)
        {
            try
            {
                int idUtente = 1;
                string nome = data.nome;
                string cognome = data.cognome;
                string email = data.email;
                string username = data.username;
                string password = data.password;
                bool operaio = false;
                bool capoAzienda = false;
                bool amministratoreApp = false;
                bool permessoAccesso = false;
                if (utenteDisponibile(username) == null)
                {

                    _cn = new SqlConnection(_connectionString);
                    _cn.Open();
                    _cmd = new SqlCommand();
                    _cmd.Connection = _cn;
                    _cmd.CommandType = CommandType.Text;
                    _cmd.Parameters.AddWithValue("@idUtente", idUtente);
                    _cmd.Parameters.AddWithValue("@nome", nome);
                    _cmd.Parameters.AddWithValue("@cognome", cognome);
                    _cmd.Parameters.AddWithValue("@email", email);
                    _cmd.Parameters.AddWithValue("@username", username);
                    _cmd.Parameters.AddWithValue("@password", criptazione(password));
                    _cmd.Parameters.AddWithValue("@operaio", operaio);
                    _cmd.Parameters.AddWithValue("@capoAzienda", capoAzienda);
                    _cmd.Parameters.AddWithValue("@amministratoreApp", amministratoreApp);
                    _cmd.Parameters.AddWithValue("@permessoAccesso", permessoAccesso);
                    _cmd.CommandText = "INSERT into utenti (idUtente, nome, cognome, email, username, password, operaio, capoAzienda, amministratoreApp, permessoAccesso) values (@idUtente, @nome, @cognome, @email, @username, @password, @operaio, @capoAzienda, @amministratoreApp, @permessoAccesso)";
                    _cmd.ExecuteNonQuery();

                    var toAddress = new MailAddress(email, username);


                    var fromAddress = new MailAddress("tibaldiGestionale@gmail.com", "Gestionale Tibaldi");

                    const string fromPassword = "hvfgohfmhkebtuqw";
                    const string subject = "Richiesta cambio password";
                    string body = "Ciao! la tua registrazione è sta confermata, congratulazione! ";


                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };

                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                    return ("Inserimento avvenuto con successo");
                }
                else
                {
                    return ("ATTENZIONE! Username già presente nel database");
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        [HttpPost]
        public string invioEmail([FromBody] dynamic data)
        {

            try
            {
                string email = data.email;
                string username = data.username;
                string check = data.check;

                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                int id = 0;
                _cmd.Parameters.AddWithValue("@email", email);
                _cmd.CommandText = "select idUtente from utenti where email=@email";
                _dr = _cmd.ExecuteReader();
                while (_dr.Read())
                {

                    id = Convert.ToInt32(_dr["idUtente"]);
                }

                if (check == "2")
                {
                    var toAddress = new MailAddress(email, username);
                    const string fromPassword = "hvfgohfmhkebtuqw";
                    const string subject = "Richiesta cambio password";
                    var fromAddress = new MailAddress("tibaldiGestionale@gmail.com", "Gestionale Tibaldi");
                    string url = "http://simonetibaldi-001-site1.dtempurl.com/recuperoPassword.html?var1=" + HttpUtility.UrlEncode(id.ToString()) + "&var2=" + HttpUtility.UrlEncode(criptazione(email));
                    string body = "\"Ciao! E' stato richiesto un cambio password dal tuo account, premi il link per procedere:" + url;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };

                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }
                else
                {
                    var toAddress = new MailAddress(email, username);
                    const string fromPassword = "hvfgohfmhkebtuqw";
                    const string subject = "Richiesta cambio credenziali";
                    var fromAddress = new MailAddress("tibaldiGestionale@gmail.com", "Gestionale Tibaldi");
                    string url = "http://simonetibaldi-001-site1.dtempurl.com/recuperoUsername.html?var1=" + HttpUtility.UrlEncode(id.ToString()) + "&var2=" + HttpUtility.UrlEncode(criptazione(email));
                    string body = "\"Ciao! E' stato richiesto un cambio credenziali dal tuo account, premi il link per procedere:" + url;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };

                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }
                

                return "Mail inviata correttamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string invioEmailAmministratore([FromBody] dynamic data)
        {

            try
            {
              
                string username = data.username;
                string password = data.password;

                
                    var toAddress = new MailAddress("simo2004tibaldi@gmail.com", "simone tibaldi");
                    const string fromPassword = "hvfgohfmhkebtuqw";
                    const string subject = "Richiesta cambio password";
                    var fromAddress = new MailAddress("tibaldiGestionale@gmail.com", "Gestionale Tibaldi");
                   
                    string body = "\"Attenzione! Attività sospetta da queste credenziali:" + "username:" + username ;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };

                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                


                return "Mail inviata correttamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public utentiModel utenteDisponibile(string utente)
        {
 
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@username", utente);

            _cmd.CommandText = "select * from utenti where username=@username";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {
                utenti = new utentiModel();

                utenti.Username = _dr["username"].ToString();


            }
            return utenti;
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
            _cmd.CommandText = "select top 1 idUtente from utenti order by idUtente desc";
            _dr = _cmd.ExecuteReader();
            while (_dr.Read())
            {

                id = Convert.ToInt32(_dr["idUtente"]);
            }

            return id;
        }


        [HttpGet]
        public string ricercaLogin(string p1, string p2)
        {
            bool fatto = false;
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@username", p1);
            _cmd.Parameters.AddWithValue("@password", criptazione(p2));
            _cmd.CommandText = "select * from utenti where username=@username and password=@password";
            _dr = _cmd.ExecuteReader();
            
            while (_dr.Read())
            {
                utenti = new utentiModel();
                utenti.Id = Convert.ToInt32(_dr["idUtente"].ToString());
                utenti.Nome = _dr["nome"].ToString();
                utenti.Cognome = _dr["cognome"].ToString();
                utenti.Email = _dr["email"].ToString();
                utenti.Username = _dr["username"].ToString();
                utenti.Password = _dr["password"].ToString();
                utenti.Operaio = Convert.ToBoolean(_dr["operaio"].ToString());
                utenti.CapoAzienda = Convert.ToBoolean(_dr["capoAzienda"].ToString());
                utenti.AmministratoreApp = Convert.ToBoolean(_dr["amministratoreApp"].ToString());
                utenti.PermessoAccesso = Convert.ToBoolean(_dr["permessoAccesso"].ToString());
                fatto = true;
            }
           
                if (fatto)
                {
                        if (utenti.PermessoAccesso == true)
                        {
                            return "login avvenuto con successo";
                        }
                        else
                        {
                            return "Attenzione, utente non abilitato all'accesso";
                        }
                }
                else
                {
                    return "Attenzione, errore nell'inserimento delle credenziali";
                }
       
            
        }
        [HttpGet]
        public string controlloPagina(string p1, string p2)
        {
            string check = "";
            _cn = new SqlConnection(_connectionString);
            _cn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _cn;
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.AddWithValue("@id", p1);
            _cmd.CommandText = "select email from utenti where idUtente=@id";
            _dr = _cmd.ExecuteReader();
            string email = "";
            while (_dr.Read())
            {
                email = _dr["email"].ToString();
            }
            if (p2 == criptazione(email))
            {
                check = "ok";
                return check;
            }
            else
            {
                check = "nok";
                return check;
            }
        }
        [HttpGet]
        public string aggiornaPassword(string p1, int p2)
        {
            try
            {
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@password", criptazione(p1));
                _cmd.Parameters.AddWithValue("@idUtente", p2);
                _cmd.CommandText = "UPDATE utenti set password=@password where idUtente=@idUtente  ";
                _cmd.ExecuteNonQuery();
                return ("Inserimento avvenuto con successo");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        [HttpPost]
        public string aggiornaCredenziali([FromBody] dynamic data)
        {
            string p1 = data.password;
            int p2 = data.var1;
            string p3 = data.username;
            if (utenteDisponibile(p3) == null)
            {
                try
                {
                    _cn = new SqlConnection(_connectionString);
                    _cn.Open();
                    _cmd = new SqlCommand();
                    _cmd.Connection = _cn;
                    _cmd.CommandType = CommandType.Text;
                    _cmd.Parameters.AddWithValue("@password", criptazione(p1));
                    _cmd.Parameters.AddWithValue("@idUtente", p2);
                    _cmd.Parameters.AddWithValue("@username", p3);
                    _cmd.CommandText = "UPDATE utenti set password=@password,username=@username where idUtente=@idUtente  ";
                    _cmd.ExecuteNonQuery();
                    return ("Inserimento avvenuto con successo");
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                return ("ATTENZIONE! Username già presente nel database");
           
            }



        }

        public string criptazione(string criptato)
        {
            string nomeSHA256;
            SHA256 mySHA256 = SHA256.Create();
            byte[] hashValue = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(criptato));
            StringBuilder builder = new StringBuilder();//è una stringa è una rivoluzione della classe di stringa, la caratteristica
            for (int i = 0; i < hashValue.Length; i++)
            {
                builder.Append(hashValue[i].ToString("x2"));//x2-->trasforma ogni singolo byte in un esadecimale a 2 cifre
            }
            nomeSHA256 = builder.ToString();

            return nomeSHA256;
        }

        public string delete([FromBody] dynamic data)
        {
            try
            {
                int idUtente = data.idUtente;
                _cn = new SqlConnection(_connectionString);
                _cn.Open();
                _cmd = new SqlCommand();
                _cmd.Connection = _cn;
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.AddWithValue("@idUtente", idUtente);
                _cmd.CommandText = "DELETE from utenti where idUtente=@idUtente ";
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