using BLL.Entities;
using BLL.Interfaces;
using BLL.Services;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using BLL.Utils;
using WebApiExample.Middleware;

namespace RestTest
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
            services.AddDbContext<DBContext>(
                options => options.UseInMemoryDatabase("db1")
                /*{
                    var connection = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
                    options.UseMySQL(connection);
                },*/
                //,ServiceLifetime.Transient
            );
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestTest", Version = "v1" });
            });

            services.AddAutoMapper(
                config => {
                    config.AddProfile<AutoMappingBllDal>();
                    config.AddProfile<AutoMappingWebBll>();
                },
                typeof(Startup)
            );

            services.AddMvc().AddXmlSerializerFormatters().AddXmlDataContractSerializerFormatters();

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHandleException();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestTest v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private static void RegisterServices(IServiceCollection services)
        {
            services
                .AddTransient<IRepository<RoleDAL>, Repository<RoleDAL>>()
                .AddTransient<ITrainingService<Role>, RoleService>()

                .AddTransient<ITrainingService<User>, UserService>()
                .AddTransient<IRepository<UserDAL>, Repository<UserDAL>>()

                .AddTransient<ITrainingService<UserRole>, UserRoleService>()
                .AddTransient<IRepository<UserRoleDAL>, Repository<UserRoleDAL>>()

                .AddTransient<ITrainingService<Course>, CourseService>()
                .AddTransient<IRepository<CourseDAL>, Repository<CourseDAL>>()

                .AddTransient<ITrainingService<Lesson>, LessonService>()
                .AddTransient<IRepository<LessonDAL>, Repository<LessonDAL>>()

                .AddTransient<ITrainingService<Training>, TrainingService>()
                .AddTransient<IRepository<TrainingDAL>, Repository<TrainingDAL>>()

                .AddTransient<ReportsService>()

                .AddTransient<ISmsSender>(e => new SenderUtils("", "", "")) // stub
                .AddTransient<ITrainigEmailSender>(e => new SenderUtils("", "", "")) // stub

                ;
        }
    }
}
