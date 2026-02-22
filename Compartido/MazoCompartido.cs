namespace Compartido
{
    public class MazoCompartido
    {
        private List<CartaInfo> _cartas;
        private List<CartaInfo> _restantes;

        public MazoCompartido()
        {
            _cartas = new List<CartaInfo>();
            _restantes = new List<CartaInfo>();
            InicializarCartas();
        }

        private void InicializarCartas()
        {
            _cartas.Add(new CartaInfo(1, "El Gallo"));
            _cartas.Add(new CartaInfo(2, "El Diablo"));
            _cartas.Add(new CartaInfo(3, "La Dama"));
            _cartas.Add(new CartaInfo(4, "El Catrin"));
            _cartas.Add(new CartaInfo(5, "El Paraguas"));
            _cartas.Add(new CartaInfo(6, "La Sirena"));
            _cartas.Add(new CartaInfo(7, "La Escalera"));
            _cartas.Add(new CartaInfo(8, "La Botella"));
            _cartas.Add(new CartaInfo(9, "El Barril"));
            _cartas.Add(new CartaInfo(10, "El Arbol"));
            _cartas.Add(new CartaInfo(11, "El Melon"));
            _cartas.Add(new CartaInfo(12, "El Valiente"));
            _cartas.Add(new CartaInfo(13, "El Gorrito"));
            _cartas.Add(new CartaInfo(14, "La Muerte"));
            _cartas.Add(new CartaInfo(15, "La Pera"));
            _cartas.Add(new CartaInfo(16, "La Bandera"));
            _cartas.Add(new CartaInfo(17, "El Bandolon"));
            _cartas.Add(new CartaInfo(18, "El Violoncello"));
            _cartas.Add(new CartaInfo(19, "La Garza"));
            _cartas.Add(new CartaInfo(20, "El Pajaro"));
            _cartas.Add(new CartaInfo(21, "La Mano"));
            _cartas.Add(new CartaInfo(22, "La Bota"));
            _cartas.Add(new CartaInfo(23, "La Luna"));
            _cartas.Add(new CartaInfo(24, "El Cotorro"));
            _cartas.Add(new CartaInfo(25, "El Borracho"));
            _cartas.Add(new CartaInfo(26, "El Negrito"));
            _cartas.Add(new CartaInfo(27, "El Corazon"));
            _cartas.Add(new CartaInfo(28, "La Sandia"));
            _cartas.Add(new CartaInfo(29, "El Tambor"));
            _cartas.Add(new CartaInfo(30, "El Camaron"));
            _cartas.Add(new CartaInfo(31, "Las Jaras"));
            _cartas.Add(new CartaInfo(32, "El Musico"));
            _cartas.Add(new CartaInfo(33, "La Arana"));
            _cartas.Add(new CartaInfo(34, "El Soldado"));
            _cartas.Add(new CartaInfo(35, "La Estrella"));
            _cartas.Add(new CartaInfo(36, "El Cazo"));
            _cartas.Add(new CartaInfo(37, "El Mundo"));
            _cartas.Add(new CartaInfo(38, "El Apache"));
            _cartas.Add(new CartaInfo(39, "El Nopal"));
            _cartas.Add(new CartaInfo(40, "El Alacran"));
            _cartas.Add(new CartaInfo(41, "La Rosa"));
            _cartas.Add(new CartaInfo(42, "La Calavera"));
            _cartas.Add(new CartaInfo(43, "La Campana"));
            _cartas.Add(new CartaInfo(44, "El Cantarito"));
            _cartas.Add(new CartaInfo(45, "El Venado"));
            _cartas.Add(new CartaInfo(46, "El Sol"));
            _cartas.Add(new CartaInfo(47, "La Corona"));
            _cartas.Add(new CartaInfo(48, "La Chalupa"));
            _cartas.Add(new CartaInfo(49, "El Pino"));
            _cartas.Add(new CartaInfo(50, "El Pescado"));
            _cartas.Add(new CartaInfo(51, "La Palma"));
            _cartas.Add(new CartaInfo(52, "La Maceta"));
            _cartas.Add(new CartaInfo(53, "El Arpa"));
            _cartas.Add(new CartaInfo(54, "La Rana"));
        }

        public void Barajar()
        {
            _restantes = new List<CartaInfo>(_cartas);
            Random rng = new Random();
            _restantes = _restantes.OrderBy(c => rng.Next()).ToList();
        }

        public CartaInfo? SacarCarta()
        {
            if (_restantes.Count == 0) return null;
            CartaInfo sacada = _restantes[0];
            _restantes.RemoveAt(0);
            return sacada;
        }

        public bool HayCartasRestantes() => _restantes.Count > 0;

        public List<CartaInfo> ObtenerTodasLasCartas() => new List<CartaInfo>(_cartas);
    }
}