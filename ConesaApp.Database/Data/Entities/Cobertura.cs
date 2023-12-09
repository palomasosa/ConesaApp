using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConesaApp.Database.Data.Entities
{
    [Index(nameof(CoberturaID), Name = "coberturaID_UQ", IsUnique = true)]
    public class Cobertura
    {
        [Key] public int CoberturaID { get; set; }
        public string Tipo { get; set; }
       public virtual List<Poliza> Polizas { get; set; }
    }
}
