using JuegoDeLoteria.Forms;
using JuegoDeLoteria.Juego;

namespace JuegoDeLoteria.Controles
{
    public partial class LobbyControl : UserControl
    {
        public event Action? OnJuegoIniciado;

        public LobbyControl()
        {
            InitializeComponent();
            CargarFormasDeGanar();
            ConfigurarIntervalo();
            RegistrarEventos();
        }

        private void RegistrarEventos()
        {
            MainForm.Cliente.OnJugadorUnido += OnJugadorUnido;
            MainForm.Cliente.OnJugadorSalio += OnJugadorSalio;
            MainForm.Cliente.OnJuegoIniciado += OnJuegoIniciado_Recibido;
            MainForm.Cliente.OnNuevoHost += OnNuevoHost;
        }

        public void InicializarLobby()
        {
            lstJugadores.Items.Clear();
            lblCodigoSala.Text = "Sala: " + MainForm.Cliente.CodigoSala;

            // En lugar de leer EsHost directo, espera el evento si aún no llegó
            MainForm.Cliente.OnUnidoASala += OnUnidoASala_Handler;
            MostrarControlesHost(MainForm.Cliente.EsHost);

            lstJugadores.Items.Add("(Tú) " + MainForm.Cliente.NombreJugador);
            foreach (var jugador in MainForm.Cliente.JugadoresExistentes)
                lstJugadores.Items.Add(jugador.Value);

            chatControl1.InicializarChat();
        }

        private void OnUnidoASala_Handler(bool esHost)
        {
            MainForm.Cliente.OnUnidoASala -= OnUnidoASala_Handler;
            this.Invoke(() => MostrarControlesHost(esHost));
        }

        private void CargarFormasDeGanar()
        {
            cmbFormaDeGanar.Items.Clear();
            foreach (FormasdeGanar forma in Enum.GetValues(typeof(FormasdeGanar)))
                cmbFormaDeGanar.Items.Add(forma);
            cmbFormaDeGanar.SelectedIndex = 0;
        }

        private void ConfigurarIntervalo()
        {
            nudIntervalo.Minimum = 1;
            nudIntervalo.Maximum = 30;
            nudIntervalo.Value = 5;
        }
        private async void btnIniciarJuego_Click(object sender, EventArgs e)
        {
            string formaDeGanar = cmbFormaDeGanar.SelectedItem!.ToString()!;
            int intervalo = (int)nudIntervalo.Value;
            await MainForm.Cliente.IniciarJuegoAsync(formaDeGanar, intervalo);
        }

        private void OnJugadorUnido(string id, string nombre)
        {
            this.Invoke(() => lstJugadores.Items.Add(nombre));
        }

        private void OnJugadorSalio(string id)
        {
            this.Invoke(() =>
            {
                if (lstJugadores.Items.Contains(id))
                    lstJugadores.Items.Remove(id);
            });
        }

        private void OnJuegoIniciado_Recibido(string formaDeGanar)
        {
            this.Invoke(() => OnJuegoIniciado?.Invoke());
        }

        private void OnNuevoHost(string id)
        {
            this.Invoke(() =>
            {
                if (id == MainForm.Cliente.ConnectionId)
                {
                    MainForm.Cliente.EsHost = true; // necesitas hacer EsHost setteable
                    MostrarControlesHost(true);
                }
            });
        }
        private void lblEsperando_Click(object sender, EventArgs e)
        {

        }
        private void MostrarControlesHost(bool esHost)
        {
            btnIniciarJuego.Visible = esHost;
            cmbFormaDeGanar.Visible = esHost;
            nudIntervalo.Visible = esHost;
            chkCartasDobles.Visible = esHost;
            chkManual.Visible = esHost;
            lblEsperando.Visible = !esHost;
        }

        private async void chkCartasDobles_CheckedChanged(object sender, EventArgs e)
        {
            await MainForm.Cliente.ActualizarConfiguracionAsync(
                chkCartasDobles.Checked,
                chkManual.Checked);
        }

        private async void chkManual_CheckedChanged(object sender, EventArgs e)
        {
            await MainForm.Cliente.ActualizarConfiguracionAsync(
                chkCartasDobles.Checked,
                chkManual.Checked);
        }
    }
}

