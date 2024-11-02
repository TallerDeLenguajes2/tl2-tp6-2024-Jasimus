using Microsoft.AspNetCore.Mvc;
using Producto_space;
using ProductoRepository_space;

[ApiController]
[Route("Productos")]

public class ProductosController : ControllerBase
{
    ProductoRepository pr = new ProductoRepository();
    [HttpGet]
    public IActionResult ListarProductos()
    {
        return View(pr.ListarProductos);
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