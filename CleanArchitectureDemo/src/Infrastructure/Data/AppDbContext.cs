
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Catalog> CatalogSet { get; set; }
        public virtual DbSet<Order> OrderSet { get; set; }
        public virtual DbSet<OrderItem> OrderItemSet { get; set; }
        public virtual DbSet<HierarchyItem> HierarchyItemSet { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            builder.Entity<Order>(o =>
            {
                o.OwnsOne(c => c.ResidentAddress);
                o.OwnsOne(c => c.ShippingAddress);
                o.OwnsOne(c => c.Recipient);
                o.OwnsOne(c => c.CreditCard);
            });

            // create self-reference model binding
            builder.Entity<HierarchyItem>()
                .HasMany(p => p.Children)
                .WithOne(c => c.Parent)
                .HasForeignKey(c => c.ParentId)
                .IsRequired(false);

            builder.Entity<HierarchyItem>().OwnsMany(p =>
                p.MetadataList, ownedNavigationBuilder =>
                {
                    ownedNavigationBuilder.ToJson();
                });

            builder.Entity<HierarchyItem>().OwnsOne(p =>
                p.Metadata, ownedNavigationBuilder =>
                {
                    ownedNavigationBuilder.ToJson();
                });
        }
    }
}