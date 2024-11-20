using auth.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Moodie.Data;
using Moodie.Helper;

namespace auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // EF Core uses this method at design time to access the DbContext
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder => webBuilder.UseStartup<Startup>());
        }
    }
}

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins("http://localhost:4200") // Your Angular app URL
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });
        });
        services.AddControllers();
        services.AddScoped<IUserRepo, UserRepo>();
        services.AddScoped<IHabitRepo, HabitRepo>();  // Add this line
        services.AddScoped<IMoodRepo, MoodRepo>();
        services.AddScoped<JWTService>();
        services.AddScoped<EmailService>();
        services.AddScoped<AverageMood>();
        services.AddScoped<INotesRepo, NotesRepo>();
        services.AddScoped<IUserInfoRepo, UserInfoRepo>();  // Add this line
        services.AddScoped<IGoalRepo, GoalRepo>();
        services.AddScoped<IUserImageRepo, UserImageRepo>();
        services.AddScoped<ISettingsRepo, SettingsRepo>();
        services.AddScoped<IActivityRepo,ActivityRepo>();
        services.AddScoped<IHabitRepo, HabitRepo>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
            c.RoutePrefix = "swagger"; // Change this to your desired URL prefix
        });
    }
}
