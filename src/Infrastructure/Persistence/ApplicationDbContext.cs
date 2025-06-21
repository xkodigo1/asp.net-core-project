using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<UserSpecialization> UserSpecializations { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<ServiceType> ServiceTypes { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<ServiceOrder> ServiceOrders { get; set; }
    public DbSet<Diagnostic> Diagnostics { get; set; }
    public DbSet<DiagnosticDetail> DiagnosticDetails { get; set; }
    public DbSet<Spare> Spares { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryDetail> InventoryDetails { get; set; }
    public DbSet<Audit> Audits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Aplicar todas las configuraciones de este ensamblado
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    entry.State = EntityState.Modified;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
} 