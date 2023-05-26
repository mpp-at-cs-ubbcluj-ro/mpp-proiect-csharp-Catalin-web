using DataStore.Provicers.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Services;
using WebApi.Settings;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var settingsProvider = SettingsProviderFactory.Create();
            services.AddSingleton<ISettingsProvider>((_) => settingsProvider);

            services.AddDbContext<TripContext>(options =>
            {
                options.UseSqlite(settingsProvider.ConnectionString);
            });
            
            // Controller:
            services.AddControllers(options =>
            {
                options.AllowEmptyInputInBodyModelBinding = true;
            });

            // Swager:
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "1",
                    Title = "Trip Service",
                    Description = "This is the trip service.",
                    Contact = new OpenApiContact
                    {
                        Name = "Catalin",
                        Email = "catalinbgnr@gmail.com"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var serviceSettings = app.ApplicationServices.GetService<ISettingsProvider>();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
            // Web sockets middleware
            app.UseWebSockets();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            // LIVE ON: http://localhost:12500/trip/swagger/index.html
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "trip/swagger/{documentName}/trip.json";
            });
            app.UseSwaggerUI(
                options =>
                {
                    options.RoutePrefix = "trip/swagger";
                    options.SwaggerEndpoint("v1/trip.json", "Trip Service");
                });
        }
    } 
}
