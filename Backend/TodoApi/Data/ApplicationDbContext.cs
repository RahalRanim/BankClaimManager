using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using System.Numerics;

namespace TodoApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Compte> Comptes { get; set; }
        public DbSet<Reclamation> Reclamations { get; set; }
    }
}

