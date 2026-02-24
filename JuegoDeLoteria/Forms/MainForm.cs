using JuegoDeLoteria.Controles;
using JuegoDeLoteria.Redes;

namespace JuegoDeLoteria.Forms
{
    public partial class MainForm : Form
    {
        private MenuControl menuControl;
        private DialogoControl dialogoControl;
        private UnirseControl unirseControl;
        private LobbyControl lobbyControl;
        private SeleccionTableroControl seleccionTableroControl;
        private JuegoControl juegoControl;
        private PostJuegoControl postJuegoControl;
        private ConfiguracionControl configuracionControl;

        public static ClienteDeJuego Cliente = new ClienteDeJuego();

        public MainForm()
        {
            InitializeComponent();
            InicializarControles();
            MostrarControl(menuControl);
        }

        private void InicializarControles()
        {
            menuControl = new MenuControl();
            dialogoControl = new DialogoControl();
            unirseControl = new UnirseControl();
            lobbyControl = new LobbyControl();
            seleccionTableroControl = new SeleccionTableroControl();
            juegoControl = new JuegoControl();
            postJuegoControl = new PostJuegoControl();
            configuracionControl = new ConfiguracionControl();

            foreach (Control control in new Control[]
            {
                menuControl, dialogoControl, unirseControl,
                lobbyControl, seleccionTableroControl, juegoControl,
                postJuegoControl, configuracionControl
            })
            {
                control.Dock = DockStyle.Fill;
                control.Visible = false;
                this.Controls.Add(control);
            }

            menuControl.OnJugar += () =>
            {
                MostrarControl(dialogoControl);
                dialogoControl.IniciarDialogo();
            };
            menuControl.OnConfiguracion += () => MostrarControl(configuracionControl);
            menuControl.OnSalir += () => Application.Exit();

            dialogoControl.OnTerminado += () => MostrarControl(unirseControl);

            unirseControl.OnUnido += () =>
            {
                MostrarControl(lobbyControl);
                lobbyControl.InicializarLobby();
            };

            lobbyControl.OnJuegoIniciado += () =>
            {
                MostrarControl(seleccionTableroControl);
            };

            seleccionTableroControl.OnTablerosSeleccionados += (tableros) =>
            {
                MostrarControl(juegoControl);
                juegoControl.InicializarJuego(MainForm.Cliente.FormaDeGanar, tableros);
            };

            juegoControl.OnJuegoTerminado += (ganador) =>
            {
                MostrarControl(postJuegoControl);
                postJuegoControl.InicializarPostJuego(ganador);
            };

            postJuegoControl.OnJugarDeNuevo += () =>
            {
                MostrarControl(lobbyControl);
                lobbyControl.InicializarLobby();
            };
            postJuegoControl.OnSalir += () => MostrarControl(menuControl);

            configuracionControl.OnRegresar += () => MostrarControl(menuControl);

            Cliente.OnConexionPerdida += () =>
            {
                this.Invoke(() =>
                {
                    MessageBox.Show("Se perdió la conexión con el servidor.");
                    MostrarControl(menuControl);
                });
            };
        }

        private void MostrarControl(Control control)
        {
            foreach (Control c in this.Controls)
                c.Visible = false;
            control.Visible = true;
        }
    }
}