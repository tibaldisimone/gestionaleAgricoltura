using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esercizioProva.Models
{
    public class coordinateModel
    {
        private int idCoordinate;
        private float lat1;
        private float long1;
        private float lat2;
        private float long2;
        private float lat3;
        private float long3;
        private float lat4;
        private float long4;

        public int IdCoordinate { get => idCoordinate; set => idCoordinate = value; }
        public float Lat1 { get => lat1; set => lat1 = value; }
        public float Long1 { get => long1; set => long1 = value; }
        public float Lat2 { get => lat2; set => lat2 = value; }
        public float Long2 { get => long2; set => long2 = value; }
        public float Lat3 { get => lat3; set => lat3 = value; }
        public float Long3 { get => long3; set => long3 = value; }
        public float Lat4 { get => lat4; set => lat4 = value; }
        public float Long4 { get => long4; set => long4 = value; }
    }
}