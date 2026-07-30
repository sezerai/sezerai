using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Persistence.Configurations;

public class BlogYazisiConfiguration : IEntityTypeConfiguration<BlogYazisi>
{
    public void Configure(EntityTypeBuilder<BlogYazisi> builder)
    {
        builder.ToTable("BlogYazilari");

        // Note: BlogYazisi doesn't inherit from BaseEntity, so configure Id manually
        builder.HasKey(b => b.Baslik); // Temporary - should add Id property

        builder.Property(b => b.Baslik)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.Slug)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(b => b.Ozet)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(b => b.IcerikHtml)
            .IsRequired();

        builder.Property(b => b.KapakGorseli)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(b => b.Yazar)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.MetaBaslik)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.MetaAciklama)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(b => b.MetaAnahtarKelimeler)
            .IsRequired()
            .HasMaxLength(500);

        // Indexes
        builder.HasIndex(b => b.Slug).IsUnique();
        builder.HasIndex(b => b.YayinTarihi);
    }
}
