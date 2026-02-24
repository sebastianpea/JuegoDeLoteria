namespace JuegoDeLoteria.Controles
{
    partial class LobbyControl
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
            lstJugadores = new ListBox();
            lblCodigoSala = new Label();
            cmbFormaDeGanar = new ComboBox();
            nudIntervalo = new NumericUpDown();
            btnIniciarJuego = new Button();
            lblEsperando = new Label();
            ((System.ComponentModel.ISupportInitialize)nudIntervalo).BeginInit();
            SuspendLayout();
            // 
            // lstJugadores
            // 
            lstJugadores.FormattingEnabled = true;
            lstJugadores.Location = new Point(835, 26);
            lstJugadores.Name = "lstJugadores";
            lstJugadores.Size = new Size(488, 704);
            lstJugadores.TabIndex = 0;
            // 
            // lblCodigoSala
            // 
            lblCodigoSala.AutoSize = true;
            lblCodigoSala.Location = new Point(86, 339);
            lblCodigoSala.Name = "lblCodigoSala";
            lblCodigoSala.Size = new Size(86, 20);
            lblCodigoSala.TabIndex = 1;
            lblCodigoSala.Text = "CodigoSala";
            // 
            // cmbFormaDeGanar
            // 
            cmbFormaDeGanar.FormattingEnabled = true;
            cmbFormaDeGanar.Location = new Point(55, 107);
            cmbFormaDeGanar.Name = "cmbFormaDeGanar";
            cmbFormaDeGanar.Size = new Size(548, 28);
            cmbFormaDeGanar.TabIndex = 2;
            // 
            // nudIntervalo
            // 
            nudIntervalo.Location = new Point(642, 108);
            nudIntervalo.Name = "nudIntervalo";
            nudIntervalo.Size = new Size(150, 27);
            nudIntervalo.TabIndex = 3;
            // 
            // btnIniciarJuego
            // 
            btnIniciarJuego.Location = new Point(279, 330);
            btnIniciarJuego.Name = "btnIniciarJuego";
            btnIniciarJuego.Size = new Size(163, 29);
            btnIniciarJuego.TabIndex = 4;
            btnIniciarJuego.Text = "IniciarJuego";
            btnIniciarJuego.UseVisualStyleBackColor = true;
            btnIniciarJuego.Click += btnIniciarJuego_Click;
            // 
            // lblEsperando
            // 
            lblEsperando.AutoSize = true;
            lblEsperando.Location = new Point(318, 630);
            lblEsperando.Name = "lblEsperando";
            lblEsperando.Size = new Size(79, 20);
            lblEsperando.TabIndex = 5;
            lblEsperando.Text = "Esperando";
            // 
            // LobbyControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblEsperando);
            Controls.Add(btnIniciarJuego);
            Controls.Add(nudIntervalo);
            Controls.Add(cmbFormaDeGanar);
            Controls.Add(lblCodigoSala);
            Controls.Add(lstJugadores);
            Name = "LobbyControl";
            Size = new Size(1359, 773);
            ((System.ComponentModel.ISupportInitialize)nudIntervalo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstJugadores;
        private Label lblCodigoSala;
        private ComboBox cmbFormaDeGanar;
        private NumericUpDown nudIntervalo;
        private Button btnIniciarJuego;
        private Label lblEsperando;
    }
}
