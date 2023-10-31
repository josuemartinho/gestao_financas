using Microsoft.EntityFrameworkCore;
using sistemaFinanceiro.Models;

namespace sistemaFinanceiro.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<TransacaoModel> Transacoes { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }
    }

}
