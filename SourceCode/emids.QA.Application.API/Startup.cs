using ADO.DataAccessHelper;
using emids.QA.Application.Business;
using emids.QA.Application.Business.Patient;
using emids.QA.Application.Common.Config;
using emids.QA.Application.DataAccess.Contracts;
using emids.QA.Application.DataAccess.Patient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

namespace emids.QA.Application.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            //services.AddMvc()
            //.AddMvcOptions(options =>
            //{
            //    options.Filters.Add(new ModelValidationErrorHandlerFilterAttribute());
            //});

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Framework Automation"
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            services.AddScoped<ModelValidationErrorHandlerFilter>();
            services.Configure<ApplicationConfiguration>(Configuration.GetSection("ApplicationConfiguration"));

            // configure jwt authentication
            var appSettingsSection = Configuration.GetSection("ApplicationConfiguration");
            var appSettings = appSettingsSection.Get<ApplicationConfiguration>();
            DatabaseProvider<MySqlClientFactory>.Set("MySql.Connection", appSettings.DatabaseConnectionString);
            services.AddScoped<IPatientDataAccess, PatientDataAccess>();
            services.AddScoped<IPatientBusiness, PatientBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("MyPolicy");
            app.UseStaticFiles();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Framework Automation");
            });
        }
    }
}
