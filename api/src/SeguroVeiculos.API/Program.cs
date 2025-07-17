using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SeguroVeiculos.Application.Services;
using SeguroVeiculos.Application.Validators;
using SeguroVeiculos.Domain.Interfaces;
using SeguroVeiculos.Infrastructure.Data;
using SeguroVeiculos.Infrastructure.Repositories;
using SeguroVeiculos.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Entity Framework
builder.Services.AddDbContext<SeguroVeiculosDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                     "Data Source=seguro_veiculos.db"));

// MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(SeguroVeiculos.Application.Commands.CriarSeguroCommand).Assembly);
});

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CriarSeguroCommandValidator>();

// Repositories
builder.Services.AddScoped<ISeguroRepository, SeguroRepository>();
builder.Services.AddScoped<ISeguradorepository, SeguradorRepository>();

// Services
builder.Services.AddHttpClient<ISeguradorService, SeguradorService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SeguroVeiculosDbContext>();
    context.Database.EnsureCreated();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run("http://0.0.0.0:5000");

