using Project.BillingProcessing.Customer.Api.Photos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();
builder.Services.AddScoped<IChargeService, ChargeService>();
builder.Services.AddScoped<IChargeRepository, ChargeRepository>();
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddScoped<IChargeAppService, ChargeAppService>();
builder.Services.AddSingleton<MongoContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<CustomersGrpcService>();
builder.Services.AddGrpcClient<CustomerProtoService.CustomerProtoServiceClient>(opt => opt.Address = new Uri(builder.Configuration["GrpcService:CustomerUrl"]));

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
