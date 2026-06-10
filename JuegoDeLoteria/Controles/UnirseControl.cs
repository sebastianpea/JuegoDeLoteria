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

            try
            {
                btnUnirse.Enabled = false;
                lblError.Visible = false; 

                MainForm.Cliente.OnError -= OnErrorRecibido;
                MainForm.Cliente.OnError += OnErrorRecibido;

                MainForm.Cliente.OnUnidoASala -= AlUnirseConExito;
                MainForm.Cliente.OnUnidoASala += AlUnirseConExito;

                bool conectado = await ConectarAlServidor(); 
                if (!conectado) return; 

              
                await MainForm.Cliente.UnirseASalaAsync(txtNombre.Text, txtCodigoSala.Text); 
            }
            catch (Exception ex)
            {
                MostrarError($"Error inesperado: {ex.Message}");
                DesuscribirEventos();
                btnUnirse.Enabled = true;
            }
        }
        private void AlUnirseConExito(string codigoSala, bool esHost, Dictionary<string, string> jugadoresExistentes)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(() => AlUnirseConExito(codigoSala, esHost, jugadoresExistentes));
                return;
            }

            DesuscribirEventos();

            OnUnido?.Invoke(); 
        }
        private void OnErrorRecibido(string mensaje)
        {
            this.Invoke(() =>
            {
                DesuscribirEventos();
                MostrarError(mensaje); 
                btnUnirse.Enabled = true; 
            });
        }
        private void DesuscribirEventos()
        {
            MainForm.Cliente.OnError -= OnErrorRecibido;
            MainForm.Cliente.OnUnidoASala -= AlUnirseConExito;
        }
        private async Task<bool> ConectarAlServidor()
        {
            try
            {
                await MainForm.Cliente.ConectarAsync(txtIP.Text); 
                return true; 
            }
            catch (Exception ex)
            {
                string mensajeError = ex switch
                {
                    HttpRequestException => "No se pudo encontrar el servidor. Verifica la IP.",
                    TaskCanceledException => "La conexión tardó demasiado. Intenta de nuevo.",
                    _ => $"Error de conexión: {ex.Message}"
                };
                MostrarError(mensajeError);
                btnUnirse.Enabled = true;
                return false;
            }
        }

        private bool ValidarCampos() {  return true; } 
        private void MostrarError(string mensaje) {  } 
    }
}
