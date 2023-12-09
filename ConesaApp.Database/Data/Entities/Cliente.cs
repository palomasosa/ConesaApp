using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConesaApp.Database.Data.Entities
{
    [Index(nameof(ClienteID), Name = "clienteID_UQ", IsUnique = true)]
    public class Cliente
    {
        [Key] public int ClienteID { get; set; }
        public bool Activo { get; set; } = true;
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Mail { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string? Ciudad { get; set; }
        public virtual List<Pago> Pagos { get; set; }
        public virtual List<Vehiculo> Vehiculos { get; set; }
    }
}
