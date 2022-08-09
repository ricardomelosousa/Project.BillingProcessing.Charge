

namespace Project.BillingProcessing.Customer.Api;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddSingleton<MongoContext>();
        services.AddScoped<IDataService, DataService>();
        services.AddGrpc();
        services.AddScoped<IChargeService, ChargeService>();
        services.AddScoped<IChargeRepository, ChargeRepository>();
        services.AddScoped<IChargeAppService, ChargeAppService>();
        services.AddControllersWithViews();
        services.AddAutoMapper(typeof(Startup));
        services.AddScoped<CustomersGrpcService>();
        services.AddGrpcClient<CustomerProtoService.CustomerProtoServiceClient>(opt=> opt.Address = new Uri(Configuration["GrpcService:CustomerUrl"]));
       



    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employees}/{action=Index}/{id?}");
            //endpoints.MapGrpcService<GreeterService>();
        });
    }
}

