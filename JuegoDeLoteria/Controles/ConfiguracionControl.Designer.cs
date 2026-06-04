namespace JuegoDeLoteria.Controles
{
    partial class ConfiguracionControl
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
            trackBarVolumen = new TrackBar();
            lblVolumen = new Label();
            btnGuardar = new Button();
            btnRegresar = new Button();
            ((System.ComponentModel.ISupportInitialize)trackBarVolumen).BeginInit();
            SuspendLayout();
            // 
            // trackBarVolumen
            // 
            trackBarVolumen.Location = new Point(605, 525);
            trackBarVolumen.Margin = new Padding(6, 4, 6, 4);
            trackBarVolumen.Name = "trackBarVolumen";
            trackBarVolumen.Size = new Size(786, 56);
            trackBarVolumen.TabIndex = 0;
            trackBarVolumen.Scroll += trackBarVolumen_Scroll;
            // 
            // lblVolumen
            // 
            lblVolumen.AutoSize = true;
            lblVolumen.Location = new Point(1104, 452);
            lblVolumen.Margin = new Padding(6, 0, 6, 0);
            lblVolumen.Name = "lblVolumen";
            lblVolumen.Size = new Size(118, 30);
            lblVolumen.TabIndex = 1;
            lblVolumen.Text = "Volumen";
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.Black;
            btnGuardar.Font = new Font("Determination Mono Web", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(850, 625);
            btnGuardar.Margin = new Padding(6, 4, 6, 4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(224, 63);
            btnGuardar.TabIndex = 2;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnRegresar
            // 
            btnRegresar.BackColor = Color.Black;
            btnRegresar.Font = new Font("Determination Mono Web", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRegresar.ForeColor = Color.White;
            btnRegresar.Location = new Point(850, 696);
            btnRegresar.Margin = new Padding(6, 4, 6, 4);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.Size = new Size(224, 65);
            btnRegresar.TabIndex = 3;
            btnRegresar.Text = "Regresar";
            btnRegresar.UseVisualStyleBackColor = false;
            btnRegresar.Click += btnRegresar_Click;
            // 
            // ConfiguracionControl
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Black;
            Controls.Add(btnRegresar);
            Controls.Add(btnGuardar);
            Controls.Add(lblVolumen);
            Controls.Add(trackBarVolumen);
            Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(6, 4, 6, 4);
            Name = "ConfiguracionControl";
            Size = new Size(1920, 1080);
            ((System.ComponentModel.ISupportInitialize)trackBarVolumen).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar trackBarVolumen;
        private Label lblVolumen;
        private Button btnGuardar;
        private Button btnRegresar;
    }
}
