using Microsoft.Extensions.Configuration.AzureAppConfiguration;

var builder = WebApplication.CreateBuilder(args);

var allowedSpecificOrigins = "allowedSpecificOrigins";
var azureAppConfigConnectionString = builder.Configuration["AppConfigConnectionString"];

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddAzureAppConfiguration(options =>
    {
        options.Connect(azureAppConfigConnectionString)
        .Select(KeyFilter.Any, hostingContext.HostingEnvironment.EnvironmentName);
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
