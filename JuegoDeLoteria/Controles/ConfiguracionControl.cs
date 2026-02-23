using JuegoDeLoteria.Managers;

namespace JuegoDeLoteria.Controles
{
    public partial class ConfiguracionControl : UserControl
    {
        public event Action? OnRegresar;

        public ConfiguracionControl()
        {
            InitializeComponent();
            CargarConfiguracion();
        }

        private void CargarConfiguracion()
        {
            trackBarVolumen.Minimum = 0;
            trackBarVolumen.Maximum = 100;
            trackBarVolumen.Value = Properties.Settings.Default.Volumen;
            lblVolumen.Text = trackBarVolumen.Value.ToString();
        }

        private void trackBarVolumen_Scroll(object sender, EventArgs e)
        {
            lblVolumen.Text = trackBarVolumen.Value.ToString();
            AudioManager.AplicarVolumen(trackBarVolumen.Value);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Volumen = trackBarVolumen.Value;
            Properties.Settings.Default.Save();
            OnRegresar?.Invoke();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            OnRegresar?.Invoke();
        }
    }
}