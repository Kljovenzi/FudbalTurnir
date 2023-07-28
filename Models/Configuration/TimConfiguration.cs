using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FudbalskiTurnir_FilipNikolic.Models.Configuration
{
    public class TimConfiguration : IEntityTypeConfiguration<TimModel>
    {
        public void Configure(EntityTypeBuilder<TimModel> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            builder.Property(p => p.Ime).HasMaxLength(50);
            builder.Property(p => p.BrojPobeda).HasColumnType("int");
            builder.Property(p => p.BrojIzgubljenih).HasColumnType("int");
            builder.Property(p => p.BrojRemija).HasColumnType("int");
            builder.HasOne(p => p.Turnir)
                .WithMany()
                .HasForeignKey(p => p.TurnirId)
                .OnDelete(DeleteBehavior.SetNull);
           builder.Property(p => p.BrojOsvojenihBodovaNaTurniru).HasColumnType("int");
        }
    }
}
