using Biblioteca.Domain;
using Biblioteca.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Persistence;

public class BibliotecaDbContext : DbContext
{
    public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options)
        : base(options)
    {
    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseDomainModel &&
                   (e.State == EntityState.Added || e.State == EntityState.Modified));
        foreach (var entry in entries)
        {
            var entity = (BaseDomainModel)entry.Entity;
            var now = DateTime.UtcNow;
            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = now;
            }
            else
            {
                // Preserve the original CreatedAt value on updates
                entry.Property(nameof(BaseDomainModel.CreatedAt)).IsModified = false;
            }
            entity.UpdatedAt = now;
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Author>? Authors { get; set; }
    public DbSet<Book>? Books { get; set; }
    public DbSet<Loan>? Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Name).IsRequired().HasMaxLength(100);
            entity.Property(a => a.LastName).IsRequired().HasMaxLength(100);
            entity.HasIndex(a => new { a.Name, a.LastName })
                  .HasDatabaseName("IX_Authors_Name_LastName");


        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
            entity.Property(b => b.NumberOfPages).IsRequired();

            entity.Property(b => b.AuthorId).IsRequired();
            // Ensure NumberOfPages is greater than 0 at the database level
            entity.HasCheckConstraint("CK_Books_NumberOfPages", "[NumberOfPages] > 0");
        });


        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(l => l.LoanDate).IsRequired();
            entity.Property(l => l.ReturnDate).IsRequired(false);
            entity.Property(l => l.BorrowerName).IsRequired();
            entity.Property(l => l.BookId).IsRequired();

        });


        // Configure the Author-Book relationship
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<Book>()
            .HasMany(a => a.Loans)
            .WithOne(b => b.Book)
            .HasForeignKey(b => b.BookId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

    }


}