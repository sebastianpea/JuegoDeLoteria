using JuegoDeLoteria.Forms;
using JuegoDeLoteria.Juego;

namespace JuegoDeLoteria.Controles
{
    public partial class SeleccionTableroControl : UserControl
    {
        public event Action<List<Tablero>>? OnTablerosSeleccionados;

        private List<Tablero> tablerosSeleccionados = new List<Tablero>();
        private MazoDeCartas mazo = new MazoDeCartas();
        private int tableroActual = 0;
        private int cantidadTableros = 1;
        private PictureBox?[,] celdas = new PictureBox?[4, 4];
        private Dictionary<(int, int), Carta> cartasEnCeldas = new Dictionary<(int, int), Carta>();

        public SeleccionTableroControl()
        {
            InitializeComponent();
            ConfigurarNud();
            MostrarCartasDisponibles();
            DibujarTableroVacio();
        }

        private void ConfigurarNud()
        {
            nudCantidadTableros.Minimum = 1;
            nudCantidadTableros.Maximum = 4;
            nudCantidadTableros.Value = 1;
            btnConfirmar.Enabled = false;
            ActualizarInstrucciones();
        }

        private void MostrarCartasDisponibles()
        {
            flpCartasDisponibles.Controls.Clear();
            var todasLasCartas = mazo.ObtenerTodasLasCartas();

            foreach (var carta in todasLasCartas)
            {
                var pb = new PictureBox();
                pb.Size = new Size(55, 55);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Image = carta.ObtenerImagen();
                pb.Tag = carta;
                pb.Cursor = Cursors.Hand;
                pb.Margin = new Padding(3);
                pb.MouseDown += CartaDisponible_MouseDown;
                flpCartasDisponibles.Controls.Add(pb);
            }
        }

        private void DibujarTableroVacio()
        {
            pnlTablero.Controls.Clear();
            celdas = new PictureBox?[4, 4];
            cartasEnCeldas.Clear();

            for (int fila = 0; fila < 4; fila++)
            {
                for (int col = 0; col < 4; col++)
                {
                    var celda = new PictureBox();
                    celda.Size = new Size(70, 70);
                    celda.Location = new Point(col * 75, fila * 75);
                    celda.SizeMode = PictureBoxSizeMode.Zoom;
                    celda.BorderStyle = BorderStyle.FixedSingle;
                    celda.BackColor = Color.White;
                    celda.Tag = (fila, col);
                    celda.AllowDrop = true;
                    celda.DragEnter += Celda_DragEnter;
                    celda.DragDrop += Celda_DragDrop;
                    celda.DragOver += Celda_DragOver;
                    celda.MouseDown += Celda_MouseDown;
                    pnlTablero.Controls.Add(celda);
                    celdas[fila, col] = celda;
                }
            }
        }

        private void CartaDisponible_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is PictureBox pb && pb.Tag is Carta carta && pb.Enabled)
                pb.DoDragDrop(carta, DragDropEffects.Copy);
        }

        private void Celda_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is not PictureBox celda) return;
            if (celda.Tag is not (int fila, int col)) return;
            if (!cartasEnCeldas.ContainsKey((fila, col))) return;

            var carta = cartasEnCeldas[(fila, col)];
            cartasEnCeldas.Remove((fila, col));
            celda.Image = null;
            celda.BackColor = Color.White;
            RestaurarCartaDisponible(carta);
            VerificarTableroCompleto();
            celda.DoDragDrop(carta, DragDropEffects.Copy);
        }

        private void Celda_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetData(typeof(Carta)) is Carta)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Celda_DragOver(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetData(typeof(Carta)) is Carta)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Celda_DragDrop(object? sender, DragEventArgs e)
        {
            if (sender is not PictureBox celda) return;
            if (celda.Tag is not (int fila, int col)) return;
            if (e.Data?.GetData(typeof(Carta)) is not Carta carta) return;

            if (CartaYaEnTablero(carta))
            {
                MessageBox.Show("Esa carta ya está en el tablero.");
                return;
            }

            if (cartasEnCeldas.ContainsKey((fila, col)))
            {
                RestaurarCartaDisponible(cartasEnCeldas[(fila, col)]);
                cartasEnCeldas.Remove((fila, col));
            }

            cartasEnCeldas[(fila, col)] = carta;
            celda.Image = carta.ObtenerImagen();
            celda.BackColor = Color.LightGoldenrodYellow;
            MarcarCartaUsada(carta);
            VerificarTableroCompleto();
        }

        private bool CartaYaEnTablero(Carta carta)
        {
            return cartasEnCeldas.Values.Any(c => c.Id == carta.Id);
        }

        private void MarcarCartaUsada(Carta carta)
        {
            foreach (Control control in flpCartasDisponibles.Controls)
            {
                if (control is PictureBox pb && pb.Tag is Carta c && c.Id == carta.Id)
                {
                    pb.BackColor = Color.LightGray;
                    pb.Enabled = false;
                }
            }
        }

        private void RestaurarCartaDisponible(Carta carta)
        {
            foreach (Control control in flpCartasDisponibles.Controls)
            {
                if (control is PictureBox pb && pb.Tag is Carta c && c.Id == carta.Id)
                {
                    pb.BackColor = Color.Transparent;
                    pb.Enabled = true;
                }
            }
        }

        private void VerificarTableroCompleto()
        {
            btnConfirmar.Enabled = cartasEnCeldas.Count == 16;
            ActualizarInstrucciones(cartasEnCeldas.Count);
        }

        private void ActualizarInstrucciones(int colocadas = 0)
        {
            lblInstrucciones.Text = $"Tablero {tableroActual + 1} de {(int)nudCantidadTableros.Value} — {colocadas}/16 cartas colocadas";
        }

        private void btnAleatorio_Click(object sender, EventArgs e)
        {
            DibujarTableroVacio();

            foreach (Control control in flpCartasDisponibles.Controls)
            {
                control.BackColor = Color.Transparent;
                control.Enabled = true;
            }

            var cartasAleatorias = mazo.ObtenerTodasLasCartas()
                .OrderBy(c => Guid.NewGuid())
                .Take(16)
                .ToList();

            int index = 0;
            for (int fila = 0; fila < 4; fila++)
            {
                for (int col = 0; col < 4; col++)
                {
                    var carta = cartasAleatorias[index++];
                    var celda = celdas[fila, col];
                    if (celda != null)
                    {
                        celda.Image = carta.ObtenerImagen();
                        celda.BackColor = Color.LightGoldenrodYellow;
                    }
                    cartasEnCeldas[(fila, col)] = carta;
                    MarcarCartaUsada(carta);
                }
            }

            VerificarTableroCompleto();
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            var cartas = new List<Carta>();
            for (int fila = 0; fila < 4; fila++)
                for (int col = 0; col < 4; col++)
                    cartas.Add(cartasEnCeldas[(fila, col)]);

            tablerosSeleccionados.Add(new Tablero(cartas));
            tableroActual++;
            cantidadTableros = (int)nudCantidadTableros.Value;

            if (tableroActual < cantidadTableros)
            {
                DibujarTableroVacio();
                foreach (Control control in flpCartasDisponibles.Controls)
                {
                    control.BackColor = Color.Transparent;
                    control.Enabled = true;
                }
                btnConfirmar.Enabled = false;
                ActualizarInstrucciones();
            }
            else
            {
                btnConfirmar.Enabled = false;
                btnAleatorio.Enabled = false;
                nudCantidadTableros.Enabled = false;
                lblInstrucciones.Text = "Esperando a los demás jugadores...";

                // cambia a la pantalla de juego y pasa los tableros seleccionados
                OnTablerosSeleccionados?.Invoke(tablerosSeleccionados);

                // dice al servidor que ya seleccionó sus tableros y que esta listo
                await MainForm.Cliente.JugadorListoAsync();
            }
        }
    }
}