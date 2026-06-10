namespace JuegoDeLoteria.Controles
{
    partial class UnirseControl
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
            txtNombre = new TextBox();
            txtCodigoSala = new TextBox();
            txtIP = new TextBox();
            btnUnirse = new Button();
            lblError = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.Black;
            txtNombre.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombre.ForeColor = Color.White;
            txtNombre.Location = new Point(531, 548);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(265, 38);
            txtNombre.TabIndex = 0;
            // 
            // txtCodigoSala
            // 
            txtCodigoSala.BackColor = Color.Black;
            txtCodigoSala.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCodigoSala.ForeColor = Color.White;
            txtCodigoSala.Location = new Point(807, 548);
            txtCodigoSala.Name = "txtCodigoSala";
            txtCodigoSala.Size = new Size(265, 38);
            txtCodigoSala.TabIndex = 1;
            // 
            // txtIP
            // 
            txtIP.BackColor = Color.Black;
            txtIP.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIP.ForeColor = Color.White;
            txtIP.Location = new Point(1078, 548);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(265, 38);
            txtIP.TabIndex = 2;
            // 
            // btnUnirse
            // 
            btnUnirse.BackColor = Color.Black;
            btnUnirse.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUnirse.ForeColor = Color.White;
            btnUnirse.Location = new Point(872, 629);
            btnUnirse.Name = "btnUnirse";
            btnUnirse.Size = new Size(143, 56);
            btnUnirse.TabIndex = 3;
            btnUnirse.Text = "Unirse";
            btnUnirse.UseVisualStyleBackColor = false;
            btnUnirse.Click += btnUnirse_Click;
            // 
            // lblError
            // 
            lblError.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblError.Font = new Font("Determination Mono Web", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblError.ForeColor = Color.White;
            lblError.Location = new Point(693, 254);
            lblError.Name = "lblError";
            lblError.Size = new Size(510, 128);
            lblError.TabIndex = 4;
            lblError.Text = "Error";
            lblError.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Determination Mono Web", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(531, 461);
            label1.Name = "label1";
            label1.Size = new Size(265, 53);
            label1.TabIndex = 5;
            label1.Text = "Nombre";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label2.Font = new Font("Determination Mono Web", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(802, 461);
            label2.Name = "label2";
            label2.Size = new Size(270, 53);
            label2.TabIndex = 6;
            label2.Text = "Sala";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label3.Font = new Font("Determination Mono Web", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(1078, 461);
            label3.Name = "label3";
            label3.Size = new Size(265, 53);
            label3.TabIndex = 7;
            label3.Text = "Red";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UnirseControl
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Black;
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblError);
            Controls.Add(btnUnirse);
            Controls.Add(txtIP);
            Controls.Add(txtCodigoSala);
            Controls.Add(txtNombre);
            Name = "UnirseControl";
            Size = new Size(1920, 1080);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNombre;
        private TextBox txtCodigoSala;
        private TextBox txtIP;
        private Button btnUnirse;
        private Label lblError;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
