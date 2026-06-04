using JuegoDeLoteria.Forms;

namespace JuegoDeLoteria.Controles
{
    public partial class ChatControl : UserControl
    {
        public ChatControl()
        {
            InitializeComponent();
            this.BackColor = Color.White;
        }

        public void InicializarChat()
        {
            rtbMensajes.Clear();
            MainForm.Cliente.OnMensajeRecibido += OnMensajeRecibido;
        }

        public void DetenerChat()
        {
            MainForm.Cliente.OnMensajeRecibido -= OnMensajeRecibido;
        }

        private void OnMensajeRecibido(string nombre, string mensaje)
        {
            this.Invoke(() =>
            {
                rtbMensajes.AppendText($"{nombre}: {mensaje}\n");
                rtbMensajes.ScrollToCaret();
            });
        }

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            await EnviarMensaje();
        }

        private async void txtMensaje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await EnviarMensaje();
            }
        }

        private async Task EnviarMensaje()
        {
            if (string.IsNullOrWhiteSpace(txtMensaje.Text)) return;
            await MainForm.Cliente.EnviarMensajeAsync(txtMensaje.Text);
            txtMensaje.Clear();
        }
    }
}