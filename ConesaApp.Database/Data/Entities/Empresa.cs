using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConesaApp.Database.Data.Entities
{
    [Index(nameof(EmpresaID), Name = "empresaID_UQ", IsUnique = true)]
    public class Empresa
    {
        [Key] public int EmpresaID { get; set; }
        public string Nombre { get; set; }
        public List<Poliza> Polizas { get; set; }
    }
}
