using BLL.Interfaces;
using DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Tests
{
    public class RestWebAppFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        private static bool _databaseInitialized = false;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<DBContext>)
                );
                services.Remove(descriptor);

                services.AddDbContext<DBContext>(options =>
                {
                    options.UseInMemoryDatabase("db");

                    /*options.UseInMemoryDatabase(
                        databaseName: Guid.NewGuid().ToString()
                        //,databaseRoot: new InMemoryDatabaseRoot()
                    );*/
                });



                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<DBContext>();
                var logger = scopedServices.GetRequiredService<ILogger<RestWebAppFactory<TStartup>>>();

                MockRealWorldSenders(services);

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                try
                {
                    if (!_databaseInitialized)
                    {
                        _databaseInitialized = true;
                        db.Database.EnsureDeleted();
                        db.Database.EnsureCreated();
                        Utils.InitializeDbForTests(sp);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " +
                        "database with test messages. Error: {Message}", ex.Message);
                }
            });
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder().ConfigureWebHost((builder) =>
            {
                builder.UseStartup<TStartup>();
            });
        }

        private static void MockRealWorldSenders(IServiceCollection services)
        {
            services.Remove(services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(ISmsSender)
                ));
            services.Remove(services.SingleOrDefault(
               d => d.ServiceType ==
                   typeof(ITrainigEmailSender)
            ));
            var smsMock = new Mock<ISmsSender>();
            smsMock.Setup(m => m.SendSMSNotification("", "")).Returns(true);
            services.AddSingleton<ISmsSender>(e => smsMock.Object);
            var emailMock = new Mock<ITrainigEmailSender>();
            emailMock.Setup(m => m.SendNotificationToLector("", "")).Returns(new Task<bool>(() => true));
            emailMock.Setup(m => m.SendNotificationToStudent("", "")).Returns(new Task<bool>(() => true));
            services.AddSingleton<ITrainigEmailSender>(e => emailMock.Object);
        }
    }

}
