using JuegoDeLoteria.Forms;
using JuegoDeLoteria.Juego;

namespace JuegoDeLoteria.Controles
{
    public partial class JuegoControl : UserControl
    {
        public event Action<string>? OnJuegoTerminado;

        private List<Tablero> tableros = new List<Tablero>();
        private FormasdeGanar formaDeGanar;
        private PictureBox? fichaArrastrada = null;
        private Point offsetArrastre;
        private System.Windows.Forms.Timer cuentaRegresiva;
        private int segundosRestantes;
        private int intervaloSegundos;

        public JuegoControl()
        {
            InitializeComponent();
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
            lblNombreCartaActual.Text = "Esperando primera carta...";

            cuentaRegresiva = new System.Windows.Forms.Timer();
            cuentaRegresiva.Interval = 1000;
            cuentaRegresiva.Tick += CuentaRegresiva_Tick;

            DibujarTableros();
            CrearFichas();

            MainForm.Cliente.OnCartaMencionada += OnCartaMencionada;
            MainForm.Cliente.OnVerificarLoteria += OnVerificarLoteria;
            MainForm.Cliente.OnJuegoTerminado += OnJuegoTerminado_Recibido;
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

            foreach (var tablero in tableros)
            {
                var panelTablero = new Panel();
                panelTablero.Width = 220;
                panelTablero.Height = 220;
                panelTablero.Margin = new Padding(10);

                for (int fila = 0; fila < tablero.Tamaño; fila++)
                {
                    for (int col = 0; col < tablero.Tamaño; col++)
                    {
                        var carta = tablero.Cartas[fila * tablero.Tamaño + col];
                        var pb = new PictureBox();
                        pb.Size = new Size(50, 50);
                        pb.Location = new Point(col * 55, fila * 55);
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        pb.Image = carta.ObtenerImagen();
                        pb.Tag = (tablero, fila, col);
                        pb.AllowDrop = true;
                        pb.DragEnter += Celda_DragEnter;
                        pb.DragDrop += Celda_DragDrop;
                        pb.DragOver += Celda_DragOver;
                        panelTablero.Controls.Add(pb);
                    }
                }

                pnlTableros.Controls.Add(panelTablero);
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
                ficha.Size = new Size(40, 40);
                ficha.BackColor = Color.Gold;
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

        private void Celda_DragEnter(object? sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Celda_DragOver(object? sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Celda_DragDrop(object? sender, DragEventArgs e)
        {
            if (sender is PictureBox celda && celda.Tag is (Tablero tablero, int fila, int col))
            {
                if (tablero.Marcado[fila, col])
                {
                    tablero.QuitarFicha(fila, col);
                    celda.BackColor = Color.Transparent;
                }
                else
                {
                    tablero.PonerFicha(fila, col);
                    celda.BackColor = Color.Gold;
                }
            }
        }

        private void OnCartaMencionada(int id, string nombre)
        {
            this.Invoke(() =>
            {
                var carta = new Carta(id, nombre, nombre.ToLower().Replace(" ", "_"));
                pbCartaActual.Image = carta.ObtenerImagen();
                lblNombreCartaActual.Text = nombre;

                var pbHistorial = new PictureBox();
                pbHistorial.Size = new Size(40, 40);
                pbHistorial.SizeMode = PictureBoxSizeMode.Zoom;
                pbHistorial.Image = carta.ObtenerImagen();
                flpHistorial.Controls.Add(pbHistorial);

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
                OnJuegoTerminado?.Invoke(ganador);
            });
        }
    }
}