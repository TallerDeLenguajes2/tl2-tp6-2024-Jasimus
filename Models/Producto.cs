namespace Producto_space
{
    public class Producto
    {
        int idProducto;
        string descripcion;
        int precio;

        public int IdProducto { get => idProducto; set => idProducto = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Precio { get => precio; set => precio = value; }

        public Producto(int id, string descripcion, int? precio)
        {
            IdProducto = id;
            Descripcion = descripcion;
            Precio = precio ?? default(int);
        }

        public Producto()
        {
            
        }
    }
}