namespace Cliente_space
{
    public class Cliente
    {
        int clienteId;
        string nombre;
        string? email;
        string? telefono;

        public int clienteId {get => clienteId; set => clienteId = value};
        public string Nombre {get => nombre; set => nombre = value};
        public string? Email {get => email; set => email = value};
        public string? Telefono {get => telefono; set => telefono = value};

        public Cliente(int id, string nombre, string email, string telefono)
        {
            clienteId = id;
            this.nombre = nombre;
            this.email = email;
            this.telefono = telefono;
        }
    }
}