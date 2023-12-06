using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConesaApp.Database.Data.Entities
{
    [Index(nameof(UsuarioID), Name = "UsuarioID_UQ", IsUnique = true)]
    public class Usuario
    {
        [Key] public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Contraseña { get; set; }
        public List<Pago>? Pagos { get; set; }
    }
}
