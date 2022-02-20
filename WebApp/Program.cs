using Gas.Services.Devices;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
config.AddJsonFile("local.settings.json", false, true);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(new CosmosClientBuilder(config.GetConnectionString("Cosmos"))
                                .WithSerializerOptions(new CosmosSerializationOptions
                                {
                                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                                })
                                .WithThrottlingRetryOptions(TimeSpan.FromMinutes(1), 32));
builder.Services.AddSingleton<IDeviceService>(new DeviceService(builder.Configuration.GetConnectionString("IotHub")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();

