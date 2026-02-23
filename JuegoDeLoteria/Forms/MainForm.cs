using JuegoDeLoteria.Forms;

namespace JuegoDeLoteria
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }
        private void btnJugar_Click(object sender, EventArgs e)
        {
            this.Hide();
            var dialogForm = new DialogoForm();
            dialogForm.FormClosed += (s, a) => this.Close();
            dialogForm.Show();
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            var configuracionForm = new ConfiguracionForm();
            configuracionForm.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
