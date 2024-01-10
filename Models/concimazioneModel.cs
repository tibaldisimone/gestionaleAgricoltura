using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class concimazioneModel
    {
        private int idConcimazione;
        private DateTime data;
        private string nome;

        private int idZona;

        public int IdConcimazione { get => idConcimazione; set => idConcimazione = value; }
        public DateTime Data { get => data; set => data = value; }
       

        public int IdZona { get => idZona; set => idZona = value; }
        public string Nome { get => nome; set => nome = value; }
    }
}