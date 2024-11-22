using Cliente_space;
namespace ClienteRepository_space;
using Microsoft.Data.Sqlite;

public class  ClienteRepository
{
    string connectionString = "Data Source=db/Tienda.db;Cache=Shared;";

    public int CrearCliente(Cliente cliente)
    {
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string query = "INSERT INTO Clientes(Nombre, Email, Telefono) VALUES(@nombre, @email, @telefono);";

            var command = new SqliteCommand(query, connection);

            command.Parameters.AddWithValue("@nombre", cliente.Nombre);
            command.Parameters.AddWithValue("@email", cliente.Email);
            command.Parameters.AddWithValue("@telefono", cliente.Telefono);

            int cant = command.ExecuteNonQuery();

            connection.Close();

            return cant;
        }
    }

    public List<Cliente> ListarClientes()
    {
        List<Cliente> clientes = new List<Cliente>();
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Clientes;";

            var command = new SqliteCommand(query, connection);

            using(var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Cliente cliente = new Cliente(Convert.ToInt32(reader["ClienteId"]), reader["Nombre"].ToString(), reader["Email"].ToString(), reader["Telefono"].ToString());

                    clientes.Add(cliente);
                }
            }
            connection.Close();

        }
        
        return clientes;
    }

    public Cliente ObtenerCliente(int idCliente)
    {
        Cliente cliente = null;
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Clientes WHERE ClienteId = @idCliente;";

            var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@idCliente", idCliente);

            using(var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    cliente = new Cliente(Convert.ToInt32(reader["ClienteId"]), reader["Nombre"].ToString(), reader["Email"].ToString(), reader["Telefono"].ToString());
                }
            }
            connection.Close();

        }
        
        return cliente;
    }
}