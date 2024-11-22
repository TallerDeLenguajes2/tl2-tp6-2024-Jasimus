using Presupuesto_space;
using PresupuestoDetalle_space;
using ProductoRepository_space;
using Microsoft.Data.Sqlite;
using ClienteRepository_space;
using Cliente_space;
using System.Security.Cryptography.X509Certificates;
using Producto_space;

namespace PresupuestoRepository_space;
public class PresupuestoRepository
{
    string connectionString = "Data Source=db/Tienda.db;Cache=Shared;";
    ClienteRepository cr = new ClienteRepository();
    public int CrearPresupuesto(int clienteId)
    {
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
                
            string query = "INSERT INTO Presupuestos(ClienteId, FechaCreacion) VALUES (@clienteId, @fechaCreacion);";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@clienteId", clienteId);
            command.Parameters.AddWithValue("@fechaCreacion", DateTime.Now);

            return command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public List<Presupuesto> ListarPresupuestos()
    {
        List<Presupuesto> presupuestos = new List<Presupuesto>();
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            
            string query = "SELECT * FROM Presupuestos;";

            SqliteCommand command = new SqliteCommand(query, connection);

            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Cliente cliente = cr.ObtenerCliente(Convert.ToInt32(reader["ClienteId"]));
                    Presupuesto p = new Presupuesto(Convert.ToInt32(reader["idPresupuesto"]), cliente);
                    presupuestos.Add(p);
                }
            }


            connection.Close();
        }
        return presupuestos;
    }

    public Presupuesto DetallePresupuesto(int id)
    {
        Presupuesto presupuesto = null;
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            
            string query = "SELECT * FROM Presupuestos INNER JOIN PresupuestosDetalle USING(idPresupuesto) INNER JOIN Productos USING(idProducto) WHERE idPresupuesto = @id;";
            string query1 = "SELECT * FROM Clientes WHERE ClienteId = @idCliente;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);


            using (var reader = command.ExecuteReader())
            {
                int i = 0;
                while(reader.Read())
                {
                    if (i == 0)
                    {
                        Cliente cliente = cr.ObtenerCliente(Convert.ToInt32(reader["ClienteId"]));
                        presupuesto = new Presupuesto(Convert.ToInt32(reader["idPresupuesto"]), cliente);
                    }
                    PresupuestoDetalle dp = new PresupuestoDetalle(Convert.ToInt32(reader["idProducto"]), reader["Descripcion"].ToString(), Convert.ToInt32(reader["Precio"]), Convert.ToInt32(reader["Cantidad"]));

                    presupuesto.Detalle.Add(dp);
                    i++;
                }            
            }

            SqliteCommand command1 = new SqliteCommand(query1, connection);
            command1.Parameters.AddWithValue("@idCliente", presupuesto.Cliente.ClienteId);

            connection.Close();
            return presupuesto;
        }
    }

    public int AgregarProducto(int idPre, int idProd, int cant)
    {
        var pr = new ProductoRepository();
        var productos = pr.ListarProductos();
        int prod = productos.Count(p => p.IdProducto == idProd);
        var presupuestos = ListarPresupuestos();
        int pres = presupuestos.Count(pr => pr.IdPresupuesto == idPre);

        if(prod != 0 && pres != 0)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                
                string query = "INSERT INTO PresupuestosDetalle(idPresupuesto, idProducto, Cantidad) VALUES (@idPre, @idProd, @cant);";

                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@idPre", idPre);
                command.Parameters.AddWithValue("@idProd", idProd);
                command.Parameters.AddWithValue("@cant", cant);
                
                return command.ExecuteNonQuery();
                connection.Close();
            }

        }
        else return 0;
    }

    public int EliminarPresupuesto(int id)
    {
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            
            string query = "DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @id;";
            string query1 = "DELETE FROM Presupuestos WHERE idPresupuesto = @id;";

            SqliteCommand command1 = new SqliteCommand(query, connection);
            command1.Parameters.AddWithValue("@id", id);
            command1.ExecuteNonQuery();
            SqliteCommand command = new SqliteCommand(query1, connection);
            command.Parameters.AddWithValue("@id", id);
            return command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public Presupuesto ObtenerPresupuesto(int id)
    {
        Presupuesto presupuesto = null;
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            
            string query = "SELECT * FROM presupuestos WHERE idPresupuesto = @id";
            string query1 = "SELECT * FROM productos INNER JOIN (SELECT * FROM PresupuestosDetalle WHERE idPresupuesto = @id) P USING(idProducto)";
            SqliteCommand command1 = new SqliteCommand(query, connection);
            SqliteCommand command2 = new SqliteCommand(query1, connection);
            command1.Parameters.AddWithValue("@id", id);
            command2.Parameters.AddWithValue("@id", id);
            using(var reader = command1.ExecuteReader())
            {
                while(reader.Read())
                {
                    var cliente = cr.ObtenerCliente(Convert.ToInt32(reader["ClienteId"]));
                    presupuesto = new Presupuesto(Convert.ToInt32(reader["idPresupuesto"]), cliente);
                }
            }
            using(var reader = command2.ExecuteReader())
            {
                while(reader.Read())
                {
                    PresupuestoDetalle pd = new PresupuestoDetalle(Convert.ToInt32(reader["idProducto"]), reader["Descripcion"].ToString(), Convert.ToInt32(reader["Precio"]), Convert.ToInt32(reader["Cantidad"]));
                    presupuesto.Detalle.Add(pd);
                }
            }

            connection.Close();
        }
        return presupuesto;
    }
}
