using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class raccoltaFinaleModel
    {
        private int idRaccoltaFinale;
        private DateTime data;
        private int quantita;
        private int numZone;
        private bool conclusa;

        public int IdRaccoltaFinale { get => idRaccoltaFinale; set => idRaccoltaFinale = value; }
        public DateTime Data { get => data; set => data = value; }
        public int Quantita { get => quantita; set => quantita = value; }
        public int NumZone { get => numZone; set => numZone = value; }
        public bool Conclusa { get => conclusa; set => conclusa = value; }
    }
}