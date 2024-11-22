using ClienteRepository_space;
using Microsoft.AspNetCore.Mvc;
using Cliente_space;

namespace ClientesController_space;

public class ClientesController : Controller
{
    ClienteRepository cr = new ClienteRepository();


    [HttpGet]
    public IActionResult Index()
    {
        return RedirectToAction("ListarClientes", "Clientes");
    }

    [HttpGet]
    public IActionResult ListarClientes()
    {
        var clientes = cr.ListarClientes();
        return View(clientes);
    }

    [HttpGet]
    public IActionResult CrearCliente()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CrearCliente(Cliente cliente)
    {

        int cant = cr.CrearCliente(cliente);
        if (cant != 0)
        {
            return RedirectToAction("Index", "Clientes");
        }
        ViewBag.error = "no se pudo crear el cliente";
        return View();
    }

    [HttpGet]
    public IActionResult ModificarCliente(int id)
    {
        return View();
    }

    // [HttpPost]
    // public IActionResult ModificarCliente(Cliente cliente)
    // {

    // }

    [HttpGet]
    public IActionResult EliminarCliente(int id)
    {
        return View();
    }

    // [HttpPost]
    // public IActionResult EliminarCliente(Cliente cliente)
    // {
        
    // }

}