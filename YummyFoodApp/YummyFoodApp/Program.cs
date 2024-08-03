using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using System.Text.Json.Serialization;
using YummyFood.BAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(j =>
{
    j.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    j.JsonSerializerOptions.PropertyNamingPolicy = null;
    j.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// IPAddress config
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});


// configureService
ConfigureServices.RegistreSevice(builder.Services, builder.Configuration);

// Configure Serilog
var _log = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(_log);




var app = builder.Build();
//app.MapGet("/", () => $"EnvironmentName: {app.Environment.EnvironmentName}\n" 
//                       + $"ApplicationName: {app.Environment.ApplicationName}\n"
//                       +$"WebRootPath:{app.Environment.WebRootPath}\n"
//                       +$"ContentRootPath:{app.Environment.ContentRootPath}");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders();
app.UseStaticFiles();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
