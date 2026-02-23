using NAudio.Wave;

namespace JuegoDeLoteria.Managers
{
    public static class AudioManager
    {
        private static WaveOutEvent? _outputDevice;
        private static AudioFileReader? _audioFile;
        private static List<string> _playlist = new List<string>();
        private static int _indiceActual = 0;

        public static void CargarPlaylist(List<string> canciones)
        {
            _playlist = canciones;
            _indiceActual = 0;
        }

        public static void ReproducirSiguiente()
        {
            if (_playlist.Count == 0) return;

            DetenerMusica();

            _audioFile = new AudioFileReader(_playlist[_indiceActual]);
            _outputDevice = new WaveOutEvent();
            _outputDevice.Init(_audioFile);
            _outputDevice.PlaybackStopped += OnCancionTerminada;
            _outputDevice.Play();
            AplicarVolumen(Properties.Settings.Default.Volumen);

            _indiceActual = (_indiceActual + 1) % _playlist.Count;
        }

        private static void OnCancionTerminada(object? sender, StoppedEventArgs e)
        {
            ReproducirSiguiente();
        }

        public static void AplicarVolumen(int volumen)
        {
            if (_audioFile != null)
                _audioFile.Volume = volumen / 100f;
        }

        public static void DetenerMusica()
        {
            _outputDevice?.Stop();
            _outputDevice?.Dispose();
            _audioFile?.Dispose();
            _outputDevice = null;
            _audioFile = null;
        }
    }
}