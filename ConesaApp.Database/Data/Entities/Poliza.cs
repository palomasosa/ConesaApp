//using ConesaApp.Database.Migrations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConesaApp.Database.Data.Entities
{
    [Index(nameof(PolizaID), Name = "polizaID_UQ", IsUnique = true)]
    public class Poliza
    {
        public int PolizaID { get; set; }
        [Required(ErrorMessage = "Nro de poliza es obligatorio")]
        public int NroPoliza { get; set; }
        public bool Actualizado { get; set; }
        [Required(ErrorMessage = "Valor asegurado es obligatorio")]
        public decimal ValorAsegurado { get; set; }
        [Required(ErrorMessage = "Inicio vigencia es obligatorio")]
        public DateTime InicioVigencia { get; set; }
        [Required(ErrorMessage = "Fin vigencia es obligatorio")]
        public DateTime FinVigencia { get; set; }
        [Required(ErrorMessage = " Valor cuota es obligatorio")]
        public decimal ValorCuota { get; set; }
        public int EmpresaID { get; set; }
        public Empresa Empresa { get; set; }
        public int CoberturaID { get; set; }
        public Cobertura Cobertura { get; set; }
        public virtual List<Pago> Pagos { get; set; }
        public virtual Vehiculo Vehiculo { get; set; }
    }
}
