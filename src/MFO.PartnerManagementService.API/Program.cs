using MFO.PartnerManagementService.Application.Interfaces;
using MFO.PartnerManagementService.Application.Interfaces.Repositories;
using MFO.PartnerManagementService.Infrastructure.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddScoped<IUserContextProvider, UserContextProvider>();
builder.Services.AddDbContext<PartnerManagementDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PartnerManagementDbContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();