using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Product.Api.Filters;
using Product.Api.Middlewares;
using Product.Cache;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

[assembly: ApiConventionType(typeof(DefaultApiConventions))] //Setting convention at application level
namespace Product.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction =>
            {
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                setupAction.Filters.Add(
                    new ProducesDefaultResponseTypeAttribute());

                setupAction.ReturnHttpNotAcceptable = true; //return default media type when unsupported one is applied

                setupAction.OutputFormatters.Add(new XmlSerializerOutputFormatter());

                var jsonOutputFormatter = setupAction.OutputFormatters
                    .OfType<SystemTextJsonOutputFormatter>().FirstOrDefault();

                if (jsonOutputFormatter != null && jsonOutputFormatter.SupportedMediaTypes.Contains("text/json")) // remove text/json as it isn't the approved media type for working with JSON at API level
                    jsonOutputFormatter.SupportedMediaTypes.Remove("text/json");

            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddMemoryCache();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ICacheProvider, CacheProvider>();

            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbContext")).EnableSensitiveDataLogging());

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ProductHelper>();
            services.AddScoped<CategoryHelper>();
            services.AddScoped<ValidateEntityExistsAttribute<LookupCategory>>();
            services.AddScoped<TimerAction>();
            services.AddScoped<ResetCache>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                    // if there are modelstate errors & all keys were correctly
                    // found/parsed we're dealing with validation errors
                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    // if one of the keys wasn't correctly found / couldn't be parsed
                    // we're dealing with null/unparsable input
                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });


            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("ProductApiSpecification", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Product Api",
                    Version = "1",
                    Description = "Through this API you can access product and their features."
                });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/ProductApiSpecification/swagger.json", "Product Api");
                setupAction.RoutePrefix = "";
            });

            app.ConfigureCustomExceptionMiddleware();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
