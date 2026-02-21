using System.Drawing;
using JuegoDeLoteria.Properties;

namespace JuegoDeLoteria.Juego
{
    public class Carta
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Imagen { get; private set; }

        public Carta(int id, string nombre, string imagen)
        {
            Id = id;
            Nombre = nombre;
            Imagen = imagen;
        }
        public Image ObtenerImagen()
        {
            return (Image)Resources.ResourceManager.GetObject(Imagen)!;
        }



    }
}
