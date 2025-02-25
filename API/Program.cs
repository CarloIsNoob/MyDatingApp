using System.Text;
using API.Data;
using API.Entities;
using API.Extensions;
using API.Extentions;
using API.Interfaces;
using API.Middleware;
using API.Services;
using API.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExeptionMiddleware>();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
.WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();

app.UseAuthorization();

app.UseWebSockets();

app.MapControllers();

app.MapHub<PresenceHub>("hubs/presence");

app.MapHub<MessageHub>("hubs/message");

using var scoped = app.Services.CreateScope();

var services = scoped.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    await context.Database.MigrateAsync();
    await context.Database.ExecuteSqlRawAsync("DELETE FROM [Connections]");
    await Seed.SeedUsers(userManager, roleManager);
}
catch (Exception ex)
{
    
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
