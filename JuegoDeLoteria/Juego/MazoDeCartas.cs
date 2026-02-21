using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JuegoDeLoteria.Juego
{
    public class MazoDeCartas
    {
        public List<Carta> cartas { get; private set; }
        public List<Carta> cartasRestantes { get; private set; }

        public MazoDeCartas()
        {
            cartas = new List<Carta>();
            cartasRestantes = new List<Carta>();
            InicializarCartas();
        }

        private void InicializarCartas()
        {
            cartas.Add(new Carta(1, "El Gallo", "gallo.png"));
            cartas.Add(new Carta(2, "El Diablo", "diablo.png"));
            cartas.Add(new Carta(3, "La Dama", "dama.png"));
            cartas.Add(new Carta(4, "El Catrín", "catrin.png"));
            cartas.Add(new Carta(5, "El Paraguas", "paraguas.png"));
            cartas.Add(new Carta(6, "La Sirena", "sirena.png"));
            cartas.Add(new Carta(7, "La Escalera", "escalera.png"));
            cartas.Add(new Carta(8, "La Botella", "botella.png"));
            cartas.Add(new Carta(9, "El Barril", "barril.png"));
            cartas.Add(new Carta(10, "El Árbol", "arbol.png"));
            cartas.Add(new Carta(11, "El Melón", "melon.png"));
            cartas.Add(new Carta(12, "El Valiente", "valiente.png"));
            cartas.Add(new Carta(13, "El Gorrito", "gorrito.png"));
            cartas.Add(new Carta(14, "La Muerte", "muerte.png"));
            cartas.Add(new Carta(15, "La Pera", "pera.png"));
            cartas.Add(new Carta(16, "La Bandera", "bandera.png"));
            cartas.Add(new Carta(17, "El Bandolón", "bandolon.png"));
            cartas.Add(new Carta(18, "El Violoncello", "violoncello.png"));
            cartas.Add(new Carta(19, "La Garza", "garza.png"));
            cartas.Add(new Carta(20, "El Pájaro", "pajaro.png"));
            cartas.Add(new Carta(21, "La Mano", "mano.png"));
            cartas.Add(new Carta(22, "La Bota", "bota.png"));
            cartas.Add(new Carta(23, "La Luna", "luna.png"));
            cartas.Add(new Carta(24, "El Cotorro", "cotorro.png"));
            cartas.Add(new Carta(25, "El Borracho", "borracho.png"));
            cartas.Add(new Carta(26, "El Negrito", "negrito.png"));
            cartas.Add(new Carta(27, "El Corazón", "corazon.png"));
            cartas.Add(new Carta(28, "La Sandía", "sandia.png"));
            cartas.Add(new Carta(29, "El Tambor", "tambor.png"));
            cartas.Add(new Carta(30, "El Camarón", "camaron.png"));
            cartas.Add(new Carta(31, "Las Jaras", "jaras.png"));
            cartas.Add(new Carta(32, "El Músico", "musico.png"));
            cartas.Add(new Carta(33, "La Araña", "arana.png"));
            cartas.Add(new Carta(34, "El Soldado", "soldado.png"));
            cartas.Add(new Carta(35, "La Estrella", "estrella.png"));
            cartas.Add(new Carta(36, "El Cazo", "cazo.png"));
            cartas.Add(new Carta(37, "El Mundo", "mundo.png"));
            cartas.Add(new Carta(38, "El Apache", "apache.png"));
            cartas.Add(new Carta(39, "El Nopal", "nopal.png"));
            cartas.Add(new Carta(40, "El Alacrán", "alacran.png"));
            cartas.Add(new Carta(41, "La Rosa", "rosa.png"));
            cartas.Add(new Carta(42, "La Calavera", "calavera.png"));
            cartas.Add(new Carta(43, "La Campana", "campana.png"));
            cartas.Add(new Carta(44, "El Cantarito", "cantarito.png"));
            cartas.Add(new Carta(45, "El Venado", "venado.png"));
            cartas.Add(new Carta(46, "El Sol", "sol.png"));
            cartas.Add(new Carta(47, "La Corona", "corona.png"));
            cartas.Add(new Carta(48, "La Chalupa", "chalupa.png"));
            cartas.Add(new Carta(49, "El Pino", "pino.png"));
            cartas.Add(new Carta(50, "El Pescado", "pescado.png"));
            cartas.Add(new Carta(51, "La Palma", "palma.png"));
            cartas.Add(new Carta(52, "La Maceta", "maceta.png"));
            cartas.Add(new Carta(53, "El Arpa", "arpa.png"));
            cartas.Add(new Carta(54, "La Rana", "rana.png"));
        }

        public void Barajar()
        {
            cartasRestantes = new List<Carta>(cartas);
            Random rng = new Random();
            cartasRestantes = cartasRestantes.OrderBy(c => rng.Next()).ToList();
        }

        public Carta? SacarCarta()
        {
            if (cartasRestantes.Count == 0)
                return null;
            Carta sacada = cartasRestantes[0];
            cartasRestantes.RemoveAt(0);
            return sacada;
        }
        public bool HayCartasRestantes() => cartasRestantes.Count > 0;
        public List <Carta> ObtenerCartasRestantes() => new List<Carta>(cartasRestantes);
    }
    
}
