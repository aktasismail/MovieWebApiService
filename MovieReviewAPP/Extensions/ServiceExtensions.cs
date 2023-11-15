using BusinessLayer.Abstact;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EfCore;
using EntitiesLayer;
using EntitiesLayer.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Filters;

namespace MovieReviewAPP.Extensions
{
    public static class ServiceExtensions
    {
        public static void DbContextConfigure(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<MovieDbContext>(optios =>
        optios.UseSqlServer(configuration.GetConnectionString("MovieConnection")));
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        public static void ConfigureServiceManager(this IServiceCollection services)=>
            services.AddScoped<IServiceManager, ServiceManager>();
        public static void ConfigureLinks(this IServiceCollection services)=>
            services.AddScoped<IMovieLinks,MovieLinks>();
        public static void ConfigureLogService(this IServiceCollection services) =>
            services.AddSingleton<ILogService, LoggerManager>();
        public static void ConfigureFilter(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilter>();
            services.AddScoped<MediaTypeAttribute>();
        }
        public static void ConfigureCors (this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .WithExposedHeaders("X-Pagination"));
            });
        }
        public static void ConfigureDataShaper(this IServiceCollection services)
        {
            services.AddScoped<IDataShaper<MovieDTO>, DataShaper<MovieDTO>>();
        }
        public static void ConfigureCustomMediaType(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                var JsonOutput = options.OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();
                if (JsonOutput is not null)
                {
                    JsonOutput.SupportedMediaTypes.Add("application/ismailaktas.hateoas+json");
                    JsonOutput.SupportedMediaTypes.Add("application/ismailaktas.apiroot+json");
                }
                var XmlOutput = options.OutputFormatters
                .OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();
                if (XmlOutput is not null)
                {
                    XmlOutput.SupportedMediaTypes.Add("application/ismailaktas.hateoas+xml");
                    XmlOutput.SupportedMediaTypes.Add("application/ismailaktas.apiroot+xml");
                }
            });
        }
        public static void ConfigureVerisoning(this IServiceCollection services)
        {
            services.AddApiVersioning(option =>
            {
                option.ReportApiVersions= true;
                option.AssumeDefaultVersionWhenUnspecified= true;
                option.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(option =>
            {
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 8;
                option.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<MovieDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
