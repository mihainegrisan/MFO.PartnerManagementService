using MFO.PartnerManagementService.Application;
using MFO.PartnerManagementService.Application.Interfaces;
using MFO.PartnerManagementService.Application.Interfaces.Repositories;
using MFO.PartnerManagementService.Infrastructure.Persistence;
using MFO.PartnerManagementService.Infrastructure.Repositories;
using MFO.PartnerManagementService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Read connection string via GetConnectionString so it works at runtime and design-time
var partnerManagementDbConnection = builder.Configuration.GetConnectionString("PartnerManagementDbContext");
if (string.IsNullOrWhiteSpace(partnerManagementDbConnection))
{
    throw new InvalidOperationException("Connection string 'PartnerManagementDbContext' is not configured. Add it under ConnectionStrings in appsettings.json.");
}

builder.Services.AddHealthChecks()
    // Add a health check for a SQL Server database
    .AddCheck(
        "PartnerManagementDB-check",
        //new SqlConnectionHealthCheck(builder.Configuration["ConnectionStrings:PartnerManagementDbContext"]),
        new SqlConnectionHealthCheck(partnerManagementDbConnection),
        HealthStatus.Unhealthy,
        ["partnermanagementdb"]);

builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

builder.Services.AddControllers();

builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddScoped<IUserContextProvider, UserContextProvider>();
builder.Services.AddDbContext<PartnerManagementDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PartnerManagementDbContext")));
builder.Services.AddScoped<IPartnerQueryRepository, PartnerQueryRepository>();
builder.Services.AddScoped<IPartnerRepository, PartnerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();