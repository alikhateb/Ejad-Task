using Ejad.Api.Middleware;
using Ejad.Api.Middleware.Exception;
using Ejad.Core;
using Ejad.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddCore();
builder.Services.AddSingleton<ExceptionMiddleware>();

var app = builder.Build();
app.UseExceptionMiddleware();
app.UseAutomaticMigration<AppDbContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();
app.Run();
