using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProductCatalog.API.Configuration;
using ProductCatalog.Core.Extensions;
using ProductCatalog.Infrastructure.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
.AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddSingleton<IConfigureOptions<MvcOptions>>(sp =>
{
    var jsonOpts = sp.GetRequiredService<IOptionsMonitor<JsonOptions>>();
    return new ConfigureMvcJsonOption(jsonOpts);
});

builder.Services.AddCore(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder.WithOrigins("https://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
