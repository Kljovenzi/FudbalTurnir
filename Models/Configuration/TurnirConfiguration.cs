using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FudbalskiTurnir_FilipNikolic.Models.Configuration
{
    public class TurnirConfiguration : IEntityTypeConfiguration<TurnirModel>
    {
        public void Configure(EntityTypeBuilder<TurnirModel> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Ime).HasMaxLength(50);
            builder.HasMany(p=>p.Timovi)
                   .WithOne(p=>p.Turnir)
                   .HasForeignKey(p=>p.TurnirId);

        }
    }
}
