using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class concimeUtilizzatoModel
    {
        private int idConcime;
        private int idConcimazione;
        private string nome;

        public int IdConcime { get => idConcime; set => idConcime = value; }
        public int IdConcimazione { get => idConcimazione; set => idConcimazione = value; }
        public string Nome { get => nome; set => nome = value; }
    }
}