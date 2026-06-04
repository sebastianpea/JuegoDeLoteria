using JuegoDeLoteria.Forms;
using JuegoDeLoteria.Juego;

namespace JuegoDeLoteria.Controles
{
    public partial class PostJuegoControl : UserControl
    {
        public event Action? OnJugarDeNuevo;
        public event Action? OnSalir;

        public PostJuegoControl()
        {
            InitializeComponent();
        }

        public void InicializarPostJuego(string ganador)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(() => InicializarPostJuego(ganador));
                return;
            }

            lblGanador.Text = ganador == "Nadie ganó"
                ? "¡Nadie ganó esta ronda!"
                : $"¡{ganador} ganó!";

            lblTituloRestantes.Text = "Cargando cartas restantes...";
            flpCartasRestantes.Controls.Clear();

            MainForm.Cliente.OnCartasRestantes += OnCartasRestantes;
            _ = MainForm.Cliente.ObtenerCartasRestantesAsync();
        }

        private void OnCartasRestantes(List<int> ids)
        {
            this.Invoke(() =>
            {
                MainForm.Cliente.OnCartasRestantes -= OnCartasRestantes;

                var mazo = new MazoDeCartas();
                var todasLasCartas = mazo.ObtenerTodasLasCartas();

                flpCartasRestantes.Controls.Clear();

                if (ids.Count == 0)
                {
                    lblTituloRestantes.Text = "¡Se llamaron todas las cartas!";
                    return;
                }

                lblTituloRestantes.Text = $"Cartas que no se llamaron ({ids.Count}):";

                foreach (var id in ids)
                {
                    var carta = todasLasCartas.FirstOrDefault(c => c.Id == id);
                    if (carta == null) continue;

                    var panel = new Panel();
                    panel.Size = new Size(70, 90);
                    panel.Margin = new Padding(5);

                    var pb = new PictureBox();
                    pb.Size = new Size(70, 70);
                    pb.Location = new Point(0, 0);
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.Image = carta.ObtenerImagen();

                    var lblNombre = new Label();
                    lblNombre.Size = new Size(70, 20);
                    lblNombre.Location = new Point(0, 70);
                    lblNombre.Text = carta.Nombre;
                    lblNombre.TextAlign = ContentAlignment.MiddleCenter;
                    lblNombre.Font = new Font("Arial", 6);

                    var tooltip = new ToolTip();
                    tooltip.SetToolTip(pb, carta.Nombre);

                    panel.Controls.Add(pb);
                    panel.Controls.Add(lblNombre);
                    flpCartasRestantes.Controls.Add(panel);
                }
            });
        }

        private async void btnJugarDeNuevo_Click(object sender, EventArgs e)
        {
            await MainForm.Cliente.JugarDeNuevoAsync();
            OnJugarDeNuevo?.Invoke();
        }

        private async void btnSalir_Click(object sender, EventArgs e)
        {
            await MainForm.Cliente.DesconectarAsync();
            OnSalir?.Invoke();
        }
    }
}