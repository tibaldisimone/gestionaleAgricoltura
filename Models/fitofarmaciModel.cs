using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class fitofarmaciModel
    {

        private int idFitofarmaco;
        private string nome;
        private string descrizione;
        private int carenza;

        public int IdFitofarmaco { get => idFitofarmaco; set => idFitofarmaco = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Descrizione { get => descrizione; set => descrizione = value; }
        public int Carenza { get => carenza; set => carenza = value; }
       
    }
}