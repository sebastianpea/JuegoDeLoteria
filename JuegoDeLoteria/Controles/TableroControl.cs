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
            _celdas = new PictureBox[tablero.Tamaño, tablero.Tamaño];
            _panelesCeldas = new Panel[tablero.Tamaño, tablero.Tamaño];
            ConfigurarTablero();
            DibujarCeldas();
        }

        private void ConfigurarTablero()
        {
            int n = _tablero.Tamaño;
            int celdaSize = Math.Min(80, 340 / n); // escala según tamaño
            int padding = 5;
            int totalSize = (celdaSize + padding) * n + padding;
            this.Size = new Size(totalSize, totalSize);
            this.BackColor = Color.SaddleBrown;
            this.Padding = new Padding(padding);
        }

        private void DibujarCeldas()
        {
            int n = _tablero.Tamaño;
            int celdaSize = Math.Min(80, 340 / n);
            int padding = 5;

            for (int fila = 0; fila < n; fila++)
            {
                for (int col = 0; col < n; col++)
                {
                    var carta = _tablero.Cartas[fila * n + col];

                    var panel = new Panel();
                    panel.Size = new Size(celdaSize, celdaSize);
                    panel.Location = new Point(
                        padding + col * (celdaSize + padding),
                        padding + fila * (celdaSize + padding));
                    panel.BackColor = Color.Cornsilk;
                    panel.Tag = (fila, col);
                    panel.Cursor = Cursors.Hand;
                    panel.Click += Celda_Click;

                    var pbCarta = new PictureBox();
                    pbCarta.Size = new Size(celdaSize - 20, celdaSize - 22);
                    pbCarta.Location = new Point(10, 3);
                    pbCarta.SizeMode = PictureBoxSizeMode.Zoom;
                    pbCarta.Image = carta.ObtenerImagen();
                    pbCarta.Tag = (fila, col);
                    pbCarta.Cursor = Cursors.Hand;
                    pbCarta.Click += Celda_Click;

                    var lblNombre = new Label();
                    lblNombre.Size = new Size(celdaSize, 15);
                    lblNombre.Location = new Point(0, celdaSize - 17);
                    lblNombre.Text = carta.Nombre;
                    lblNombre.TextAlign = ContentAlignment.MiddleCenter;
                    lblNombre.Font = new Font("Arial", 6, FontStyle.Bold);
                    lblNombre.BackColor = Color.Cornsilk;
                    lblNombre.Tag = (fila, col);
                    lblNombre.Cursor = Cursors.Hand;
                    lblNombre.Click += Celda_Click;

                    panel.Controls.Add(pbCarta);
                    panel.Controls.Add(lblNombre);

                    _celdas[fila, col] = pbCarta;
                    _panelesCeldas[fila, col] = panel;
                    this.Controls.Add(panel);
                }
            }
        }

        private void Celda_Click(object? sender, EventArgs e)
        {
            var (fila, col) = ObtenerFilaCol(sender);
            if (fila == -1) return;

            if (_tablero.Marcado[fila, col])
                QuitarFicha(fila, col);
            else
                PonerFicha(fila, col);
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
            var carta = _tablero.Cartas[fila * _tablero.Tamaño + col];

            // Restaurar imagen original sin tinte
            _celdas[fila, col].Image = carta.ObtenerImagen();

            var panel = _panelesCeldas[fila, col];
            panel.BackColor = Color.Cornsilk;

            foreach (Control hijo in panel.Controls)
                if (hijo is Label lbl)
                    lbl.BackColor = Color.Cornsilk;
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
            if (sender is Label l && l.Tag is (int f3, int c3))
                return (f3, c3);
            return (-1, -1);
        }

        public void MarcarCartaMencionada(int cartaId)
        {
            for (int fila = 0; fila < _tablero.Tamaño; fila++)
            {
                for (int col = 0; col < _tablero.Tamaño; col++)
                {
                    if (_tablero.Cartas[fila * _tablero.Tamaño + col].Id == cartaId)
                    {
                        var panel = _panelesCeldas[fila, col];
                        panel.BackColor = Color.LightYellow;

                        foreach (Control hijo in panel.Controls)
                            if (hijo is Label lbl)
                                lbl.BackColor = Color.LightYellow;
                    }
                }
            }
        }

        public void MarcarCeldaInvalida(int fila, int col)
        {
            var panel = _panelesCeldas[fila, col];
            panel.BackColor = Color.IndianRed;

            var pb = _celdas[fila, col];
            if (pb.Image != null)
                pb.Image = AplicarTinte(pb.Image, Color.Red, 150);

            foreach (Control hijo in panel.Controls)
                if (hijo is Label lbl)
                    lbl.BackColor = Color.IndianRed;
        }

        private Image AplicarTinte(Image imagenOriginal, Color tinte, int alfa = 120)
        {
            var bitmap = new Bitmap(imagenOriginal.Width, imagenOriginal.Height);
            using var g = Graphics.FromImage(bitmap);
            g.DrawImage(imagenOriginal, 0, 0, imagenOriginal.Width, imagenOriginal.Height);
            using var brush = new SolidBrush(Color.FromArgb(alfa, tinte));
            g.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
            return bitmap;
        }
    }
}