using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class operaiModel
    {

        private int idOperaio;
        private string cognome;
        private string nome;
        private int idUtente;

        public int IdOperaio { get => idOperaio; set => idOperaio = value; }
        public string Cognome { get => cognome; set => cognome = value; }
        public string Nome { get => nome; set => nome = value; }
        public int IdUtente { get => idUtente; set => idUtente = value; }
    }
}