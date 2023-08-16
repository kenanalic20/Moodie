
using auth.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Moodie.Data;
using Swashbuckle.AspNetCore.SwaggerUI;
using Moodie.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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
        services.AddScoped<IMoodRepo, MoodRepo>();
        services.AddScoped<JWTService>();
        services.AddScoped<AverageMood>();
        services.AddScoped<INotesRepo, NotesRepo>();
    
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
       app.UseAuthentication();
         app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

}

