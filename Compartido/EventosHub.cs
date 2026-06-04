namespace Compartido
{
    public static class EventosHub
    {
        // Server → Client events
        public const string JugadorUnido = "JugadorUnido";
        public const string JugadorSalio = "JugadorSalio";
        public const string UnidoASala = "UnidoASala";
        public const string ErrorAlUnirse = "ErrorAlUnirse";
        public const string NuevoHost = "NuevoHost";
        public const string JuegoIniciado = "JuegoIniciado";
        public const string CartaMencionada = "CartaMencionada";
        public const string VerificarLoteria = "VerificarLoteria";
        public const string JuegoTerminado = "JuegoTerminado";
        public const string LoteriaNovalida = "LoteriaNovalida";
        public const string JuegoReiniciado = "JuegoReiniciado";
        public const string Error = "Error";
        public const string CartasRestantes = "CartasRestantes";
        public const string ActualizarListos = "ActualizarListos";
        public const string ConteoIniciado = "ConteoIniciado";
        public const string MensajeRecibido = "MensajeRecibido";

        // Client → Server methods
        public const string UnirseASala = "UnirseASala";
        public const string IniciarJuego = "IniciarJuego";
        public const string JugadorListo = "JugadorListo";
        public const string ReclamarLoteria = "ReclamarLoteria";
        public const string ResultadoLoteria = "ResultadoLoteria";
        public const string JugarDeNuevo = "JugarDeNuevo";
        public const string ObtenerCartasRestantes = "ObtenerCartasRestantes";
        public const string EnviarMensaje = "EnviarMensaje";
    }
}