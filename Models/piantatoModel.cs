using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class piantatoModel
    {
        private int idPiantato;
        private DateTime data;
        private string varieta;
        private int numPiante;
        private int idZona;

        public int IdPiantato { get => idPiantato; set => idPiantato = value; }
        public DateTime Data { get => data; set => data = value; }
        public string Varieta { get => varieta; set => varieta = value; }
        public int NumPiante { get => numPiante; set => numPiante = value; }
        public int IdZona { get => idZona; set => idZona = value; }
    }
}