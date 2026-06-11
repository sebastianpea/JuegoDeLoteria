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
            label1 = new Label();
            chatControl1 = new ChatControl();
            chkCartasDobles = new CheckBox();
            chkManual = new CheckBox();
            nudTamañoTablero = new NumericUpDown();
            nudCantidadTableros = new NumericUpDown();
            pnlPatronPersonalizado = new Panel();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)nudIntervalo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTamañoTablero).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidadTableros).BeginInit();
            SuspendLayout();
            // 
            // lstJugadores
            // 
            lstJugadores.BackColor = Color.Black;
            lstJugadores.Font = new Font("Determination Mono Web", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstJugadores.ForeColor = Color.White;
            lstJugadores.FormattingEnabled = true;
            lstJugadores.ItemHeight = 44;
            lstJugadores.Location = new Point(1433, 0);
            lstJugadores.Margin = new Padding(3, 2, 3, 2);
            lstJugadores.Name = "lstJugadores";
            lstJugadores.Size = new Size(463, 1016);
            lstJugadores.TabIndex = 0;
            // 
            // lblCodigoSala
            // 
            lblCodigoSala.AutoSize = true;
            lblCodigoSala.Font = new Font("Determination Mono Web", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCodigoSala.ForeColor = SystemColors.Control;
            lblCodigoSala.Location = new Point(815, 348);
            lblCodigoSala.Name = "lblCodigoSala";
            lblCodigoSala.Size = new Size(250, 48);
            lblCodigoSala.TabIndex = 1;
            lblCodigoSala.Text = "CodigoSala";
            // 
            // cmbFormaDeGanar
            // 
            cmbFormaDeGanar.BackColor = Color.Black;
            cmbFormaDeGanar.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbFormaDeGanar.ForeColor = Color.White;
            cmbFormaDeGanar.FormattingEnabled = true;
            cmbFormaDeGanar.Location = new Point(36, 50);
            cmbFormaDeGanar.Margin = new Padding(3, 2, 3, 2);
            cmbFormaDeGanar.Name = "cmbFormaDeGanar";
            cmbFormaDeGanar.Size = new Size(480, 38);
            cmbFormaDeGanar.TabIndex = 2;
            cmbFormaDeGanar.SelectedIndexChanged += cmbFormaDeGanar_SelectedIndexChanged;
            // 
            // nudIntervalo
            // 
            nudIntervalo.BackColor = Color.Black;
            nudIntervalo.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nudIntervalo.ForeColor = Color.White;
            nudIntervalo.Location = new Point(532, 50);
            nudIntervalo.Margin = new Padding(3, 2, 3, 2);
            nudIntervalo.Name = "nudIntervalo";
            nudIntervalo.Size = new Size(131, 38);
            nudIntervalo.TabIndex = 3;
            // 
            // btnIniciarJuego
            // 
            btnIniciarJuego.BackColor = Color.Black;
            btnIniciarJuego.Font = new Font("Determination Mono Web", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnIniciarJuego.ForeColor = Color.White;
            btnIniciarJuego.Location = new Point(736, 112);
            btnIniciarJuego.Margin = new Padding(3, 2, 3, 2);
            btnIniciarJuego.Name = "btnIniciarJuego";
            btnIniciarJuego.Size = new Size(418, 133);
            btnIniciarJuego.TabIndex = 4;
            btnIniciarJuego.Text = "IniciarJuego";
            btnIniciarJuego.UseVisualStyleBackColor = false;
            btnIniciarJuego.Click += btnIniciarJuego_Click;
            // 
            // lblEsperando
            // 
            lblEsperando.AutoSize = true;
            lblEsperando.Font = new Font("Determination Mono Web", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEsperando.ForeColor = SystemColors.Control;
            lblEsperando.Location = new Point(829, 266);
            lblEsperando.Name = "lblEsperando";
            lblEsperando.Size = new Size(227, 48);
            lblEsperando.TabIndex = 5;
            lblEsperando.Text = "Esperando";
            lblEsperando.Click += lblEsperando_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Determination Mono Web", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(71, 184);
            label1.Name = "label1";
            label1.Size = new Size(0, 48);
            label1.TabIndex = 6;
            // 
            // chatControl1
            // 
            chatControl1.BackColor = Color.Black;
            chatControl1.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chatControl1.Location = new Point(19, 489);
            chatControl1.Margin = new Padding(6, 4, 6, 4);
            chatControl1.Name = "chatControl1";
            chatControl1.Size = new Size(471, 724);
            chatControl1.TabIndex = 8;
            // 
            // chkCartasDobles
            // 
            chkCartasDobles.AutoSize = true;
            chkCartasDobles.Font = new Font("Determination Mono Web", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkCartasDobles.ForeColor = SystemColors.Control;
            chkCartasDobles.Location = new Point(36, 112);
            chkCartasDobles.Name = "chkCartasDobles";
            chkCartasDobles.Size = new Size(300, 45);
            chkCartasDobles.TabIndex = 9;
            chkCartasDobles.Text = "Cartas Dobles";
            chkCartasDobles.UseVisualStyleBackColor = true;
            chkCartasDobles.CheckedChanged += chkCartasDobles_CheckedChanged;
            // 
            // chkManual
            // 
            chkManual.AutoSize = true;
            chkManual.Font = new Font("Determination Mono Web", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkManual.ForeColor = SystemColors.Control;
            chkManual.Location = new Point(36, 163);
            chkManual.Name = "chkManual";
            chkManual.Size = new Size(160, 45);
            chkManual.TabIndex = 10;
            chkManual.Text = "Manual";
            chkManual.UseVisualStyleBackColor = true;
            chkManual.CheckedChanged += chkManual_CheckedChanged;
            // 
            // nudTamañoTablero
            // 
            nudTamañoTablero.BackColor = Color.Black;
            nudTamañoTablero.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nudTamañoTablero.ForeColor = Color.White;
            nudTamañoTablero.Location = new Point(36, 234);
            nudTamañoTablero.Margin = new Padding(3, 2, 3, 2);
            nudTamañoTablero.Name = "nudTamañoTablero";
            nudTamañoTablero.Size = new Size(131, 38);
            nudTamañoTablero.TabIndex = 11;
            // 
            // nudCantidadTableros
            // 
            nudCantidadTableros.BackColor = Color.Black;
            nudCantidadTableros.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nudCantidadTableros.ForeColor = Color.White;
            nudCantidadTableros.Location = new Point(36, 289);
            nudCantidadTableros.Margin = new Padding(3, 2, 3, 2);
            nudCantidadTableros.Name = "nudCantidadTableros";
            nudCantidadTableros.Size = new Size(131, 38);
            nudCantidadTableros.TabIndex = 12;
            // 
            // pnlPatronPersonalizado
            // 
            pnlPatronPersonalizado.BackColor = Color.FromArgb(15, 15, 15);
            pnlPatronPersonalizado.BorderStyle = BorderStyle.FixedSingle;
            pnlPatronPersonalizado.Location = new Point(652, 446);
            pnlPatronPersonalizado.Name = "pnlPatronPersonalizado";
            pnlPatronPersonalizado.Size = new Size(606, 570);
            pnlPatronPersonalizado.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Determination Mono Web", 22.2F);
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(200, 234);
            label2.Name = "label2";
            label2.Size = new Size(270, 38);
            label2.TabIndex = 14;
            label2.Text = "Tamaño Tablero";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Determination Mono Web", 22.2F);
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(200, 289);
            label3.Name = "label3";
            label3.Size = new Size(325, 38);
            label3.TabIndex = 15;
            label3.Text = "Cantidad Tableros";
            // 
            // LobbyControl
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Black;
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(pnlPatronPersonalizado);
            Controls.Add(nudCantidadTableros);
            Controls.Add(nudTamañoTablero);
            Controls.Add(chkManual);
            Controls.Add(chkCartasDobles);
            Controls.Add(chatControl1);
            Controls.Add(label1);
            Controls.Add(lblEsperando);
            Controls.Add(btnIniciarJuego);
            Controls.Add(nudIntervalo);
            Controls.Add(cmbFormaDeGanar);
            Controls.Add(lblCodigoSala);
            Controls.Add(lstJugadores);
            Font = new Font("Determination Mono Web", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(3, 2, 3, 2);
            Name = "LobbyControl";
            Size = new Size(1920, 1080);
            Load += LobbyControl_Load;
            ((System.ComponentModel.ISupportInitialize)nudIntervalo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTamañoTablero).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidadTableros).EndInit();
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
        private Label label1;
        private ChatControl chatControl1;
        private CheckBox chkCartasDobles;
        private CheckBox chkManual;
        private NumericUpDown nudTamañoTablero;
        private NumericUpDown nudCantidadTableros;
        private Panel pnlPatronPersonalizado;
        private Label label2;
        private Label label3;
    }
}
