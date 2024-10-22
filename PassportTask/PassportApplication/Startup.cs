using Mapster;
using MapsterMapper;

using PassportApplication.Extensions;

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
        }

        /// <summary>
        /// Builder configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Services setup
        /// </summary>
        /// <param name="services">Instance of an object implementing IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabase(Configuration);
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddQuartzService(Configuration);
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
