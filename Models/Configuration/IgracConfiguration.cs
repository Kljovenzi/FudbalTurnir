using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FudbalskiTurnir_FilipNikolic.Models.Configuration
{
    public class IgracConfiguration : IEntityTypeConfiguration<IgracModel>
    {
        public void Configure(EntityTypeBuilder<IgracModel> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Ime).HasMaxLength(30);
            builder.Property( p=> p.Prezime).HasMaxLength(30);
            builder.HasOne(p => p.Tim)
                .WithMany()
                .HasForeignKey(p => p.TimId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
