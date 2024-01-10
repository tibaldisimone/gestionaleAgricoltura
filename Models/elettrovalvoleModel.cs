using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class elettrovalvoleModel
    {
        private int idElettrovalvola;
        private float oraInizioMattina;
        private float oraFineMattina;
        private float oraInizioSera;
        private float oraFineSera;
        private int durata;
        private string giorni;
        private int idZona;

        public int IdElettrovalvola { get => idElettrovalvola; set => idElettrovalvola = value; }
        public float OraInizioMattina { get => oraInizioMattina; set => oraInizioMattina = value; }
        public float OraFineMattina { get => oraFineMattina; set => oraFineMattina = value; }
        public float OraInizioSera { get => oraInizioSera; set => oraInizioSera = value; }
        public float OraFineSera { get => oraFineSera; set => oraFineSera = value; }
        public int Durata { get => durata; set => durata = value; }
        public string Giorni { get => giorni; set => giorni = value; }
        public int IdZona { get => idZona; set => idZona = value; }
   
    }
}