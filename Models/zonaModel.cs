using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class zonaModel
    {
        private int idZona;
        private int numSerre;
        private string nome;

        public int IdZona { get => idZona; set => idZona = value; }
        public int NumSerre { get => numSerre; set => numSerre = value; }
        public string Nome { get => nome; set => nome = value; }
    }
}