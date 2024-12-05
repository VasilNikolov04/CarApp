using CarApp.Core.Services.Contracts;
using CarApp.Infrastructure.Constants.Enum;
using CarApp.Infrastructure.Data;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static CarApp.Infrastructure.Constants.ApplicationConstants;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.RegisterRepositories(typeof(ApplicationUser).Assembly);

            services.RegisterUserDefinedServices(typeof(ICarListingService).Assembly);

            services.AddHttpClient();

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
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
            })
            .AddRoles<IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<CarDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            return services;
        }
        public static void RegisterRepositories(this IServiceCollection services, Assembly modelsAssembly)
        {
            Type[] typesToExclude = new Type[] { typeof(ApplicationUser), typeof(ReportReason) };

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

        public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app,
            string email, string username, string password, string fname, string lname)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateAsyncScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            RoleManager<IdentityRole>? roleManager = serviceProvider
               .GetService<RoleManager<IdentityRole>>();
            IUserStore<ApplicationUser>? userStore = serviceProvider
                .GetService<IUserStore<ApplicationUser>>();
            UserManager<ApplicationUser>? userManager = serviceProvider
                .GetService<UserManager<ApplicationUser>>();

            if (roleManager == null)
            {
                throw new ArgumentNullException(nameof(roleManager),
                    $"Service for {nameof(RoleManager<IdentityRole>)} cannot be obtained");
            }
            if (userStore == null)
            {
                throw new ArgumentNullException(nameof(userStore),
                    $"Service for {typeof(IUserStore<ApplicationUser>)} cannot be obtained!");
            }

            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager),
                    $"Service for {typeof(UserManager<ApplicationUser>)} cannot be obtained!");
            }
            Task.Run(async () =>
            {
                bool roleExists = await roleManager.RoleExistsAsync(AdminRoleName);
                IdentityRole? adminRole = null;
                if (!roleExists)
                {
                    adminRole = new IdentityRole(AdminRoleName);

                    IdentityResult result = await roleManager.CreateAsync(adminRole);
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException($"Error occurred while creating the {AdminRoleName} role!");
                    }
                }
                else
                {
                    adminRole = await roleManager.FindByNameAsync(AdminRoleName);
                }

                ApplicationUser? adminUser = await userManager.FindByEmailAsync(email);
                if (adminUser == null)
                {
                    adminUser = await
                        CreateAdminUserAsync(email, username, password, fname, lname, userStore, userManager);
                }

                if (await userManager.IsInRoleAsync(adminUser, AdminRoleName))
                {
                    return app;
                }

                IdentityResult userResult = await userManager.AddToRoleAsync(adminUser, AdminRoleName);
                if (!userResult.Succeeded)
                {
                    throw new InvalidOperationException($"Error occurred while adding the user {username} to the {AdminRoleName} role!");
                }

                return app;
            })
                .GetAwaiter()
                .GetResult();

            return app;

        }
        private static async Task<ApplicationUser> CreateAdminUserAsync(string email, string username, 
            string password, string fname, string lname,IUserStore<ApplicationUser> userStore, 
            UserManager<ApplicationUser> userManager)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                Email = email,
                FirstName = fname,
                LastName = lname,
            };

            await userStore.SetUserNameAsync(applicationUser, username, CancellationToken.None);
            IdentityResult result = await userManager.CreateAsync(applicationUser, password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error occurred while registering {AdminRoleName} user!");
            }

            return applicationUser;
        }
    }       
}
