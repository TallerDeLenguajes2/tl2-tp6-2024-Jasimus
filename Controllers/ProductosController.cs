using Microsoft.AspNetCore.Mvc;
using Producto_space;
using ProductoRepository_space;
namespace tp6_tallerII.Controllers;
public class ProductosController : Controller
{
    ProductoRepository pr = new ProductoRepository();
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult ListarProductos()
    {
        return View(pr.ListarProductos());
    }

    [HttpGet]
    public IActionResult CrearProducto()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CrearProducto(Producto producto)
    {
        int cant = pr.CrearProducto(producto);
        return RedirectToAction("Index", "Productos");
    }

    [HttpGet]
    public IActionResult ModificarProducto(int id)
    {
        var producto = pr.ObtenerProducto(id);
        return View(producto);
    }

    [HttpPost]
    public IActionResult ModificarProducto(Producto producto)
    {
        int cant = pr.ModificarProducto(producto.IdProducto, producto);
        return View(cant);
    }

    [HttpDelete]
    public IActionResult EliminarProducto(int id)
    {
        int cant = pr.EliminarProducto(id);
        return View(cant);
    }
}