using CarApp.Infrastructure.Data;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            
            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection") ?? 
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<CarDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit= false;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<CarDbContext>();                

            return services;
        }
        public static void RegisterRepositories(this IServiceCollection services, Assembly modelsAssembly)
        {
            Type[] typesToExclude = new Type[] { typeof(ApplicationUser) };

            Type[] modelsType = modelsAssembly
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface &&
                        !t.Name.ToLower().EndsWith("attribute"))
                .ToArray();

            foreach (Type type in modelsType)
            {
                if (!typesToExclude.Contains(type))
                {
                    Type repositoryInterface = typeof(IRepository<,>);
                    Type repositoryInstanceType = typeof(BaseRepository<,>);

                    PropertyInfo? idPropInfo = type
                        .GetProperties()
                        .Where(p => p.Name.ToLower() == "id")
                        .SingleOrDefault();

                    Type[] constructArgs = new Type[2];
                    constructArgs[0] = type;

                    if (idPropInfo == null)
                    {
                        constructArgs[1] = typeof(object);
                    }
                    else
                    {
                        constructArgs[1] = idPropInfo.PropertyType;
                    }

                    repositoryInterface = repositoryInterface.MakeGenericType(constructArgs);
                    repositoryInstanceType = repositoryInstanceType.MakeGenericType(constructArgs);

                    services.AddScoped(repositoryInterface, repositoryInstanceType);
                }
            }
        }
        public static void RegisterUserDefinedServices(this IServiceCollection services, Assembly serviceAssembly)
        {
            Type[] serviceInterfaceTypes = serviceAssembly
                .GetTypes()
                .Where(t => t.IsInterface)
                .ToArray();

            Type[] serviceTypes = serviceAssembly
                .GetTypes()
                .Where(t => !t.IsInterface &&
                            !t.IsAbstract &&
                             t.Name.ToLower().EndsWith("service"))
                .ToArray();

            foreach (Type serviceInterfaceType in serviceInterfaceTypes)
            {
                //hardcoded. Change ASAP
                if(serviceInterfaceType.Name.ToLower() == "dropdownviewmodel")
                {
                    continue;
                }
                Type? serviceType = serviceTypes
                    .SingleOrDefault(t => "i" + t.Name.ToLower() == serviceInterfaceType.Name.ToLower());

                if (serviceType == null)
                {
                    throw new NullReferenceException($"Service type could not be obtained for the service {serviceInterfaceType.Name}");
                }
                services.AddScoped(serviceInterfaceType, serviceType);
            }
        }
    }       
}
