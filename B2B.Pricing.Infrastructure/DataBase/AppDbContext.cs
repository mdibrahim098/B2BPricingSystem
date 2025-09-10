using B2B.Pricing.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace B2B.Pricing.Infrastructure.DataBase
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets for each entity
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<ProductTierPricing> ProductTierPricings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerGroup> CustomerGroups { get; set; }
        public DbSet<CustomerGroupPricing> CustomerGroupPricings { get; set; }
        public DbSet<CustomerContractPricing> CustomerContractPricings { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        // Configure the model relationships and table names using Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Product Tier Pricing relationship
            modelBuilder.Entity<Product>()
                .HasMany(p => p.TierPricing)
                .WithOne(pt => pt.Product)
                .HasForeignKey(pt => pt.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Customer and CustomerGroup relationship
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.CustomerGroup)
                .WithMany(g => g.Customers)
                .HasForeignKey(c => c.CustomerGroupId)
                .OnDelete(DeleteBehavior.SetNull); // If the customer group is deleted, set customer group to null

            // Configure CustomerGroupPricing and Product relationship
            modelBuilder.Entity<CustomerGroupPricing>()
                .HasOne(cgp => cgp.Product)
                .WithMany(p => p.CustomerGroupPricing)
                .HasForeignKey(cgp => cgp.ProductId);

            // Configure CustomerContractPricing and Customer relationship
            modelBuilder.Entity<CustomerContractPricing>()
                .HasOne(ccp => ccp.Customer)
                .WithMany(c => c.Contracts)
                .HasForeignKey(ccp => ccp.CustomerId);
        }

    }
}
