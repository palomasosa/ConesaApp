using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConesaApp.Database.Data.Entities
{
    [Index(nameof(PagoID), Name = "pagoID_UQ", IsUnique = true)]
    public class Pago
    {
        [Key] public int PagoID { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        //CLAVES FORÁNEAS
        public int PolizaID { get; set; }
        public Poliza Poliza { get; set; }
        public int ClienteID { get; set; }
        public Cliente Cliente { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
        public int MetodoPagoID { get; set; }
        public MetodoPago MetodoPago { get; set; }  
    }
}
