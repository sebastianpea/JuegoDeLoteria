using JuegoDeLoteria.Forms;
using JuegoDeLoteria.Juego;

namespace JuegoDeLoteria.Controles
{
    public partial class JuegoControl : UserControl
    {
        public event Action? OnJuegoTerminado;

        private List<Tablero> tableros = new List<Tablero>();
        private FormasdeGanar formaDeGanar;
        private List<PictureBox> fichasDisponibles = new List<PictureBox>();
        private PictureBox? fichaArrastrada = null;
        private Point offsetArrastre;

        public JuegoControl()
        {
            InitializeComponent();
        }

        public void InicializarJuego(string formaDeGanar, List<Tablero> tableros)
        {
            this.tableros = tableros;
            this.formaDeGanar = Enum.Parse<FormasdeGanar>(formaDeGanar);

            pnlTableros.Controls.Clear();
            flpHistorial.Controls.Clear();

            DibujarTableros();
            CrearFichas();

            MainForm.Cliente.OnCartaMencionada += OnCartaMencionada;
            MainForm.Cliente.OnVerificarLoteria += OnVerificarLoteria;
            MainForm.Cliente.OnJuegoTerminado += OnJuegoTerminado_Recibido;
        }

        private void DibujarTableros()
        {
            foreach (var tablero in tableros)
            {
                var panelTablero = new Panel();
                panelTablero.Width = 220;
                panelTablero.Height = 220;

                for (int fila = 0; fila < tablero.Tamaño; fila++)
                {
                    for (int col = 0; col < tablero.Tamaño; col++)
                    {
                        var carta = tablero.Cartas[fila * tablero.Tamaño + col];
                        var pb = new PictureBox();
                        pb.Size = new Size(50, 50);
                        pb.Location = new Point(col * 55, fila * 55);
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        pb.Image = carta.ObtenerImagen();
                        pb.Tag = (tablero, fila, col);
                        pb.AllowDrop = true;
                        pb.DragEnter += Celda_DragEnter;
                        pb.DragDrop += Celda_DragDrop;
                        panelTablero.Controls.Add(pb);
                    }
                }

                pnlTableros.Controls.Add(panelTablero);
            }
        }

        private void CrearFichas()
        {
            pnlFichas.Controls.Clear();
            for (int i = 0; i < 10; i++)
            {
                var ficha = new PictureBox();
                ficha.Size = new Size(40, 40);
                ficha.BackColor = Color.Gold;
                ficha.Cursor = Cursors.Hand;
                ficha.MouseDown += Ficha_MouseDown;
                ficha.MouseMove += Ficha_MouseMove;
                ficha.MouseUp += Ficha_MouseUp;
                pnlFichas.Controls.Add(ficha);
                fichasDisponibles.Add(ficha);
            }
        }

        private void Ficha_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is PictureBox ficha)
            {
                fichaArrastrada = ficha;
                offsetArrastre = e.Location;
                ficha.BringToFront();
                ficha.DoDragDrop(ficha, DragDropEffects.Move);
            }
        }

        private void Ficha_MouseMove(object? sender, MouseEventArgs e)
        {
            if (fichaArrastrada != null && e.Button == MouseButtons.Left)
            {
                var pos = fichaArrastrada.PointToScreen(e.Location);
                pos = this.PointToClient(pos);
                fichaArrastrada.Location = new Point(
                    pos.X - offsetArrastre.X,
                    pos.Y - offsetArrastre.Y);
            }
        }

        private void Ficha_MouseUp(object? sender, MouseEventArgs e)
        {
            fichaArrastrada = null;
        }

        private void Celda_DragEnter(object? sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Celda_DragDrop(object? sender, DragEventArgs e)
        {
            if (sender is PictureBox celda && celda.Tag is (Tablero tablero, int fila, int col))
            {
                tablero.PonerFicha(fila, col);
                celda.BackColor = Color.Gold;
            }
        }

        private void OnCartaMencionada(int id, string nombre)
        {
            this.Invoke(() =>
            {
                var carta = MainForm.Cliente is not null
                    ? new Carta(id, nombre, nombre.ToLower().Replace(" ", "_"))
                    : null;

                if (carta != null)
                {
                    pbCartaActual.Image = carta.ObtenerImagen();
                    lblNombreCartaActual.Text = nombre;

                    var pbHistorial = new PictureBox();
                    pbHistorial.Size = new Size(40, 40);
                    pbHistorial.SizeMode = PictureBoxSizeMode.Zoom;
                    pbHistorial.Image = carta.ObtenerImagen();
                    flpHistorial.Controls.Add(pbHistorial);
                }
            });
        }

        private async void btnLoteria_Click(object sender, EventArgs e)
        {
            btnLoteria.Enabled = false;
            await MainForm.Cliente.ReclamarLoteriaAsync();
        }

        private void OnVerificarLoteria(List<int> cartasMencionadas)
        {
            this.Invoke(async () =>
            {
                bool esValido = tableros.Any(t =>
                    t.VerificarVictoria(cartasMencionadas, formaDeGanar));

                await MainForm.Cliente.EnviarResultadoAsync(esValido);

                if (!esValido)
                    btnLoteria.Enabled = true;
            });
        }

        private void OnJuegoTerminado_Recibido(string ganador)
        {
            this.Invoke(() =>
            {
                MainForm.Cliente.OnCartaMencionada -= OnCartaMencionada;
                MainForm.Cliente.OnVerificarLoteria -= OnVerificarLoteria;
                MainForm.Cliente.OnJuegoTerminado -= OnJuegoTerminado_Recibido;
                OnJuegoTerminado?.Invoke();
            });
        }
    }
}
