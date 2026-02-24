namespace JuegoDeLoteria.Controles
{
    partial class SeleccionTableroControl
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
            nudCantidadTableros = new NumericUpDown();
            btnAleatorio = new Button();
            btnConfirmar = new Button();
            lblInstrucciones = new Label();
            pnlTablero = new FlowLayoutPanel();
            flpCartasDisponibles = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)nudCantidadTableros).BeginInit();
            SuspendLayout();
            // 
            // nudCantidadTableros
            // 
            nudCantidadTableros.Location = new Point(1076, 26);
            nudCantidadTableros.Name = "nudCantidadTableros";
            nudCantidadTableros.Size = new Size(263, 27);
            nudCantidadTableros.TabIndex = 0;
            // 
            // btnAleatorio
            // 
            btnAleatorio.Location = new Point(605, 624);
            btnAleatorio.Name = "btnAleatorio";
            btnAleatorio.Size = new Size(94, 29);
            btnAleatorio.TabIndex = 1;
            btnAleatorio.Text = "Aleatorio";
            btnAleatorio.UseVisualStyleBackColor = true;
            btnAleatorio.Click += btnAleatorio_Click;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(743, 624);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(94, 29);
            btnConfirmar.TabIndex = 0;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // lblInstrucciones
            // 
            lblInstrucciones.AutoSize = true;
            lblInstrucciones.Location = new Point(16, 48);
            lblInstrucciones.Name = "lblInstrucciones";
            lblInstrucciones.Size = new Size(94, 20);
            lblInstrucciones.TabIndex = 4;
            lblInstrucciones.Text = "Instrucciones";
            // 
            // pnlTablero
            // 
            pnlTablero.Location = new Point(265, 26);
            pnlTablero.Name = "pnlTablero";
            pnlTablero.Size = new Size(413, 480);
            pnlTablero.TabIndex = 5;
            // 
            // flpCartasDisponibles
            // 
            flpCartasDisponibles.Location = new Point(698, 74);
            flpCartasDisponibles.Name = "flpCartasDisponibles";
            flpCartasDisponibles.Size = new Size(631, 521);
            flpCartasDisponibles.TabIndex = 0;
            // 
            // SeleccionTableroControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(flpCartasDisponibles);
            Controls.Add(pnlTablero);
            Controls.Add(lblInstrucciones);
            Controls.Add(btnConfirmar);
            Controls.Add(btnAleatorio);
            Controls.Add(nudCantidadTableros);
            Name = "SeleccionTableroControl";
            Size = new Size(1361, 789);
            ((System.ComponentModel.ISupportInitialize)nudCantidadTableros).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown nudCantidadTableros;
        private Button btnAleatorio;
        private Button btnConfirmar;
        private Label lblInstrucciones;
        private FlowLayoutPanel pnlTablero;
        private FlowLayoutPanel flpCartasDisponibles;
    }
}
