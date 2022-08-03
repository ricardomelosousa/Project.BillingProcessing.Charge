using Project.BillingProcessing.Charge.Api.Models;
using Project.BillingProcessing.Charge.Domain.SeedWork;
using Project.BillingProcessing.Charge.Domain.Service;
using Project.BillingProcessing.Charge.Domain.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ChargeDatabaseSettings>(
    builder.Configuration.GetSection("ChargeDatabase"));

builder.Services.AddScoped<IDocument, Document>();
builder.Services.AddScoped<IChargeService, ChargeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
