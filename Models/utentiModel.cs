using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class utentiModel
    {
        private int id;
        private string nome;
        private string cognome;
        private string email;
        private string username;
        private string password;
        private bool operaio;
        private bool capoAzienda;
        private bool amministratoreApp;
        private bool permessoAccesso;

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cognome { get => cognome; set => cognome = value; }
        public string Email { get => email; set => email = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public bool Operaio { get => operaio; set => operaio = value; }
        public bool CapoAzienda { get => capoAzienda; set => capoAzienda = value; }
        public bool AmministratoreApp { get => amministratoreApp; set => amministratoreApp = value; }
        public bool PermessoAccesso { get => permessoAccesso; set => permessoAccesso = value; }
    }
}