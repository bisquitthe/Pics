using DataAccess;
using DataAccess.FileRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Models;
using MongoDB.Driver;
using Services;

namespace Pics
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public string ContentRoot { get; private set; }
    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      AddCollectionsToServiceCollection(services);
      AddReposToServiceCollection(services);
      services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.RequireHttpsMetadata = false;
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = AuthOptions.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
          };
        });
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      // In production, the React files will be served from this directory
      services.AddSpaStaticFiles(configuration =>
      {
        configuration.RootPath = "ClientApp/build";
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        //app.UseWebpackDevMiddleware();
      }
      else
      {
        app.UseExceptionHandler("/Error");
      }

      app.UseStaticFiles();
      app.UseSpaStaticFiles();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller}/{action=Index}/{id?}");
        routes.MapSpaFallbackRoute("spa-fallback", new {controller = "Home", action = "Index"});
      });

      app.UseSpa(spa =>
      {
        spa.Options.SourcePath = "ClientApp";

        if (env.IsDevelopment())
        {
          spa.UseReactDevelopmentServer(npmScript: "start");
        }
      });

      this.ContentRoot = env.ContentRootPath;
    }

    private void AddCollectionsToServiceCollection(IServiceCollection services)
    {
      var connectionString = this.Configuration.GetValue<string>("ConnectionString");
      var mongoClient = new MongoClient(connectionString);
      var db = mongoClient.GetDatabase("Pics");
      services.AddSingleton(factory => db.GetCollection<Image>("Images"));
      services.AddSingleton(factory => db.GetCollection<User>("Users"));
    }

    private void AddReposToServiceCollection(IServiceCollection services)
    {
      services.AddTransient<IImageRepository, ImageRepository>();
      services.AddTransient<IFileRepository<Image>, ImageFileRepository>(_ =>
        new ImageFileRepository(this.ContentRoot));
      services.AddTransient<IImageService, ImageService>();
      services.AddTransient<IUserRepository, UserRepository>();
      services.AddTransient<IAuthService, AuthService>();
      services.AddTransient<IUserService, UserService>();
    }   
  }
}
