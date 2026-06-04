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
            pnlFichas = new FlowLayoutPanel();
            chatControl2 = new ChatControl();
            ((System.ComponentModel.ISupportInitialize)pbCartaActual).BeginInit();
            SuspendLayout();
            // 
            // pbCartaActual
            // 
            pbCartaActual.Location = new Point(715, 9);
            pbCartaActual.Name = "pbCartaActual";
            pbCartaActual.Size = new Size(568, 548);
            pbCartaActual.SizeMode = PictureBoxSizeMode.Zoom;
            pbCartaActual.TabIndex = 0;
            pbCartaActual.TabStop = false;
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
            flpHistorial.Location = new Point(1307, 9);
            flpHistorial.Name = "flpHistorial";
            flpHistorial.Size = new Size(599, 533);
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
            pnlTableros.Location = new Point(785, 563);
            pnlTableros.Name = "pnlTableros";
            pnlTableros.Size = new Size(582, 490);
            pnlTableros.TabIndex = 0;
            // 
            // pnlFichas
            // 
            pnlFichas.AutoScroll = true;
            pnlFichas.Location = new Point(1373, 563);
            pnlFichas.Name = "pnlFichas";
            pnlFichas.Size = new Size(533, 503);
            pnlFichas.TabIndex = 1;
            // 
            // chatControl2
            // 
            chatControl2.BackColor = Color.White;
            chatControl2.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chatControl2.Location = new Point(48, 329);
            chatControl2.Margin = new Padding(6, 4, 6, 4);
            chatControl2.Name = "chatControl2";
            chatControl2.Size = new Size(579, 724);
            chatControl2.TabIndex = 4;
            // 
            // JuegoControl
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Black;
            Controls.Add(chatControl2);
            Controls.Add(pnlFichas);
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
        private FlowLayoutPanel pnlFichas;
        private ChatControl chatControl2;
    }
}
