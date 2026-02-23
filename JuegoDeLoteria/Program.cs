using JuegoDeLoteria;
using JuegoDeLoteria.Forms;
using JuegoDeLoteria.Managers;

AudioManager.CargarPlaylist(new List<string>
{
    "Musica/cancion1.mp3",
});
AudioManager.ReproducirSiguiente();

Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);
Application.Run(new MainForm());