using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using PhoneGuideApp.DB_Model;
using PhoneGuideApp.Interfaces;

namespace PhoneGuideApp
{
    public class Startup
    {

        //////
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PhoneGuideDBEntities>(db => db.UseSqlite(connectionString));
            services.AddScoped<IPhoneGuideDBEntities, PhoneGuideDBEntities>();
            services.AddScoped<IUserContext, UserContext>();

        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=PhonesPage}/{id?}");
            app.MapRazorPages();
            app.Run();
        }

    }
}
