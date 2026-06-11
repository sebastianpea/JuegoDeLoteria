namespace JuegoDeLoteria.Juego
{
    public class Tablero
    {
        public List<Carta> Cartas { get; private set; }
        public bool[,] Marcado { get; private set; }
        public int Tamaño { get; private set; }

        public Tablero(List<Carta> cartas, int tamaño = 4)
        {
            Tamaño = tamaño;
            Cartas = cartas;
            Marcado = new bool[Tamaño, Tamaño];
        }

        public void PonerFicha(int fila, int columna) => Marcado[fila, columna] = true;
        public void QuitarFicha(int fila, int columna) => Marcado[fila, columna] = false;

        public bool VerificarVictoria(List<int> IdCartasMencionadas, FormasdeGanar formasdeGanar,
            List<bool>? patronPersonalizado = null)
        {
            for (int r = 0; r < Tamaño; r++)
                for (int c = 0; c < Tamaño; c++)
                    if (Marcado[r, c] && !IdCartasMencionadas.Contains(Cartas[r * Tamaño + c].Id))
                        Marcado[r, c] = false;

            return patronPersonalizado != null
                ? RevisarPatronPersonalizado(patronPersonalizado)
                : RevisarVictoria(formasdeGanar);
        }

        public bool RevisarPatronPersonalizado(List<bool> patron)
        {
            for (int f = 0; f < Tamaño; f++)
                for (int c = 0; c < Tamaño; c++)
                    if (patron[f * Tamaño + c] && !Marcado[f, c])
                        return false;
            return true;
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
                FormasdeGanar.CualquierFilaColumnaDiagonal =>
                    RevisarCualquierFila() || RevisarCualquierColumna() || RevisarCualquierDiagonal(),
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
            bool d1 = true, d2 = true;
            for (int i = 0; i < Tamaño; i++)
            {
                if (!Marcado[i, i]) d1 = false;
                if (!Marcado[i, Tamaño - 1 - i]) d2 = false;
            }
            return d1 || d2;
        }

        private bool RevisarCuatroEsquinas() =>
            Marcado[0, 0] && Marcado[0, Tamaño - 1] &&
            Marcado[Tamaño - 1, 0] && Marcado[Tamaño - 1, Tamaño - 1];

        private bool RevisarCuatroEnElCentro()
        {
            int centro = Tamaño / 2;
            return Marcado[centro - 1, centro - 1] && Marcado[centro - 1, centro] &&
                   Marcado[centro, centro - 1] && Marcado[centro, centro];
        }

        private bool RevisarFormaDeX()
        {
            bool d1 = true, d2 = true;
            for (int i = 0; i < Tamaño; i++)
            {
                if (!Marcado[i, i]) d1 = false;
                if (!Marcado[i, Tamaño - 1 - i]) d2 = false;
            }
            return d1 && d2;
        }

        private bool RevisarFormaDeL()
        {
            bool l1 = true, l2 = true, l3 = true, l4 = true;
            for (int i = 0; i < Tamaño; i++) if (!Marcado[i, 0]) { l1 = false; break; }
            if (l1) for (int c = 0; c < Tamaño; c++) if (!Marcado[Tamaño - 1, c]) { l1 = false; break; }

            for (int i = 0; i < Tamaño; i++) if (!Marcado[i, Tamaño - 1]) { l2 = false; break; }
            if (l2) for (int c = 0; c < Tamaño; c++) if (!Marcado[Tamaño - 1, c]) { l2 = false; break; }

            for (int i = 0; i < Tamaño; i++) if (!Marcado[i, 0]) { l3 = false; break; }
            if (l3) for (int c = 0; c < Tamaño; c++) if (!Marcado[0, c]) { l3 = false; break; }

            for (int i = 0; i < Tamaño; i++) if (!Marcado[i, Tamaño - 1]) { l4 = false; break; }
            if (l4) for (int c = 0; c < Tamaño; c++) if (!Marcado[0, c]) { l4 = false; break; }

            return l1 || l2 || l3 || l4;
        }
    }
}