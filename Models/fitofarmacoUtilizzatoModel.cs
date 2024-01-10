using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class fitofarmacoUtilizzatoModel
    {
        private int idTrattamento;
        private int idFitofarmaco;
        private string nome;

        public int IdTrattamento { get => idTrattamento; set => idTrattamento = value; }
        public int IdFitofarmaco { get => idFitofarmaco; set => idFitofarmaco = value; }
        public string Nome { get => nome; set => nome = value; }
    }
}