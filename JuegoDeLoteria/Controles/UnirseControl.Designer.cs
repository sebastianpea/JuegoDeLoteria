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
            SuspendLayout();
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(303, 466);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(125, 27);
            txtNombre.TabIndex = 0;
            // 
            // txtCodigoSala
            // 
            txtCodigoSala.Location = new Point(508, 466);
            txtCodigoSala.Name = "txtCodigoSala";
            txtCodigoSala.Size = new Size(125, 27);
            txtCodigoSala.TabIndex = 1;
            // 
            // txtIP
            // 
            txtIP.Location = new Point(704, 466);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(125, 27);
            txtIP.TabIndex = 2;
            // 
            // btnUnirse
            // 
            btnUnirse.Location = new Point(526, 524);
            btnUnirse.Name = "btnUnirse";
            btnUnirse.Size = new Size(94, 29);
            btnUnirse.TabIndex = 3;
            btnUnirse.Text = "Unirse";
            btnUnirse.UseVisualStyleBackColor = true;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Location = new Point(551, 358);
            lblError.Name = "lblError";
            lblError.Size = new Size(41, 20);
            lblError.TabIndex = 4;
            lblError.Text = "Error";
            // 
            // UnirseControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblError);
            Controls.Add(btnUnirse);
            Controls.Add(txtIP);
            Controls.Add(txtCodigoSala);
            Controls.Add(txtNombre);
            Name = "UnirseControl";
            Size = new Size(1281, 792);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNombre;
        private TextBox txtCodigoSala;
        private TextBox txtIP;
        private Button btnUnirse;
        private Label lblError;
    }
}
