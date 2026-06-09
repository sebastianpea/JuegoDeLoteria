namespace JuegoDeLoteria.Controles
{
    partial class JuegoControl
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
            pbCartaActual = new PictureBox();
            lblNombreCartaActual = new Label();
            flpHistorial = new FlowLayoutPanel();
            btnLoteria = new Button();
            lblCuentaRegresiva = new Label();
            pnlTableros = new FlowLayoutPanel();
            chatControl2 = new ChatControl();
            btnPausar = new Button();
            btnMasFast = new Button();
            btnMasSlow = new Button();
            btnSiguienteCarta = new Button();
            ((System.ComponentModel.ISupportInitialize)pbCartaActual).BeginInit();
            SuspendLayout();
            // 
            // pbCartaActual
            // 
            pbCartaActual.Location = new Point(498, 3);
            pbCartaActual.Name = "pbCartaActual";
            pbCartaActual.Size = new Size(568, 548);
            pbCartaActual.SizeMode = PictureBoxSizeMode.Zoom;
            pbCartaActual.TabIndex = 0;
            pbCartaActual.TabStop = false;
            pbCartaActual.Click += pbCartaActual_Click;
            // 
            // lblNombreCartaActual
            // 
            lblNombreCartaActual.AutoSize = true;
            lblNombreCartaActual.BackColor = Color.Black;
            lblNombreCartaActual.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNombreCartaActual.ForeColor = Color.White;
            lblNombreCartaActual.Location = new Point(82, 244);
            lblNombreCartaActual.Name = "lblNombreCartaActual";
            lblNombreCartaActual.Size = new Size(178, 30);
            lblNombreCartaActual.TabIndex = 1;
            lblNombreCartaActual.Text = "CartaActual";
            lblNombreCartaActual.Click += lblNombreCartaActual_Click;
            // 
            // flpHistorial
            // 
            flpHistorial.AutoScroll = true;
            flpHistorial.Location = new Point(1085, 653);
            flpHistorial.Name = "flpHistorial";
            flpHistorial.Size = new Size(806, 407);
            flpHistorial.TabIndex = 2;
            // 
            // btnLoteria
            // 
            btnLoteria.BackColor = Color.Black;
            btnLoteria.Font = new Font("Determination Mono Web", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLoteria.ForeColor = Color.White;
            btnLoteria.Location = new Point(82, 34);
            btnLoteria.Name = "btnLoteria";
            btnLoteria.Size = new Size(302, 149);
            btnLoteria.TabIndex = 0;
            btnLoteria.Text = "Loteria";
            btnLoteria.UseVisualStyleBackColor = false;
            btnLoteria.Click += btnLoteria_Click;
            // 
            // lblCuentaRegresiva
            // 
            lblCuentaRegresiva.AutoSize = true;
            lblCuentaRegresiva.BackColor = Color.Black;
            lblCuentaRegresiva.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCuentaRegresiva.ForeColor = Color.White;
            lblCuentaRegresiva.Location = new Point(82, 197);
            lblCuentaRegresiva.Name = "lblCuentaRegresiva";
            lblCuentaRegresiva.Size = new Size(238, 30);
            lblCuentaRegresiva.TabIndex = 3;
            lblCuentaRegresiva.Text = "CuentaRegresiva";
            // 
            // pnlTableros
            // 
            pnlTableros.AutoScroll = true;
            pnlTableros.Location = new Point(1097, 12);
            pnlTableros.Name = "pnlTableros";
            pnlTableros.Size = new Size(691, 606);
            pnlTableros.TabIndex = 0;
            // 
            // chatControl2
            // 
            chatControl2.BackColor = Color.Black;
            chatControl2.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chatControl2.Location = new Point(19, 469);
            chatControl2.Margin = new Padding(6, 4, 6, 4);
            chatControl2.Name = "chatControl2";
            chatControl2.Size = new Size(470, 724);
            chatControl2.TabIndex = 5;
            // 
            // btnPausar
            // 
            btnPausar.BackColor = Color.Black;
            btnPausar.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPausar.ForeColor = Color.White;
            btnPausar.Location = new Point(82, 288);
            btnPausar.Name = "btnPausar";
            btnPausar.Size = new Size(151, 50);
            btnPausar.TabIndex = 6;
            btnPausar.Text = "Pausar";
            btnPausar.UseVisualStyleBackColor = false;
            btnPausar.Click += btnPausar_Click;
            // 
            // btnMasFast
            // 
            btnMasFast.BackColor = Color.Black;
            btnMasFast.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnMasFast.ForeColor = Color.White;
            btnMasFast.Location = new Point(641, 569);
            btnMasFast.Name = "btnMasFast";
            btnMasFast.Size = new Size(58, 64);
            btnMasFast.TabIndex = 7;
            btnMasFast.Text = "+";
            btnMasFast.UseVisualStyleBackColor = false;
            btnMasFast.Click += btnMasFast_Click;
            // 
            // btnMasSlow
            // 
            btnMasSlow.BackColor = Color.Black;
            btnMasSlow.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnMasSlow.ForeColor = Color.White;
            btnMasSlow.Location = new Point(886, 569);
            btnMasSlow.Name = "btnMasSlow";
            btnMasSlow.Size = new Size(58, 64);
            btnMasSlow.TabIndex = 8;
            btnMasSlow.Text = "-";
            btnMasSlow.UseVisualStyleBackColor = false;
            btnMasSlow.Click += btnMasSlow_Click;
            // 
            // btnSiguienteCarta
            // 
            btnSiguienteCarta.BackColor = Color.Black;
            btnSiguienteCarta.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSiguienteCarta.ForeColor = Color.White;
            btnSiguienteCarta.Location = new Point(44, 451);
            btnSiguienteCarta.Name = "btnSiguienteCarta";
            btnSiguienteCarta.Size = new Size(343, 64);
            btnSiguienteCarta.TabIndex = 9;
            btnSiguienteCarta.Text = "Siguiente Carta";
            btnSiguienteCarta.UseVisualStyleBackColor = false;
            btnSiguienteCarta.Click += btnSiguienteCarta_Click;
            // 
            // JuegoControl
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Black;
            Controls.Add(btnSiguienteCarta);
            Controls.Add(btnMasSlow);
            Controls.Add(btnMasFast);
            Controls.Add(btnPausar);
            Controls.Add(chatControl2);
            Controls.Add(pnlTableros);
            Controls.Add(lblCuentaRegresiva);
            Controls.Add(btnLoteria);
            Controls.Add(flpHistorial);
            Controls.Add(lblNombreCartaActual);
            Controls.Add(pbCartaActual);
            Name = "JuegoControl";
            Size = new Size(1920, 1080);
            ((System.ComponentModel.ISupportInitialize)pbCartaActual).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbCartaActual;
        private Label lblNombreCartaActual;
        private FlowLayoutPanel flpHistorial;
        private Button btnLoteria;
        private Label lblCuentaRegresiva;
        private FlowLayoutPanel pnlTableros;
        private ChatControl chatControl1;
        private ChatControl chatControl2;
        private Button btnPausar;
        private Button btnMasFast;
        private Button btnMasSlow;
        private Button btnSiguienteCarta;
    }
}
