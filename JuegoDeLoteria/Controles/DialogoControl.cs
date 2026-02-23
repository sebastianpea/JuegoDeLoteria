namespace JuegoDeLoteria.Controles
{
    public partial class DialogoControl : UserControl
    {
        public event Action? OnTerminado;

        private List<string> lineasTutorial = new List<string>
        {
            "hola",
            "acompañar",
            "ya sabes como se juega?"
        };

        private List<string> lineasExplicacion = new List<string>
        {
            "pues",
            "que",
            "wei"
        };

        private int indiceActual = 0;
        private bool mostandoExplicacion = false;
        private System.Windows.Forms.Timer timerTexto;
        private string textoCompleto = "";
        private int indiceLetra = 0;

        public DialogoControl()
        {
            InitializeComponent();
            btnSi.Visible = false;
            btnNo.Visible = false;
            btnContinuar.Visible = false;
            timerTexto = new System.Windows.Forms.Timer();
            timerTexto.Interval = 30;
            timerTexto.Tick += TimerTexto_Tick;
        }

        public void IniciarDialogo()
        {
            indiceActual = 0;
            MostrarLinea(lineasTutorial[indiceActual]);
        }

        private void MostrarLinea(string texto)
        {
            textoCompleto = texto;
            indiceLetra = 0;
            lblDialogo.Text = "";
            btnContinuar.Visible = false;
            btnSi.Visible = false;
            btnNo.Visible = false;
            timerTexto.Start();
        }

        private void TimerTexto_Tick(object? sender, EventArgs e)
        {
            if (indiceLetra < textoCompleto.Length)
            {
                lblDialogo.Text += textoCompleto[indiceLetra];
                indiceLetra++;
            }
            else
            {
                timerTexto.Stop();

                // si estamos mostrando el tutorial y es la última línea, mostramos las opciones
                if (!mostandoExplicacion && indiceActual == lineasTutorial.Count - 1)
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
            indiceActual++;
            List<string> lineas = mostandoExplicacion ? lineasExplicacion : lineasTutorial;

            if (indiceActual < lineas.Count)
            {
                MostrarLinea(lineas[indiceActual]);
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
            mostandoExplicacion = true;
            indiceActual = 0;
            MostrarLinea(lineasExplicacion[indiceActual]);
        }
    }
}
