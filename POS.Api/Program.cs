using Microsoft.AspNetCore.Hosting;
using POS.Api.Extensions;
using POS.Application.Extensions;
using POS.Infrastructure.Extensions;
using POS.Utilities.AppSettings;
using QuestPDF.Infrastructure;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);
QuestPDF.Settings.License = LicenseType.Community;
var Configuration = builder.Configuration;
// Add services to the container.
//var Cors = "Cors"; profe
//chat gpt

var Cors = "AllowSpecificOrigin";
//var Cors = "Cors";

builder.Services.AddInjectionInfrastructure(Configuration);
builder.Services.AddInjectionApplication(Configuration);
builder.Services.AddAuthentication(Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("GoogleSettings"));

builder.Services.AddCors(options =>
{

    options.AddPolicy(name: Cors,
        builder =>
        {
            // builder.WithOrigins("https://sistemacreditos.netlify.app"); or�gen espec�fico
            builder.WithOrigins("*");
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

var app = builder.Build();
app.UseCors(Cors);

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseWatchDogExceptionLogger();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.UseWatchDog(configuration =>
{
    configuration.WatchPageUsername = Configuration.GetSection("WatchDog:Username").Value;
    configuration.WatchPagePassword = Configuration.GetSection("WatchDog:Password").Value;
});

app.Run();

public partial class Program
{


}