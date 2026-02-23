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
            trackBarVolumen.Location = new Point(480, 353);
            trackBarVolumen.Name = "trackBarVolumen";
            trackBarVolumen.Size = new Size(307, 56);
            trackBarVolumen.TabIndex = 0;
            trackBarVolumen.Scroll += trackBarVolumen_Scroll;
            // 
            // lblVolumen
            // 
            lblVolumen.AutoSize = true;
            lblVolumen.Location = new Point(589, 301);
            lblVolumen.Name = "lblVolumen";
            lblVolumen.Size = new Size(67, 20);
            lblVolumen.TabIndex = 1;
            lblVolumen.Text = "Volumen";
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(599, 415);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.TabIndex = 2;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnRegresar
            // 
            btnRegresar.Location = new Point(599, 471);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.Size = new Size(94, 29);
            btnRegresar.TabIndex = 3;
            btnRegresar.Text = "Regresar";
            btnRegresar.UseVisualStyleBackColor = true;
            btnRegresar.Click += btnRegresar_Click;
            // 
            // ConfiguracionControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnRegresar);
            Controls.Add(btnGuardar);
            Controls.Add(lblVolumen);
            Controls.Add(trackBarVolumen);
            Name = "ConfiguracionControl";
            Size = new Size(1404, 751);
            Load += ConfiguracionControl_Load;
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
