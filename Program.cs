using FudbalskiTurnir_FilipNikolic.Models.DBContext;
using FudbalskiTurnir_FilipNikolic.Models.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FudbalskiTurnir_FilipNikolic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("SQLDatabaseConnection");
            builder.Services.AddDbContextPool<DBContext_FudbalskiTurnir>(options => options.UseSqlServer(connectionString));
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<DBContext_FudbalskiTurnir>();
            builder.Services.AddTransient<IFudbalskiTurnir, FudbalskiTurnir>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Turnir}/{action=TurnirOverview}/{id?}");

            app.Run();
        }
    }
}