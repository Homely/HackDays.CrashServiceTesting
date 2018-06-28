using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Configuration;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            HostingEnvironment = env;
            Configuration = config;
        }

        public IHostingEnvironment HostingEnvironment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SentryOptions>(Configuration.GetSection("Sentry"));

            services.AddScoped<IErrorReporter, SentryErrorReporter>();
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<SentryMiddleware>();

            app.UseMvc();
        }
    }
}
