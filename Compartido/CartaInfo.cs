namespace Compartido
{
    public class CartaInfo
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        public CartaInfo(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
