using FoodInfrastructure.DbContextClass;
using FoodInfrastructure.RepositoryImpl;
using FoodOrderCoreProject.Domain.RepositoryInterfaces;
using FoodOrderCoreProject.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace FoodOrder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           // builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
            });
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
            {
                option.Password.RequiredLength = 10;
                option.Password.RequireLowercase = true;
                option.Password.RequireUppercase = true;
                option.Password.RequireDigit = true;
                option.Password.RequireNonAlphanumeric = true;
            }).
                AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders().
                AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, int>>().
                AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, int>>();
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization(option =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                option.FallbackPolicy = policy;
                option.AddPolicy("NotAuthorize", context =>
                {
                    context.RequireAssertion(option1 =>
                    {
                        return !option1.User.Identity.IsAuthenticated;
                    });
                });
            });
            builder.Services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Account/Register";
            });
            builder.Services.AddScoped<IProductRepository, ProductRepositoryImpl>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepositoryImpl>();
            builder.Services.AddControllersWithViews();
            var app = builder.Build();
            app.UseStaticFiles();
            //app.UseExceptionHandler("/error");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
