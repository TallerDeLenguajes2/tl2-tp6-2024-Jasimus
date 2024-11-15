using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Producto_space;
using tp6_tallerII.Controllers;

namespace ProductoRepository_space;
public class ProductoRepository
{
    string connectionString = "Data Source=db/Tienda.db;Cache=Shared;";

    public int CrearProducto(Producto producto)
    {
           string query = "INSERT INTO Productos(Descripcion, Precio) VALUES (@descripcion, @precio);";
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            
            SqliteCommand command = new SqliteCommand(query, connection);
           
            // command.Parameters.Add(new SqliteParameter("@descripcion", producto.Descripcion));
            // command.Parameters.Add(new SqliteParameter("@precio", producto.Precio));
             command.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            command.Parameters.AddWithValue("@precio", producto.Precio);

            return command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public int ModificarProducto(int id, Producto producto)
    {
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string query = "UPDATE Productos SET Descripcion = @descripcion, Precio = @precio WHERE idProducto = @id;";
            
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            command.Parameters.AddWithValue("@precio", producto.Precio);
            command.Parameters.AddWithValue("@id", id);

            return command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public List<Producto> ListarProductos()
    {
        List<Producto> productos = new List<Producto>();
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Productos;";
            SqliteCommand command = new SqliteCommand(query, connection);
            

            using(var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Producto p = new Producto(Convert.ToInt32(reader["idProducto"]), reader["Descripcion"].ToString(), Convert.ToInt32(reader["Precio"]));
                    productos.Add(p);
                }
            }
            return productos;
            connection.Close();
        }
    }

    public Producto ObtenerProducto(int id)
    {
        Producto producto = null;
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Productos WHERE idProducto = @id;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);


            using(var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    producto = new Producto(Convert.ToInt32(reader["idProducto"]), reader["Descripcion"].ToString(), Convert.ToInt32(reader["Precio"]));
                }
            }

            connection.Close();
        }
        return producto;
    }

    public int EliminarProducto(int id)
    {
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string query1 = "DELETE FROM PresupuestosDetalle WHERE idProducto = @id";
            string query = "DELETE FROM Productos WHERE idProducto = @id;";
            SqliteCommand command = new SqliteCommand(query, connection);
            var command1 = new SqliteCommand(query1, connection);

            command.Parameters.AddWithValue("@id", id);
            command1.Parameters.AddWithValue("@id", id);

            command1.ExecuteNonQuery();
            return command.ExecuteNonQuery();

            connection.Close();
        }
    }
}