using JuegoDeLoteria.Forms;
using JuegoDeLoteria.Juego;
using JuegoDeLoteria.Managers;

namespace JuegoDeLoteria.Controles
{
    public partial class JuegoControl : UserControl
    {
        private List<Tablero> tableros = new List<Tablero>();
        private FormasdeGanar formaDeGanar; 
        private Point offsetArrastre;
        private System.Windows.Forms.Timer cuentaRegresiva;
        private int segundosRestantes; 
        private int intervaloSegundos; 
        public bool EsHost { get; set; } 
        public event Action<string>? OnJuegoTerminado;
        private List<bool>? _patronPersonalizado = null;
        private List<(int id, string nombre)> _historialCartas = new List<(int, string)>();
        private int _indiceHistorial = -1;

        public JuegoControl()
        {
            InitializeComponent();
            this.BackColor = Color.Black; 
        }
        public void InicializarJuego(List<Tablero> tableros, int intervaloSegundos,
            FormasdeGanar formaSeleccionada, List<bool>? patronPersonalizado = null)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(() => InicializarJuego(tableros, intervaloSegundos,
                    formaSeleccionada, patronPersonalizado));
                return;
            }
            if (tableros == null)
            {
                MessageBox.Show("Error: No se recibieron los tableros.");
                return;
            }
            DesregistrarEventosSignalR();

            MainForm.Cliente.OnDesempateIniciado += OnDesempateIniciado;

            this.tableros = tableros;
            this.intervaloSegundos = intervaloSegundos;
            this.formaDeGanar = formaSeleccionada;
            this._patronPersonalizado = patronPersonalizado;

            _historialCartas.Clear();
            _indiceHistorial = -1;
            btnRetroceder.Enabled = false;
            btnAdelantar.Enabled = false;

            pnlTableros.Controls.Clear(); 
            flpHistorial.Controls.Clear(); 
            btnLoteria.Enabled = true; 
            lblCuentaRegresiva.Text = ""; 
            lblNombreCartaActual.Text = "Esperando a que todos estén listos..."; 

            cuentaRegresiva = new System.Windows.Forms.Timer(); 
            cuentaRegresiva.Interval = 1000; 
            cuentaRegresiva.Tick += CuentaRegresiva_Tick; 

            DibujarTableros(); 
            CrearFichas(); 

            //signalR

            MainForm.Cliente.OnCartaMencionada += OnCartaMencionada; 
            MainForm.Cliente.OnVerificarLoteria += OnVerificarLoteria; 
            MainForm.Cliente.OnJuegoTerminado += OnJuegoTerminado_Recibido;
            MainForm.Cliente.OnActualizarListos += OnActualizarListos; 
            MainForm.Cliente.OnConteoIniciado += OnConteoIniciado; 

            bool esHost = MainForm.Cliente.EsHost; 
            btnPausar.Visible = esHost && !MainForm.Cliente.EsManual; 
            btnMasFast.Visible = esHost && !MainForm.Cliente.EsManual; 
            btnMasSlow.Visible = esHost && !MainForm.Cliente.EsManual;
            btnSiguienteCarta.Visible = esHost && MainForm.Cliente.EsManual;

            MainForm.Cliente.OnJuegoPausado += OnJuegoPausado; 
            MainForm.Cliente.OnJuegoReanudado += OnJuegoReanudado; 
            MainForm.Cliente.OnCambiarVelocidad += OnCambiarVelocidad; 

            chatControl2.InicializarChat(); 
        }

        private bool _pausado = false; 

        private void OnActualizarListos(int listos, int total)
        {
            this.Invoke(() =>
            {
                lblNombreCartaActual.Text = $"Esperando a que todos estén listos... ({listos}/{total})"; 
            });
        }

        private void OnConteoIniciado()
        {
            this.Invoke(() =>
            {
                lblNombreCartaActual.Text = "¡El juego ha comenzado!"; 
            });
        }

        private void CuentaRegresiva_Tick(object? sender, EventArgs e)
        {
            segundosRestantes--; 
            lblCuentaRegresiva.Text = $"Próxima carta en: {segundosRestantes}s"; 

            if (segundosRestantes <= 0)
                cuentaRegresiva.Stop();
        }

        private void DibujarTableros()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(() => DibujarTableros());
                return;
            }

            pnlTableros.Controls.Clear();

            foreach (var tablero in tableros)
            {
                var tableroControl = new TableroControl(tablero);
                tableroControl.Margin = new Padding(10); 
                pnlTableros.Controls.Add(tableroControl); 
            }
        }

        private void CrearFichas()
        {
        }

        private void Ficha_MouseDown(object? sender, MouseEventArgs e)
        {
        }

        private void OnCartaMencionada(int id, string nombre)
        {
            this.Invoke((Delegate)(() =>
            {
                var mazo = new MazoDeCartas();
                var carta = mazo.ObtenerTodasLasCartas().FirstOrDefault(c => c.Id == id);

                if (carta != null)
                {
                    // Guardar en historial
                    _historialCartas.Add((id, carta.Nombre));
                    _indiceHistorial = _historialCartas.Count - 1;
                    btnRetroceder.Enabled = _historialCartas.Count > 1;
                    btnAdelantar.Enabled = false;

                    MostrarCartaEnPantalla(carta);
                    AudioManager.HablarCarta(carta.Nombre);

                    var pbHistorial = new PictureBox();
                    pbHistorial.Size = new Size(50, 50);
                    pbHistorial.SizeMode = PictureBoxSizeMode.Zoom;
                    pbHistorial.Image = carta.ObtenerImagen();
                    flpHistorial.Controls.Add(pbHistorial);
                }

                foreach (Control c in pnlTableros.Controls)
                    if (c is TableroControl tableroControl)
                        tableroControl.MarcarCartaMencionada(id);

                segundosRestantes = intervaloSegundos;
                cuentaRegresiva.Stop();
                cuentaRegresiva.Start();
                lblCuentaRegresiva.Text = $"Próxima carta en: {segundosRestantes}s";
            }));
        }

        private void MostrarCartaEnPantalla(Carta carta)
        {
            var mazo = new MazoDeCartas(); // solo para obtener imagen si no la tenemos
            pbCartaActual.Image = carta.ObtenerImagen();
            lblNombreCartaActual.Text = carta.Nombre;
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
                // Primero guardamos snapshot del marcado ANTES de VerificarVictoria
                // porque ese método limpia Marcado internamente
                var snapshots = tableros.Select(t =>
                {
                    var snap = new bool[t.Tamaño, t.Tamaño];
                    Array.Copy(t.Marcado, snap, t.Marcado.Length);
                    return snap;
                }).ToList();

                bool esValido = tableros.Any(t =>
                {
                    t.VerificarVictoria(cartasMencionadas, this.formaDeGanar, _patronPersonalizado);
                    return _patronPersonalizado != null
                        ? t.RevisarPatronPersonalizado(_patronPersonalizado)
                        : t.RevisarVictoria(this.formaDeGanar);
                });

                if (!esValido)
                {
                    // Obtener los TableroControl en el mismo orden que la lista tableros
                    var tablerosControl = pnlTableros.Controls
                        .OfType<TableroControl>()
                        .ToList();

                    for (int t = 0; t < tableros.Count && t < tablerosControl.Count; t++)
                    {
                        var tablero = tableros[t];
                        var snap = snapshots[t];
                        var tc = tablerosControl[t];

                        for (int f = 0; f < tablero.Tamaño; f++)
                        {
                            for (int c = 0; c < tablero.Tamaño; c++)
                            {
                                // Usar snapshot: estaba marcado Y la carta no se ha llamado
                                if (snap[f, c] &&
                                    !cartasMencionadas.Contains(tablero.Cartas[f * tablero.Tamaño + c].Id))
                                {
                                    tc.MarcarCeldaInvalida(f, c);
                                }
                            }
                        }
                    }

                    string cartasInvalidas = ObtenerNombresCartasInvalidas(cartasMencionadas, snapshots);
                    MessageBox.Show(
                        $" Tu Lotería no es válida.\n\nCartas marcadas que aún no se han llamado:\n{cartasInvalidas}\n\n(Marcadas en rojo en tu tablero)",
                        "Lotería inválida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    btnLoteria.Enabled = true;
                }

                await MainForm.Cliente.EnviarResultadoAsync(esValido);
            });
        }

        private string ObtenerNombresCartasInvalidas(List<int> cartasMencionadas, List<bool[,]> snapshots)
        {
            var nombres = new List<string>();
            for (int t = 0; t < tableros.Count; t++)
            {
                var tablero = tableros[t];
                var snap = snapshots[t];
                for (int f = 0; f < tablero.Tamaño; f++)
                    for (int c = 0; c < tablero.Tamaño; c++)
                    {
                        var carta = tablero.Cartas[f * tablero.Tamaño + c];
                        if (snap[f, c] && !cartasMencionadas.Contains(carta.Id))
                            nombres.Add(carta.Nombre);
                    }
            }
            return string.Join("\n", nombres.Distinct());
        }

        private void OnJuegoTerminado_Recibido(string ganador, Dictionary<string, int> puntajes)
        {
            this.Invoke(() =>
            {
                DesregistrarEventosSignalR();
                chatControl2.DetenerChat(); 
                OnJuegoTerminado?.Invoke(ganador); 
            });
        }

        private void DesregistrarEventosSignalR()
        {
            MainForm.Cliente.OnDesempateIniciado -= OnDesempateIniciado; 
            if (cuentaRegresiva != null) cuentaRegresiva.Stop(); 
            MainForm.Cliente.OnCartaMencionada -= OnCartaMencionada; 
            MainForm.Cliente.OnVerificarLoteria -= OnVerificarLoteria; 
            MainForm.Cliente.OnJuegoTerminado -= OnJuegoTerminado_Recibido;
            MainForm.Cliente.OnActualizarListos -= OnActualizarListos;
            MainForm.Cliente.OnConteoIniciado -= OnConteoIniciado;
            MainForm.Cliente.OnJuegoPausado -= OnJuegoPausado; 
            MainForm.Cliente.OnJuegoReanudado -= OnJuegoReanudado; 
            MainForm.Cliente.OnCambiarVelocidad -= OnCambiarVelocidad; 
        }

        private void lblNombreCartaActual_Click(object sender, EventArgs e) { }
        private void pbCartaActual_Click(object sender, EventArgs e) { }

        private async void btnPausar_Click(object sender, EventArgs e)
        {
            if (_pausado)
                await MainForm.Cliente.ReanudarJuegoAsync();
            else
                await MainForm.Cliente.PausarJuegoAsync(); 
        }

        private async void btnMasFast_Click(object sender, EventArgs e)
        {
            int nuevoIntervalo = Math.Max(1, intervaloSegundos - 1);
            await MainForm.Cliente.CambiarVelocidadAsync(nuevoIntervalo); 
        }

        private async void btnMasSlow_Click(object sender, EventArgs e)
        {
            int nuevoIntervalo = Math.Min(30, intervaloSegundos + 1); 
            await MainForm.Cliente.CambiarVelocidadAsync(nuevoIntervalo); 
        }

        private async void btnSiguienteCarta_Click(object sender, EventArgs e)
        {
            await MainForm.Cliente.SolicitarCartaAsync(); 
        }

        private void OnJuegoPausado()
        {
            this.Invoke(() =>
            {
                _pausado = true; 
                btnPausar.Text = "Reanudar"; 
                lblCuentaRegresiva.Text = "Pausado"; 
                if (cuentaRegresiva != null) cuentaRegresiva.Stop(); 
            });
        }

        private void OnJuegoReanudado()
        {
            this.Invoke(() =>
            {
                _pausado = false; 
                btnPausar.Text = "Pausar"; 
                if (cuentaRegresiva != null) cuentaRegresiva.Start();
            });
        }

        private void OnCambiarVelocidad(int nuevoIntervalo)
        {
            this.Invoke(() =>
            {
                intervaloSegundos = nuevoIntervalo; 
                lblCuentaRegresiva.Text = $"Velocidad: {nuevoIntervalo}s"; 
            });
        }

        private void OnDesempateIniciado(string jugadores)
        {
            this.Invoke(() =>
            {
                MessageBox.Show($"¡Empate entre: {jugadores}!\nSe seguirán dando cartas para desempatar."); 
                btnLoteria.Enabled = true; 
            });
        }
        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            if (_indiceHistorial <= 0) return;
            _indiceHistorial--;
            MostrarCartaDelHistorial(_indiceHistorial);
            btnAdelantar.Enabled = true;
            btnRetroceder.Enabled = _indiceHistorial > 0;
        }

        private void btnAdelantar_Click(object sender, EventArgs e)
        {
            if (_indiceHistorial >= _historialCartas.Count - 1) return;
            _indiceHistorial++;
            MostrarCartaDelHistorial(_indiceHistorial);
            btnRetroceder.Enabled = true;
            btnAdelantar.Enabled = _indiceHistorial < _historialCartas.Count - 1;
        }

        private void MostrarCartaDelHistorial(int indice)
        {
            var (id, nombre) = _historialCartas[indice];
            var mazo = new MazoDeCartas();
            var carta = mazo.ObtenerTodasLasCartas().FirstOrDefault(c => c.Id == id);
            if (carta != null)
            {
                pbCartaActual.Image = carta.ObtenerImagen();
                lblNombreCartaActual.Text = $"[{indice + 1}/{_historialCartas.Count}] {carta.Nombre}";
            }
        }
    }
}