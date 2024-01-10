using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class oreOperaiModel
    {
        private int idOre;
        private int numOreLavorate;
        private DateTime data;
        private int idOperaio;

        public int IdOre { get => idOre; set => idOre = value; }
        public int NumOreLavorate { get => numOreLavorate; set => numOreLavorate = value; }
        public DateTime Data { get => data; set => data = value; }
        public int IdOperaio { get => idOperaio; set => idOperaio = value; }
  
    }
}