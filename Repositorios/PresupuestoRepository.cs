using Presupuesto_space;
using PresupuestoDetalle_space;
using ProductoRepository_space;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography.X509Certificates;

namespace PresupuestoRepository_space;
public class PresupuestoRepository
{
    string connectionString = "Data Source=db/Tienda.db;Cache=Shared;";
    public int CrearPresupuesto(Presupuesto presupuesto)
    {
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
                
            string query = "INSERT INTO Presupuestos(NombreDestinatario, FechaCreacion) VALUES (@nombreDest, @fechaCreacion);";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@nombreDest", presupuesto.NombreDestinatario);
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
                    Presupuesto p = new Presupuesto(Convert.ToInt32(reader["idPresupuesto"]), reader["NombreDestinatario"].ToString());
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

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            using (var reader = command.ExecuteReader())
            {
                int i = 0;
                while(reader.Read())
                {
                    if (i == 0)
                    {
                        presupuesto = new Presupuesto(Convert.ToInt32(reader["idPresupuesto"]), reader["NombreDestinatario"].ToString());
                    }
                    PresupuestoDetalle dp = new PresupuestoDetalle(Convert.ToInt32(reader["idProducto"]), reader["Descripcion"].ToString(), Convert.ToInt32(reader["Precio"]), Convert.ToInt32(reader["Cantidad"]));

                    presupuesto.Detalle.Add(dp);
                    i++;
                }
                return presupuesto;
            }

            connection.Close();
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

}
