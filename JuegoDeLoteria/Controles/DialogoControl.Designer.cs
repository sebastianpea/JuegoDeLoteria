namespace JuegoDeLoteria.Controles
{
    partial class DialogoControl
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
            pbPersonaje = new PictureBox();
            lblDialogo = new Label();
            btnContinuar = new Button();
            btnSi = new Button();
            btnNo = new Button();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).BeginInit();
            SuspendLayout();
            // 
            // pbPersonaje
            // 
            pbPersonaje.Location = new Point(83, 56);
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(404, 541);
            pbPersonaje.TabIndex = 0;
            pbPersonaje.TabStop = false;
            // 
            // lblDialogo
            // 
            lblDialogo.AutoSize = true;
            lblDialogo.Location = new Point(631, 195);
            lblDialogo.Name = "lblDialogo";
            lblDialogo.Size = new Size(50, 20);
            lblDialogo.TabIndex = 1;
            lblDialogo.Text = "label1";
            // 
            // btnContinuar
            // 
            btnContinuar.Location = new Point(616, 568);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(94, 29);
            btnContinuar.TabIndex = 2;
            btnContinuar.Text = "Continuar";
            btnContinuar.UseVisualStyleBackColor = true;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // btnSi
            // 
            btnSi.Location = new Point(743, 568);
            btnSi.Name = "btnSi";
            btnSi.Size = new Size(94, 29);
            btnSi.TabIndex = 3;
            btnSi.Text = "Si";
            btnSi.UseVisualStyleBackColor = true;
            btnSi.Click += btnSi_Click;
            // 
            // btnNo
            // 
            btnNo.Location = new Point(862, 568);
            btnNo.Name = "btnNo";
            btnNo.Size = new Size(94, 29);
            btnNo.TabIndex = 4;
            btnNo.Text = "No";
            btnNo.UseVisualStyleBackColor = true;
            btnNo.Click += btnNo_Click;
            // 
            // DialogoControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnNo);
            Controls.Add(btnSi);
            Controls.Add(btnContinuar);
            Controls.Add(lblDialogo);
            Controls.Add(pbPersonaje);
            Name = "DialogoControl";
            Size = new Size(1352, 756);
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbPersonaje;
        private Label lblDialogo;
        private Button btnContinuar;
        private Button btnSi;
        private Button btnNo;
    }
}
