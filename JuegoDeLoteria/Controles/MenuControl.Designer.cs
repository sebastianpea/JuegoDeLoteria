namespace JuegoDeLoteria.Controles
{
    partial class MenuControl
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
            btnJugar = new Button();
            btnConfiguracion = new Button();
            btnSalir = new Button();
            lblTitulo = new Label();
            SuspendLayout();
            // 
            // btnJugar
            // 
            btnJugar.BackColor = Color.Black;
            btnJugar.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnJugar.ForeColor = Color.White;
            btnJugar.Location = new Point(53, 256);
            btnJugar.Name = "btnJugar";
            btnJugar.Size = new Size(167, 63);
            btnJugar.TabIndex = 0;
            btnJugar.Text = "Jugar";
            btnJugar.UseVisualStyleBackColor = false;
            btnJugar.Click += btnJugar_Click;
            // 
            // btnConfiguracion
            // 
            btnConfiguracion.BackColor = Color.Black;
            btnConfiguracion.Font = new Font("Determination Mono Web", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConfiguracion.ForeColor = Color.White;
            btnConfiguracion.Location = new Point(53, 345);
            btnConfiguracion.Name = "btnConfiguracion";
            btnConfiguracion.Size = new Size(178, 64);
            btnConfiguracion.TabIndex = 1;
            btnConfiguracion.Text = "Configuración";
            btnConfiguracion.UseVisualStyleBackColor = false;
            btnConfiguracion.Click += btnConfiguracion_Click;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.Black;
            btnSalir.Font = new Font("Determination Mono Web", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSalir.ForeColor = Color.White;
            btnSalir.Location = new Point(53, 434);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(167, 61);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Determination Mono Web", 72F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(688, 49);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(472, 123);
            lblTitulo.TabIndex = 3;
            lblTitulo.Text = "Lotería";
            // 
            // MenuControl
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackgroundImage = Properties.Resources.FireGuyBackground;
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(lblTitulo);
            Controls.Add(btnSalir);
            Controls.Add(btnConfiguracion);
            Controls.Add(btnJugar);
            DoubleBuffered = true;
            Name = "MenuControl";
            Size = new Size(1920, 1080);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnJugar;
        private Button btnConfiguracion;
        private Button btnSalir;
        private Label lblTitulo;
    }
}
