using B2B.Pricing.Domain.Models;
using B2B.Pricing.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace B2B.Pricing.Infrastructure.Seeds
{
    public class DbInitializer
    {


        public static void Initialize(AppDbContext db)
        {
            // Apply migrations to ensure the database schema is up to date
            db.Database.Migrate();

            // Seed demo data only if tables are empty
            if (!db.Products.Any() && !db.Customers.Any() && !db.CustomerGroups.Any() && !db.CustomerGroupPricings.Any() && !db.ProductTierPricings.Any() && !db.CustomerContractPricings.Any() && !db.ProductInventories.Any() && !db.CartItems.Any())
            {
                // 1. Seed Products
                var product1 = new Product { Name = "Laptop", Description = "High-performance laptop", BasePrice = 1000m };
                var product2 = new Product { Name = "Smartphone", Description = "Latest smartphone", BasePrice = 700m };
                var product3 = new Product { Name = "Headphones", Description = "Noise-canceling headphones", BasePrice = 150m };
                var product4 = new Product { Name = "Keyboard", Description = "Mechanical keyboard", BasePrice = 120m };
                var product5 = new Product { Name = "Mouse", Description = "Wireless mouse", BasePrice = 50m };

                db.Products.AddRange(product1, product2, product3, product4, product5);

                // Save Products to generate IDs
                db.SaveChanges();

                // 2. Seed Customer Groups
                var group1 = new CustomerGroup { Name = "VIP Customers", Description = "Premium group with special discounts" };
                var group2 = new CustomerGroup { Name = "Standard Customers", Description = "Regular group for all customers" };

                db.CustomerGroups.AddRange(group1, group2);

                // Save Customer Groups to generate IDs
                db.SaveChanges();

                // 3. Seed Tier Rules (for Product and Group tiers)
                var productTier1 = new ProductTierPricing
                {
                    ProductId = product1.Id, // Laptop
                    MinQuantity = 1,
                    MaxQuantity = 10,
                    TierPrice = 950m
                };

                var productTier2 = new ProductTierPricing
                {
                    ProductId = product1.Id, // Laptop
                    MinQuantity = 11,
                    MaxQuantity = 50,
                    TierPrice = 900m
                };

                var groupTier1 = new CustomerGroupPricing
                {
                    CustomerGroupId = group1.Id,
                    ProductId = product1.Id,
                    MinQuantity = 1,
                    MaxQuantity = 10,
                    GroupPrice = 920m
                };

                var groupTier2 = new CustomerGroupPricing
                {
                    CustomerGroupId = group2.Id,
                    ProductId = product2.Id, // Smartphone
                    MinQuantity = 1,
                    MaxQuantity = 5,
                    GroupPrice = 650m
                };

                var productTier3 = new ProductTierPricing
                {
                    ProductId = product3.Id, // Headphones
                    MinQuantity = 1,
                    MaxQuantity = 20,
                    TierPrice = 130m
                };

                db.ProductTierPricings.AddRange(productTier1, productTier2, productTier3);
                db.CustomerGroupPricings.AddRange(groupTier1, groupTier2);

                // Save Tier Rules to generate IDs
                db.SaveChanges();

                // 4. Seed Customers (Save Customers before adding Contract Pricing)
                var customer1 = new Customer { Name = "John Doe", ContactDetails = "john@example.com", CustomerGroupId = group1.Id };
                var customer2 = new Customer { Name = "Jane Smith", ContactDetails = "jane@example.com", CustomerGroupId = group2.Id };
                var customer3 = new Customer { Name = "Mark Johnson", ContactDetails = "mark@example.com", CustomerGroupId = null }; // No group, no contract
                var customer4 = new Customer { Name = "Sarah Lee", ContactDetails = "sarah@example.com", CustomerGroupId = null }; // No group, no contract

                db.Customers.AddRange(customer1, customer2, customer3, customer4);

                // Save Customers to generate IDs
                db.SaveChanges();

                // 5. Seed Contract Rules (Date-bounded contract pricing)
                var contract1 = new CustomerContractPricing
                {
                    CustomerId = customer1.Id, // Now that customer1 exists
                    ProductId = product1.Id,
                    Price = 850m,
                    ValidFrom = DateTime.UtcNow.AddDays(-1),
                    ValidTo = DateTime.UtcNow.AddMonths(1)
                };

                var contract2 = new CustomerContractPricing
                {
                    CustomerId = customer2.Id, // Now that customer2 exists
                    ProductId = product2.Id,
                    Price = 650m,
                    ValidFrom = DateTime.UtcNow.AddDays(-2),
                    ValidTo = DateTime.UtcNow.AddMonths(2)
                };

                db.CustomerContractPricings.AddRange(contract1, contract2);

                // Save Contract Rules to generate IDs
                db.SaveChanges();

                // 6. Seed Product Inventory
                var inventory1 = new ProductInventory { ProductId = product1.Id, QuantityAvailable = 100 };
                var inventory2 = new ProductInventory { ProductId = product2.Id, QuantityAvailable = 50 };
                var inventory3 = new ProductInventory { ProductId = product3.Id, QuantityAvailable = 200 };
                var inventory4 = new ProductInventory { ProductId = product4.Id, QuantityAvailable = 150 };
                var inventory5 = new ProductInventory { ProductId = product5.Id, QuantityAvailable = 300 };

                db.ProductInventories.AddRange(inventory1, inventory2, inventory3, inventory4, inventory5);

                // Save Product Inventory
                db.SaveChanges();

                // 7. Seed Cart Items (for testing pricing and cart functionality)
                var cartItem1 = new CartItem
                {
                    ProductId = product1.Id,
                    Quantity = 2,
                    ResolvedPrice = 950m, // Assuming the product is at the product tier price
                    PriceSource = "product_tier"
                };

                var cartItem2 = new CartItem
                {
                    ProductId = product2.Id,
                    Quantity = 3,
                    ResolvedPrice = 650m, // Assuming group pricing for customer group 2
                    PriceSource = "group_tier"
                };

                var cartItem3 = new CartItem
                {
                    ProductId = product3.Id,
                    Quantity = 5,
                    ResolvedPrice = 130m, // Assuming product tier pricing
                    PriceSource = "product_tier"
                };

                db.CartItems.AddRange(cartItem1, cartItem2, cartItem3);

                // Save Cart Items
                db.SaveChanges();
            }
        }


    }
}
