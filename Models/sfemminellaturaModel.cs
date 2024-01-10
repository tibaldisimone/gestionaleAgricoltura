using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class sfemminellaturaModel
    {

        private int idSfemminellatura;
        private DateTime data;
        private int idZona;

        private string nome;

        public int IdSfemminellatura { get => idSfemminellatura; set => idSfemminellatura = value; }
        public DateTime Data { get => data; set => data = value; }
        public int IdZona { get => idZona; set => idZona = value; }
        public string Nome { get => nome; set => nome = value; }
    }
}