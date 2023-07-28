using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FudbalskiTurnir_FilipNikolic.Models.Configuration
{
    public class RezultatConfiguration : IEntityTypeConfiguration<RezultatModel>
    {
        public void Configure(EntityTypeBuilder<RezultatModel> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Tim_1_BrojGolova).HasColumnType("int");
            builder.Property(p=> p.Tim_2_BrojGolova).HasColumnType("int");
            builder.Property(p => p.Tim_1_Ime).HasMaxLength(100);
            builder.Property(p => p.Tim_2_Ime).HasMaxLength(100);
            builder.Property(p=>p.Tim_1_Id).HasColumnType("int");
            builder.Property(p => p.Tim_2_Id).HasColumnType("int");
            builder.HasOne(p => p.Turnir)
                .WithMany()
                .HasForeignKey(p => p.TurnirId);
        }
    }
}
