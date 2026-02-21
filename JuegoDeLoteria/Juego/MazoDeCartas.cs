

namespace JuegoDeLoteria.Juego
{
    public class MazoDeCartas
    {
        public List<Carta> cartas;
        public List<Carta> cartasRestantes;

        public MazoDeCartas()
        {
            cartas = new List<Carta>();
            cartasRestantes = new List<Carta>();
            InicializarCartas();
        }

        private void InicializarCartas()
        {
            cartas.Add(new Carta(1, "El Gallo", "el_gallo"));
            cartas.Add(new Carta(2, "El Diablo", "el_diablo"));
            cartas.Add(new Carta(3, "La Dama", "la_dama"));
            cartas.Add(new Carta(4, "El Catrin", "el_catrin"));
            cartas.Add(new Carta(5, "El Paraguas", "el_paraguas"));
            cartas.Add(new Carta(6, "La Sirena", "la_sirena"));
            cartas.Add(new Carta(7, "La Escalera", "la_escalera"));
            cartas.Add(new Carta(8, "La Botella", "la_botella"));
            cartas.Add(new Carta(9, "El Barril", "el_barril"));
            cartas.Add(new Carta(10, "El Arbol", "el_arbol"));
            cartas.Add(new Carta(11, "El Melon", "el_melon"));
            cartas.Add(new Carta(12, "El Valiente", "el_valiente"));
            cartas.Add(new Carta(13, "El Gorrito", "el_gorro"));
            cartas.Add(new Carta(14, "La Muerte", "la_muerte"));
            cartas.Add(new Carta(15, "La Pera", "la_pera"));
            cartas.Add(new Carta(16, "La Bandera", "la_bandera"));
            cartas.Add(new Carta(17, "El Bandolon", "el_bandolon"));
            cartas.Add(new Carta(18, "El Violoncello", "el_violoncello"));
            cartas.Add(new Carta(19, "La Garza", "la_garza"));
            cartas.Add(new Carta(20, "El Pajaro", "el_pajaro"));
            cartas.Add(new Carta(21, "La Mano", "la_mano"));
            cartas.Add(new Carta(22, "La Bota", "la_bota"));
            cartas.Add(new Carta(23, "La Luna", "la_luna"));
            cartas.Add(new Carta(24, "El Cotorro", "el_cotorro"));
            cartas.Add(new Carta(25, "El Borracho", "el_borracho"));
            cartas.Add(new Carta(26, "El Negrito", "el_negrito"));
            cartas.Add(new Carta(27, "El Corazon", "el_corazon"));
            cartas.Add(new Carta(28, "La Sandia", "la_sandia"));
            cartas.Add(new Carta(29, "El Tambor", "el_tambor"));
            cartas.Add(new Carta(30, "El Camaron", "el_camaron"));
            cartas.Add(new Carta(31, "Las Jaras", "las_jaras"));
            cartas.Add(new Carta(32, "El Musico", "el_musico"));
            cartas.Add(new Carta(33, "La Araña", "la_arana"));
            cartas.Add(new Carta(34, "El Soldado", "el_soldado"));
            cartas.Add(new Carta(35, "La Estrella", "la_estrella"));
            cartas.Add(new Carta(36, "El Cazo", "el_cazo"));
            cartas.Add(new Carta(37, "El Mundo", "el_mundo"));
            cartas.Add(new Carta(38, "El Apache", "el_apache"));
            cartas.Add(new Carta(39, "El Nopal", "el_nopal"));
            cartas.Add(new Carta(40, "El Alacran", "el_alacran"));
            cartas.Add(new Carta(41, "La Rosa", "la_rosa"));
            cartas.Add(new Carta(42, "La Calavera", "la_calavera"));
            cartas.Add(new Carta(43, "La Campana", "la_campana"));
            cartas.Add(new Carta(44, "El Cantarito", "el_cantarito"));
            cartas.Add(new Carta(45, "El Venado", "el_venado"));
            cartas.Add(new Carta(46, "El Sol", "el_sol"));
            cartas.Add(new Carta(47, "La Corona", "la_corona"));
            cartas.Add(new Carta(48, "La Chalupa", "la_chalupa"));
            cartas.Add(new Carta(49, "El Pino", "el_pino"));
            cartas.Add(new Carta(50, "El Pescado", "el_pescado"));
            cartas.Add(new Carta(51, "La Palma", "la_palma"));
            cartas.Add(new Carta(52, "La Maceta", "la_maceta"));
            cartas.Add(new Carta(53, "El Arpa", "el_arpa"));
            cartas.Add(new Carta(54, "La Rana", "la_rana"));
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
        public List <Carta> ObtenerTodasLasCartas() => new List<Carta>(cartas);
    }
    
}
