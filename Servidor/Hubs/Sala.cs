using Compartido;

namespace Servidor.Hubs
{
    public class Sala
    {
        public string Codigo { get; set; }
        public string HostId { get; set; }
        public Dictionary<string, string> Jugadores { get; set; }
        public List<int> CartasMencionadas { get; set; }
        public bool EnJuego { get; set; }
        public MazoCompartido Mazo { get; set; }
        public HashSet<string> JugadoresListos { get; set; }
        public int IntervaloSegundos { get; set; }
        public bool PermitirCartasDobles { get; set; }
        public bool EstaPausado { get; set; }
        public bool EsManual { get; set; }
        public List<string> JugadoresEnDesempate { get; set; } = new List<string>();
        public bool EnDesempate { get; set; }
        public Dictionary<string, List<int>> TablerosJugadores { get; set; } = new();

        public Sala(string codigo, string hostId)
        {
            Codigo = codigo;
            HostId = hostId;
            Jugadores = new Dictionary<string, string>();
            CartasMencionadas = new List<int>();
            EnJuego = false;
            Mazo = new MazoCompartido();
            JugadoresListos = new HashSet<string>();
            IntervaloSegundos = 5;
            PermitirCartasDobles = false;
            EstaPausado = false;
            EsManual = false;
        }

        public bool TodosListos()
        {
            return Jugadores.Count > 0 &&
                   JugadoresListos.Count == Jugadores.Count;
        }
    }
}