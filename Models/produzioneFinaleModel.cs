using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class produzioneFinaleModel
    {

        private int idProduzioneFinale;
        private int kgFinaliVerdi;
        private int colliFinaliVerde;
        private int kgFinaliRossi;
        private int colliFinaliRossi;
        private int kgSeconda;
        private int colliFinaliSeconda;
        private DateTime data;
        private int idRaccoltaFinale;

        public int IdProduzioneFinale { get => idProduzioneFinale; set => idProduzioneFinale = value; }
        public int KgFinaliVerdi { get => kgFinaliVerdi; set => kgFinaliVerdi = value; }
        public int ColliFinaliVerde { get => colliFinaliVerde; set => colliFinaliVerde = value; }
        public int KgFinaliRossi { get => kgFinaliRossi; set => kgFinaliRossi = value; }
        public int ColliFinaliRossi { get => colliFinaliRossi; set => colliFinaliRossi = value; }
        public int KgSeconda { get => kgSeconda; set => kgSeconda = value; }
        public int ColliFinaliSeconda { get => colliFinaliSeconda; set => colliFinaliSeconda = value; }
        public DateTime Data { get => data; set => data = value; }
        public int IdRaccoltaFinale { get => idRaccoltaFinale; set => idRaccoltaFinale = value; }
    }
}