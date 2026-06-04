using JuegoDeLoteria.Managers;
using JuegoDeLoteria.Forms;

namespace JuegoDeLoteria
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            AudioManager.CargarPlaylist(new List<string>
            {
                "Musica/The One Who Got Away.mp3",
                "Musica/cancion1.mp3"
            });
            AudioManager.ReproducirSiguiente();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}