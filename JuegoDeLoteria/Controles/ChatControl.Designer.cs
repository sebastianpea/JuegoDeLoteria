namespace JuegoDeLoteria.Controles
{
    partial class ChatControl
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            rtbMensajes = new RichTextBox();
            txtMensaje = new TextBox();
            btnEnviar = new Button();
            SuspendLayout();
            // 
            // rtbMensajes
            // 
            rtbMensajes.BackColor = Color.Black;
            rtbMensajes.ForeColor = Color.White;
            rtbMensajes.Location = new Point(17, 62);
            rtbMensajes.Name = "rtbMensajes";
            rtbMensajes.ReadOnly = true;
            rtbMensajes.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbMensajes.Size = new Size(368, 449);
            rtbMensajes.TabIndex = 0;
            rtbMensajes.Text = "";
            // 
            // txtMensaje
            // 
            txtMensaje.BackColor = Color.Black;
            txtMensaje.ForeColor = Color.White;
            txtMensaje.Location = new Point(27, 526);
            txtMensaje.Name = "txtMensaje";
            txtMensaje.Size = new Size(278, 38);
            txtMensaje.TabIndex = 1;
            txtMensaje.KeyDown += txtMensaje_KeyDown;
            // 
            // btnEnviar
            // 
            btnEnviar.BackColor = Color.Black;
            btnEnviar.ForeColor = Color.White;
            btnEnviar.Location = new Point(311, 526);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(136, 40);
            btnEnviar.TabIndex = 2;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = false;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // ChatControl
            // 
            AutoScaleDimensions = new SizeF(15F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            Controls.Add(btnEnviar);
            Controls.Add(txtMensaje);
            Controls.Add(rtbMensajes);
            Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(6, 4, 6, 4);
            Name = "ChatControl";
            Size = new Size(463, 579);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox rtbMensajes;
        private TextBox txtMensaje;
        private Button btnEnviar;
    }
}
