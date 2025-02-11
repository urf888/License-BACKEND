using System;
using Microsoft.EntityFrameworkCore;
using models;
namespace data;
public partial class PlanAlimentatieSiAntrenamentDbContext : DbContext
{
    public PlanAlimentatieSiAntrenamentDbContext()
    {
    }

    public PlanAlimentatieSiAntrenamentDbContext(DbContextOptions<PlanAlimentatieSiAntrenamentDbContext> options)
        : base(options)
    {
    }

    public DbSet<Alimentatie> Alimentatii { get; set; }
    public DbSet<Antrenament> Antrenamente { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=MSSQLLocalDB;Initial Catalog=PlanAlimentatieSiAntrenamentDB;Integrated Security=True;trusted_connection=true;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
           {
               entity.HasKey(e => e.IdUser).HasName("PK__User__3717C9822D17B98F");

               entity.ToTable("User");

               entity.HasIndex(e => e.Name, "UQ__User__72E12F1B653C9BF8").IsUnique();

               entity.HasIndex(e => e.Email, "UQ__User__AB6E61649F9DA34A").IsUnique();

               entity.Property(e => e.IdUser).HasColumnName("idUser");
               entity.Property(e => e.Email)
                   .HasMaxLength(100)
                   .IsUnicode(false)
                   .HasColumnName("email");
               entity.Property(e => e.Name)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("name");
               entity.Property(e => e.Password)
                   .HasMaxLength(100)
                   .IsUnicode(false)
                   .HasColumnName("password");
               entity.Property(e => e.PhoneNumber)
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .HasColumnName("phoneNumber");
               entity.Property(e => e.Surname)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("surname");
           });
    }
}
