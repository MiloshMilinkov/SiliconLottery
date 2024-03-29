using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using SL_API.Middleware;
using Microsoft.AspNetCore.Mvc;
using SL_API.Errors;
using SL_API.Extensions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Core.Entities.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection(); LATER SEE IF IT IS NEEDED
app.UseStaticFiles();


app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope= app.Services.CreateScope();
var services=scope.ServiceProvider;
var context=services.GetRequiredService<StoreContext>();
var identityContext=services.GetRequiredService<AppIdentityDbContext>();
var userManager=services.GetRequiredService<UserManager<AppUser>>();
var logger=services.GetRequiredService<ILogger<Program>>();

try
{
    await identityContext.Database.MigrateAsync();
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
    await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
}
catch (Exception ex)
{
    logger.LogError(ex,"An error occured during migration.");
}

app.Run();
