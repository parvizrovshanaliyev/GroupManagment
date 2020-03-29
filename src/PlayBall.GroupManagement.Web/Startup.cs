using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlayBall.GroupManagement.Web.Demo;
using PlayBall.GroupManagement.Web.IoC;

namespace PlayBall.GroupManagement.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
           
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region services
            //
            services.AddControllersWithViews();
            //
            #region configure
            // using IOptions
            //services.Configure<SomeRootConfiguration>(_configuration.GetSection("SomeRoot"));
            //
            // injecting POCO
            //services.AddSingleton(_configuration.GetSection("SomeRoot").Get<SomeRootConfiguration>());
            // injecting POCO , prettier
            services.ConfigurePOCO<SomeRootConfiguration>(_configuration.GetSection("SomeRoot"));
            services.ConfigurePOCO<DemoSecretsConfiguration>(_configuration.GetSection("DemoSecrets"));
            #endregion
            //
            // if using default DI container , uncomment
            //services.AddBusiness();
            //
            // Add services to the collection
            services.AddOptions();

            #endregion

            #region core 3

            //services.AddControllersWithViews();

            #endregion
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutoFacModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add("X-Powered-By", "Asp Net Core : From 0 to Over Kill");
                    return Task.CompletedTask;
                });
                await next.Invoke();

            });


            #region routing
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller}/{action=Index}/{id?}");
            //});
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // attribute base routing 
                //endpoints.MapControllers();
                // convention-based routing 
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Groups}/{action=Index}/{id?}");

            });
            #endregion
        }
    }
}
