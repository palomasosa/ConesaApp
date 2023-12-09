using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConesaApp.Database.Data.Entities
{
    [Index(nameof(VehiculoID), Name = "vehiculoID_UQ", IsUnique = true)]
    public class Vehiculo
    {
        [Key] public int VehiculoID { get; set; }
        [Required(ErrorMessage = "Año es obligatorio")]
        public int? Año { get; set; }
        [Required(ErrorMessage = "Patente es obligatorio")]
        public string Patente { get; set; }
        [Required(ErrorMessage = " Marca es obligatorio")]
        public string? Marca { get; set; }
        public int ClienteID { get; set; }
        public virtual Cliente Cliente { get; set; }
        public int PolizaID { get; set; }
        public virtual Poliza Poliza { get; set; }
    }
}
