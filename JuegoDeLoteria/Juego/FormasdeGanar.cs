using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoDeLoteria.Juego
{
    public class FormasdeGanar
    {
        public enum FormaDeGanar
        {
            TableroCompleto,
            CualquierFila,
            CualquierColumna,
            CualquierDiagonal,
            CuatroEsquinas,
            CuatroEnElCentro,
            FormaDeCruz,
            FormaDeL,
        }
    }
}