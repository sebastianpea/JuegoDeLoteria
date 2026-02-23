namespace JuegoDeLoteria.Controles
{
    partial class MenuControl
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
            btnJugar = new Button();
            btnConfiguracion = new Button();
            btnSalir = new Button();
            SuspendLayout();
            // 
            // btnJugar
            // 
            btnJugar.Location = new Point(624, 426);
            btnJugar.Name = "btnJugar";
            btnJugar.Size = new Size(94, 29);
            btnJugar.TabIndex = 0;
            btnJugar.Text = "Jugar";
            btnJugar.UseVisualStyleBackColor = true;
            // 
            // btnConfiguracion
            // 
            btnConfiguracion.Location = new Point(603, 478);
            btnConfiguracion.Name = "btnConfiguracion";
            btnConfiguracion.Size = new Size(133, 29);
            btnConfiguracion.TabIndex = 1;
            btnConfiguracion.Text = "Configuración";
            btnConfiguracion.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(624, 533);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(94, 29);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            // 
            // MenuControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnSalir);
            Controls.Add(btnConfiguracion);
            Controls.Add(btnJugar);
            Name = "MenuControl";
            Size = new Size(1399, 784);
            ResumeLayout(false);
        }

        #endregion

        private Button btnJugar;
        private Button btnConfiguracion;
        private Button btnSalir;
    }
}
