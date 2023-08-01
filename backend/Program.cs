
using auth.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Moodie.Data;
using Swashbuckle.AspNetCore.SwaggerUI;
using Moodie.Helper;
namespace auth
{
    public class Program
    {
        public static void Main(string[] args)
            => CreateHostBuilder(args).Build().Run();

        // EF Core uses this method at design time to access the DbContext
        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder => webBuilder.UseStartup<Startup>());
        
        
    }
}

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get;  }
        
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors();
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        services.AddControllers();
        services.AddScoped<IUserRepo, UserRepo>();
        services.AddScoped<JWTService>();
        services.AddScoped<AverageMood>();
    
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(options=>options.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins(new []{"http://localhost:4200"}));
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

}

