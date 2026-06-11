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
            ConfigurarControles();
        }

        private void CargarFormasDeGanar()
        {
            cmbFormaDeGanar.Items.Clear();
            foreach (FormasdeGanar forma in Enum.GetValues(typeof(FormasdeGanar)))
                cmbFormaDeGanar.Items.Add(forma);
            cmbFormaDeGanar.Items.Add("Personalizado");
            cmbFormaDeGanar.SelectedIndex = 0;
        }

        private void ConfigurarControles()
        {
            nudIntervalo.Minimum = 1;
            nudIntervalo.Maximum = 30;
            nudIntervalo.Value = 5;

            nudTamañoTablero.Minimum = 3;
            nudTamañoTablero.Maximum = 10;
            nudTamañoTablero.Value = 4;

            nudCantidadTableros.Minimum = 1;
            nudCantidadTableros.Maximum = 4;
            nudCantidadTableros.Value = 1;

            pnlPatronPersonalizado.Visible = false;
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
            lblCodigoSala.Text = $"Sala: {MainForm.Cliente.CodigoSala}";
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

        private async void EnviarConfiguracion()
        {
            if (!MainForm.Cliente.EsHost) return;
            await MainForm.Cliente.ActualizarConfiguracionAsync(
                chkCartasDobles.Checked,
                chkManual.Checked,
                (int)nudTamañoTablero.Value,
                (int)nudCantidadTableros.Value);
        }

        // Dibuja la cuadrícula del patrón personalizado según el tamaño actual
        private bool[,] _patronCeldas = new bool[4, 4];

        private void DibujarCuadriculaPatron()
        {
            int n = (int)nudTamañoTablero.Value;
            _patronCeldas = new bool[n, n];
            pnlPatronPersonalizado.Controls.Clear();

            int margen = 20;
            int espacioDisponible = Math.Min(pnlPatronPersonalizado.Width, pnlPatronPersonalizado.Height) - margen * 2;
            int celdaSize = (espacioDisponible / n) - 3;

            for (int f = 0; f < n; f++)
            {
                for (int c = 0; c < n; c++)
                {
                    int fila = f, col = c;
                    var btn = new Button();
                    btn.Size = new Size(celdaSize, celdaSize);
                    btn.Location = new Point(margen + c * (celdaSize + 3), margen + f * (celdaSize + 3));
                    btn.BackColor = Color.White;
                    btn.ForeColor = Color.Black;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Color.Gray;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.Tag = (fila, col);
                    btn.Click += (s, e) =>
                    {
                        _patronCeldas[fila, col] = !_patronCeldas[fila, col];
                        btn.BackColor = _patronCeldas[fila, col] ? Color.Gold : Color.White;
                    };
                    pnlPatronPersonalizado.Controls.Add(btn);
                }
            }
        }

        private List<bool> ObtenerPatronComoLista()
        {
            int n = _patronCeldas.GetLength(0);
            var lista = new List<bool>();
            for (int f = 0; f < n; f++)
                for (int c = 0; c < n; c++)
                    lista.Add(_patronCeldas[f, c]);
            return lista;
        }

        private void cmbFormaDeGanar_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool esPersonalizado = cmbFormaDeGanar.SelectedItem?.ToString() == "Personalizado";
            pnlPatronPersonalizado.Visible = esPersonalizado;
            if (esPersonalizado)
                DibujarCuadriculaPatron();
        }

        private async void nudTamañoTablero_ValueChanged(object sender, EventArgs e)
        {
            if (cmbFormaDeGanar.SelectedItem?.ToString() == "Personalizado")
                DibujarCuadriculaPatron();
            await Task.Run(EnviarConfiguracion);
        }

        private async void nudCantidadTableros_ValueChanged(object sender, EventArgs e)
        {
            await Task.Run(EnviarConfiguracion);
        }

        private async void btnIniciarJuego_Click(object sender, EventArgs e)
        {
            try
            {
                btnIniciarJuego.Enabled = false;

                string formaDeGanar;
                List<bool>? patron = null;

                if (cmbFormaDeGanar.SelectedItem?.ToString() == "Personalizado")
                {
                    formaDeGanar = "Personalizado";
                    patron = ObtenerPatronComoLista();
                    if (!patron.Any(b => b))
                    {
                        MessageBox.Show("Debes seleccionar al menos una celda en el patrón personalizado.");
                        btnIniciarJuego.Enabled = true;
                        return;
                    }
                }
                else
                {
                    formaDeGanar = cmbFormaDeGanar.SelectedItem?.ToString() ?? "TableroCompleto";
                }

                await MainForm.Cliente.IniciarJuegoAsync(
                    (int)nudIntervalo.Value,
                    formaDeGanar,
                    (int)nudTamañoTablero.Value,
                    (int)nudCantidadTableros.Value,
                    patron);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar el juego: {ex.Message}");
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
                    MainForm.Cliente.JugadoresExistentes.Remove(id);
                ActualizarListaJugadores();
            });
        }

        private void OnJuegoIniciado_Recibido(string formaDeGanar)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new Action(() => OnJuegoIniciado?.Invoke()));
            else
                OnJuegoIniciado?.Invoke();
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

        private void MostrarControlesHost(bool esHost)
        {
            btnIniciarJuego.Visible = esHost;
            nudIntervalo.Visible = esHost;
            nudTamañoTablero.Visible = esHost;
            nudCantidadTableros.Visible = esHost;
            cmbFormaDeGanar.Visible = esHost;
            chkCartasDobles.Visible = esHost;
            chkManual.Visible = esHost;
            lblEsperando.Visible = !esHost;
        }

        private async void chkCartasDobles_CheckedChanged(object sender, EventArgs e)
            => await Task.Run(EnviarConfiguracion);

        private async void chkManual_CheckedChanged(object sender, EventArgs e)
            => await Task.Run(EnviarConfiguracion);

        private void lblEsperando_Click(object sender, EventArgs e) { }

        private void LobbyControl_Load(object sender, EventArgs e)
        {

        }
    }
}