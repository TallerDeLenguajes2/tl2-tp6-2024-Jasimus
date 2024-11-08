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

    [HttpPost]
    public IActionResult CrearProducto(Producto producto)
    {
        int cant = pr.CrearProducto(producto);
        return View(cant);
    }

    [HttpPut]
    public IActionResult ModificarProducto(int id, Producto producto)
    {
        int cant = pr.ModificarProducto(id, producto);
        return View(cant);
    }

    [HttpDelete]
    public IActionResult EliminarProducto(int id)
    {
        int cant = pr.EliminarProducto(id);
        return View(cant);
    }
}