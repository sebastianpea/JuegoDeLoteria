using JuegoDeLoteria.Controles;
using JuegoDeLoteria.Redes;

namespace JuegoDeLoteria.Forms
{
    public partial class MainForm : Form
    {
        private MenuControl _menuControl;
        private DialogoControl _dialogControl;
        private UnirseControl _unirseControl;
        private LobbyControl _lobbyControl;
        private SeleccionTableroControl _seleccionTableroControl;
        private JuegoControl _juegControl;
        private PostJuegoControl _postJuegoControl;
        private ConfiguracionControl _configuracionControl;

        public static ClienteDeJuego Cliente = new ClienteDeJuego();

        public MainForm()
        {
            InitializeComponent();
            InicializarControles();
            MostrarControl(_menuControl);
        }

        private void InicializarControles()
        {
            _menuControl = new MenuControl();
            _dialogControl = new DialogoControl();
            _unirseControl = new UnirseControl();
            _lobbyControl = new LobbyControl();
            _seleccionTableroControl = new SeleccionTableroControl();
            _juegControl = new JuegoControl();
            _postJuegoControl = new PostJuegoControl();
            _configuracionControl = new ConfiguracionControl();

            foreach (Control control in new Control[]
            {
                _menuControl, _dialogControl, _unirseControl,
                _lobbyControl, _seleccionTableroControl, _juegControl,
                _postJuegoControl, _configuracionControl
            })
            {
                control.Dock = DockStyle.Fill;
                control.Visible = false;
                this.Controls.Add(control);
            }

            _menuControl.OnJugar += () =>
            {
                MostrarControl(_dialogControl);
                _dialogControl.IniciarDialogo();
            };
            _menuControl.OnConfiguracion += () => MostrarControl(_configuracionControl);
            _menuControl.OnSalir += () => Application.Exit();

            _dialogControl.OnTerminado += () => MostrarControl(_unirseControl);

            _unirseControl.OnUnido += () =>
            {
                MostrarControl(_lobbyControl);
                _lobbyControl.InicializarLobby();
            };

            _lobbyControl.OnJuegoIniciado += () =>
            {
                MostrarControl(_seleccionTableroControl);
            };

            _seleccionTableroControl.OnTablerosSeleccionados += (tableros) =>
            {
                MostrarControl(_juegControl);
                _juegControl.InicializarJuego(MainForm.Cliente.FormaDeGanar, tableros);
            };

            _juegControl.OnJuegoTerminado += (ganador) =>
            {
                MostrarControl(_postJuegoControl);
                _postJuegoControl.InicializarPostJuego(ganador);
            };

            _postJuegoControl.OnJugarDeNuevo += () =>
            {
                MostrarControl(_lobbyControl);
                _lobbyControl.InicializarLobby();
            };
            _postJuegoControl.OnSalir += () => MostrarControl(_menuControl);

            _configuracionControl.OnRegresar += () => MostrarControl(_menuControl);
        }

        private void MostrarControl(Control control)
        {
            foreach (Control c in this.Controls)
                c.Visible = false;
            control.Visible = true;
        }
    }
}