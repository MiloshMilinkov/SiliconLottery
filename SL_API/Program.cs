using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using SL_API.Middleware;
using Microsoft.AspNetCore.Mvc;
using SL_API.Errors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registers my DbContext(StoreContext) as a service to use. We give it the connection string
//which we created inside appsettings.Development.json.
builder.Services.AddDbContext<StoreContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Addind our service only for the lifetime of the http request, good for data gathering!
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Flatting out an error from an object to an array of strings
//Overly complicated code that only does the above and stores the error in the class we created
//The end result is an array of errors displayed as text
builder.Services.Configure<ApiBehaviorOptions>(options =>{
    options.InvalidModelStateResponseFactory=actionContext =>{
        var errors=actionContext.ModelState.Where(e => e.Value.Errors.Count>0)
                                           .SelectMany(x => x.Value.Errors)
                                           .Select(x=>x.ErrorMessage)
                                           .ToArray();

        var errorResponse= new ApiValidationErrorResponse{Errors=errors};
        return new BadRequestObjectResult(errorResponse);
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection(); LATER SEE IF IT IS NEEDED
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

using var scope= app.Services.CreateScope();
var services=scope.ServiceProvider;
var context=services.GetRequiredService<StoreContext>();
var logger=services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    
    logger.LogError(ex,"An error occured during migration.");
}



app.Run();
