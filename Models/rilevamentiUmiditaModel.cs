using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class rilevamentiUmiditaModel
    {
        private int idRilevamentoUmidita;
        private int valoreUmidita;
        private DateTime dataOra;
        private int idZona;

        public int IdRilevamentoUmidita { get => idRilevamentoUmidita; set => idRilevamentoUmidita = value; }
        public int ValoreUmidita { get => valoreUmidita; set => valoreUmidita = value; }
        public DateTime DataOra { get => dataOra; set => dataOra = value; }
        public int IdZona { get => idZona; set => idZona = value; }
    }
}