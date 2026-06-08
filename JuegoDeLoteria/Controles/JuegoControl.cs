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

            chatControl2.InicializarChat();
        }

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
            if (this.InvokeRequired)
            {
                this.Invoke(() => CrearFichas());
                return;
            }

            pnlFichas.Controls.Clear();
            for (int i = 0; i < 20; i++)
            {
                var ficha = new PictureBox();
                ficha.Size = new Size(50, 50);
                ficha.SizeMode = PictureBoxSizeMode.Zoom;
                ficha.Image = Properties.Resources.ficha;
                ficha.BackColor = Color.Transparent;
                ficha.Cursor = Cursors.Hand;
                ficha.MouseDown += Ficha_MouseDown;
                pnlFichas.Controls.Add(ficha);
            }
        }

        private void Ficha_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is PictureBox ficha)
            {
                offsetArrastre = e.Location;
                ficha.DoDragDrop(ficha, DragDropEffects.Move);
            }
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

                await MainForm.Cliente.EnviarResultadoAsync(esValido);

                if (!esValido)
                {
                    btnLoteria.Enabled = true;
                    MessageBox.Show("Tu Lotería no es válida, continúa jugando.");
                }
            });
        }

        private void OnJuegoTerminado_Recibido(string ganador)
        {
            this.Invoke(() =>
            {
                cuentaRegresiva.Stop();
                MainForm.Cliente.OnCartaMencionada -= OnCartaMencionada;
                MainForm.Cliente.OnVerificarLoteria -= OnVerificarLoteria;
                MainForm.Cliente.OnJuegoTerminado -= OnJuegoTerminado_Recibido;
                MainForm.Cliente.OnActualizarListos -= OnActualizarListos;
                MainForm.Cliente.OnConteoIniciado -= OnConteoIniciado;
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
    }
}