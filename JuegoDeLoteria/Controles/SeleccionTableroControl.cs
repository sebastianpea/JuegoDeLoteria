using JuegoDeLoteria.Forms;
using JuegoDeLoteria.Juego;

namespace JuegoDeLoteria.Controles
{
    public partial class SeleccionTableroControl : UserControl
    {
        public event Action<List<Tablero>>? OnTablerosSeleccionados;

        private List<Tablero> _tablerosSeleccionados = new List<Tablero>();
        private MazoDeCartas _mazo = new MazoDeCartas();
        private int _tableroActual = 0;
        private int _cantidadTableros = 1;
        private PictureBox?[,] _celdas = new PictureBox?[4, 4];
        private Dictionary<(int, int), Carta> _cartasEnCeldas = new Dictionary<(int, int), Carta>();
        private int _N = 4; // tamaño actual del tablero

        public SeleccionTableroControl()
        {
            InitializeComponent();
        }
        public void InicializarSeleccion()
        {
            int n = MainForm.Cliente.TamañoTablero;
            int cantidad = MainForm.Cliente.CantidadTableros;

            _tableroActual = 0;
            _tablerosSeleccionados.Clear();
            _cantidadTableros = cantidad;
            _mazo = new MazoDeCartas();

            nudCantidadTableros.Value = cantidad;
            nudCantidadTableros.Enabled = false; // Lo define el host, no el jugador

            MostrarCartasDisponibles();
            DibujarTableroVacio(n);
            ActualizarInstrucciones();

            btnConfirmar.Enabled = false;
            btnAleatorio.Enabled = true;
        }

        private void ConfigurarNud()
        {
            nudCantidadTableros.Minimum = 1;
            nudCantidadTableros.Maximum = 4;
            nudCantidadTableros.Value = 1;
            btnConfirmar.Enabled = false;
            ActualizarInstrucciones();
        }

        private void MostrarCartasDisponibles()
        {
            flpCartasDisponibles.Controls.Clear();
            var todasLasCartas = _mazo.ObtenerTodasLasCartas();

            foreach (var carta in todasLasCartas)
            {
                var pb = new PictureBox();
                pb.Size = new Size(55, 55);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Image = carta.ObtenerImagen();
                pb.Tag = carta;
                pb.Cursor = Cursors.Hand;
                pb.Margin = new Padding(3);
                pb.MouseDown += CartaDisponible_MouseDown;
                flpCartasDisponibles.Controls.Add(pb);
            }
        }

        private void DibujarTableroVacio(int n = 4)
        {
            pnlTablero.Controls.Clear();
            _celdas = new PictureBox?[n, n];
            _cartasEnCeldas.Clear();
            _N = n;

            int disponible = Math.Min(pnlTablero.Width, pnlTablero.Height) - 20;
            int celdaSize = Math.Max(40, (disponible / n) - 4);

            for (int fila = 0; fila < n; fila++)
            {
                for (int col = 0; col < n; col++)
                {
                    var celda = new PictureBox();
                    celda.Size = new Size(celdaSize, celdaSize);
                    celda.Location = new Point(col * (celdaSize + 4), fila * (celdaSize + 4));
                    celda.SizeMode = PictureBoxSizeMode.Zoom;
                    celda.BorderStyle = BorderStyle.FixedSingle;
                    celda.BackColor = Color.White;
                    celda.Tag = (fila, col);
                    celda.AllowDrop = true;
                    celda.DragEnter += Celda_DragEnter;
                    celda.DragDrop += Celda_DragDrop;
                    celda.DragOver += Celda_DragOver;
                    celda.MouseDown += Celda_MouseDown;
                    pnlTablero.Controls.Add(celda);
                    _celdas[fila, col] = celda;
                }
            }
        }

        private void CartaDisponible_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is PictureBox pb && pb.Tag is Carta carta && pb.Enabled)
                pb.DoDragDrop(carta, DragDropEffects.Copy);
        }

        private void Celda_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is not PictureBox celda) return;
            if (celda.Tag is not (int fila, int col)) return;
            if (!_cartasEnCeldas.ContainsKey((fila, col))) return;

            var carta = _cartasEnCeldas[(fila, col)];
            _cartasEnCeldas.Remove((fila, col));
            celda.Image = null;
            celda.BackColor = Color.White;
            RestaurarCartaDisponible(carta);
            VerificarTableroCompleto();
            celda.DoDragDrop(carta, DragDropEffects.Copy);
        }

        private void Celda_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetData(typeof(Carta)) is Carta)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Celda_DragOver(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetData(typeof(Carta)) is Carta)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Celda_DragDrop(object? sender, DragEventArgs e)
        {
            if (sender is not PictureBox celda) return;
            if (celda.Tag is not (int fila, int col)) return;
            if (e.Data?.GetData(typeof(Carta)) is not Carta carta) return;

            if (CartaYaEnTablero(carta))
            {
                MessageBox.Show("Esa carta ya está en el tablero.");
                return;
            }

            if (_cartasEnCeldas.ContainsKey((fila, col)))
            {
                RestaurarCartaDisponible(_cartasEnCeldas[(fila, col)]);
                _cartasEnCeldas.Remove((fila, col));
            }

            _cartasEnCeldas[(fila, col)] = carta;
            celda.Image = carta.ObtenerImagen();
            celda.BackColor = Color.LightGoldenrodYellow;
            MarcarCartaUsada(carta);
            VerificarTableroCompleto();
        }

        private bool CartaYaEnTablero(Carta carta)
        {
            if (!MainForm.Cliente.PermitirCartasDobles)
                return _cartasEnCeldas.Values.Any(c => c.Id == carta.Id);

            int count = _cartasEnCeldas.Values.Count(c => c.Id == carta.Id);
            return count >= 2;
        }

        private void MarcarCartaUsada(Carta carta)
        {
            if (MainForm.Cliente.PermitirCartasDobles) return;
            foreach (Control control in flpCartasDisponibles.Controls)
            {
                if (control is PictureBox pb && pb.Tag is Carta c && c.Id == carta.Id)
                {
                    pb.BackColor = Color.LightGray;
                    pb.Enabled = false;
                }
            }
        }

        private void RestaurarCartaDisponible(Carta carta)
        {
            foreach (Control control in flpCartasDisponibles.Controls)
            {
                if (control is PictureBox pb && pb.Tag is Carta c && c.Id == carta.Id)
                {
                    pb.BackColor = Color.Transparent;
                    pb.Enabled = true;
                }
            }
        }

        private void VerificarTableroCompleto()
        {
            btnConfirmar.Enabled = _cartasEnCeldas.Count == _N * _N;
            ActualizarInstrucciones(_cartasEnCeldas.Count);
        }

        private void ActualizarInstrucciones(int colocadas = 0)
        {
            lblInstrucciones.Text = $"Tablero {_tableroActual + 1} de {(int)nudCantidadTableros.Value} — {colocadas}/16 cartas colocadas";
        }

        private void btnAleatorio_Click(object sender, EventArgs e)
        {
            DibujarTableroVacio(_N);
            int totalCeldas = _N * _N;

            foreach (Control control in flpCartasDisponibles.Controls)
            {
                control.BackColor = Color.Transparent;
                control.Enabled = true;
            }

            var todasLasCartas = _mazo.ObtenerTodasLasCartas();
            List<Carta> cartasAleatorias;

            if (MainForm.Cliente.PermitirCartasDobles)
            {
                var rng = new Random();
                cartasAleatorias = Enumerable.Range(0, 16)
                    .Select(_ => todasLasCartas[rng.Next(todasLasCartas.Count)])
                    .ToList();
            }
            else
            {
                cartasAleatorias = todasLasCartas
                    .OrderBy(c => Guid.NewGuid())
                    .Take(totalCeldas)
                    .ToList();
            }

            int index = 0;
            for (int fila = 0; fila < _N; fila++)
            {
                for (int col = 0; col < _N; col++)
                {
                    var carta = cartasAleatorias[index++];
                    var celda = _celdas[fila, col];
                    if (celda != null)
                    {
                        celda.Image = carta.ObtenerImagen();
                        celda.BackColor = Color.LightGoldenrodYellow;
                    }
                    _cartasEnCeldas[(fila, col)] = carta;

                    if (!MainForm.Cliente.PermitirCartasDobles)
                        MarcarCartaUsada(carta);
                }
            }

            VerificarTableroCompleto();
        }

        private bool TableroEsIdentico(List<Carta> nuevasCartas)
        {
            foreach (var tablero in _tablerosSeleccionados)
            {
                bool identico = true;
                for (int i = 0; i < 16; i++)
                {
                    if (tablero.Cartas[i].Id != nuevasCartas[i].Id)
                    {
                        identico = false;
                        break;
                    }
                }
                if (identico) return true;
            }
            return false;
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            var cartas = new List<Carta>();
            for (int fila = 0; fila < _N; fila++)
                for (int col = 0; col < _N; col++)
                    cartas.Add(_cartasEnCeldas[(fila, col)]);

            if (TableroEsIdentico(cartas))
            {
                MessageBox.Show("Este tablero es idéntico a uno que ya seleccionaste. Por favor elige cartas diferentes.");
                return;
            }

            _tablerosSeleccionados.Add(new Tablero(cartas));
            _tableroActual++;
            _cantidadTableros = (int)nudCantidadTableros.Value;

            if (_tableroActual < _cantidadTableros)
            {
                DibujarTableroVacio();
                foreach (Control control in flpCartasDisponibles.Controls)
                {
                    control.BackColor = Color.Transparent;
                    control.Enabled = true;
                }
                btnConfirmar.Enabled = false;
                ActualizarInstrucciones();
            }
            else
            {
                btnConfirmar.Enabled = false;
                btnAleatorio.Enabled = false;
                nudCantidadTableros.Enabled = false;
                lblInstrucciones.Text = "Esperando a los demás jugadores...";

                var todosLosIds = _tablerosSeleccionados
                    .SelectMany(t => t.Cartas.Select(c => c.Id))
                    .ToList();
                await MainForm.Cliente.EnviarTableroAsync(todosLosIds);

                OnTablerosSeleccionados?.Invoke(_tablerosSeleccionados);
                await MainForm.Cliente.JugadorListoAsync();
            }
        }
        private void btnGuardarTablero_Click(object sender, EventArgs e)
        {
            if (_cartasEnCeldas.Count != 16)
            {
                MessageBox.Show("El tablero debe estar completo para guardarlo.");
                return;
            }

            using var dialog = new SaveFileDialog();
            dialog.Filter = "Tablero JSON|*.json";
            dialog.Title = "Guardar tablero";
            if (dialog.ShowDialog() != DialogResult.OK) return;

            var data = new List<object>();
            for (int fila = 0; fila < 4; fila++)
                for (int col = 0; col < 4; col++)
                {
                    var carta = _cartasEnCeldas[(fila, col)];
                    data.Add(new { fila, col, carta.Id, carta.Nombre });
                }

            string json = System.Text.Json.JsonSerializer.Serialize(data,
                new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dialog.FileName, json);
            MessageBox.Show("Tablero guardado.");
        }

        private void btnCargarTablero_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            dialog.Filter = "Tablero JSON|*.json";
            dialog.Title = "Cargar tablero";
            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                string json = File.ReadAllText(dialog.FileName);
                var data = System.Text.Json.JsonSerializer.Deserialize<List<System.Text.Json.JsonElement>>(json);
                if (data == null || data.Count != 16)
                {
                    MessageBox.Show("El archivo no es válido.");
                    return;
                }

                DibujarTableroVacio();
                foreach (Control control in flpCartasDisponibles.Controls)
                {
                    control.BackColor = Color.Transparent;
                    control.Enabled = true;
                }

                var todasLasCartas = _mazo.ObtenerTodasLasCartas();

                foreach (var item in data)
                {
                    int fila = item.GetProperty("fila").GetInt32();
                    int col = item.GetProperty("col").GetInt32();
                    int id = item.GetProperty("Id").GetInt32();

                    var carta = todasLasCartas.FirstOrDefault(c => c.Id == id);
                    if (carta == null) continue;

                    var celda = _celdas[fila, col];
                    if (celda == null) continue;

                    _cartasEnCeldas[(fila, col)] = carta;
                    celda.Image = carta.ObtenerImagen();
                    celda.BackColor = Color.LightGoldenrodYellow;
                    MarcarCartaUsada(carta);
                }

                VerificarTableroCompleto();
                MessageBox.Show("Tablero cargado.");
            }
            catch
            {
                MessageBox.Show("Error al leer el archivo.");
            }
        }
    }
}