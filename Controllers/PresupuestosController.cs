using Microsoft.AspNetCore.Mvc;
using Presupuesto_space;
using PresupuestoRepository_space;
using ProductoRepository_space;
using tp6_tallerII.Controllers;

public class PresupuestosController : Controller
{
    PresupuestoRepository pr = new PresupuestoRepository();
    ProductoRepository productos = new ProductoRepository();

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
        return View();
    }

    [HttpPost]

    public IActionResult CrearPresupuesto(Presupuesto presupuesto)
    {
        int cant = pr.CrearPresupuesto(presupuesto);
        return RedirectToAction("Index", "Presupuestos");
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
            ViewBag.Productos = productos.ListarProductos();
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

        ViewBag.Mensaje = "El producto ya está incluído en este presupuesto";
        return RedirectToAction("DetallePresupuesto", "Presupuestos", new {id = idPre});

    }

}