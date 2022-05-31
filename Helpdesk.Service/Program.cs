using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Shared.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NLog.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Helpdesk.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ListenForIntegrationEvents();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            ////-----------
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging")); //logging settings under appsettings.json
                                                                                              //logging.AddConsole(); //Adds a console logger named 'Console' to the factory.
                                                                                              //logging.AddDebug(); //Adds a debug logger named 'Debug' to the factory.
                                                                                              //logging.AddEventSourceLogger(); //Adds an event logger named 'EventSource' to the factory.
                                                                                              // Enable NLog as one of the Logging Provider
                logging.AddNLog();
            })
            ////-----------------------
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

        //------------ For RabbitMQ messaging starts ------------------------------
        private static void ListenForIntegrationEvents()
        {
            var factory = new ConnectionFactory();

            //factory.UserName = "guest";
            //factory.Password = "guest";
            //factory.VirtualHost = "/";
            factory.HostName = "192.168.43.197";
            factory.Port = AmqpTcpEndpoint.UseDefaultPort;

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                //var contextOptions = new DbContextOptionsBuilder<PostServiceContext>()
                var contextOptions = new DbContextOptionsBuilder<HelpDeskDBContext>()
                .UseSqlite(@"Data Source=FintrakHelpDeskDB.db")
                    //.UseSqlite(@"Data Source=post.db")
                    //.UseSqlServer(@"Data Source=192.168.43.197,51484;Initial Catalog=FintrakHelpDeskDB;User =sa;Password=password@1;Integrated Security=false;TrustServerCertificate=true;Persist Security Info=false;Persist Security Info=false;")
                    .Options;

                var dbContext = new HelpDeskDBContext(contextOptions);

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                //Console.WriteLine(" [x] Received {0}", message);

                var data = JObject.Parse(message);
                var type = ea.RoutingKey;
                if (type == "user.add")
                {
                    dbContext.UsersSet.Add(new Users()
                    {
                        //UserId = data["UserId"].Value<int>(),
                        Email = data["Email"].Value<string>(),
                        FirstName = data["FirstName"].Value<string>(),
                        LastName = data["LastName"].Value<string>(),
                        Gender = data["Gender"].Value<string>(),
                        OrganizationId = int.Parse(data["OrganizationId"].Value<string>()),
                    });
                    dbContext.SaveChanges();
                }
                else if (type == "user.update")
                {
                    //var user = dbContext.UsersSet.First(a => a.UserId == data["UserId"].Value<int>());
                    var user = dbContext.UsersSet.First(a => a.Email == data["Email_0"].Value<string>());

                    user.FirstName = data["_0FirstName"].Value<string>();
                    user.LastName = data["_0LastName"].Value<string>();
                    user.Gender = data["_0Gender"].Value<string>();
                    dbContext.SaveChanges();
                }
            };
            channel.BasicConsume(queue: "user.postservice",
                                     autoAck: true,
                                     consumer: consumer);
        }
        //------------ For RabbitMQ messaging ends ------------------------------

    }
}
