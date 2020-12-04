using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SistemaDeMonitoramento.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeMonitoramento
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<MonitoramentoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MonitoramentoConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "aparelho",
                    pattern: "{controller=Aparelhos}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "ambiente",
                    pattern: "{controller=Ambientes}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "residencia",
                    pattern: "{controller=Residencias}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "usuario",
                    pattern: "{controller=Usuarios}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                   name: "residenciausuario",
                   pattern: "{controller=ResidenciasUsuarios}/{action=Index}/{id?}");
            });
        }
    }
}
