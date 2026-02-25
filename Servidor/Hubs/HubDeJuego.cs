using Compartido;
using Microsoft.AspNetCore.SignalR;

namespace Servidor.Hubs
{
    public class HubDeJuego : Hub
    {
        private static Dictionary<string, Sala> salas = new Dictionary<string, Sala>();
        private readonly IHubContext<HubDeJuego> hubContext;

        public HubDeJuego(IHubContext<HubDeJuego> hubContext)
        {
            this.hubContext = hubContext;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            foreach (var sala in salas.Values)
            {
                if (sala.Jugadores.ContainsKey(Context.ConnectionId))
                {
                    sala.Jugadores.Remove(Context.ConnectionId);
                    sala.JugadoresListos.Remove(Context.ConnectionId);

                    if (sala.Jugadores.Count == 0)
                    {
                        salas.Remove(sala.Codigo);
                        break;
                    }

                    if (sala.HostId == Context.ConnectionId)
                    {
                        sala.HostId = sala.Jugadores.Keys.First();
                        await hubContext.Clients.Group(sala.Codigo)
                            .SendAsync("NuevoHost", sala.HostId);
                    }

                    await hubContext.Clients.Group(sala.Codigo)
                        .SendAsync("JugadorSalio", Context.ConnectionId);

                    if (sala.EnJuego && sala.TodosListos())
                        await IniciarConteoDeCartas(sala);

                    break;
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task UnirseASala(string nombre, string codigoSala)
        {
            if (!salas.ContainsKey(codigoSala))
                salas[codigoSala] = new Sala(codigoSala, Context.ConnectionId);

            Sala sala = salas[codigoSala];

            if (sala.EnJuego)
            {
                await Clients.Caller.SendAsync("ErrorAlUnirse", "El juego ya comenzó.");
                return;
            }

            sala.Jugadores[Context.ConnectionId] = nombre;
            await Groups.AddToGroupAsync(Context.ConnectionId, codigoSala);

            bool esHost = sala.HostId == Context.ConnectionId;
            await Clients.Caller.SendAsync("UnidoASala", codigoSala, esHost);

            foreach (var jugador in sala.Jugadores)
            {
                if (jugador.Key != Context.ConnectionId)
                    await Clients.Caller.SendAsync("JugadorUnido", jugador.Key, jugador.Value);
            }

            await Clients.OthersInGroup(codigoSala)
                .SendAsync("JugadorUnido", Context.ConnectionId, nombre);
        }

        public async Task IniciarJuego(string codigoSala, string formaDeGanar, int intervaloSegundos)
        {
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];

            if (sala.HostId != Context.ConnectionId)
            {
                await Clients.Caller.SendAsync("Error", "Solo el host puede iniciar el juego.");
                return;
            }

            sala.EnJuego = true;
            sala.CartasMencionadas.Clear();
            sala.JugadoresListos.Clear();
            sala.IntervaloSegundos = intervaloSegundos;
            sala.Mazo.Barajar();

            Console.WriteLine($"Juego iniciado en sala {codigoSala} con intervalo {intervaloSegundos}s");

            await Clients.Group(codigoSala).SendAsync("JuegoIniciado", formaDeGanar);
        }

        public async Task JugadorListo(string codigoSala)
        {
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];
            sala.JugadoresListos.Add(Context.ConnectionId);

            int listos = sala.JugadoresListos.Count;
            int total = sala.Jugadores.Count;

            Console.WriteLine($"Jugador listo en sala {codigoSala}: {listos}/{total}");

            await Clients.Group(codigoSala)
                .SendAsync("ActualizarListos", listos, total);

            if (sala.TodosListos())
                await IniciarConteoDeCartas(sala);
        }

        private async Task IniciarConteoDeCartas(Sala sala)
        {
            Console.WriteLine($"Iniciando conteo para sala {sala.Codigo}");
            await hubContext.Clients.Group(sala.Codigo).SendAsync("ConteoIniciado");

            _ = Task.Run(async () =>
            {
                Console.WriteLine($"Task.Run iniciado para sala {sala.Codigo}");

                await Task.Delay(1000);

                while (sala.Mazo.HayCartasRestantes() && sala.EnJuego)
                {
                    var carta = sala.Mazo.SacarCarta();
                    if (carta == null)
                    {
                        Console.WriteLine("Carta es null, saliendo del loop");
                        break;
                    }

                    Console.WriteLine($"Enviando carta: {carta.Nombre}");
                    sala.CartasMencionadas.Add(carta.Id);

                    await hubContext.Clients.Group(sala.Codigo)
                        .SendAsync("CartaMencionada", carta.Id, carta.Nombre);

                    await Task.Delay(sala.IntervaloSegundos * 1000);
                }

                Console.WriteLine($"Loop terminado. EnJuego={sala.EnJuego}");

                if (sala.EnJuego)
                {
                    sala.EnJuego = false;
                    await hubContext.Clients.Group(sala.Codigo)
                        .SendAsync("JuegoTerminado", "Nadie ganó.");
                }
            });
        }

        public async Task ReclamarLoteria(string codigoSala)
        {
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];
            if (!sala.EnJuego) return;

            await Clients.Caller.SendAsync("VerificarLoteria", sala.CartasMencionadas);
        }

        public async Task ResultadoLoteria(string codigoSala, bool esValido)
        {
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];
            string nombreGanador = sala.Jugadores[Context.ConnectionId];

            if (esValido)
            {
                sala.EnJuego = false;
                await hubContext.Clients.Group(codigoSala)
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
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];

            if (sala.HostId != Context.ConnectionId)
            {
                await Clients.Caller.SendAsync("Error", "Solo el host puede reiniciar el juego.");
                return;
            }

            sala.EnJuego = false;
            sala.CartasMencionadas.Clear();
            sala.JugadoresListos.Clear();
            sala.Mazo = new MazoCompartido();

            await hubContext.Clients.Group(codigoSala).SendAsync("JuegoReiniciado");
        }

        public async Task ObtenerCartasRestantes(string codigoSala)
        {
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];

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