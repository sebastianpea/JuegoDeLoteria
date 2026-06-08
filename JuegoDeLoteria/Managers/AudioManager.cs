using NAudio.Wave;
using System.Speech.Synthesis;

namespace JuegoDeLoteria.Managers
{
    public static class AudioManager
    {
        private static WaveOutEvent? outputDevice;
        private static AudioFileReader? audioFile;
        private static List<string> playlist = new List<string>();
        private static int indiceActual = 0;

        public static void CargarPlaylist(List<string> canciones)
        {
            playlist = canciones;
            indiceActual = 0;
        }

        public static void ReproducirSiguiente()
        {
            if (playlist.Count == 0) return;

            DetenerMusica();

            audioFile = new AudioFileReader(playlist[indiceActual]);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.PlaybackStopped += OnCancionTerminada;
            outputDevice.Play();
            AplicarVolumen(Properties.Settings.Default.Volumen);

            indiceActual = (indiceActual + 1) % playlist.Count;
        }

        private static void OnCancionTerminada(object? sender, StoppedEventArgs e)
        {
            ReproducirSiguiente();
        }

        public static void AplicarVolumen(int volumen)
        {
            if (audioFile != null)
                audioFile.Volume = volumen / 100f;
        }

        public static void DetenerMusica()
        {
            outputDevice?.Stop();
            outputDevice?.Dispose();
            audioFile?.Dispose();
            outputDevice = null;
            audioFile = null;
        }
          private static SpeechSynthesizer? _voz;

        public static void HablarCarta(string nombre)
        {
            Task.Run(() =>
            {
                _voz ??= new SpeechSynthesizer();
                _voz.Rate = 0;
                _voz.Volume = 100;
                _voz.Speak(nombre);
            });
        
        }
    }
}