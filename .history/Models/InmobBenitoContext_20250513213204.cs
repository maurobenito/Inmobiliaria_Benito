using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria_Benito.Models
{
    public class InmobBenitoContext : DbContext
    {
        public InmobBenitoContext(DbContextOptions<InmobBenitoContext> options)
            : base(options)
        {
        }

        public DbSet<Inquilino> Inquilinos { get; set; }
        // otras DbSet...
    }
}
