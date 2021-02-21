using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormatarPontosGPS.Entities
{
    class PontoGps {
        public int Ponto { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public string Descricao { get; set; }

        public PontoGps() { }
        
        public PontoGps(int ponto, double x, double y, double z, string descricao) {
            Ponto = ponto;
            X = x;
            Y = y;
            Z = z;
            Descricao = descricao;
        }
    }
}
