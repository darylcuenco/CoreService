using core.Implementations;
using core.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IDBm, DBMImplementations>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
int port = configuration.GetValue<int>("PORT");
string key = configuration.GetValue<string>("CERT_KEY");
#if DEBUG
string path = @"..\..\..\..\certs\my-cert.pfx";
#else
string path = configuration.GetValue<string>("CERT_PATH");
#endif
Console.WriteLine("base path: " + Path.Combine(AppContext.BaseDirectory, path) + " port: " + port + " cert path: " + path + " cert key: " + key);
builder.WebHost.UseKestrel(options =>
{
    //options.ListenAnyIP(port);
    options.ListenAnyIP(port, listenOptions =>
    {
        listenOptions.UseHttps(Path.Combine(AppContext.BaseDirectory, path), key);
    }); // Listen on the specified port
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
app.UseRouting();
app.MapControllers();
app.Run();


