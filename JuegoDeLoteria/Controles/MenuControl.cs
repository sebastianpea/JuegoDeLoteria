namespace JuegoDeLoteria.Controles
{
    public partial class MenuControl : UserControl
    {
        public event Action? OnJugar;
        public event Action? OnConfiguracion;
        public event Action? OnSalir;

        public MenuControl()
        {
            InitializeComponent();
        }

        private void btnJugar_Click(object sender, EventArgs e)
        {
            OnJugar?.Invoke();
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            OnConfiguracion?.Invoke();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            OnSalir?.Invoke();
        }
    }
}
