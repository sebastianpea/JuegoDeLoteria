using Compartido;
using Microsoft.AspNetCore.SignalR;

namespace Servidor.Hubs
{
    public class HubDeJuego : Hub
    {
        private static Dictionary<string, Sala> salas = new Dictionary<string, Sala>();
        private readonly IHubContext<HubDeJuego> _hubContext;

        public HubDeJuego(IHubContext<HubDeJuego> hubContext)
        {
            _hubContext = hubContext;
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
                        await _hubContext.Clients.Group(sala.Codigo)
                            .SendAsync(EventosHub.NuevoHost, sala.HostId);
                    }

                    await _hubContext.Clients.Group(sala.Codigo)
                        .SendAsync(EventosHub.JugadorSalio, Context.ConnectionId);

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
                await Clients.Caller.SendAsync(EventosHub.ErrorAlUnirse, "El juego ya comenzó.");
                return;
            }

            if (sala.Jugadores.Values.Any(n => n.ToLower() == nombre.ToLower()))
            {
                await Clients.Caller.SendAsync(EventosHub.ErrorAlUnirse, $"El nombre '{nombre}' ya está en uso en esta sala.");
                return;
            }

            sala.Jugadores[Context.ConnectionId] = nombre;
            await Groups.AddToGroupAsync(Context.ConnectionId, codigoSala);

            bool esHost = sala.HostId == Context.ConnectionId;
            var jugadoresExistentes = sala.Jugadores
                .Where(j => j.Key != Context.ConnectionId)
                .ToDictionary(j => j.Key, j => j.Value);
            await Clients.Caller.SendAsync(EventosHub.UnidoASala, codigoSala, esHost, jugadoresExistentes);

            foreach (var jugador in sala.Jugadores)
            {
                if (jugador.Key != Context.ConnectionId)
                    await Clients.Caller.SendAsync(EventosHub.JugadorUnido, jugador.Key, jugador.Value);
            }

            await Clients.OthersInGroup(codigoSala)
                .SendAsync(EventosHub.JugadorUnido, Context.ConnectionId, nombre);
        }

        public async Task IniciarJuego(string codigoSala, string formaDeGanar, int intervaloSegundos)
        {
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];

            if (sala.HostId != Context.ConnectionId)
            {
                await Clients.Caller.SendAsync(EventosHub.Error, "Solo el host puede iniciar el juego.");
                return;
            }

            sala.EnJuego = true;
            sala.CartasMencionadas.Clear();
            sala.JugadoresListos.Clear();
            sala.IntervaloSegundos = intervaloSegundos;
            sala.Mazo.Barajar();

            Console.WriteLine($"Juego iniciado en sala {codigoSala} con intervalo {intervaloSegundos}s");

            await Clients.Group(codigoSala).SendAsync(EventosHub.JuegoIniciado, formaDeGanar);
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
                .SendAsync(EventosHub.ActualizarListos, listos, total);

            if (sala.TodosListos())
                await IniciarConteoDeCartas(sala);
        }

        private async Task IniciarConteoDeCartas(Sala sala)
        {
            Console.WriteLine($"Iniciando conteo para sala {sala.Codigo}");
            await _hubContext.Clients.Group(sala.Codigo).SendAsync(EventosHub.ConteoIniciado);

            if (sala.EsManual) return;

            _ = Task.Run(async () =>
            {
                Console.WriteLine($"Task.Run iniciado para sala {sala.Codigo}");

                await Task.Delay(1000);

                while (sala.Mazo.HayCartasRestantes() && sala.EnJuego)
                {
                    while (sala.EstaPausado && sala.EnJuego)
                        await Task.Delay(500);

                    if (!sala.EnJuego) break;

                    var carta = sala.Mazo.SacarCarta();
                    if (carta == null) break;

                    Console.WriteLine($"Enviando carta: {carta.Nombre}");
                    sala.CartasMencionadas.Add(carta.Id);

                    await _hubContext.Clients.Group(sala.Codigo)
                        .SendAsync(EventosHub.CartaMencionada, carta.Id, carta.Nombre);

                    await Task.Delay(sala.IntervaloSegundos * 1000);
                }

                Console.WriteLine($"Loop terminado. EnJuego={sala.EnJuego}");

                if (sala.EnJuego)
                {
                    sala.EnJuego = false;
                    await _hubContext.Clients.Group(sala.Codigo)
                        .SendAsync(EventosHub.JuegoTerminado, "Nadie ganó.");
                }
            });
        }

        public async Task ReclamarLoteria(string codigoSala)
        {
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];
            if (!sala.EnJuego) return;

            await Clients.Caller.SendAsync(EventosHub.VerificarLoteria, sala.CartasMencionadas);
        }
        public async Task JugarDeNuevo(string codigoSala)
        {
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];

            if (sala.HostId != Context.ConnectionId)
            {
                await Clients.Caller.SendAsync(EventosHub.Error, "Solo el host puede reiniciar el juego.");
                return;
            }

            sala.EnJuego = false;
            sala.CartasMencionadas.Clear();
            sala.JugadoresListos.Clear();
            sala.Mazo = new MazoCompartido();

            await _hubContext.Clients.Group(codigoSala).SendAsync(EventosHub.JuegoReiniciado);
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

            await Clients.Caller.SendAsync(EventosHub.CartasRestantes, cartasRestantes);
        }

        public async Task EnviarMensaje(string codigoSala, string mensaje)
        {
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];
            string nombre = sala.Jugadores[Context.ConnectionId];

            await Clients.Group(codigoSala)
                .SendAsync(EventosHub.MensajeRecibido, nombre, mensaje);
        }

        public async Task ActualizarConfiguracion(string codigoSala, bool permitirCartasDobles, bool esManual)
        {
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];
            if (sala.HostId != Context.ConnectionId) return;

            sala.PermitirCartasDobles = permitirCartasDobles;
            sala.EsManual = esManual;

            await Clients.Group(codigoSala)
                .SendAsync(EventosHub.ActualizarConfiguracion, permitirCartasDobles, esManual);
        }

        public async Task PausarJuego(string codigoSala)
        {
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];
            if (sala.HostId != Context.ConnectionId) return;

            sala.EstaPausado = true;
            await Clients.Group(codigoSala)
                .SendAsync(EventosHub.JuegoPausado);
        }

        public async Task ReanudarJuego(string codigoSala)
        {
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];
            if (sala.HostId != Context.ConnectionId) return;

            sala.EstaPausado = false;
            await Clients.Group(codigoSala)
                .SendAsync(EventosHub.JuegoReanudado);
        }

        public async Task CambiarVelocidad(string codigoSala, int nuevoIntervalo)
        {
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];
            if (sala.HostId != Context.ConnectionId) return;

            sala.IntervaloSegundos = nuevoIntervalo;
            await Clients.Group(codigoSala)
                .SendAsync(EventosHub.CambiarVelocidad, nuevoIntervalo);
        }

        public async Task CartaSolicitada(string codigoSala)
        {
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];
            if (sala.HostId != Context.ConnectionId) return;
            if (!sala.EsManual) return;

            var carta = sala.Mazo.SacarCarta();
            if (carta == null)
            {
                sala.EnJuego = false;
                await Clients.Group(codigoSala)
                    .SendAsync(EventosHub.JuegoTerminado, "Nadie ganó.");
                return;
            }

            sala.CartasMencionadas.Add(carta.Id);
            await Clients.Group(codigoSala)
                .SendAsync(EventosHub.CartaMencionada, carta.Id, carta.Nombre);
        }

        public async Task ResultadoLoteria(string codigoSala, bool esValido)
        {
            if (!salas.ContainsKey(codigoSala)) return;

            Sala sala = salas[codigoSala];
            string nombreGanador = sala.Jugadores[Context.ConnectionId];

            if (esValido)
            {
                if (sala.EnDesempate)
                {
                    // Ya hay alguien en desempate — agregar a la lista
                    sala.JugadoresEnDesempate.Add(Context.ConnectionId);

                    if (sala.JugadoresEnDesempate.Count >= 2)
                        await ResolverDesempate(sala, codigoSala);

                    return;
                }

                // Primer reclamo válido — esperar 3 segundos por si alguien más reclama
                sala.JugadoresEnDesempate.Add(Context.ConnectionId);
                sala.EnDesempate = true;

                if (sala.Jugadores.Count > 1)
                {
                    await _hubContext.Clients.Group(codigoSala)
                        .SendAsync(EventosHub.DesempateIniciado, nombreGanador);
                }

                _ = Task.Run(async () =>
                {
                    await Task.Delay(3000);

                    if (sala.JugadoresEnDesempate.Count == 1)
                    {
                        // Solo uno reclamó — gana directamente
                        sala.EnJuego = false;
                        sala.EnDesempate = false;
                        string ganador = sala.Jugadores[sala.JugadoresEnDesempate[0]];
                        sala.JugadoresEnDesempate.Clear();
                        await _hubContext.Clients.Group(codigoSala)
                            .SendAsync(EventosHub.JuegoTerminado, ganador);
                    }
                    else
                    {
                        await ResolverDesempate(sala, codigoSala);
                    }
                });
            }
            else
            {
                await Clients.Caller
                    .SendAsync(EventosHub.LoteriaNovalida, "Tu Lotería no es válida, continúa jugando.");
            }
        }

        private async Task ResolverDesempate(Sala sala, string codigoSala)
        {
            string? ganadorId = null;

            var cartasDesc = sala.CartasMencionadas.OrderByDescending(id => id).ToList();

            foreach (int cartaId in cartasDesc)
            {
                var tienenLaCarta = sala.JugadoresEnDesempate
                    .Where(jId => sala.TablerosJugadores.ContainsKey(jId) &&
                                  sala.TablerosJugadores[jId].Contains(cartaId))
                    .ToList();

                if (tienenLaCarta.Count == 1)
                {
                    ganadorId = tienenLaCarta[0];
                    break;
                }
            }

            sala.EnJuego = false;
            sala.EnDesempate = false;
            sala.JugadoresEnDesempate.Clear();

            string ganador = ganadorId != null
                ? sala.Jugadores[ganadorId]
                : "Nadie ganó";

            await _hubContext.Clients.Group(codigoSala)
                .SendAsync(EventosHub.JuegoTerminado, ganador);
        }
        public async Task EnviarTablero(string codigoSala, List<int> idsCartas)
        {
            if (!salas.ContainsKey(codigoSala)) return;
            salas[codigoSala].TablerosJugadores[Context.ConnectionId] = idsCartas;
            await Task.CompletedTask;
        }
    }
}