using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class lancioInsettiModel
    {
        private int idLancioInsetti;
        private DateTime data;

        private int idZona;

        public int IdLancioInsetti { get => idLancioInsetti; set => idLancioInsetti = value; }
        public DateTime Data { get => data; set => data = value; }
  
        public int IdZona { get => idZona; set => idZona = value; }
    }
}