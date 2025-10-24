using Microsoft.EntityFrameworkCore;
using nvd_231230727_de02.Models;

namespace nvd_231230727_de02.Data;

public partial class Ngovandung231230727De02Context : DbContext
{
    public Ngovandung231230727De02Context()
    {
    }

    public Ngovandung231230727De02Context(DbContextOptions<Ngovandung231230727De02Context> options)
        : base(options)
    {
    }

    public virtual DbSet<NvdCatalog> NvdCatalogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Dung;Initial Catalog=ngovandung_231230727_de02;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NvdCatalog>(entity =>
        {
            entity.HasKey(e => e.NvdId).HasName("PK__NvdCatal__CA45E3779A41E60A");

            entity.ToTable("NvdCatalog");

            entity.Property(e => e.NvdId)
                .ValueGeneratedNever()
                .HasColumnName("nvdId");
            entity.Property(e => e.NvdCateActive).HasColumnName("nvdCateActive");
            entity.Property(e => e.NvdCateName)
                .HasMaxLength(255)
                .HasColumnName("nvdCateName");
            entity.Property(e => e.NvdCatePrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("nvdCatePrice");
            entity.Property(e => e.NvdCateQty).HasColumnName("nvdCateQty");
            entity.Property(e => e.NvdPicture).HasColumnName("nvdPicture");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
