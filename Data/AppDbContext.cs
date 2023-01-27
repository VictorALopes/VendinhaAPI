using Microsoft.EntityFrameworkCore;
using Vendinha.Models;

namespace Vendinha.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Divida> Dividas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=BD_Vendinha;Cache=Shared");
        }
    }
}