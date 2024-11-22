using PresupuestoDetalle_space;
using Cliente_space;
namespace Presupuesto_space
{
    public class Presupuesto
    {   
        int idPresupuesto;
        Cliente cliente;
        List<PresupuestoDetalle> detalle;

        public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value; }

        public Presupuesto()
        {
            idPresupuesto = 0;
        }

        public Presupuesto(int idPresupuesto, Cliente cliente)
        {
            detalle = new List<PresupuestoDetalle>();
            this.idPresupuesto = idPresupuesto;
            this.cliente = cliente;
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