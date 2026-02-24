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
        }

        public bool TodosListos()
        {
            return Jugadores.Count > 0 &&
                   JugadoresListos.Count == Jugadores.Count;
        }
    }
}