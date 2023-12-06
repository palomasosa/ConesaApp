using ConesaApp.Database.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConesaApp.Database.Data
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cobertura> Coberturas { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<MetodoPago> MetodoPagos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Poliza> Polizas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach ( /// Desactiva la eliminacion en cascada de todas las relaciones
                var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}
