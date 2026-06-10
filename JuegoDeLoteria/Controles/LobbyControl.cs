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
            MainForm.Cliente.OnJugadorUnido -= OnJugadorUnido;
            MainForm.Cliente.OnJugadorSalio -= OnJugadorSalio;
            MainForm.Cliente.OnJuegoIniciado -= OnJuegoIniciado_Recibido;
            MainForm.Cliente.OnNuevoHost -= OnNuevoHost;

            MainForm.Cliente.OnJugadorUnido += OnJugadorUnido;
            MainForm.Cliente.OnJugadorSalio += OnJugadorSalio;
            MainForm.Cliente.OnJuegoIniciado += OnJuegoIniciado_Recibido;
            MainForm.Cliente.OnNuevoHost += OnNuevoHost;

            MostrarControlesHost(MainForm.Cliente.EsHost);
            ActualizarListaJugadores();
            chatControl1.InicializarChat();
        }

        private void ActualizarListaJugadores()
        {
            lstJugadores.Items.Clear(); 
            var puntajes = MainForm.Cliente.Puntajes; 

            lstJugadores.Items.Add(FormatearJugador("(Tú) " + MainForm.Cliente.NombreJugador, puntajes)); 

            foreach (var jugador in MainForm.Cliente.JugadoresExistentes)
                lstJugadores.Items.Add(FormatearJugador(jugador.Value, puntajes)); 
        }

        private string FormatearJugador(string nombre, Dictionary<string, int> puntajes)
        {
            string nombreLimpio = nombre.Replace("(Tú) ", ""); 
            int puntos = puntajes.ContainsKey(nombreLimpio) ? puntajes[nombreLimpio] : 0;
            return $"{nombre} - {puntos}pts";
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
            try
            {
                btnIniciarJuego.Enabled = false;
                int intervalo = (int)nudIntervalo.Value;
                string formaDeGanar = cmbFormaDeGanar.SelectedItem?.ToString() ?? "TableroCompleto";
                await MainForm.Cliente.IniciarJuegoAsync(intervalo, formaDeGanar);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar el juego: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIniciarJuego.Enabled = true;
            }
        }

        private void OnJugadorUnido(string id, string nombre)
        {
            this.Invoke(() =>
            {
                MainForm.Cliente.JugadoresExistentes[id] = nombre;
                ActualizarListaJugadores();
            });
        }

        private void OnJugadorSalio(string id)
        {
            this.Invoke(() =>
            {
                if (MainForm.Cliente.JugadoresExistentes.ContainsKey(id)) 
                {
                    MainForm.Cliente.JugadoresExistentes.Remove(id); 
                }
                ActualizarListaJugadores();
            });
        }

        private void OnJuegoIniciado_Recibido(string formaDeGanar)
        {
            if (this.InvokeRequired) 
            {
                this.BeginInvoke(new Action(() => OnJuegoIniciado?.Invoke()));
            }
            else
            {
                OnJuegoIniciado?.Invoke(); 
            }
        }

        private void OnNuevoHost(string id)
        {
            this.Invoke(() => 
            {
                if (id == MainForm.Cliente.ConnectionId)
                {
                    MainForm.Cliente.EsHost = true; 
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