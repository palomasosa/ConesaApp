using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConesaApp.Database.Data.Entities
{
    [Index(nameof(ClienteID), Name = "clienteID_UQ", IsUnique = true)]
    public class Cliente
    {
        [Key] public int ClienteID { get; set; }
        [Required(ErrorMessage = "Nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Apellido es obligatorio")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "e-mail es obligatorio")]
        public string? Mail { get; set; }
        [Required(ErrorMessage = "direccion es obligatorio")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "telefono es obligatorio")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "ciudad es obligatorio")]
        public bool Activo { get; set; }
        public string? Ciudad { get; set; }
        public virtual List<Pago> Pagos { get; set; }
        public virtual List<Vehiculo> Vehiculos { get; set; }
    }
}
