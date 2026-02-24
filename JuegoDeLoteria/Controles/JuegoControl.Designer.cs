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
            pnlTableros = new Panel();
            pnlFichas = new Panel();
            btnLoteria = new Button();
            ((System.ComponentModel.ISupportInitialize)pbCartaActual).BeginInit();
            SuspendLayout();
            // 
            // pbCartaActual
            // 
            pbCartaActual.Location = new Point(1057, 56);
            pbCartaActual.Name = "pbCartaActual";
            pbCartaActual.Size = new Size(291, 422);
            pbCartaActual.TabIndex = 0;
            pbCartaActual.TabStop = false;
            // 
            // lblNombreCartaActual
            // 
            lblNombreCartaActual.AutoSize = true;
            lblNombreCartaActual.Location = new Point(1168, 495);
            lblNombreCartaActual.Name = "lblNombreCartaActual";
            lblNombreCartaActual.Size = new Size(86, 20);
            lblNombreCartaActual.TabIndex = 1;
            lblNombreCartaActual.Text = "CartaActual";
            // 
            // flpHistorial
            // 
            flpHistorial.Location = new Point(1000, 531);
            flpHistorial.Name = "flpHistorial";
            flpHistorial.Size = new Size(439, 182);
            flpHistorial.TabIndex = 2;
            // 
            // pnlTableros
            // 
            pnlTableros.Location = new Point(394, 124);
            pnlTableros.Name = "pnlTableros";
            pnlTableros.Size = new Size(487, 334);
            pnlTableros.TabIndex = 0;
            // 
            // pnlFichas
            // 
            pnlFichas.Location = new Point(32, 227);
            pnlFichas.Name = "pnlFichas";
            pnlFichas.Size = new Size(313, 426);
            pnlFichas.TabIndex = 1;
            // 
            // btnLoteria
            // 
            btnLoteria.Location = new Point(606, 551);
            btnLoteria.Name = "btnLoteria";
            btnLoteria.Size = new Size(94, 29);
            btnLoteria.TabIndex = 0;
            btnLoteria.Text = "Loteria";
            btnLoteria.UseVisualStyleBackColor = true;
            btnLoteria.Click += btnLoteria_Click;
            // 
            // JuegoControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnLoteria);
            Controls.Add(pnlFichas);
            Controls.Add(pnlTableros);
            Controls.Add(flpHistorial);
            Controls.Add(lblNombreCartaActual);
            Controls.Add(pbCartaActual);
            Name = "JuegoControl";
            Size = new Size(1625, 739);
            ((System.ComponentModel.ISupportInitialize)pbCartaActual).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbCartaActual;
        private Label lblNombreCartaActual;
        private FlowLayoutPanel flpHistorial;
        private Panel pnlTableros;
        private Panel pnlFichas;
        private Button btnLoteria;
    }
}
