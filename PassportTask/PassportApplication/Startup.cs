using Mapster;
using MapsterMapper;

using PassportApplication.Extensions;
using PassportApplication.Options;

namespace PassportApplication
{
    /// <summary>
    /// Application setup
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// Constructor of Setup
        /// </summary>
        /// <param name="configuration">Builder configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Settings = new Settings(configuration);
        }

        /// <summary>
        /// Builder configuration
        /// </summary>
        public IConfiguration Configuration { get; }
        public Settings Settings { get; }

        /// <summary>
        /// Services setup
        /// </summary>
        /// <param name="services">Instance of an object implementing IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabase(Settings);
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddQuartzService(Settings);
            services.AddSingleton(s => Settings.FormatSettings);
            services.AddRepository(Settings);
            services.AddSingleton(TypeAdapterConfig.GlobalSettings);
            services.AddScoped<IMapper, ServiceMapper>();
        }

        /// <summary>
        /// Usage setup
        /// </summary>
        /// <param name="app">Instance of an object implementing IApplicationBuilder</param>
        /// <param name="env">Instance of an object implementing IWebHostEnvironment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandlerMiddleware();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
