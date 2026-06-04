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
            lblTituloRestantes = new Label();
            SuspendLayout();
            // 
            // lblGanador
            // 
            lblGanador.AutoSize = true;
            lblGanador.Font = new Font("Determination Mono Web", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGanador.ForeColor = Color.White;
            lblGanador.Location = new Point(53, 33);
            lblGanador.Name = "lblGanador";
            lblGanador.Size = new Size(174, 45);
            lblGanador.TabIndex = 0;
            lblGanador.Text = "Ganador";
            // 
            // flpCartasRestantes
            // 
            flpCartasRestantes.AutoScroll = true;
            flpCartasRestantes.Location = new Point(1259, 69);
            flpCartasRestantes.Name = "flpCartasRestantes";
            flpCartasRestantes.Size = new Size(608, 983);
            flpCartasRestantes.TabIndex = 1;
            // 
            // btnJugarDeNuevo
            // 
            btnJugarDeNuevo.BackColor = Color.Black;
            btnJugarDeNuevo.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnJugarDeNuevo.ForeColor = Color.White;
            btnJugarDeNuevo.Location = new Point(87, 897);
            btnJugarDeNuevo.Name = "btnJugarDeNuevo";
            btnJugarDeNuevo.Size = new Size(238, 101);
            btnJugarDeNuevo.TabIndex = 0;
            btnJugarDeNuevo.Text = "Jugar De Nuevo";
            btnJugarDeNuevo.UseVisualStyleBackColor = false;
            btnJugarDeNuevo.Click += btnJugarDeNuevo_Click;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.Black;
            btnSalir.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSalir.ForeColor = Color.White;
            btnSalir.Location = new Point(715, 897);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(155, 101);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // lblTituloRestantes
            // 
            lblTituloRestantes.AutoSize = true;
            lblTituloRestantes.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTituloRestantes.ForeColor = Color.White;
            lblTituloRestantes.Location = new Point(1259, 15);
            lblTituloRestantes.Name = "lblTituloRestantes";
            lblTituloRestantes.Size = new Size(253, 30);
            lblTituloRestantes.TabIndex = 3;
            lblTituloRestantes.Text = "Titulo Restantes";
            // 
            // PostJuegoControl
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Black;
            Controls.Add(lblTituloRestantes);
            Controls.Add(btnSalir);
            Controls.Add(btnJugarDeNuevo);
            Controls.Add(flpCartasRestantes);
            Controls.Add(lblGanador);
            Name = "PostJuegoControl";
            Size = new Size(1920, 1080);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblGanador;
        private FlowLayoutPanel flpCartasRestantes;
        private Button btnJugarDeNuevo;
        private Button btnSalir;
        private Label lblTituloRestantes;
    }
}
