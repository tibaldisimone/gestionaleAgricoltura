using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class prezziModel
    {

        private int idPrezzo;
        private float prezzoVerdi;
        private float prezzoRosse;
        private float prezzoSeconda;
        private int idProduzioneFinale;

        public int IdPrezzo { get => idPrezzo; set => idPrezzo = value; }
        public float PrezzoVerdi { get => prezzoVerdi; set => prezzoVerdi = value; }
        public float PrezzoRosse { get => prezzoRosse; set => prezzoRosse = value; }
        public float PrezzoSeconda { get => prezzoSeconda; set => prezzoSeconda = value; }
        public int IdProduzioneFinale { get => idProduzioneFinale; set => idProduzioneFinale = value; }
    }
}