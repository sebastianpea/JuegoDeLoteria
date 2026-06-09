using JuegoDeLoteria.Forms;
using JuegoDeLoteria.Juego;
using JuegoDeLoteria.Managers;

namespace JuegoDeLoteria.Controles
{
    public partial class JuegoControl : UserControl
    {
        public event Action<string>? OnJuegoTerminado;

        private List<Tablero> tableros = new List<Tablero>();
        private FormasdeGanar formaDeGanar;
        private Point offsetArrastre;
        private System.Windows.Forms.Timer cuentaRegresiva;
        private int segundosRestantes;
        private int intervaloSegundos;
        public bool EsHost { get; set; }

        public JuegoControl()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
        }

        public void InicializarJuego(string formaDeGanar, List<Tablero> tableros, int intervaloSegundos)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(() => InicializarJuego(formaDeGanar, tableros, intervaloSegundos));
                return;
            }

            MainForm.Cliente.OnDesempateIniciado += OnDesempateIniciado;

            this.tableros = tableros;
            this.formaDeGanar = Enum.Parse<FormasdeGanar>(formaDeGanar);
            this.intervaloSegundos = intervaloSegundos;

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
            this.Invoke(() =>
            {
                var mazo = new MazoDeCartas();
                var carta = mazo.ObtenerTodasLasCartas().FirstOrDefault(c => c.Id == id);

                if (carta != null)
                {
                    pbCartaActual.Image = carta.ObtenerImagen();
                    lblNombreCartaActual.Text = carta.Nombre;

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

                if (!esValido)
                {
                    foreach (var tablero in tableros)
                    {
                        for (int f = 0; f < 4; f++)
                        {
                            for (int c = 0; c < 4; c++)
                            {
                                if (tablero.Marcado[f, c] &&
                                    !cartasMencionadas.Contains(tablero.Cartas[f * 4 + c].Id))
                                {
                                    foreach (Control ctrl in pnlTableros.Controls)
                                    {
                                        if (ctrl is TableroControl tc)
                                            tc.MarcarCeldaInvalida(f, c);
                                    }
                                }
                            }
                        }
                    }

                    string cartasInvalidas = ObtenerNombresCartasInvalidas(cartasMencionadas);
                    MessageBox.Show($"Tu Lotería no es válida.\n\nCartas marcadas que aún no se han llamado:\n{cartasInvalidas}");
                    btnLoteria.Enabled = true;
                }

                await MainForm.Cliente.EnviarResultadoAsync(esValido);
            });
        }

        private string ObtenerNombresCartasInvalidas(List<int> cartasMencionadas)
        {
            var nombres = new List<string>();
            foreach (var tablero in tableros)
            {
                for (int f = 0; f < 4; f++)
                {
                    for (int c = 0; c < 4; c++)
                    {
                        var carta = tablero.Cartas[f * 4 + c];
                        if (tablero.Marcado[f, c] && !cartasMencionadas.Contains(carta.Id))
                            nombres.Add(carta.Nombre);
                    }
                }
            }
            return string.Join("\n", nombres.Distinct());
        }

        private void OnJuegoTerminado_Recibido(string ganador)
        {
            this.Invoke(() =>
            {
                MainForm.Cliente.OnDesempateIniciado -= OnDesempateIniciado;
                cuentaRegresiva.Stop();
                MainForm.Cliente.OnCartaMencionada -= OnCartaMencionada;
                MainForm.Cliente.OnVerificarLoteria -= OnVerificarLoteria;
                MainForm.Cliente.OnJuegoTerminado -= OnJuegoTerminado_Recibido;
                MainForm.Cliente.OnActualizarListos -= OnActualizarListos;
                MainForm.Cliente.OnConteoIniciado -= OnConteoIniciado;
                MainForm.Cliente.OnJuegoPausado -= OnJuegoPausado;
                MainForm.Cliente.OnJuegoReanudado -= OnJuegoReanudado;
                MainForm.Cliente.OnCambiarVelocidad -= OnCambiarVelocidad;
                chatControl2.DetenerChat();
                OnJuegoTerminado?.Invoke(ganador);
            });
        }

        private void lblNombreCartaActual_Click(object sender, EventArgs e)
        {

        }

        private void pbCartaActual_Click(object sender, EventArgs e)
        {

        }

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
                cuentaRegresiva.Stop();
            });
        }

        private void OnJuegoReanudado()
        {
            this.Invoke(() =>
            {
                _pausado = false;
                btnPausar.Text = "Pausar";
                cuentaRegresiva.Start();
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
    }
}