using Microsoft.EntityFrameworkCore;

namespace ConesaApp.Database.Data.Entities
{
    [Index(nameof(PolizaID), Name = "polizaID_UQ", IsUnique = true)]
    public class Poliza
    {
        public int PolizaID { get; set; }
        public int NroPoliza { get; set; }
        public bool Actualizado { get; set; }
        public decimal ValorAsegurado { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime FinVigencia { get; set; }
        public decimal ValorCuota { get; set; }
        public int EmpresaID { get; set; }
        public Empresa Empresa { get; set; }
        public int CoberturaID { get; set; }
        public Cobertura Cobertura { get; set; }
        public virtual List<Pago> Pagos { get; set; }
        public virtual Vehiculo Vehiculo { get; set; }
    }
}
