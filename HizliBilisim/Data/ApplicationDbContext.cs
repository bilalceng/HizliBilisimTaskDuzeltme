using HizliBilisim.models;
using Microsoft.EntityFrameworkCore;

namespace HizliBilisim.data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceLine> InvoiceLines { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.UserId);
        modelBuilder.Entity<Customer>().HasKey(c => c.CustomerId);
        modelBuilder.Entity<Invoice>().HasKey(i => i.InvoiceId);
        modelBuilder.Entity<InvoiceLine>().HasKey(il => il.InvoiceLineId);
        
        
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.User)
            .WithMany(u => u.Customers)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Customer)
            .WithMany(c => c.Invoices)
            .HasForeignKey(i => i.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.User)
            .WithMany(u => u.Invoices)
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<InvoiceLine>()
            .HasOne(il => il.Invoice)
            .WithMany(i => i.InvoiceLines)
            .HasForeignKey(il => il.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<InvoiceLine>()
            .HasOne(il => il.User)
            .WithMany(u => u.InvoiceLines)
            .HasForeignKey(il => il.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    }