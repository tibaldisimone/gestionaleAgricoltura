using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class esecuzioneSfemminellaturaModel
    {
        private int idSfemminellatura;
        private int idOperaio;
        private DateTime data;
        private List<sfemminellaturaModel> _lstSfemminellatura;

        public int IdSfemminellatura { get => idSfemminellatura; set => idSfemminellatura = value; }
        public int IdOperaio { get => idOperaio; set => idOperaio = value; }
        public DateTime Data { get => data; set => data = value; }
        public List<sfemminellaturaModel> LstSfemminellatura { get => _lstSfemminellatura; set => _lstSfemminellatura = value; }
    }
}