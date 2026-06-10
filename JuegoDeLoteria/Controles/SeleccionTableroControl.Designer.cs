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
            label1 = new Label();
            btnCargarTablero = new Button();
            btnGuardarTablero = new Button();
            ((System.ComponentModel.ISupportInitialize)nudCantidadTableros).BeginInit();
            SuspendLayout();
            // 
            // nudCantidadTableros
            // 
            nudCantidadTableros.BackColor = Color.Black;
            nudCantidadTableros.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nudCantidadTableros.ForeColor = Color.White;
            nudCantidadTableros.Location = new Point(945, 41);
            nudCantidadTableros.Name = "nudCantidadTableros";
            nudCantidadTableros.Size = new Size(263, 38);
            nudCantidadTableros.TabIndex = 0;
            // 
            // btnAleatorio
            // 
            btnAleatorio.BackColor = Color.Black;
            btnAleatorio.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAleatorio.ForeColor = Color.White;
            btnAleatorio.Location = new Point(285, 820);
            btnAleatorio.Name = "btnAleatorio";
            btnAleatorio.Size = new Size(163, 52);
            btnAleatorio.TabIndex = 1;
            btnAleatorio.Text = "Aleatorio";
            btnAleatorio.UseVisualStyleBackColor = false;
            btnAleatorio.Click += btnAleatorio_Click;
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = Color.Black;
            btnConfirmar.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConfirmar.ForeColor = Color.White;
            btnConfirmar.Location = new Point(466, 820);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(163, 52);
            btnConfirmar.TabIndex = 0;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // lblInstrucciones
            // 
            lblInstrucciones.AutoSize = true;
            lblInstrucciones.Font = new Font("Determination Mono Web", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInstrucciones.ForeColor = Color.White;
            lblInstrucciones.Location = new Point(15, 54);
            lblInstrucciones.Name = "lblInstrucciones";
            lblInstrucciones.Size = new Size(278, 41);
            lblInstrucciones.TabIndex = 4;
            lblInstrucciones.Text = "Instrucciones";
            // 
            // pnlTablero
            // 
            pnlTablero.Location = new Point(487, 120);
            pnlTablero.Name = "pnlTablero";
            pnlTablero.Size = new Size(404, 598);
            pnlTablero.TabIndex = 5;
            // 
            // flpCartasDisponibles
            // 
            flpCartasDisponibles.Location = new Point(945, 100);
            flpCartasDisponibles.Name = "flpCartasDisponibles";
            flpCartasDisponibles.Size = new Size(860, 772);
            flpCartasDisponibles.TabIndex = 0;
            // 
            // label1
            // 
            label1.Font = new Font("Determination Mono Web", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(15, 144);
            label1.Name = "label1";
            label1.Size = new Size(287, 371);
            label1.TabIndex = 6;
            label1.Text = "Selecciona las cartas que deseas en tu tablero, así como la cantidad de Tableros que desees, cuando acabes selecciona Confirmar\r\n";
            // 
            // btnCargarTablero
            // 
            btnCargarTablero.BackColor = Color.Black;
            btnCargarTablero.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCargarTablero.ForeColor = Color.White;
            btnCargarTablero.Location = new Point(25, 518);
            btnCargarTablero.Name = "btnCargarTablero";
            btnCargarTablero.Size = new Size(163, 52);
            btnCargarTablero.TabIndex = 7;
            btnCargarTablero.Text = "Cargar";
            btnCargarTablero.UseVisualStyleBackColor = false;
            btnCargarTablero.Click += btnCargarTablero_Click;
            // 
            // btnGuardarTablero
            // 
            btnGuardarTablero.BackColor = Color.Black;
            btnGuardarTablero.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGuardarTablero.ForeColor = Color.White;
            btnGuardarTablero.Location = new Point(25, 586);
            btnGuardarTablero.Name = "btnGuardarTablero";
            btnGuardarTablero.Size = new Size(163, 52);
            btnGuardarTablero.TabIndex = 8;
            btnGuardarTablero.Text = "Guardar";
            btnGuardarTablero.UseVisualStyleBackColor = false;
            btnGuardarTablero.Click += btnGuardarTablero_Click;
            // 
            // SeleccionTableroControl
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Black;
            Controls.Add(btnGuardarTablero);
            Controls.Add(btnCargarTablero);
            Controls.Add(label1);
            Controls.Add(flpCartasDisponibles);
            Controls.Add(pnlTablero);
            Controls.Add(lblInstrucciones);
            Controls.Add(btnConfirmar);
            Controls.Add(btnAleatorio);
            Controls.Add(nudCantidadTableros);
            Name = "SeleccionTableroControl";
            Size = new Size(1920, 1080);
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
        private Label label1;
        private Button btnCargarTablero;
        private Button btnGuardarTablero;
    }
}
