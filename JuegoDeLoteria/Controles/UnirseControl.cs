using JuegoDeLoteria.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace JuegoDeLoteria.Controles
{
    public partial class UnirseControl : UserControl
    {
        public event Action? OnUnido;

        public UnirseControl()
        {
            InitializeComponent();
            lblError.Visible = false;
        }

        private async void btnUnirse_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            btnUnirse.Enabled = false;
            lblError.Visible = false;

            MainForm.Cliente.OnError += OnErrorRecibido;

            bool conectado = await ConectarAlServidor();
            if (!conectado) return;

            await MainForm.Cliente.UnirseASalaAsync(txtNombre.Text, txtCodigoSala.Text);

            await Task.Delay(500);

            OnUnido?.Invoke();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarError("Por favor ingresa tu nombre.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCodigoSala.Text))
            {
                MostrarError("Por favor ingresa el código de sala.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtIP.Text))
            {
                MostrarError("Por favor ingresa la IP del servidor.");
                return false;
            }
            return true;
        }

        private async Task<bool> ConectarAlServidor()
        {
            try
            {
                await MainForm.Cliente.ConectarAsync(txtIP.Text);
                return true;
            }
            catch (HttpRequestException)
            {
                MostrarError("No se pudo encontrar el servidor. Verifica la IP.");
                btnUnirse.Enabled = true;
                return false;
            }
            catch (TaskCanceledException)
            {
                MostrarError("La conexión tardó demasiado. Intenta de nuevo.");
                btnUnirse.Enabled = true;
                return false;
            }
        }

        private void OnErrorRecibido(string mensaje)
        {
            this.Invoke(() =>
            {
                MostrarError(mensaje);
                btnUnirse.Enabled = true;
            });
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
