using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Common;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Protel.Context;
using Protel.Services;
using Protel.Worker;

namespace Protel
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

            //--- Context
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ProtelDbConnection")));

            //--- Dependency Injection
            services.AddTransient<IAppDbContext, AppDbContext>();
            services.AddTransient<ICurrencyService, CurrencyService>();

            //--- Hangfire
            var storage = new SqlServerStorage(
                Configuration.GetConnectionString("HangfireDbConnection"),
                new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true
                });
            services.AddHangfire(x => x.UseStorage(storage));
            GlobalConfiguration.Configuration.UseStorage(storage);


            var manager = new RecurringJobManager();

            manager.AddOrUpdate("parse-currency",
              Job.FromExpression<CurrencyJob>(a => a.Work(JobCancellationToken.Null, DateTime.Now)),
              Cron.MinuteInterval(2), TimeZoneInfo.Local);
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
            app.UseHangfireDashboard("/job");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseHangfireServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
