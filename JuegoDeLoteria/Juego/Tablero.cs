
using System.Runtime.CompilerServices;

namespace JuegoDeLoteria.Juego
{
    internal class Tablero
    {
        public List<Carta> Cartas { get; private set; }
        public bool[,] Marcado { get; private set; }
        public int Tamaño => 4;

        public Tablero(List<Carta> cartas)
        {
            Cartas = cartas;
            Marcado = new bool[Tamaño, Tamaño];
        }
        public void PonerFicha(int fila, int columna)
        {
            Marcado[fila, columna] = true;
        }

        public void QuitarFicha(int fila, int columna)
        {
            Marcado[fila, columna] = false;
        }

        public bool VerificarVictoria(List<int> IdCartasMencionadas, FormasdeGanar formasdeGanar)
        {
            for (int r = 0; r < Tamaño; r++)
                for (int c = 0; c < Tamaño; c++)
                    if (Marcado[r, c] && !IdCartasMencionadas.Contains(Cartas[r * Tamaño + c].Id))
                        Marcado[r, c] = false;
            return RevisarVictoria(formasdeGanar);
        }

        public bool RevisarVictoria(FormasdeGanar formasdeGanar)

        {
            return formasdeGanar switch
            {
                FormasdeGanar.TableroCompleto => RevisarTableroCompleto(),
                FormasdeGanar.CualquierFila => RevisarCualquierFila(),
                FormasdeGanar.CualquierColumna => RevisarCualquierColumna(),
                FormasdeGanar.CualquierDiagonal => RevisarCualquierDiagonal(),
                FormasdeGanar.CuatroEsquinas => RevisarCuatroEsquinas(),
                FormasdeGanar.CualquierFilaColumnaDiagonal => RevisarCualquierFila() || RevisarCualquierColumna() || RevisarCualquierDiagonal(),
                FormasdeGanar.CuatroEnElCentro => RevisarCuatroEnElCentro(),
                FormasdeGanar.FormaDeX => RevisarFormaDeX(),
                FormasdeGanar.FormaDeL => RevisarFormaDeL(),

                _ => false
            };
        }

        private bool RevisarTableroCompleto()
        {
            for (int i = 0; i < Tamaño; i++)
                for (int j = 0; j < Tamaño; j++)
                    if (!Marcado[i, j]) return false;
            return true;
        }
        private bool RevisarCualquierFila()
        {
            for (int i = 0; i < Tamaño; i++)
            {
                bool filaCompleta = true;
                for (int j = 0; j < Tamaño; j++)
                    if (!Marcado[i, j]) { filaCompleta = false; break; }
                if (filaCompleta) return true;
            }
            return false;
        }
        private bool RevisarCualquierColumna()
        {
            for (int j = 0; j < Tamaño; j++)
            {
                bool columnaCompleta = true;
                for (int i = 0; i < Tamaño; i++)
                    if (!Marcado[i, j]) { columnaCompleta = false; break; }
                if (columnaCompleta) return true;
            }
            return false;
        }

        private bool RevisarCualquierDiagonal()
        {
            bool diagonal1 = true; bool diagonal2 = true;
            for (int i = 0; i < Tamaño; i++)
            {
                if (!Marcado[i, i]) diagonal1 = false;
                if (!Marcado[i, Tamaño - 1 - i]) diagonal2 = false;
            }
            return diagonal1 || diagonal2;
        }

        private bool RevisarCuatroEsquinas()
        {
            return Marcado[0, 0] && Marcado[0, Tamaño - 1] && Marcado[Tamaño - 1, 0] && Marcado[Tamaño - 1, Tamaño - 1];
        }

        private bool RevisarCuatroEnElCentro()
        {
            int centro = Tamaño / 2;
            return Marcado[centro - 1, centro - 1] && Marcado[centro - 1, centro] && Marcado[centro, centro - 1] && Marcado[centro, centro];
        }

        private bool RevisarFormaDeX()
        {
            bool diag1 = true, diag2 = true;
            for (int i = 0; i < Tamaño; i++)
            {
                if (!Marcado[i, i]) diag1 = false;
                if (!Marcado[i, Tamaño - 1 - i]) diag2 = false;
            }
            return diag1 && diag2;
        }

        private bool RevisarFormaDeL()
        {
            // L normal: Primera columna completa y última fila completa
            bool l1 = true;
                for (int i = 0; i < Tamaño; i++) if (!Marcado[i, 0]) { l1 = false; break; }
                if (l1) for (int c = 0; c < Tamaño; c++) if (!Marcado[Tamaño - 1, c]) { l1 = false; break; }

            // L normal en espejo: Última columna completa + última fila completa
            bool l2 = true;
                for (int i = 0; i < Tamaño; i++) if (!Marcado[i, Tamaño - 1]) { l2 = false; break; }
                if (l2) for (int c = 0; c < Tamaño; c++) if (!Marcado[Tamaño - 1, c]) { l2 = false; break; }

            // L al reves: columna izquierda completa y fila superior completa
            bool l3 = true;
                for (int i = 0; i < Tamaño; i++) if (!Marcado[i, 0]) { l3 = false; break; }
                if (l3) for (int c = 0; c < Tamaño; c++) if (!Marcado[0, c]) { l3 = false; break; }

            // L al reves en espejo: columna derecha completa y fila superior completa
            bool l4 = true;
                for (int i = 0; i < Tamaño; i++) if (!Marcado[i, Tamaño - 1]) { l4 = false; break; }
                if (l4) for (int c = 0; c < Tamaño; c++) if (!Marcado[0, c]) { l4 = false; break; }

                return l1 || l2 || l3 || l4;
        }

    }
}
