using Microsoft.EntityFrameworkCore;
using Pandape.Infrastructure.DataBase;

namespace Pandape.Web;

public class StartUp
{

    public StartUp(IConfiguration configuration){
        Configuration = configuration;
    }

    public IConfiguration Configuration;

    public void ConfigureServices(IServiceCollection services){

        services.AddSingleton<IEntityConfiguration, CandidateConfiguration>();


        string? connectionString = Configuration.GetConnectionString("PandaContext");
        if(string.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException(nameof(connectionString));
        services.AddDbContext<PandaContext>(options => {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddControllersWithViews();
    }

    public void Configure(WebApplication app) {


        using(var scope = app.Services.CreateAsyncScope()){
            PandaContext context = scope.ServiceProvider.GetRequiredService<PandaContext>();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }

        // Configure the HTTP request pipeline.
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
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }

}
