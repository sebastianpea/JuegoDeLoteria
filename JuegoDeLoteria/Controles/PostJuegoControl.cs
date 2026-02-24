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
            lblGanador.Text = ganador == "Nadie ganó"
                ? "¡Nadie ganó esta ronda!"
                : $"¡{ganador} ganó!";

            flpCartasRestantes.Controls.Clear();

            MainForm.Cliente.OnCartasRestantes += OnCartasRestantes;
            MainForm.Cliente.ObtenerCartasRestantesAsync();
        }

        private void OnCartasRestantes(List<int> ids)
        {
            this.Invoke(() =>
            {
                MainForm.Cliente.OnCartasRestantes -= OnCartasRestantes;

                var todasLasCartas = new MazoDeCartas().ObtenerTodasLasCartas();

                foreach (var id in ids)
                {
                    var carta = todasLasCartas.FirstOrDefault(c => c.Id == id);
                    if (carta == null) continue;

                    var pb = new PictureBox();
                    pb.Size = new Size(60, 60);
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.Image = carta.ObtenerImagen();

                    var tooltip = new ToolTip();
                    tooltip.SetToolTip(pb, carta.Nombre);

                    flpCartasRestantes.Controls.Add(pb);
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