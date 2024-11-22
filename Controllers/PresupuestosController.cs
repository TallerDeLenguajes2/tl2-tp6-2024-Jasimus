using Microsoft.AspNetCore.Mvc;
using Presupuesto_space;
using PresupuestoDetalle_space;
using PresupuestoRepository_space;
using ProductoRepository_space;
using ClienteRepository_space;
using tp6_tallerII.Controllers;

public class PresupuestosController : Controller
{
    PresupuestoRepository pr = new PresupuestoRepository();
    ProductoRepository productos = new ProductoRepository();
    ClienteRepository cr = new ClienteRepository();

    [HttpGet]
    public IActionResult Index()
    {
        return View("ListarPresupuestos", pr.ListarPresupuestos());
    }

    [HttpGet]
    public IActionResult ListarPresupuestos()
    {
        return View(pr.ListarPresupuestos());
    }

    [HttpGet]

    public IActionResult CrearPresupuesto()
    {
        var clientes = cr.ListarClientes();
        return View(clientes);
    }

    [HttpPost]

    public IActionResult CrearPresupuesto(int idCliente)
    {
        int cant = pr.CrearPresupuesto(idCliente);
        return RedirectToAction("ListarPresupuestos", "Presupuestos");
    }

    [HttpGet("{id}")]
    public IActionResult DetallePresupuesto(int id)
    {
        int cant = pr.ListarPresupuestos().Count(pr => pr.IdPresupuesto == id);
        if(cant!=0)
        {
            var presupuesto = pr.ObtenerPresupuesto(id);
            return View(presupuesto);

        }
        else return RedirectToAction("Index", "Presupuestos");
    }

    [HttpGet]
    public IActionResult AgregarProducto(int id)
    {
        var presupuesto = pr.ObtenerPresupuesto(id);
        if(presupuesto != null)
        {
            ViewBag.Presupuesto = presupuesto;
            var produc = productos.ListarProductos();
            ViewBag.cantidadProductos = 0;
            foreach(PresupuestoDetalle pd in presupuesto.Detalle)
            {
                int i = produc.FindIndex(p => p.IdProducto == pd.Producto.IdProducto);
                produc.RemoveAt(i);
            }
            ViewBag.Productos = produc;
            return View();
        }
        else return RedirectToAction("Index", "Presupuestos");
    }

    [HttpPost]
    public IActionResult AgregarProducto(int idPre, int idProd, int cant)
    {
        int c = pr.AgregarProducto(idPre, idProd, cant);
        return RedirectToAction("DetallePresupuesto", "Presupuestos", new {id = idPre});
    }

    [HttpGet]
    public IActionResult AgregarProductoCantidad(int idPre, int idProd)
    {
        var presupuesto = pr.ObtenerPresupuesto(idPre);
        int cant = presupuesto.Detalle.Count(d => d.Producto.IdProducto == idProd);
        if(cant == 0)
        {
            var producto = productos.ObtenerProducto(idProd);

            ViewBag.IdPre = idPre;
            ViewBag.Producto = producto;
            return View();
        }

        return RedirectToAction("DetallePresupuesto", "Presupuestos", new {id = idPre});

    }

    [HttpGet]
    public IActionResult EliminarPresupuesto(int id)
    {
        var presupuesto = pr.ObtenerPresupuesto(id);
        return View(presupuesto);
    }

    [HttpPost]
    public IActionResult EliminarPresupuesto(Presupuesto presupuesto)
    {
        int i = presupuesto.IdPresupuesto;
        int cant = pr.EliminarPresupuesto(i);
        return RedirectToAction("ListarPresupuestos", "Presupuestos");
    }


}