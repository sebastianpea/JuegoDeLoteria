using Compartido;
using Microsoft.AspNetCore.SignalR;

namespace Servidor.Hubs
{
    public class HubDeJuego : Hub
    {
        private static Dictionary<string, Sala> _salas = new Dictionary<string, Sala>();

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            foreach (var sala in _salas.Values)
            {
                if (sala.Jugadores.ContainsKey(Context.ConnectionId))
                {
                    sala.Jugadores.Remove(Context.ConnectionId);

                    if (sala.Jugadores.Count == 0)
                    {
                        _salas.Remove(sala.Codigo);
                        break;
                    }

                    if (sala.HostId == Context.ConnectionId)
                    {
                        sala.HostId = sala.Jugadores.Keys.First();
                        await Clients.Group(sala.Codigo)
                            .SendAsync("NuevoHost", sala.HostId);
                    }

                    await Clients.Group(sala.Codigo)
                        .SendAsync("JugadorSalio", Context.ConnectionId);
                    break;
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task UnirseASala(string nombre, string codigoSala)
        {
            if (!_salas.ContainsKey(codigoSala))
            {
                _salas[codigoSala] = new Sala(codigoSala, Context.ConnectionId);
            }

            Sala sala = _salas[codigoSala];

            if (sala.EnJuego)
            {
                await Clients.Caller.SendAsync("ErrorAlUnirse", "El juego ya comenzó.");
                return;
            }

            sala.Jugadores[Context.ConnectionId] = nombre;
            await Groups.AddToGroupAsync(Context.ConnectionId, codigoSala);

            bool esHost = sala.HostId == Context.ConnectionId;
            await Clients.Caller.SendAsync("UnidoASala", codigoSala, esHost);

            await Clients.OthersInGroup(codigoSala)
                .SendAsync("JugadorUnido", Context.ConnectionId, nombre);
        }

        public async Task IniciarJuego(string codigoSala, string formaDeGanar, int intervaloSegundos)
        {
            if (!_salas.ContainsKey(codigoSala)) return;

            Sala sala = _salas[codigoSala];

            if (sala.HostId != Context.ConnectionId)
            {
                await Clients.Caller.SendAsync("Error", "Solo el host puede iniciar el juego.");
                return;
            }

            sala.EnJuego = true;
            sala.CartasMencionadas.Clear();
            sala.Mazo.Barajar();

            await Clients.Group(codigoSala).SendAsync("JuegoIniciado", formaDeGanar);

            _ = Task.Run(async () =>
            {
                while (sala.Mazo.HayCartasRestantes() && sala.EnJuego)
                {
                    await Task.Delay(intervaloSegundos * 1000);

                    var carta = sala.Mazo.SacarCarta();
                    if (carta == null) break;

                    sala.CartasMencionadas.Add(carta.Id);

                    await Clients.Group(codigoSala)
                        .SendAsync("CartaMencionada", carta.Id, carta.Nombre);
                }

                if (sala.EnJuego)
                {
                    sala.EnJuego = false;
                    await Clients.Group(codigoSala).SendAsync("JuegoTerminado", "Nadie ganó.");
                }
            });
        }

        public async Task ReclamarLoteria(string codigoSala)
        {
            if (!_salas.ContainsKey(codigoSala)) return;

            Sala sala = _salas[codigoSala];

            if (!sala.EnJuego) return;

            await Clients.Caller.SendAsync("VerificarLoteria", sala.CartasMencionadas);
        }

        public async Task ResultadoLoteria(string codigoSala, bool esValido)
        {
            if (!_salas.ContainsKey(codigoSala)) return;

            Sala sala = _salas[codigoSala];
            string nombreGanador = sala.Jugadores[Context.ConnectionId];

            if (esValido)
            {
                sala.EnJuego = false;
                await Clients.Group(codigoSala)
                    .SendAsync("JuegoTerminado", nombreGanador);
            }
            else
            {
                await Clients.Caller
                    .SendAsync("LoteriaNovalida", "Tu Lotería no es válida, continúa jugando.");
            }
        }

        public async Task JugarDeNuevo(string codigoSala)
        {
            if (!_salas.ContainsKey(codigoSala)) return;

            Sala sala = _salas[codigoSala];

            if (sala.HostId != Context.ConnectionId)
            {
                await Clients.Caller.SendAsync("Error", "Solo el host puede reiniciar el juego.");
                return;
            }

            sala.EnJuego = false;
            sala.CartasMencionadas.Clear();
            sala.Mazo = new MazoCompartido();

            await Clients.Group(codigoSala).SendAsync("JuegoReiniciado");
        }

        public async Task ObtenerCartasRestantes(string codigoSala)
        {
            if (!_salas.ContainsKey(codigoSala)) return;

            Sala sala = _salas[codigoSala];

            List<int> todasLasCartas = sala.Mazo.ObtenerTodasLasCartas()
                .Select(c => c.Id)
                .ToList();

            List<int> cartasRestantes = todasLasCartas
                .Where(id => !sala.CartasMencionadas.Contains(id))
                .ToList();

            await Clients.Caller.SendAsync("CartasRestantes", cartasRestantes);
        }
    }
}
