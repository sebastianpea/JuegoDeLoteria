using JuegoDeLoteria.Juego;

namespace JuegoDeLoteria.Controles
{
    public partial class TableroControl : UserControl
    {
        private Tablero _tablero;
        private PictureBox[,] _celdas = new PictureBox[4, 4];
        private Panel[,] _panelesCeldas = new Panel[4, 4];

        public TableroControl(Tablero tablero)
        {
            InitializeComponent();
            _tablero = tablero;
            ConfigurarTablero();
            DibujarCeldas();
        }

        private void ConfigurarTablero()
        {
            int celdaSize = 80;
            int padding = 5;
            int totalSize = (celdaSize + padding) * 4 + padding;
            this.Size = new Size(totalSize, totalSize);
            this.BackColor = Color.SaddleBrown;
            this.Padding = new Padding(padding);
        }

        private void DibujarCeldas()
        {
            int celdaSize = 80;
            int padding = 5;

            for (int fila = 0; fila < 4; fila++)
            {
                for (int col = 0; col < 4; col++)
                {
                    var carta = _tablero.Cartas[fila * 4 + col];

                    var panel = new Panel();
                    panel.Size = new Size(celdaSize, celdaSize);
                    panel.Location = new Point(
                        padding + col * (celdaSize + padding),
                        padding + fila * (celdaSize + padding));
                    panel.BackColor = Color.Cornsilk;
                    panel.Tag = (fila, col);
                    panel.AllowDrop = true;
                    panel.DragEnter += Celda_DragEnter;
                    panel.DragDrop += Celda_DragDrop;
                    panel.DragOver += Celda_DragOver;

                    var pbCarta = new PictureBox();
                    pbCarta.Size = new Size(celdaSize - 20, celdaSize - 22);
                    pbCarta.Location = new Point(10, 3);
                    pbCarta.SizeMode = PictureBoxSizeMode.Zoom;
                    pbCarta.Image = carta.ObtenerImagen();
                    pbCarta.Tag = (fila, col);
                    pbCarta.AllowDrop = true;
                    pbCarta.DragEnter += Celda_DragEnter;
                    pbCarta.DragDrop += Celda_DragDrop;
                    pbCarta.DragOver += Celda_DragOver;
                    pbCarta.MouseDown += Celda_MouseDown;

                    var lblNombre = new Label();
                    lblNombre.Size = new Size(celdaSize, 15);
                    lblNombre.Location = new Point(0, celdaSize - 17);
                    lblNombre.Text = carta.Nombre;
                    lblNombre.TextAlign = ContentAlignment.MiddleCenter;
                    lblNombre.Font = new Font("Arial", 6, FontStyle.Bold);
                    lblNombre.BackColor = Color.Cornsilk;

                    panel.Controls.Add(pbCarta);
                    panel.Controls.Add(lblNombre);

                    _celdas[fila, col] = pbCarta;
                    _panelesCeldas[fila, col] = panel;
                    this.Controls.Add(panel);
                }
            }
        }

        private void Celda_DragEnter(object? sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Celda_DragOver(object? sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Celda_DragDrop(object? sender, DragEventArgs e)
        {
            var (fila, col) = ObtenerFilaCol(sender);
            if (fila == -1) return;

            if (_tablero.Marcado[fila, col])
                QuitarFicha(fila, col);
            else
                PonerFicha(fila, col);
        }

        private void Celda_MouseDown(object? sender, MouseEventArgs e)
        {
            var (fila, col) = ObtenerFilaCol(sender);
            if (fila == -1) return;
            if (!_tablero.Marcado[fila, col]) return;

            QuitarFicha(fila, col);
        }

        private void PonerFicha(int fila, int col)
        {
            _tablero.PonerFicha(fila, col);
            var carta = _tablero.Cartas[fila * 4 + col];
            var imagenConFicha = ObtenerImagenConFicha(carta.NombreRecurso);
            _celdas[fila, col].Image = imagenConFicha;
        }

        private void QuitarFicha(int fila, int col)
        {
            _tablero.QuitarFicha(fila, col);
            var carta = _tablero.Cartas[fila * 4 + col];
            _celdas[fila, col].Image = carta.ObtenerImagen();
        }

        private Image? ObtenerImagenConFicha(string nombreRecurso)
        {
            string nombreConFicha = nombreRecurso + "_ficha";
            var imagen = Properties.Resources.ResourceManager
                .GetObject(nombreConFicha) as Image;
            return imagen ?? Properties.Resources.ResourceManager
                .GetObject(nombreRecurso) as Image;
        }

        private (int fila, int col) ObtenerFilaCol(object? sender)
        {
            if (sender is PictureBox pb && pb.Tag is (int f1, int c1))
                return (f1, c1);
            if (sender is Panel p && p.Tag is (int f2, int c2))
                return (f2, c2);
            return (-1, -1);
        }

        public void MarcarCartaMencionada(int cartaId)
        {
            for (int fila = 0; fila < 4; fila++)
            {
                for (int col = 0; col < 4; col++)
                {
                    var carta = _tablero.Cartas[fila * 4 + col];
                    if (carta.Id == cartaId)
                    {
                        var panel = _panelesCeldas[fila, col];
                        panel.BackColor = Color.LightYellow;
                    }
                }
            }
        }
    }
}