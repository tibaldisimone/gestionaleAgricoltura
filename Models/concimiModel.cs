using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class concimiModel
    {
        private int idConcime;
        private string nome;
        private string descrizione;

        public int IdConcime { get => idConcime; set => idConcime = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Descrizione { get => descrizione; set => descrizione = value; }
    }
}