using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConesaApp.Database.Data.Entities
{
    [Index(nameof(MetodoPagoID), Name = "metodoPagoID_UQ", IsUnique = true)]
    public class MetodoPago
    {
        [Key] public int MetodoPagoID { get; set; }
        public string Metodo { get; set; }
        public List<Pago>? Pagos { get; set; }
    }
}
