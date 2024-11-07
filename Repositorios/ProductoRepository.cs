using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;
using Producto_space;

namespace ProductoRepository_space;
public class ProductoRepository
{
    string connectionString = "Data Source=db/Tienda.db;Cache=Shared;";

    public int CrearProducto(Producto producto)
    {
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
                
            string query = "INSERT INTO Productos(Descripcion, Precio) VALUES (@descripcion, @precio);";

            SqliteCommand command = new SqliteCommand(query, connection);
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
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Productos;";
            SqliteCommand command = new SqliteCommand(query, connection);
            
            var productos = new List<Producto>();

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

            string query = "DELETE FROM Productos WHERE idProducto = @id;";
            SqliteCommand command = new SqliteCommand(query, connection);

            command.Parameters.AddWithValue("@id", id);

            return command.ExecuteNonQuery();

            connection.Close();
        }
    }
}