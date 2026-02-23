namespace JuegoDeLoteria.Controles
{
    public partial class DialogoControl : UserControl
    {
        public event Action? OnTerminado;

        private List<string> _lineasTutorial = new List<string>
        {
            "¡Bienvenido a la Lotería!",
            "Soy tu guía para este juego.",
            "¿Ya conoces cómo se juega la Lotería?"
        };

        private List<string> _lineasExplicacion = new List<string>
        {
            "La Lotería es un juego de azar.",
            "Cada jugador tiene un tablero con imágenes.",
            "El servidor irá llamando cartas una por una.",
            "Cuando la imagen aparezca en tu tablero, pon una ficha.",
            "¡El primero en completar el patrón gana!",
            "¡Buena suerte!"
        };

        private int _indiceActual = 0;
        private bool _mostandoExplicacion = false;
        private System.Windows.Forms.Timer _timerTexto;
        private string _textoCompleto = "";
        private int _indiceLetra = 0;

        public DialogoControl()
        {
            InitializeComponent();
            btnSi.Visible = false;
            btnNo.Visible = false;
            btnContinuar.Visible = false;
            _timerTexto = new System.Windows.Forms.Timer();
            _timerTexto.Interval = 30;
            _timerTexto.Tick += TimerTexto_Tick;
        }

        public void IniciarDialogo()
        {
            _indiceActual = 0;
            MostrarLinea(_lineasTutorial[_indiceActual]);
        }

        private void MostrarLinea(string texto)
        {
            _textoCompleto = texto;
            _indiceLetra = 0;
            lblDialogo.Text = "";
            btnContinuar.Visible = false;
            btnSi.Visible = false;
            btnNo.Visible = false;
            _timerTexto.Start();
        }

        private void TimerTexto_Tick(object? sender, EventArgs e)
        {
            if (_indiceLetra < _textoCompleto.Length)
            {
                lblDialogo.Text += _textoCompleto[_indiceLetra];
                _indiceLetra++;
            }
            else
            {
                _timerTexto.Stop();
                // Show the right buttons depending on which line we're on
                if (!_mostandoExplicacion && _indiceActual == _lineasTutorial.Count - 1)
                {
                    btnSi.Visible = true;
                    btnNo.Visible = true;
                }
                else
                {
                    btnContinuar.Visible = true;
                }
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            _indiceActual++;
            List<string> lineas = _mostandoExplicacion ? _lineasExplicacion : _lineasTutorial;

            if (_indiceActual < lineas.Count)
            {
                MostrarLinea(lineas[_indiceActual]);
            }
            else
            {
                OnTerminado?.Invoke();
            }
        }

        private void btnSi_Click(object sender, EventArgs e)
        {
            OnTerminado?.Invoke();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            _mostandoExplicacion = true;
            _indiceActual = 0;
            MostrarLinea(_lineasExplicacion[_indiceActual]);
        }
    }
}
