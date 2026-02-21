using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoDeLoteria.Juego
{      
    public enum FormasdeGanar
        {
            TableroCompleto,
            CualquierFila,
            CualquierColumna,
            CualquierDiagonal,
            CualquierFilaColumnaDiagonal,
            CuatroEsquinas,
            CuatroEnElCentro,
            FormaDeX,
            FormaDeL,
        }
}