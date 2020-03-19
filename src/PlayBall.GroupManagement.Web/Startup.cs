using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PlayBall.GroupManagement.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region services
            //
            services.AddMvc(option => option.EnableEndpointRouting = false);
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
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            #region core 3
            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Groups}/{action=Index}/{id?}");

            //    endpoints.MapAreaControllerRoute(
            //        name: "dashboard", "Dashboard",
            //        pattern: "{area=Dashboard}/{controller=Home}/{action=Index}/{id?}");
            //});
            #endregion
        }
    }
}
