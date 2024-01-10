using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class serraModel
    {
        private int idSerra;
        private int numPiante;
        private bool disinfettata;
        private bool verduraPresentePrima;
        private int kgTotaliRaccolti;
        private int idZona;
        private int annoNylon;
        private int annoGomme;

        public int IdSerra { get => idSerra; set => idSerra = value; }
        public int NumPiante { get => numPiante; set => numPiante = value; }
        public bool Disinfettata { get => disinfettata; set => disinfettata = value; }
        public bool VerduraPresentePrima { get => verduraPresentePrima; set => verduraPresentePrima = value; }
        public int KgTotaliRaccolti { get => kgTotaliRaccolti; set => kgTotaliRaccolti = value; }
        public int IdZona { get => idZona; set => idZona = value; }
        public int AnnoNylon { get => annoNylon; set => annoNylon = value; }
        public int AnnoGomme { get => annoGomme; set => annoGomme = value; }
    }
}