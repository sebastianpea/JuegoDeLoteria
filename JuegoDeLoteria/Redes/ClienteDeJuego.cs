using Compartido;
using Microsoft.AspNetCore.SignalR.Client;

namespace JuegoDeLoteria.Redes
{
    public class ClienteDeJuego
    {
        private HubConnection _conexion;

        public string CodigoSala { get; private set; }
        public bool EsHost { get; set; }
        public string FormaDeGanar { get; private set; } = string.Empty;
        public string NombreJugador { get; private set; } = string.Empty;
        public int IntervaloSegundos { get; private set; }

        public event Action<string, string>? OnJugadorUnido;
        public event Action<string>? OnJugadorSalio;
        public event Action<string>? OnJuegoIniciado;
        public event Action<int, string>? OnCartaMencionada;
        public event Action<List<int>>? OnVerificarLoteria;
        public event Action<string>? OnJuegoTerminado;
        public event Action? OnJuegoReiniciado;
        public event Action<string>? OnError;
        public event Action<string>? OnNuevoHost;
        public event Action<List<int>>? OnCartasRestantes;
        public event Action? OnConexionPerdida;
        public event Action<int, int>? OnActualizarListos;
        public event Action? OnConteoIniciado;
        public event Action<string, string>? OnMensajeRecibido;
        public string ConnectionId => _conexion?.ConnectionId ?? string.Empty;

        public ClienteDeJuego()
        {
            CodigoSala = string.Empty;
        }

        public async Task ConectarAsync(string ip)
        {
            _conexion = new HubConnectionBuilder()
                .WithUrl($"http://{ip}:5000/juego")
                .WithAutomaticReconnect()
                .Build();

            _conexion.Closed += async (error) =>
            {
                OnConexionPerdida?.Invoke();
                await Task.CompletedTask;
            };

            _conexion.On<string, string>("JugadorUnido", (id, nombre) =>
                OnJugadorUnido?.Invoke(id, nombre));

            _conexion.On<string>("JugadorSalio", (id) =>
                OnJugadorSalio?.Invoke(id));

            _conexion.On<string>("JuegoIniciado", (formaDeGanar) =>
            {
                FormaDeGanar = formaDeGanar;
                OnJuegoIniciado?.Invoke(formaDeGanar);
            });

            _conexion.On<int, string>("CartaMencionada", (id, nombre) =>
                OnCartaMencionada?.Invoke(id, nombre));

            _conexion.On<List<int>>("VerificarLoteria", (cartas) =>
                OnVerificarLoteria?.Invoke(cartas));

            _conexion.On<string>("JuegoTerminado", (ganador) =>
                OnJuegoTerminado?.Invoke(ganador));

            _conexion.On("JuegoReiniciado", () =>
                OnJuegoReiniciado?.Invoke());

            _conexion.On<string>("Error", (mensaje) =>
                OnError?.Invoke(mensaje));

            _conexion.On<string>("ErrorAlUnirse", (mensaje) =>
                OnError?.Invoke(mensaje));

            _conexion.On<string>("NuevoHost", (id) =>
                OnNuevoHost?.Invoke(id));

            _conexion.On<List<int>>("CartasRestantes", (cartas) =>
                OnCartasRestantes?.Invoke(cartas));

            _conexion.On<int, int>("ActualizarListos", (listos, total) =>
                OnActualizarListos?.Invoke(listos, total));

            _conexion.On("ConteoIniciado", () =>
                OnConteoIniciado?.Invoke());

            _conexion.On<string, string>(EventosHub.MensajeRecibido, (nombre, mensaje) =>
    OnMensajeRecibido?.Invoke(nombre, mensaje));

            await _conexion.StartAsync();
        }

        public async Task UnirseASalaAsync(string nombre, string codigoSala)
        {
            NombreJugador = nombre;
            CodigoSala = codigoSala;
            _conexion.On<string, bool>("UnidoASala", (codigo, esHost) =>
            {
                EsHost = esHost;
            });
            await _conexion.InvokeAsync("UnirseASala", nombre, codigoSala);
        }

        public async Task IniciarJuegoAsync(string formaDeGanar, int intervaloSegundos)
        {
            IntervaloSegundos = intervaloSegundos;
            await _conexion.InvokeAsync("IniciarJuego", CodigoSala, formaDeGanar, intervaloSegundos);
        }

        public async Task JugadorListoAsync()
        {
            await _conexion.InvokeAsync("JugadorListo", CodigoSala);
        }

        public async Task ReclamarLoteriaAsync()
        {
            await _conexion.InvokeAsync("ReclamarLoteria", CodigoSala);
        }

        public async Task EnviarResultadoAsync(bool esValido)
        {
            await _conexion.InvokeAsync("ResultadoLoteria", CodigoSala, esValido);
        }

        public async Task JugarDeNuevoAsync()
        {
            await _conexion.InvokeAsync("JugarDeNuevo", CodigoSala);
        }

        public async Task ObtenerCartasRestantesAsync()
        {
            await _conexion.InvokeAsync("ObtenerCartasRestantes", CodigoSala);
        }

        public async Task DesconectarAsync()
        {
            if (_conexion != null)
                await _conexion.StopAsync();
        }

        public async Task EnviarMensajeAsync(string mensaje)
        {
            await _conexion.InvokeAsync(EventosHub.EnviarMensaje, CodigoSala, mensaje);
        }
    }
}
