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
        private List<List<Carta>> cartasSeleccionadas = new List<List<Carta>>();
        private List<Carta> cartasElegidasActual = new List<Carta>();

        public SeleccionTableroControl()
        {
            InitializeComponent();
            ConfigurarNud();
        }

        private void ConfigurarNud()
        {
            nudCantidadTableros.Minimum = 1;
            nudCantidadTableros.Maximum = 4;
            nudCantidadTableros.Value = 1;
            pnlSeleccion.Visible = false;
            btnConfirmar.Visible = false;
        }

        private void btnAleatorio_Click(object sender, EventArgs e)
        {
            tablerosSeleccionados.Clear();
            int cantidad = (int)nudCantidadTableros.Value;

            for (int i = 0; i < cantidad; i++)
            {
                var cartasAleatorias = mazo.ObtenerTodasLasCartas()
                    .OrderBy(c => Guid.NewGuid())
                    .Take(16)
                    .ToList();
                tablerosSeleccionados.Add(new Tablero(cartasAleatorias));
            }

            OnTablerosSeleccionados?.Invoke(tablerosSeleccionados);
        }

        private void btnElegirPropio_Click(object sender, EventArgs e)
        {
            tableroActual = 0;
            cartasSeleccionadas.Clear();
            pnlSeleccion.Visible = true;
            btnConfirmar.Visible = true;
            lblInstrucciones.Text = $"Tablero 1 de {(int)nudCantidadTableros.Value} — Elige 16 cartas";
            MostrarCartasParaElegir();
        }

        private void MostrarCartasParaElegir()
        {
            pnlSeleccion.Controls.Clear();
            cartasElegidasActual.Clear();
            var todasLasCartas = mazo.ObtenerTodasLasCartas();

            foreach (var carta in todasLasCartas)
            {
                var pb = new PictureBox();
                pb.Size = new Size(50, 50);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Image = carta.ObtenerImagen();
                pb.Tag = carta;
                pb.Cursor = Cursors.Hand;
                pb.Click += CartaParaElegir_Click;
                pnlSeleccion.Controls.Add(pb);
            }
        }

        private void CartaParaElegir_Click(object? sender, EventArgs e)
        {
            if (sender is not PictureBox pb) return;
            if (pb.Tag is not Carta carta) return;

            if (cartasElegidasActual.Contains(carta))
            {
                cartasElegidasActual.Remove(carta);
                pb.BackColor = Color.Transparent;
            }
            else if (cartasElegidasActual.Count < 16)
            {
                cartasElegidasActual.Add(carta);
                pb.BackColor = Color.Gold;
            }

            lblInstrucciones.Text = $"Tablero {tableroActual + 1} — {cartasElegidasActual.Count}/16 cartas seleccionadas";
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (cartasElegidasActual.Count < 16)
            {
                lblInstrucciones.Text = $"Necesitas seleccionar 16 cartas. Tienes {cartasElegidasActual.Count}.";
                return;
            }

            cartasSeleccionadas.Add(new List<Carta>(cartasElegidasActual));
            tableroActual++;

            int cantidad = (int)nudCantidadTableros.Value;

            if (tableroActual < cantidad)
            {
                lblInstrucciones.Text = $"Tablero {tableroActual + 1} de {cantidad} — Elige 16 cartas";
                MostrarCartasParaElegir();
            }
            else
            {
                foreach (var cartas in cartasSeleccionadas)
                    tablerosSeleccionados.Add(new Tablero(cartas));

                OnTablerosSeleccionados?.Invoke(tablerosSeleccionados);
            }
        }
    }
}