using Microsoft.AspNetCore.Mvc;
using Producto_space;
using ProductoRepository_space;
namespace tp6_tallerII.Controllers;
public class ProductosController : Controller
{
    ProductoRepository pr = new ProductoRepository();
    public IActionResult Index()
    {
        return View("ListarProductos", pr.ListarProductos());
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
        return RedirectToAction("ListarProductos", "Productos");
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
        return RedirectToAction("ListarProductos", "Productos");
    }

    [HttpGet]
    public IActionResult EliminarProducto(int id)
    {
        var producto = pr.ObtenerProducto(id);
        return View(producto);
    }

    [HttpPost]
    public IActionResult EliminarProducto(Producto producto)
    {
        int cant = pr.EliminarProducto(producto.IdProducto);
        return RedirectToAction("ListarProductos", "Productos");
    }
}