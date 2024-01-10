using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class esecuzioneRaccoltaModel
    {
        private int idRaccolta;
        private int idOperaio;
        private DateTime data;

        public int IdRaccolta { get => idRaccolta; set => idRaccolta = value; }
        public int IdOperaio { get => idOperaio; set => idOperaio = value; }
        public DateTime Data { get => data; set => data = value; }
    }
}