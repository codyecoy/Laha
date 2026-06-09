using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SahabatAnak.Api.Models;

namespace SahabatAnak.Api.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<ServiceItem> Services => Set<ServiceItem>();
    public DbSet<TeamMember> Teams => Set<TeamMember>();
    public DbSet<CaseReport> CaseReports => Set<CaseReport>();
    public DbSet<ContactMessage> Contacts => Set<ContactMessage>();
    public DbSet<GalleryPhoto> GalleryPhotos => Set<GalleryPhoto>();
    public DbSet<DailyVisit> DailyVisits => Set<DailyVisit>();
    public DbSet<Donation> Donations => Set<Donation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppUser>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<Article>().HasIndex(x => x.Slug).IsUnique();
        modelBuilder.Entity<DailyVisit>().HasIndex(x => x.Day).IsUnique();

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType)) continue;
            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(BuildIsDeletedFilter(entityType.ClrType));
        }

        var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var listStringConverter = new ValueConverter<List<string>, string>(
            v => JsonSerializer.Serialize(v ?? new List<string>(), jsonOptions),
            v => JsonSerializer.Deserialize<List<string>>(v ?? "[]", jsonOptions) ?? new List<string>());

        var listStringComparer = new ValueComparer<List<string>>(
            (a, b) => (a ?? new List<string>()).SequenceEqual(b ?? new List<string>()),
            v => (v ?? new List<string>()).Aggregate(0, (acc, x) => HashCode.Combine(acc, x.GetHashCode())),
            v => (v ?? new List<string>()).ToList());

        modelBuilder.Entity<ServiceItem>().Property(x => x.Highlights).HasConversion(listStringConverter).Metadata.SetValueComparer(listStringComparer);
        modelBuilder.Entity<CaseReport>().Property(x => x.EvidenceUrls).HasConversion(listStringConverter).Metadata.SetValueComparer(listStringComparer);
    }

    private static LambdaExpression BuildIsDeletedFilter(Type entityType)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
        var condition = Expression.Equal(property, Expression.Constant(false));
        return Expression.Lambda(condition, parameter);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                entry.Entity.UpdatedAt = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
