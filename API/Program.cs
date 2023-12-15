using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using API.Middlewares;
using API.Initializers;

var builder = WebApplication.CreateBuilder(args);
var _config = builder.Configuration;
var isDev = builder.Environment.IsDevelopment();

builder.Services.AddApplicationServices(_config);

var app = builder.Build();

if (!isDev)
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.ConfigureBasicMiddlewares();

if (isDev)
    app.ConfigureDevelopement(_config);
else
    app.ConfigureProduction();

app.UseMiddleware<ExceptionMiddleware>();

app.ConfigureEndpoints();

await
app.ConfigureSeedData();

await app.RunAsync();
