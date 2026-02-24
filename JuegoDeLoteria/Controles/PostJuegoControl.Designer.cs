namespace JuegoDeLoteria.Controles
{
    partial class PostJuegoControl
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
            lblGanador = new Label();
            flpCartasRestantes = new FlowLayoutPanel();
            btnJugarDeNuevo = new Button();
            btnSalir = new Button();
            SuspendLayout();
            // 
            // lblGanador
            // 
            lblGanador.AutoSize = true;
            lblGanador.Location = new Point(247, 213);
            lblGanador.Name = "lblGanador";
            lblGanador.Size = new Size(66, 20);
            lblGanador.TabIndex = 0;
            lblGanador.Text = "Ganador";
            // 
            // flpCartasRestantes
            // 
            flpCartasRestantes.Location = new Point(828, 131);
            flpCartasRestantes.Name = "flpCartasRestantes";
            flpCartasRestantes.Size = new Size(397, 407);
            flpCartasRestantes.TabIndex = 1;
            // 
            // btnJugarDeNuevo
            // 
            btnJugarDeNuevo.Location = new Point(185, 574);
            btnJugarDeNuevo.Name = "btnJugarDeNuevo";
            btnJugarDeNuevo.Size = new Size(176, 79);
            btnJugarDeNuevo.TabIndex = 0;
            btnJugarDeNuevo.Text = "Jugar De Nuevo";
            btnJugarDeNuevo.UseVisualStyleBackColor = true;
            btnJugarDeNuevo.Click += btnJugarDeNuevo_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(484, 574);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(176, 79);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // PostJuegoControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnSalir);
            Controls.Add(btnJugarDeNuevo);
            Controls.Add(flpCartasRestantes);
            Controls.Add(lblGanador);
            Name = "PostJuegoControl";
            Size = new Size(1324, 741);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblGanador;
        private FlowLayoutPanel flpCartasRestantes;
        private Button btnJugarDeNuevo;
        private Button btnSalir;
    }
}
