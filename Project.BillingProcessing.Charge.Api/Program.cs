

using Project.BillingProcessing.Customer.Api.Photos;
using RabbitMQ.Client;

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

builder.Services.AddSingleton<IRabbitMQConnect>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<RabbitMQConnect>>();
    var factory = new ConnectionFactory()
    {
        HostName = builder.Configuration["HostName"],
        DispatchConsumersAsync = true,
        Uri = new Uri(builder.Configuration["EventBusConnection"])
    };

    if (!string.IsNullOrEmpty(builder.Configuration["EventBusUserName"]))
    {
        factory.UserName = builder.Configuration["EventBusUserName"];
    }

    if (!string.IsNullOrEmpty(builder.Configuration["EventBusPassword"]))
    {
        factory.Password = builder.Configuration["EventBusPassword"];
    }

    var retry = 3;
    if (!string.IsNullOrEmpty(builder.Configuration["EventBusRetryCount"]))
    {
        retry = int.Parse(builder.Configuration["EventBusRetryCount"]);
    }

    return new RabbitMQConnect(factory, logger, retry);
});


builder.Services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
{  
    var rabbitMQConnection = sp.GetRequiredService<IRabbitMQConnect>();    
    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();   
    var retry = 3;
    if (!string.IsNullOrEmpty(builder.Configuration["EventBusRetryCount"]))
    {
        retry = int.Parse(builder.Configuration["EventBusRetryCount"]);
    }
    return new EventBusRabbitMQ(rabbitMQConnection, logger, retry);
});

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
