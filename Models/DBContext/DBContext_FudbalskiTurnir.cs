using FudbalskiTurnir_FilipNikolic.Models.Configuration;
using FudbalskiTurnir_FilipNikolic.Models.Repository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FudbalskiTurnir_FilipNikolic.Models.DBContext
{
    public class DBContext_FudbalskiTurnir : IdentityDbContext
    {
        public DBContext_FudbalskiTurnir(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TimModel> Timovi { get; set; }
        public DbSet<IgracModel> Igraci { get; set; }
        public DbSet<RezultatModel> Rezultati { get; set; }
        public DbSet<TurnirModel> Turniri { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new IgracConfiguration());
            builder.ApplyConfiguration(new TimConfiguration());
            builder.ApplyConfiguration(new RezultatConfiguration());
            builder.ApplyConfiguration(new TurnirConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
