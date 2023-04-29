using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using SL_API.Middleware;
using Microsoft.AspNetCore.Mvc;
using SL_API.Errors;

namespace SL_API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration config)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //Registers my DbContext(StoreContext) as a service to use. We give it the connection string
            //which we created inside appsettings.Development.json.
            services.AddDbContext<StoreContext>(opt => {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            //Addind our service only for the lifetime of the http request, good for data gathering!
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Flatting out an error from an object to an array of strings
            //Overly complicated code that only does the above and stores the error in the class we created
            //The end result is an array of errors displayed as text
            services.Configure<ApiBehaviorOptions>(options =>{
                options.InvalidModelStateResponseFactory=actionContext =>{
                    var errors=actionContext.ModelState.Where(e => e.Value.Errors.Count>0)
                                                    .SelectMany(x => x.Value.Errors)
                                                    .Select(x=>x.ErrorMessage)
                                                    .ToArray();

                    var errorResponse= new ApiValidationErrorResponse{Errors=errors};

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;    
        }
    }
}