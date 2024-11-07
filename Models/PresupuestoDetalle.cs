using Producto_space;

namespace PresupuestoDetalle_space
{
    public class PresupuestoDetalle
    {
        Producto producto;
        int cantidad;

        public Producto Producto { get => producto; set => producto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }

        public PresupuestoDetalle(int? idProducto, string? descripcionProducto, int? precio, int? cantidad)
        {
            producto = new Producto(idProducto, descripcionProducto, precio);
            this.cantidad = cantidad ?? default(int);
        }
    }
}