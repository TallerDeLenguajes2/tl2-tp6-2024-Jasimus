using PresupuestoDetalle_space;
namespace Presupuesto_space
{
    public class Presupuesto
    {
        int idPresupuesto;
        string nombreDestinatario;
        List<PresupuestoDetalle> detalle;

        public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
        public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
        public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value; }


        public Presupuesto(int idPresupuesto, string nombreDestinatario)
        {
            detalle = new List<PresupuestoDetalle>();
            this.idPresupuesto = idPresupuesto;
            this.nombreDestinatario = nombreDestinatario;
        }

        public int MontoPresupuesto()
        {
            int monto=0;
            foreach (PresupuestoDetalle p in Detalle)
            {
                monto += p.Producto.Precio;
            }
            return monto;
        }

        public double MontoPresupuestoConIva(double iva)
        {
            return MontoPresupuesto()*(1+iva);
        }

        public int CantidadProductos()
        {
            int cant=0;
            foreach(PresupuestoDetalle p in Detalle)
            {
                cant += p.Cantidad;
            }
            
            return cant;
        }
    }
}