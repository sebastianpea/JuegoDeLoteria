using JuegoDeLoteria.Redes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuegoDeLoteria.Forms
{
    public partial class MainForm : Form
    {

        public static ClienteDeJuego Cliente = new ClienteDeJuego();
        public MainForm()
        {
            InitializeComponent();
        }

        
    }
}
