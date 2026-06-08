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
            btnSaltar = new Button();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).BeginInit();
            SuspendLayout();
            // 
            // pbPersonaje
            // 
            pbPersonaje.Image = Properties.Resources.Grillby;
            pbPersonaje.Location = new Point(90, 286);
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(581, 541);
            pbPersonaje.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonaje.TabIndex = 0;
            pbPersonaje.TabStop = false;
            // 
            // lblDialogo
            // 
            lblDialogo.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDialogo.ForeColor = Color.White;
            lblDialogo.Location = new Point(723, 286);
            lblDialogo.Name = "lblDialogo";
            lblDialogo.Size = new Size(839, 320);
            lblDialogo.TabIndex = 1;
            lblDialogo.Text = "label1";
            // 
            // btnContinuar
            // 
            btnContinuar.BackColor = Color.Black;
            btnContinuar.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnContinuar.ForeColor = Color.White;
            btnContinuar.Location = new Point(1250, 686);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(222, 65);
            btnContinuar.TabIndex = 2;
            btnContinuar.Text = "Continuar";
            btnContinuar.UseVisualStyleBackColor = false;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // btnSi
            // 
            btnSi.BackColor = Color.Black;
            btnSi.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSi.ForeColor = Color.WhiteSmoke;
            btnSi.Location = new Point(794, 686);
            btnSi.Name = "btnSi";
            btnSi.Size = new Size(222, 65);
            btnSi.TabIndex = 3;
            btnSi.Text = "Si";
            btnSi.UseVisualStyleBackColor = false;
            btnSi.Click += btnSi_Click;
            // 
            // btnNo
            // 
            btnNo.BackColor = Color.Black;
            btnNo.Font = new Font("Determination Mono Web", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNo.ForeColor = Color.White;
            btnNo.Location = new Point(1022, 686);
            btnNo.Name = "btnNo";
            btnNo.Size = new Size(222, 65);
            btnNo.TabIndex = 4;
            btnNo.Text = "No";
            btnNo.UseVisualStyleBackColor = false;
            btnNo.Click += btnNo_Click;
            // 
            // btnSaltar
            // 
            btnSaltar.BackColor = Color.Black;
            btnSaltar.Font = new Font("Determination Mono Web", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSaltar.ForeColor = Color.White;
            btnSaltar.Location = new Point(32, 34);
            btnSaltar.Name = "btnSaltar";
            btnSaltar.Size = new Size(245, 101);
            btnSaltar.TabIndex = 5;
            btnSaltar.Text = "Saltar";
            btnSaltar.UseVisualStyleBackColor = false;
            btnSaltar.Click += btnSaltar_Click;
            // 
            // DialogoControl
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Black;
            Controls.Add(btnSaltar);
            Controls.Add(btnNo);
            Controls.Add(btnSi);
            Controls.Add(btnContinuar);
            Controls.Add(lblDialogo);
            Controls.Add(pbPersonaje);
            ForeColor = Color.Black;
            Name = "DialogoControl";
            Size = new Size(1920, 1080);
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbPersonaje;
        private Label lblDialogo;
        private Button btnContinuar;
        private Button btnSi;
        private Button btnNo;
        private Button btnSaltar;
    }
}
