using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class raccoltaModel
    {
        private int idRaccolta;
        private DateTime data;
        private int quantita;
        private int idZona;
        private int idRaccoltaFinale;
        private string cognomeOperaio;

        public int IdRaccolta { get => idRaccolta; set => idRaccolta = value; }
        public DateTime Data { get => data; set => data = value; }
        public int Quantita { get => quantita; set => quantita = value; }
        public int IdZona { get => idZona; set => idZona = value; }
        public int IdRaccoltaFinale { get => idRaccoltaFinale; set => idRaccoltaFinale = value; }
        public string CognomeOperaio { get => cognomeOperaio; set => cognomeOperaio = value; }
    }
}