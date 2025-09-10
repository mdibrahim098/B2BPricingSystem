using B2B.Pricing.Application.Services;
using B2B.Pricing.Application.Services.Interfaces;
using B2B.Pricing.Infrastructure.DataBase;
using B2B.Pricing.Infrastructure.Repository;
using B2B.Pricing.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Register the ApplicationDbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register services
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerGroupRepository, CustomerGroupRepository>();
builder.Services.AddScoped<ICustomerContractPricingRepository, CustomerContractPricingRepository>();
builder.Services.AddScoped<IProductTierPricingRepository, ProductTierPricingRepository>();
builder.Services.AddScoped<ICustomerGroupPricingRepository, CustomerGroupPricingRepository>();


// Add services to the container.
builder.Services.AddControllers();


// Add Swagger for API documentation
builder.Services.AddSwaggerGen(); // Only need this once

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger for API
    app.UseSwaggerUI(); // Enable Swagger UI for browsing the API
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Apply migrations automatically on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Initialize(db); // Seed the database
}

app.Run();
